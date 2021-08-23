namespace imL.Rest.Frotcom.V2.Schema
{
    public class Sensor
    {
        public int vehicleId { get; set; }
        public int companyId { get; set; }
        public string[] sensors { get; set; }
        public string fuelLevel { get; set; }
        public string wireless { get; set; }
        public string inputs { get; set; }
        public string anA1Name { get; set; }
        public string anA2Name { get; set; }
        public string anA3Name { get; set; }
        public string diN1Name { get; set; }
        public string diN2Name { get; set; }
        public string diN3Name { get; set; }
        public string tP1Name { get; set; }
        public string tP2Name { get; set; }
        public string tP3Name { get; set; }
        public string dooR1Name { get; set; }
        public string dooR2Name { get; set; }
        public string dooR3Name { get; set; }
        public string seaL1Name { get; set; }
        public string seaL2Name { get; set; }
        public string seaL3Name { get; set; }
        public string analogFuel { get; set; }
        public bool hasCanBus { get; set; }
        public bool hasAdBlueLevelSensor { get; set; }
        public bool hasWeightSensor { get; set; }
        public bool hasGrossCombinationWeightSensor { get; set; }
        public bool hasFirstAxleWeightSensor { get; set; }
        public bool hasSecondAxleWeightSensor { get; set; }
        public bool hasTrailerWeightSensor { get; set; }
    }
}
