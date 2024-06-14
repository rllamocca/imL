namespace EmailDelivery;

public class eMailCooked
{
    internal string? CODIGO { set; get; }
    internal bool? ENVIAR { set; get; }
    internal string?[]? PARA { set; get; }
    internal string?[]? CC { set; get; }
    internal string?[]? CCO { set; get; }
    internal string? ASUNTO { set; get; }
    internal string? CUERPO { set; get; }
    internal string?[]? ADJUNTO { set; get; }
}
