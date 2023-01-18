#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

using System;
using System.Net;

namespace imL
{
#if (NET6_0_OR_GREATER)
    [Obsolete("https://docs.microsoft.com/en-us/dotnet/fundamentals/syslib-diagnostics/syslib0014")]
#endif
    public static partial class FtpHelper
    {
        static FtpWebRequest InitFtpWebRequest(FtpWebRequest _i, FtpFormat _f)
        {
            _i.UseBinary = _f.UseBinary ?? _i.UseBinary;
            _i.Timeout = _f.Timeout ?? _i.Timeout;
            _i.ReadWriteTimeout = _f.ReadWriteTimeout ?? _i.ReadWriteTimeout;
            _i.KeepAlive = _f.KeepAlive ?? _i.KeepAlive;
            _i.EnableSsl = _f.EnableSsl ?? _i.EnableSsl;
            _i.UsePassive = _f.UsePassive ?? _i.UsePassive;

            _i.ConnectionGroupName = _f.ConnectionGroupName ?? _i.ConnectionGroupName;
            _i.ServicePoint.ConnectionLimit = _f.ConnectionLimit ?? _i.ServicePoint.ConnectionLimit;

            if (_f.UseDefaultCredentials != true)
                _i.Credentials = new NetworkCredential(_f.UserName, _f.Password);

            return _i;
        }
        internal static FtpWebRequest CreateClient(string _method, string _root, FtpFormat _format)
        {
            FtpWebRequest _return = (FtpWebRequest)FtpWebRequest.Create(_format.Host + _format.Path + _root);
            _return = InitFtpWebRequest(_return, _format);
            _return.Method = _method;

            return _return;
        }
    }
}

#endif

 //static void EndGetStreamCallback(IAsyncResult _result)
        //{
        //    FtpAsyncState state = (FtpAsyncState)_result.AsyncState;

        //    Stream requestStream = null;
        //    // End the asynchronous call to get the request stream.
        //    try
        //    {
        //        requestStream = state.Request.EndGetRequestStream(_result);
        //        // Copy the file contents to the request stream.
        //        const int bufferLength = 2048;
        //        byte[] buffer = new byte[bufferLength];
        //        int count = 0;
        //        int readBytes = 0;
        //        FileStream stream = File.OpenRead(state.FileName);
        //        do
        //        {
        //            readBytes = stream.Read(buffer, 0, bufferLength);
        //            requestStream.Write(buffer, 0, readBytes);
        //            count += readBytes;
        //        }
        //        while (readBytes != 0);
        //        Console.WriteLine("Writing {0} bytes to the stream.", count);
        //        // IMPORTANT: Close the request stream before sending the request.
        //        requestStream.Close();
        //        // Asynchronously get the response to the upload request.
        //        state.Request.BeginGetResponse(
        //            new AsyncCallback(EndGetResponseCallback),
        //            state
        //        );
        //    }
        //    // Return exceptions to the main application thread.
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Could not get the request stream.");
        //        state.OperationException = e;
        //        state.OperationComplete.Set();
        //        return;
        //    }
        //}
        //static void EndGetResponseCallback(IAsyncResult _result)
        //{
        //    FtpAsyncState state = (FtpAsyncState)_result.AsyncState;
        //    FtpWebResponse response = null;
        //    try
        //    {
        //        response = (FtpWebResponse)state.Request.EndGetResponse(_result);
        //        response.Close();
        //        state.StatusDescription = response.StatusDescription;
        //        // Signal the main application thread that
        //        // the operation is complete.
        //        state.OperationComplete.Set();
        //    }
        //    // Return exceptions to the main application thread.
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Error getting response.");
        //        state.OperationException = e;
        //        state.OperationComplete.Set();
        //    }
        //}
    //}

    //public class FtpAsyncState
    //{
    //    string fileName;
    //    string status;

    //    ManualResetEvent wait;
    //    FtpWebRequest request;
    //    Exception operationException = null;

    //    public FtpAsyncState()
    //    {
    //        wait = new ManualResetEvent(false);
    //    }

    //    public ManualResetEvent OperationComplete
    //    {
    //        get { return wait; }
    //    }

    //    public FtpWebRequest Request
    //    {
    //        get { return request; }
    //        set { request = value; }
    //    }

    //    public string FileName
    //    {
    //        get { return fileName; }
    //        set { fileName = value; }
    //    }
    //    public Exception OperationException
    //    {
    //        get { return operationException; }
    //        set { operationException = value; }
    //    }
    //    public string StatusDescription
    //    {
    //        get { return status; }
    //        set { status = value; }
    //    }
    //}