using System.Linq;

using imL.JavaScript.ChartJS.Schema;

/*
check

before
on
after 
*/

namespace imL.JavaScript.ChartJS
{
    public static class ChartJSHelper
    {
        private static void ToTitle(ref Config _ref, ChartFormat _chart)
        {
            if (string.IsNullOrEmpty(_chart.Title) == false)
            {
                _ref.options.plugins.title = new Title();
                _ref.options.plugins.title.display = true;
                _ref.options.plugins.title.text = _chart.Title;
            }
        }
        private static void ToDataSets(ref Config _ref, ChartFormat _chart)
        {
            _ref.data.datasets = new Dataset[_chart.Series.Length];

            for (int _i = 0; _i < _ref.data.datasets.Length; _i++)
            {
                SerieFormat _item = _chart.Series[_i];
                _ref.data.datasets[_i] = new Dataset();
                _ref.data.datasets[_i].label = _item.Name;
                _ref.data.datasets[_i].data = _item.Values;
                _ref.data.datasets[_i].backgroundColor = new string[] { _item.BackgroundColor };
                _ref.data.datasets[_i].borderColor = new string[] { _item.BorderColor };
            }
        }
        private static void ToAxis(ref Config _ref, ChartFormat _chart)
        {
            _ref.data.labels = _chart.XAxis;
        }

        public static Config BarCharts_VerticalBarChart(ChartFormat _chart)
        {
            Config _return = new Config();
            _return.data = new Data();
            _return.data.datasets = new Dataset[_chart.Series.Length];
            _return.options = new Options();
            _return.options.plugins = new Plugins();
            _return.options.plugins.legend = new Legend();

            _return.type = "bar";
            _return.options.responsive = true;
            _return.options.plugins.legend.position = "top";

            ChartJSHelper.ToTitle(ref _return, _chart);
            ChartJSHelper.ToDataSets(ref _return, _chart);
            ChartJSHelper.ToAxis(ref _return, _chart);

            for (int _i = 0; _i < _return.data.datasets.Length; _i++)
            {
                _return.data.datasets[_i].borderWidth = 2;
            }

            return _return;
        }

        public static Config LineCharts_LineChart(ChartFormat _chart)
        {
            Config _return = new Config();
            _return.data = new Data();
            _return.data.datasets = new Dataset[_chart.Series.Length];
            _return.options = new Options();
            _return.options.plugins = new Plugins();
            _return.options.plugins.legend = new Legend();

            _return.type = "line";
            _return.options.responsive = true;
            _return.options.plugins.legend.position = "top";

            ChartJSHelper.ToTitle(ref _return, _chart);
            ChartJSHelper.ToDataSets(ref _return, _chart);
            ChartJSHelper.ToAxis(ref _return, _chart);

            return _return;
        }

        public static Config LineCharts_SteppedLineCharts(ChartFormat _chart)
        {
            Config _return = new Config();
            _return.data = new Data();
            _return.data.datasets = new Dataset[_chart.Series.Length];
            _return.options = new Options();
            _return.options.interaction = new Interaction();
            _return.options.plugins = new Plugins();

            _return.type = "line";
            _return.options.responsive = true;
            _return.options.interaction.intersect = false;
            _return.options.interaction.axis = "x";

            ChartJSHelper.ToTitle(ref _return, _chart);
            ChartJSHelper.ToDataSets(ref _return, _chart);
            ChartJSHelper.ToAxis(ref _return, _chart);

            for (int _i = 0; _i < _chart.Series.Length; _i++)
            {
                _return.data.datasets[_i].fill = false;
                _return.data.datasets[_i].stepped = true;
            }

            return _return;
        }
    }
}
