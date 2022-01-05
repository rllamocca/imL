using System.Net;

namespace imL.Utility.OldHttp
{
    public static class HttpWebRequestExtension
    {
        public static T Post<T>(this HttpWebRequest _this)
        {
            _this.Method = "GET";
            OldHttpJsonHelper.DefaultAccept(_this);

            return ((HttpWebResponse)_this.GetResponse()).ReadAsObject<T>();
        }
        public static T Post<T>(this HttpWebRequest _this, object _post)
        {
            _this.Method = "POST";
            OldHttpJsonHelper.DefaultAccept(_this);
            OldHttpJsonHelper.JsonContent(_this, _post);

            return ((HttpWebResponse)_this.GetResponse()).ReadAsObject<T>();
        }
        public static T Put<T>(this HttpWebRequest _this, object _put)
        {
            _this.Method = "PUT";
            OldHttpJsonHelper.DefaultAccept(_this);
            OldHttpJsonHelper.JsonContent(_this, _put);

            return ((HttpWebResponse)_this.GetResponse()).ReadAsObject<T>();
        }
    }
}
