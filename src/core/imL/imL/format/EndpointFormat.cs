using imL.Http;

namespace imL
{
    public record EndpointFormat
    {
        public string? Endpoint { init; get; }
        public EAuthentication? Scheme { init; get; }
        public string? Key { init; get; }
        public string? Value { init; get; }
    }
}
