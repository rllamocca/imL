namespace EmailDelivery;

public class eMailCooked
{
    internal string? Code { set; get; }
    internal bool? Send { set; get; }
    internal string?[]? To { set; get; }
    internal string?[]? CC { set; get; }
    internal string?[]? BCC { set; get; }
    internal string? Subject { set; get; }
    internal string? Body { set; get; }
    internal string?[]? PathAttachments { set; get; }

    internal object? Result { set; get; }
}
