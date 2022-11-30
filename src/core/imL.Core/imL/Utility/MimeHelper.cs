#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

using System.IO;
using System.Net.Mime;

#endif

namespace imL.Utility
{
    public class MimeHelper
    {
        public static string MediaType(string _fileextension)
        {
            _fileextension = _fileextension.ToLower();

            switch (_fileextension)
            {
                case ".log":
                case ".txt": return "text/plain";
                case ".htm":
                case ".html": return "text/html";
                case ".css": return "text/css";
                case ".js": return "text/javascript";

                case ".gif": return "image/gif";
                case ".png": return "image/png";
                case ".jpg":
                case ".jpeg": return "image/jpeg";
                case ".bmp": return "image/bmp";
                case ".webp": return "image/webp";
                case ".emf": return "image/x-emf";
                case ".tif": return "image/tiff";

                case ".midi": return "audio/midi";
                case ".mpg":
                case ".mpeg": return "audio/mpeg";
                //case ".webm": return "audio/webm";
                case ".ogg": return "audio/ogg";
                case ".wav": return "audio/wav";

                case ".webm": return "video/webm";
                case ".oga": return "video/ogg";

                case ".xml": return "application/xml";
                case ".json": return "application/json";
                case ".doc": return "application/msword";
                case ".docx": return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case ".xls": return "application/vnd.ms-excel";
                case ".xlsx": return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case ".pdf": return "application/pdf";
                case ".zip": return "application/zip";

                case ".mhtml": return "multipart/related";

                default:
                    break;
            }

            return "application/octet-stream";
        }

#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)
        public static ContentType ContentType(string _filename)
        {
            FileInfo _fi = new FileInfo(_filename);

            return new ContentType(MediaType(_fi.Extension));
        }
#endif

    }
}
