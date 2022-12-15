namespace SAMPLE.imL.Rest.Frotcom
{
    public class MyDelegatingHandler : DelegatingHandler
    {
        public MyDelegatingHandler(HttpMessageHandler _innerHandler)
            : base(_innerHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage _rq, CancellationToken _ct)
        {
            //Console.WriteLine("Request: {0}", _rq);

            if (_rq.Content != null)
                Console.WriteLine(await _rq.Content.ReadAsStringAsync(_ct));

            //Console.WriteLine();

            HttpResponseMessage _rs = await base.SendAsync(_rq, _ct);

            //Console.WriteLine("Response: {0}", _rs);

            //if (_rs.Content != null)
            //    Console.WriteLine(await _rs.Content.ReadAsStringAsync(_ct));

            //Console.WriteLine();

            return _rs;
        }
    }
}
