using System;

namespace imL.Rest.InsurDesign.Schema
{
#pragma warning disable IDE1006 // Estilos de nombres
    public class Policy
    {
        public string policy_number { get; set; }
        public string fare_product_name { get; set; }
        public DateTime? pol_insured_from { get; set; }
        public DateTime? pol_insured_to { get; set; }
        public string fare_status_policy { get; set; }
    }
#pragma warning restore IDE1006 // Estilos de nombres
}
