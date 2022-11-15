#if (NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)
using System.Text.Json;
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
#endif

namespace imL.Package.Json
{
    public static class Extension
    {
        public static T JsonNewMe<T>(this T _this)
        {
            if (_this == null)
                return default;

#if (NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)
            JsonSerializerOptions _se = new JsonSerializerOptions()
            {
#if (NET5_0) == false
                ReferenceHandler = ReferenceHandler.IgnoreCycles
#endif
            };

            return JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(_this, _se));
#else
            JsonSerializerSettings _se = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            JsonSerializerSettings _de = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };

            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(_this, _se), _de);
#endif

        }
    }
}
