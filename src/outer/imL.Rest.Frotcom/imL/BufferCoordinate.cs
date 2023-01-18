namespace imL.Rest.Frotcom
{
    public class BufferCoordinate
    {
        public string LicensePlate { get; }
        public decimal Latitude { get; }
        public decimal Longitude { get; }

        public BufferCoordinate(string _licenseplate, decimal _latitude, decimal _longitude)
        {
            LicensePlate = _licenseplate;
            Latitude = _latitude;
            Longitude = _longitude;
        }
    }
}
