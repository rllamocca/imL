using System.Net;
using System.Net.Http;

using imL.Contract;

using Microsoft.Extensions.Caching.Memory;

namespace imL.Frotcom.Hosting.Core
{
    public class BAppLocked : BLocked
    {
        protected static readonly MemoryCache _CACHE = new MemoryCache(new MemoryCacheOptions());
        //protected static readonly MemoryCacheEntryOptions _CACHE_OPTIONS = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(4));
        private static HttpClient _HTTP;

        public static MemoryCache Cache { get { lock (BAppLocked._LOCKED) { return BAppLocked._CACHE; } } }
        public static HttpClient Http { get { lock (BAppLocked._LOCKED) { return BAppLocked._HTTP; } } }

        public static new void Load(IAppInfo _app)
        {
            BLocked.Load(_app);
            //RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
            BAppLocked._HTTP = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });
            //RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
        }
    }
}