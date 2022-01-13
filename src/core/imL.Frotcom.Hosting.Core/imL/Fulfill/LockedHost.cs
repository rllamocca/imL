using System.Net;
using System.Net.Http;

using imL.Contract;

using Microsoft.Extensions.Caching.Memory;

namespace imL.Frotcom.Hosting.Core
{
    public class LockedHost : LockedBase
    {
        protected static readonly MemoryCache _CACHE = new MemoryCache(new MemoryCacheOptions());
        //protected static readonly MemoryCacheEntryOptions _CACHE_OPTIONS = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(4));
        private static HttpClient _HTTP;

        public static MemoryCache Cache { get { lock (LockedHost._LOCKED) { return LockedHost._CACHE; } } }
        public static HttpClient Http { get { lock (LockedHost._LOCKED) { return LockedHost._HTTP; } } }

        public static new void Load(IAppInfo _app)
        {
            LockedBase.Load(_app);
            LockedHost._HTTP = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });
            //RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
        }
    }
}