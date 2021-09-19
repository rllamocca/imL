using System;

namespace imL.Rest.Frotcom.Schema
{
    public class Location
    {
        public long id { set; get; }
        public DateTime? timeStamp { set; get; }
        public decimal speed { set; get; }
        public int driverId { set; get; }
        public string driverName { set; get; }
        public int coDriverId { set; get; }
        public string coDriverName { set; get; }
        public string coupledLicensePlate { set; get; }
        public decimal latitude { set; get; }
        public decimal longitude { set; get; }
        public decimal direction { set; get; }
        public bool isValidGps { set; get; }
        public bool isIgnitionOn { set; get; }
        public int[] alarmOccurrenceIds { set; get; }
        public int immobilizer { set; get; }
        public string trackingData { set; get; }
        public DateTime? dtStartTrip { set; get; }
        public DateTime? dtEndPrevTrip { set; get; }
        public bool isStopped { set; get; }
        public decimal odometerGps { set; get; }
        public decimal odometerCanbus { set; get; }
        public int fuelEventsId { set; get; }
        public bool isRefueling { set; get; }
    }
}
