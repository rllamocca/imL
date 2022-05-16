using System;

namespace imL.Rest.InsurDesign.Schema
{
#pragma warning disable IDE1006 // Estilos de nombres
    public class Beneficiario
    {
        public int count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public Beneficiary[] results { get; set; }
    }
#pragma warning restore IDE1006 // Estilos de nombres
}
