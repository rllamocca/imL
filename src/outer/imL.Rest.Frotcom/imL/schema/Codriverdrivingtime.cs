﻿namespace imL.Rest.Frotcom.Schema
{
#pragma warning disable IDE1006 // Estilos de nombres
    public class Codriverdrivingtime
    {
        public int driverId { set; get; }
        public int continuous { set; get; }
        public int continuousSec { set; get; }
        public int daily { set; get; }
        public int dailySec { set; get; }
        public int extraHours { set; get; }
        public int weekly { set; get; }
        public int weeklySec { set; get; }
        public int biweekly { set; get; }
        public int biweeklySec { set; get; }
        public bool isMultiManning { set; get; }
    }
#pragma warning restore IDE1006 // Estilos de nombres
}