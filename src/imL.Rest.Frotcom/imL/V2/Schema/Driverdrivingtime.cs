namespace imL.Rest.Frotcom.V2.Schema
{
    public class Driverdrivingtime
    {
        public int driverId { get; set; }
        public int continuous { get; set; }
        public int continuousSec { get; set; }
        public int daily { get; set; }
        public int dailySec { get; set; }
        public int extraHours { get; set; }
        public int weekly { get; set; }
        public int weeklySec { get; set; }
        public int biweekly { get; set; }
        public int biweeklySec { get; set; }
        public bool isMultiManning { get; set; }
    }
}
