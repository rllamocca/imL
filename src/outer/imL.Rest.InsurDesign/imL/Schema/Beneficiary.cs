using System;

namespace imL.Rest.InsurDesign.Schema
{
#pragma warning disable IDE1006 // Estilos de nombres
    public class Beneficiary
    {
        public string uuid { get; set; }
        public string policy_number { get; set; }
        public string fare_product_name { get; set; }
        public string fare_company_rut { get; set; }
        public string pol_insured_rut { get; set; }
        public string insured_first_name { get; set; }
        public string insured_last_name { get; set; }
        public string insured_second_last_name { get; set; }
        public DateTime? fare_birth_date { get; set; }
        public string fare_relation { get; set; }
        public DateTime? pol_insured_from { get; set; }
        public DateTime? pol_insured_to { get; set; }
        public string fare_status_policy { get; set; }
        public string fare_sex { get; set; }
        public string payment_type { get; set; }
        public string bank { get; set; }
        public string bank_account { get; set; }
        public string bank_account_type { get; set; }
        public string bank_account_owner { get; set; }
    }
#pragma warning restore IDE1006 // Estilos de nombres
}
