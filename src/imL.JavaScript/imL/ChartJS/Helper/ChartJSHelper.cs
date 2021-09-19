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

        public static Config BarCharts_Vertical(ChartFormat _chart)
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
        public static Config BarCharts_Horizontal(ChartFormat _chart)
        {
            Config _return = ChartJSHelper.BarCharts_Vertical(_chart);
            _return.options.indexAxis = "y";

            return _return;
        }
        public static Config BarCharts_Stacked(ChartFormat _chart)
        {
            Config _return = ChartJSHelper.BarCharts_Vertical(_chart);
            _return.options.scales = new Scales();
            _return.options.scales.x = new X();
            _return.options.scales.y = new Y();

            _return.options.scales.x.stacked = true;
            _return.options.scales.y.stacked = true;

            return _return;
        }
        public static Config BarCharts_StackedwithGroups(ChartFormat _chart) //AFINAR
        {
            Config _return = ChartJSHelper.BarCharts_Vertical(_chart);
            _return.options.interaction = new Interaction();
            _return.options.scales = new Scales();
            _return.options.scales.x = new X();
            _return.options.scales.y = new Y();

            _return.options.interaction.intersect = false;
            _return.options.scales.x.stacked = true;
            _return.options.scales.y.stacked = true;

            for (int _i = 0; _i < _return.data.datasets.Length; _i++)
            {
                _return.data.datasets[_i].stack = "Stack 1";
            }

            return _return;
        }
        public static Config BarCharts_Floating(ChartFormat _chart) //IMPLEMENTAR
        {
            Config _return = ChartJSHelper.BarCharts_Vertical(_chart);

            return _return;
        }
        public static Config BarCharts_BorderRadius(ChartFormat _chart)
        {
            Config _return = ChartJSHelper.BarCharts_Vertical(_chart);

            for (int _i = 0; _i < _return.data.datasets.Length; _i++)
            {
                _return.data.datasets[_i].borderRadius = 50;
                _return.data.datasets[_i].borderSkipped = false;
            }

            return _return;
        }

        public static Config LineCharts(ChartFormat _chart)
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
        public static Config LineCharts_MultiAxis(ChartFormat _chart) //AFINAR
        {
            Config _return = ChartJSHelper.LineCharts(_chart);
            _return.options.interaction = new Interaction();
            _return.options.scales = new Scales();
            _return.options.scales.y = new Y();
            _return.options.scales.y2 = new Y();

            _return.options.interaction.mode = "index";
            _return.options.interaction.intersect = false;
            _return.options.scales.y.type = "linear";
            _return.options.scales.y.display = true;
            _return.options.scales.y.position = "left";
            _return.options.scales.y2.type = "linear";
            _return.options.scales.y2.display = true;
            _return.options.scales.y2.position = "right";

            for (int _i = 0; _i < _return.data.datasets.Length; _i++)
            {
                _return.data.datasets[_i].yAxisID = "y2";
            }

            return _return;
        }
        public static Config LineCharts_Stepped(ChartFormat _chart)
        {
            Config _return = ChartJSHelper.LineCharts(_chart);
            _return.options.interaction = new Interaction();

            _return.options.interaction.intersect = false;
            _return.options.interaction.axis = "x";

            for (int _i = 0; _i < _chart.Series.Length; _i++)
            {
                _return.data.datasets[_i].fill = false;
                _return.data.datasets[_i].stepped = true;
            }

            return _return;
        }
        public static Config LineCharts_InterpolationModes(ChartFormat _chart) //AFINAR NaN
        {
            Config _return = ChartJSHelper.LineCharts(_chart);
            _return.options.interaction = new Interaction();
            _return.options.scales = new Scales();
            _return.options.scales.x = new X();
            _return.options.scales.y = new Y();
            _return.options.scales.x.title = new Title();
            _return.options.scales.y.title = new Title();

            _return.options.interaction.intersect = false;
            _return.options.scales.x.display = true;
            _return.options.scales.x.title.display = true;
            _return.options.scales.x.title.text = "values X";
            _return.options.scales.y.display = true;
            _return.options.scales.y.title.display = true;
            _return.options.scales.y.title.text = "values Y";
            _return.options.scales.y.suggestedMin = -10;
            _return.options.scales.y.suggestedMin = -200;

            for (int _i = 0; _i < _chart.Series.Length; _i++)
            {
                _return.data.datasets[_i].cubicInterpolationMode = "monotone"; //Cubic interpolation (monotone)
                _return.data.datasets[_i].tension = 0.5m; //Cubic interpolation
                _return.data.datasets[_i].fill = false; //Linear interpolation (default)
            }

            return _return;
        }
        public static Config LineCharts_Styling(ChartFormat _chart)
        {
            Config _return = ChartJSHelper.LineCharts(_chart);
            _return.options.interaction = new Interaction();
            _return.options.scales = new Scales();
            _return.options.scales.x = new X();
            _return.options.scales.y = new Y();
            _return.options.scales.x.title = new Title();
            _return.options.scales.y.title = new Title();

            _return.options.interaction.mode = "index";
            _return.options.interaction.intersect = false;
            _return.options.scales.x.display = true;
            _return.options.scales.x.title.display = true;
            _return.options.scales.x.title.text = "values X";
            _return.options.scales.y.display = true;
            _return.options.scales.y.title.display = true;
            _return.options.scales.y.title.text = "values Y";

            for (int _i = 0; _i < _chart.Series.Length; _i++)
            {
                //_return.data.datasets[_i].fill = false; //Unfilled
                _return.data.datasets[_i].borderDash = new int?[] { 5, 5 }; //Unfilled
                _return.data.datasets[_i].fill = true; //Filled
            }

            return _return;
        }
        public static Config LineCharts_SegmentStyling(ChartFormat _chart) //IMPLEMENTAR
        {
            Config _return = ChartJSHelper.LineCharts(_chart);

            return _return;
        }
    }
}
