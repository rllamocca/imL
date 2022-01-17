using System.ComponentModel.DataAnnotations;

namespace SAMPLE.imL.Package.EFCSql
{
    public class MyTableSchema
    {
        [Key]
        public decimal? Pk { set; get; }
        public DateTime? Created { set; get; }
        public DateTime? Updated { set; get; }
        public DateTime? Deleted { set; get; }
        public DateTime? Date { set; get; }
        public decimal? Value { set; get; }
    }
}
