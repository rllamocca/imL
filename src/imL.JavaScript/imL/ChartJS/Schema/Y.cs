namespace imL.JavaScript.ChartJS.Schema
{
    public class Y
    {
        public Title title { set; get; }

        public bool stacked { set; get; }
        public bool beginAtZero { set; get; }

        public string type { set; get; }
        public bool? display { set; get; }
        public string position { set; get; }

        public int? suggestedMin { set; get; }
        public int? suggestedMax { set; get; }
    }
}
/* 
 grid: {
          drawOnChartArea: false, // only want the grid lines for one axis to show up
        }
*/