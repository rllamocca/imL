#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

using System.Net.Mail;

namespace imL.Format
{
    public class SmtpFormat
    {
        public int? Timeout { set; get; }
        public string TargetName { set; get; }
        public int? Port { set; get; }
        public string PickupDirectoryLocation { set; get; }
        public string Host { set; get; }
        public bool? EnableSsl { set; get; }
        public SmtpDeliveryMethod DeliveryMethod { set; get; }

#if (NET35 || NET40) == false
        public SmtpDeliveryFormat DeliveryFormat { set; get; }
#endif
        public bool? UseDefaultCredentials { set; get; }


        public string UserName { set; get; }
        public string Password { set; get; }
    }
}

#endif