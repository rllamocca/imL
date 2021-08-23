using System;

namespace imL.Rest.Frotcom.V2.Schema
{
    public class Location
    {
        public long id { get; set; }
        public DateTime? timeStamp { get; set; }
        public decimal speed { get; set; }
        public int driverId { get; set; }
        public string driverName { get; set; }
        public int coDriverId { get; set; }
        public string coDriverName { get; set; }
        public string coupledLicensePlate { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public decimal direction { get; set; }
        public bool isValidGps { get; set; }
        public bool isIgnitionOn { get; set; }
        public int[] alarmOccurrenceIds { get; set; }
        public int immobilizer { get; set; }
        public string trackingData { get; set; }
        public DateTime? dtStartTrip { get; set; }
        public DateTime? dtEndPrevTrip { get; set; }
        public bool isStopped { get; set; }
        public decimal odometerGps { get; set; }
        public decimal odometerCanbus { get; set; }
        public int fuelEventsId { get; set; }
        public bool isRefueling { get; set; }
    }
}
