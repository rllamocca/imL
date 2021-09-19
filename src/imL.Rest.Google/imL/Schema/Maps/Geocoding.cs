namespace imL.Rest.Google.Schema.Maps
{
    public class Geocoding
    {
        public Plus_Code plus_code { set; get; }
        public Result[] results { set; get; }
        public string status { set; get; }
    }

    public class Plus_Code
    {
        public string compound_code { set; get; }
        public string global_code { set; get; }
    }

    public class Result
    {
        public Address_Components[] address_components { set; get; }
        public string formatted_address { set; get; }
        public Geometry geometry { set; get; }
        public string place_id { set; get; }
        public Plus_Code1 plus_code { set; get; }
        public string[] types { set; get; }
    }

    public class Geometry
    {
        public Location location { set; get; }
        public string location_type { set; get; }
        public Viewport viewport { set; get; }
        public Bounds bounds { set; get; }
    }

    public class Location
    {
        public float lat { set; get; }
        public float lng { set; get; }
    }

    public class Viewport
    {
        public Northeast northeast { set; get; }
        public Southwest southwest { set; get; }
    }

    public class Northeast
    {
        public float lat { set; get; }
        public float lng { set; get; }
    }

    public class Southwest
    {
        public float lat { set; get; }
        public float lng { set; get; }
    }

    public class Bounds
    {
        public Northeast1 northeast { set; get; }
        public Southwest1 southwest { set; get; }
    }

    public class Northeast1
    {
        public float lat { set; get; }
        public float lng { set; get; }
    }

    public class Southwest1
    {
        public float lat { set; get; }
        public float lng { set; get; }
    }

    public class Plus_Code1
    {
        public string compound_code { set; get; }
        public string global_code { set; get; }
    }

    public class Address_Components
    {
        public string long_name { set; get; }
        public string short_name { set; get; }
        public string[] types { set; get; }
    }
}