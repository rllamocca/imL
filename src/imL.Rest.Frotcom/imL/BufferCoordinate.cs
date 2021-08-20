namespace imL.Rest.Frotcom
{
    public class BufferCoordinate
    {
        public string LicensePlate { get; }
        public decimal Latitude { get; }
        public decimal Longitude { get; }

        public BufferCoordinate(string _licenseplate, decimal _latitude, decimal _longitude)
        {
            this.LicensePlate = _licenseplate;
            this.Latitude = _latitude;
            this.Longitude = _longitude;
        }
    }
}
