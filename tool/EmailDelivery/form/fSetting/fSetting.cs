using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

using EmailDelivery.OAuth2.Google.v4;

using imL;

namespace EmailDelivery;

public partial class fSetting : Form
{
    public fSetting()
    {
        InitializeComponent();

        _1_DeliveryMethod.DataSource = Enum.GetValues<SmtpDeliveryMethod>();
        _1_DeliveryFormat.DataSource = Enum.GetValues<SmtpDeliveryFormat>();
    }

    void fSetting_Load(object sender, EventArgs e)
    {
        try
        {
            SettingFormat? _json = JsonSerializer.Deserialize<SettingFormat>(File.ReadAllText(AppLock.PathJson));

            if (_json == null)
                throw new ArgumentNullException(nameof(_json));

            _SplitSeparator.Text = _json.SplitSeparator;
            tabControl1.SelectedIndex = _json.Selected.GetValueOrDefault();

            MailMessageFormat? _generic = _json.MailMessageFormatBasic;
            _FromAddress.Text = _generic?.FromAddress;
            _FromDisplayName.Text = _generic?.FromDisplayName;

            SmtpFormat? _basic = _json.SmtpFormatBasic;

            if (_basic == null)
                return;

            _1_Timeout.Value = _basic.Timeout.GetValueOrDefault();
            _1_TargetName.Text = _basic.TargetName;
            _1_Port.Value = _basic.Port.GetValueOrDefault();
            _1_PickupDirectoryLocation.Text = _basic.PickupDirectoryLocation;
            _1_Host.Text = _basic.Host;
            _1_EnableSsl.Checked = _basic.EnableSsl.GetValueOrDefault();
            _1_DeliveryMethod.SelectedItem = _basic.DeliveryMethod;
            _1_DeliveryFormat.SelectedItem = _basic.DeliveryFormat;
            _1_UseDefaultCredentials.Checked = _basic.UseDefaultCredentials.GetValueOrDefault();
            _1_UserName.Text = _basic.UserName;
            _1_Password.Text = _basic.Password;
        }
        catch (Exception _ex)
        {
            MessageBox.Show(_ex.Message);
        }
    }

    async void button1_Click(object sender, EventArgs e)
    {
        SettingFormat _format = new()
        {
            SplitSeparator = _SplitSeparator.Text,
            Selected = tabControl1.SelectedIndex,

            MailMessageFormatBasic = new MailMessageFormat
            {
                FromAddress = _FromAddress.Text,
                FromDisplayName = _FromDisplayName.Text
            },

            SmtpFormatBasic = new SmtpFormat
            {
                Timeout = (int?)_1_Timeout.Value,
                TargetName = _1_TargetName.Text,
                Port = (int?)_1_Port.Value,
                PickupDirectoryLocation = _1_PickupDirectoryLocation.Text,
                Host = _1_Host.Text,
                EnableSsl = _1_EnableSsl.Checked,
                DeliveryMethod = (SmtpDeliveryMethod?)_1_DeliveryMethod.SelectedItem,
                DeliveryFormat = (SmtpDeliveryFormat?)_1_DeliveryFormat.SelectedItem,
                UseDefaultCredentials = _1_UseDefaultCredentials.Checked,
                UserName = _1_UserName.Text,
                Password = _1_Password.Text
            }
        };

        //if (string.IsNullOrEmpty( _format.SmtpFormatBasic.TargetName))
        //    _format.SmtpFormatBasic.TargetName = null;

        //if (string.IsNullOrEmpty(_format.SmtpFormatBasic.PickupDirectoryLocation))
        //    _format.SmtpFormatBasic.PickupDirectoryLocation = null;

        string _json = JsonSerializer.Serialize(_format);
        await File.WriteAllTextAsync(AppLock.PathJson, _json);

        MessageBox.Show("Guardar...");
        Close();
    }

    private async void button2_Click(object sender, EventArgs e)
    {
        // Generates state and PKCE values.
        string state = randomDataBase64url(32);
        string code_verifier = randomDataBase64url(32);
        string code_challenge = base64urlencodeNoPadding(sha256(code_verifier));
        const string code_challenge_method = "S256";

        // Creates a redirect URI using an available port on the loopback address.
        string redirectURI = string.Format("http://{0}:{1}/", IPAddress.Loopback, GetRandomUnusedPort());
        output("redirect URI: " + redirectURI);

        // Creates an HttpListener to listen for requests on that redirect URI.
        var http = new HttpListener();
        http.Prefixes.Add(redirectURI);
        output("Listening..");
        http.Start();

        // Creates the OAuth 2.0 authorization request.
        string authorizationRequest = string.Format("{0}?response_type=code&scope=openid%20profile&redirect_uri={1}&client_id={2}&state={3}&code_challenge={4}&code_challenge_method={5}",
            authorizationEndpoint,
            Uri.EscapeDataString(redirectURI),
            clientID,
            state,
            code_challenge,
            code_challenge_method);

        // Opens request in the browser.
        Process.Start(new ProcessStartInfo(authorizationRequest) { UseShellExecute = true });

        // Waits for the OAuth authorization response.
        var context = await http.GetContextAsync();

        // Brings this app back to the foreground.
        this.Activate();

        // Sends an HTTP response to the browser.
        var response = context.Response;
        string responseString = string.Format("<html><head><meta http-equiv='refresh' content='10;url=https://google.com'></head><body>Please return to the app.</body></html>");
        var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
        response.ContentLength64 = buffer.Length;
        var responseOutput = response.OutputStream;
        Task responseTask = responseOutput.WriteAsync(buffer, 0, buffer.Length).ContinueWith((task) =>
        {
            responseOutput.Close();
            http.Stop();
            Console.WriteLine("HTTP server stopped.");
        });

        // Checks for errors.
        if (context.Request.QueryString.Get("error") != null)
        {
            output(String.Format("OAuth authorization error: {0}.", context.Request.QueryString.Get("error")));
            return;
        }
        if (context.Request.QueryString.Get("code") == null
            || context.Request.QueryString.Get("state") == null)
        {
            output("Malformed authorization response. " + context.Request.QueryString);
            return;
        }

        // extracts the code
        var code = context.Request.QueryString.Get("code");
        var incoming_state = context.Request.QueryString.Get("state");

        // Compares the receieved state to the expected value, to ensure that
        // this app made the request which resulted in authorization.
        if (incoming_state != state)
        {
            output(String.Format("Received request with invalid state ({0})", incoming_state));
            return;
        }
        output("Authorization code: " + code);

        // Starts the code exchange at the Token Endpoint.
        performCodeExchange(code, code_verifier, redirectURI);
    }

