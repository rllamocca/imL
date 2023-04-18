#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

using System.Net.Mail;

namespace imL
{
    public record SmtpFormat
    {
        public int? Timeout { init; get; }
        public string? TargetName { init; get; }
        public int? Port { init; get; }
        public string? PickupDirectoryLocation { init; get; }
        public string? Host { init; get; }
        public bool? EnableSsl { init; get; }
        public SmtpDeliveryMethod? DeliveryMethod { init; get; }

#if (NET35 || NET40) == false
        public SmtpDeliveryFormat? DeliveryFormat { init; get; }
#endif
        public bool? UseDefaultCredentials { init; get; }

        public string? UserName { init; get; }
        public string? Password { init; get; }


        public long? MaxSizeAttachments { init; get; } //MB
    }
}

#endif