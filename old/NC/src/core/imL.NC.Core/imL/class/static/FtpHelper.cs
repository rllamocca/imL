#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;

using imL.Format;

namespace imL.Utility
{
#if (NET6_0_OR_GREATER)
    [Obsolete("https://docs.microsoft.com/en-us/dotnet/fundamentals/syslib-diagnostics/syslib0014")]
#endif
    public static class FtpHelper
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
        internal static IEnumerable<FtpContentFormat> AnalizeListDirectoryDetails(string _root, IEnumerable<string> _list)
        {
            foreach (string _item in _list)
            {
                //drwxrwxrwx   1 user     group           0 Nov 21 22:05 221117
                //-rw-rw-rw-   1 user     group     7206316 Nov 21 15:08 20221121_7849166.zip

                //string _a = _item.Substring(0, 10);
                //string _b = _item.Substring(10, 4);
                //string _c = _item.Substring(14, 5);
                //string _d = _item.Substring(19, 10);
                string _e = _item.Substring(29, 12);
                //string _f = _item.Substring(41, 13);
                string _g = _item.Substring(54, _item.Length - 54);

                _e = _e.Trim();
                _g = _g.Trim();

                string _name = _g;

                if (_name == "." || _name == "..")
                    continue;

                long _size = Convert.ToInt64(_e);

                yield return new FtpContentFormat()
                {
                    IsDirectory = _size < 1,
                    Size = _size,
                    Name = _name,
                    FullName = _root + "/" + _name
                };
            }

        }

        public static IEnumerable<string> ListDirectory(string _root, FtpFormat _format)
        {
            FtpWebRequest _client = CreateClient(WebRequestMethods.Ftp.ListDirectory, _root, _format);

            using (FtpWebResponse _r = (FtpWebResponse)_client.GetResponse())
            using (StreamReader _sr = new StreamReader(_r.GetResponseStream()))
                while (_sr.EndOfStream == false)
                    yield return _sr.ReadLine();
        }
        public static IEnumerable<string> ListDirectoryDetails(string _root, FtpFormat _format)
        {
            FtpWebRequest _client = CreateClient(WebRequestMethods.Ftp.ListDirectoryDetails, _root, _format);

            using (FtpWebResponse _r = (FtpWebResponse)_client.GetResponse())
            using (StreamReader _sr = new StreamReader(_r.GetResponseStream()))
                while (_sr.EndOfStream == false)
                    yield return _sr.ReadLine();
        }
        public static IEnumerable<FtpContentFormat> ListDirectoryDetailsContent(string _root, FtpFormat _format)
        {
            foreach (FtpContentFormat _item in AnalizeListDirectoryDetails(_root, ListDirectoryDetails(_root, _format)))
                yield return _item;
        }
        public static IEnumerable<FtpContentFormat> ListSubdirectories(string _root, FtpFormat _format)
        {
            foreach (FtpContentFormat _item in ListDirectoryDetailsContent(_root, _format))
            {
                yield return _item;

                if (_item.IsDirectory == true)
                    foreach (FtpContentFormat _item2 in ListSubdirectories(_root + "/" + _item.Name, _format))
                        yield return _item2;
            }
        }

        public static FtpStatusCode UploadFile(string _root, Stream _up, FtpFormat _format)
        {
            FtpWebRequest _client = CreateClient(WebRequestMethods.Ftp.UploadFile, _root, _format);

            using (Stream _s = _client.GetRequestStream())
            {
                _up.CopyTo(_s);

                using (FtpWebResponse _resp = (FtpWebResponse)_client.GetResponse())
                    return _resp.StatusCode;
            }
        }
        public static FtpStatusCode UploadFile(string _root, byte[] _up, FtpFormat _format)
        {
            using (Stream _ms = new MemoryStream(_up))
                return UploadFile(_root, _ms, _format);
        }
        public static Stream DownloadFile(string _root, FtpFormat _format)
        {
            FtpWebRequest _client = CreateClient(WebRequestMethods.Ftp.DownloadFile, _root, _format);
            Stream _return = new MemoryStream();

            using (FtpWebResponse _r = (FtpWebResponse)_client.GetResponse())
            {
                using (Stream _s = _r.GetResponseStream())
                    _s.CopyTo(_return);

                _return.CheckBeginPosition();

                return _return;
            }
        }
        public static byte[] DownloadFileBytes(string _root, FtpFormat _format)
        {
            using (Stream _s = DownloadFile(_root, _format))
                return _s.ToBytes();
        }
        public static FtpStatusCode DeleteFile(string _root, FtpFormat _format)
        {
            FtpWebRequest _client = FtpHelper.CreateClient(WebRequestMethods.Ftp.DeleteFile, _root, _format);

            using (FtpWebResponse _resp = (FtpWebResponse)_client.GetResponse())
                return _resp.StatusCode;
        }


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
    }

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
}

#endif