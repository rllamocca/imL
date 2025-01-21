using System.Net;

namespace imL.Utility.OldHttp
{
    public static class HttpWebRequestExtension
    {
        public static G GetJson<G>(this HttpWebRequest _this)
        {
            _this.Method = "GET";
            OldHttpJsonHelper.DefaultAccept(_this);

            return ((HttpWebResponse)_this.GetResponse()).ReadJson<G>();
        }
        public static GRs PostJson<GRs, GRq>(this HttpWebRequest _this, GRq _post)
        {
            _this.Method = "POST";
            OldHttpJsonHelper.DefaultAccept(_this);
            OldHttpJsonHelper.JsonContent(_this, _post);

            return ((HttpWebResponse)_this.GetResponse()).ReadJson<GRs>();
        }
        public static GRs PutJson<GRs, GRq>(this HttpWebRequest _this, GRq _put)
        {
            _this.Method = "PUT";
            OldHttpJsonHelper.DefaultAccept(_this);
            OldHttpJsonHelper.JsonContent(_this, _put);

            return ((HttpWebResponse)_this.GetResponse()).ReadJson<GRs>();
        }
        public static G DeleteJson<G>(this HttpWebRequest _this)
        {
            _this.Method = "DELETE";
            OldHttpJsonHelper.DefaultAccept(_this);

            return ((HttpWebResponse)_this.GetResponse()).ReadJson<G>();
        }
    }
}