    const string clientID = "581786658708-elflankerquo1a6vsckabbhn25hclla0.apps.googleusercontent.com";
    const string clientSecret = "3f6NggMbPtrmIBpgx-MK2xXK";
    const string authorizationEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
    const string tokenEndpoint = "https://www.googleapis.com/oauth2/v4/token";
    const string userInfoEndpoint = "https://www.googleapis.com/oauth2/v3/userinfo";

    static int GetRandomUnusedPort()
    {
        var listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        var port = ((IPEndPoint)listener.LocalEndpoint).Port;
        listener.Stop();
        return port;
    }
    async void performCodeExchange(string code, string code_verifier, string redirectURI)
    {
        output("Exchanging code for tokens...");

        // builds the  request
        string tokenRequestURI = "https://www.googleapis.com/oauth2/v4/token";
        string tokenRequestBody = string.Format("code={0}&redirect_uri={1}&client_id={2}&code_verifier={3}&client_secret={4}&scope=&grant_type=authorization_code",
            code,
            System.Uri.EscapeDataString(redirectURI),
            clientID,
            code_verifier,
            clientSecret
            );

        // sends the request
        HttpWebRequest tokenRequest = (HttpWebRequest)WebRequest.Create(tokenRequestURI);
        tokenRequest.Method = "POST";
        tokenRequest.ContentType = "application/x-www-form-urlencoded";
        tokenRequest.Accept = "Accept=text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
        byte[] _byteVersion = Encoding.ASCII.GetBytes(tokenRequestBody);
        tokenRequest.ContentLength = _byteVersion.Length;
        Stream stream = tokenRequest.GetRequestStream();
        await stream.WriteAsync(_byteVersion, 0, _byteVersion.Length);
        stream.Close();

        try
        {
            // gets the response
            WebResponse tokenResponse = await tokenRequest.GetResponseAsync();
            using (StreamReader reader = new StreamReader(tokenResponse.GetResponseStream()))
            {
                // reads response body
                string responseText = await reader.ReadToEndAsync();
                output(responseText);

                // converts to dictionary
                TokenDownload tokenEndpointDecoded = JsonSerializer.Deserialize<TokenDownload>(responseText);

                string access_token = tokenEndpointDecoded.access_token;
                userinfoCall(access_token);
            }
        }
        catch (WebException ex)
        {
            if (ex.Status == WebExceptionStatus.ProtocolError)
            {
                var response = ex.Response as HttpWebResponse;
                if (response != null)
                {
                    output("HTTP: " + response.StatusCode);
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        // reads response body
                        string responseText = await reader.ReadToEndAsync();
                        output(responseText);
                    }
                }

            }
        }
    }
    async void userinfoCall(string access_token)
    {
        output("Making API Call to Userinfo...");

        // builds the  request
        string userinfoRequestURI = "https://www.googleapis.com/oauth2/v3/userinfo";

        // sends the request
        HttpWebRequest userinfoRequest = (HttpWebRequest)WebRequest.Create(userinfoRequestURI);
        userinfoRequest.Method = "GET";
        userinfoRequest.Headers.Add(string.Format("Authorization: Bearer {0}", access_token));
        userinfoRequest.ContentType = "application/x-www-form-urlencoded";
        userinfoRequest.Accept = "Accept=text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

        // gets the response
        WebResponse userinfoResponse = await userinfoRequest.GetResponseAsync();
        using (StreamReader userinfoResponseReader = new StreamReader(userinfoResponse.GetResponseStream()))
        {
            // reads response body
            string userinfoResponseText = await userinfoResponseReader.ReadToEndAsync();
            output(userinfoResponseText);
        }
    }
    void output(string output)
    {
        richTextBox1.Text = richTextBox1.Text + output + Environment.NewLine;
        Console.WriteLine(output);
    }
    static string randomDataBase64url(uint length)
    {
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        byte[] bytes = new byte[length];
        rng.GetBytes(bytes);
        return base64urlencodeNoPadding(bytes);
    }
    static byte[] sha256(string inputStirng)
    {
        byte[] bytes = Encoding.ASCII.GetBytes(inputStirng);
        SHA256Managed sha256 = new SHA256Managed();
        return sha256.ComputeHash(bytes);
    }
    static string base64urlencodeNoPadding(byte[] buffer)
    {
        string base64 = Convert.ToBase64String(buffer);

        // Converts base64 to base64url.
        base64 = base64.Replace("+", "-");
        base64 = base64.Replace("/", "_");
        // Strips padding.
        base64 = base64.Replace("=", "");

        return base64;
    }
}
