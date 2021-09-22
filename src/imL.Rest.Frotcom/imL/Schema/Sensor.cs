namespace imL.Rest.Frotcom.Schema
{
    public class Sensor
    {
        public int vehicleId { set; get; }
        public int companyId { set; get; }
        public string[] sensors { set; get; }
        public string fuelLevel { set; get; }
        public string wireless { set; get; }
        public string inputs { set; get; }
        public string anA1Name { set; get; }
        public string anA2Name { set; get; }
        public string anA3Name { set; get; }
        public string diN1Name { set; get; }
        public string diN2Name { set; get; }
        public string diN3Name { set; get; }
        public string tP1Name { set; get; }
        public string tP2Name { set; get; }
        public string tP3Name { set; get; }
        public string dooR1Name { set; get; }
        public string dooR2Name { set; get; }
        public string dooR3Name { set; get; }
        public string seaL1Name { set; get; }
        public string seaL2Name { set; get; }
        public string seaL3Name { set; get; }
        public string analogFuel { set; get; }
        public bool hasCanBus { set; get; }
        public bool hasAdBlueLevelSensor { set; get; }
        public bool hasWeightSensor { set; get; }
        public bool hasGrossCombinationWeightSensor { set; get; }
        public bool hasFirstAxleWeightSensor { set; get; }
        public bool hasSecondAxleWeightSensor { set; get; }
        public bool hasTrailerWeightSensor { set; get; }
    }
}
