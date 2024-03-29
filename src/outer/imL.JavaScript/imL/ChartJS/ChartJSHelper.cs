﻿using System.Linq;

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
        static Config Create(ChartFormat _chart)
        {
            Config _return = new Config
            {
                options = new Options
                {
                    interaction = new Interaction(),
                    scales = new Scales(),
                    plugins = new Plugins
                    {
                        legend = new Legend()
                    },

                    responsive = true
                }
            };
            _return.options.plugins.legend.position = "top";

            if (string.IsNullOrEmpty(_chart.Title) == false)
            {
                _return.options.plugins.title = new Title
                {
                    display = true,
                    text = _chart.Title
                };
            }

            if (_chart.XAxis != null && string.IsNullOrEmpty(_chart.XAxis.Name) == false)
            {
                _return.options.scales.x = new X
                {
                    title = new Title
                    {
                        display = true,
                        text = _chart.XAxis.Name
                    }
                };
            }
            if (_chart.YAxis != null && string.IsNullOrEmpty(_chart.YAxis.Name) == false)
            {
                _return.options.scales.y = new Y
                {
                    title = new Title
                    {
                        display = true,
                        text = _chart.XAxis.Name
                    }
                };
            }
            if (_chart.ZAxis != null && string.IsNullOrEmpty(_chart.ZAxis.Name) == false)
            {
                _return.options.scales.z = new Z
                {
                    title = new Title
                    {
                        display = true,
                        text = _chart.XAxis.Name
                    }
                };
            }
            /*
            if (_return.options.scales.x == null) _return.options.scales.x = new X();
            if (_return.options.scales.y == null) _return.options.scales.y = new Y();
            if (_return.options.scales.z == null) _return.options.scales.z = new Z();
            */

            return _return;
        }
        static void CleanScales(ref Config _ref)
        {
            if (_ref.options.scales.x != null) _ref.options.scales.x = null;
            if (_ref.options.scales.y != null) _ref.options.scales.y = null;
            if (_ref.options.scales.z != null) _ref.options.scales.z = null;
        }
        static void ToDataSets(ref Config _ref, ChartFormat _chart, bool _pileup = false, bool _border = true)
        {
            _ref.data = new Data();

            if (_pileup)
            {
                _ref.data.labels = _chart.Series.Select(_s => _s.Name).ToArray();
                //decimal?[] _values = _chart.Series.SelectMany(_s => _s.Values).Take(_ref.data.labels.Length).ToArray();
                decimal?[] _values = _chart.Series.Select(_s => _s.Values.FirstOrDefault()).ToArray();

                _ref.data.datasets = new Dataset[1];
                _ref.data.datasets[0] = new Dataset
                {
                    label = "pileup",
                    data = _values,
                    backgroundColor = _chart.BackgroundColor.ToArray()
                };

                if (_border)
                    _ref.data.datasets[0].borderColor = _chart.BorderColor.ToArray();
            }
            else
            {
                if (_chart.XAxis != null)
                    _ref.data.labels = _chart.XAxis.Axis.ToArray();

                _ref.data.datasets = new Dataset[_chart.Series.Count()];

                for (int _i = 0; _i < _ref.data.datasets.Length; _i++)
                {
                    SerieFormat _item = _chart.Series.ElementAt(_i);
                    _ref.data.datasets[_i] = new Dataset
                    {
                        label = _item.Name,
                        data = _item.Values.ToArray(),
                        backgroundColor = new string[] { _chart.BackgroundColor.ElementAt(_i % _chart.BackgroundColor.Count()) }
                    };

                    if (_border)
                        _ref.data.datasets[_i].borderColor = new string[] { _chart.BorderColor.ElementAt(_i % _chart.BorderColor.Count()) };
                }
            }
        }

        public static Config BarCharts_Vertical(ChartFormat _chart)
        {
            Config _return = ChartJSHelper.Create(_chart);
            _return.type = "bar";
            ChartJSHelper.ToDataSets(ref _return, _chart);

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

            if (_return.options.scales.x == null) _return.options.scales.x = new X();
            if (_return.options.scales.y == null) _return.options.scales.y = new Y();

            _return.options.scales.x.stacked = true;
            _return.options.scales.y.stacked = true;

            return _return;
        }
        public static Config BarCharts_StackedWithGroups(ChartFormat _chart)
        {
            //AFINAR
            Config _return = ChartJSHelper.BarCharts_Vertical(_chart);

            if (_return.options.scales.x == null) _return.options.scales.x = new X();
            if (_return.options.scales.y == null) _return.options.scales.y = new Y();

            _return.options.interaction.intersect = false;
            _return.options.scales.x.stacked = true;
            _return.options.scales.y.stacked = true;

            for (int _i = 0; _i < _return.data.datasets.Length; _i++)
            {
                _return.data.datasets[_i].stack = _i % 2 == 0 ? "stack 0" : "stack 1";
            }

            return _return;
        }
        public static Config BarCharts_Floating(ChartFormat _chart)
        {
            //IMPLEMENTAR
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
            Config _return = ChartJSHelper.Create(_chart);
            _return.type = "line";
            ChartJSHelper.ToDataSets(ref _return, _chart);

            return _return;
        }
        public static Config LineCharts_MultiAxis(ChartFormat _chart)
        {
            //AFINAR
            Config _return = ChartJSHelper.LineCharts(_chart);

            if (_return.options.scales.y == null) _return.options.scales.y = new Y();
            if (_return.options.scales.z == null) _return.options.scales.z = new Z();

            _return.options.scales.z.grid = new Grid();

            _return.options.interaction.mode = "index";
            _return.options.interaction.intersect = false;
            _return.options.scales.y.type = "linear";
            _return.options.scales.y.display = true;
            _return.options.scales.y.position = "left";
            _return.options.scales.z.type = "linear";
            _return.options.scales.z.display = true;
            _return.options.scales.z.position = "right";
            _return.options.scales.z.grid.drawOnChartArea = false;

            for (int _i = 0; _i < _return.data.datasets.Length; _i++)
            {
                _return.data.datasets[_i].yAxisID = _i % 2 == 0 ? "y" : "z";
            }

            return _return;
        }
        public static Config LineCharts_Stepped(ChartFormat _chart)
        {
            Config _return = ChartJSHelper.LineCharts(_chart);
            _return.options.interaction.intersect = false;
            _return.options.interaction.axis = "x";

            for (int _i = 0; _i < _chart.Series.Count(); _i++)
            {
                _return.data.datasets[_i].fill = false;
                _return.data.datasets[_i].stepped = true;
            }

            return _return;
        }
        public static Config LineCharts_InterpolationModes(ChartFormat _chart)
        {
            //AFINAR NaN
            Config _return = ChartJSHelper.LineCharts(_chart);

            if (_return.options.scales.x == null) _return.options.scales.x = new X();
            if (_return.options.scales.y == null) _return.options.scales.y = new Y();

            _return.options.interaction.intersect = false;
            _return.options.scales.x.display = true;
            _return.options.scales.y.display = true;
            _return.options.scales.y.suggestedMin = -10;
            _return.options.scales.y.suggestedMin = -200;

            for (int _i = 0; _i < _chart.Series.Count(); _i++)
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

            if (_return.options.scales.x == null) _return.options.scales.x = new X();
            if (_return.options.scales.y == null) _return.options.scales.y = new Y();

            _return.options.interaction.mode = "index";
            _return.options.interaction.intersect = false;
            _return.options.scales.x.display = true;
            _return.options.scales.y.display = true;

            for (int _i = 0; _i < _chart.Series.Count(); _i++)
            {
                //_return.data.datasets[_i].fill = false; //Unfilled
                _return.data.datasets[_i].borderDash = new int?[] { 5, 5 }; //Unfilled
                _return.data.datasets[_i].fill = true; //Filled
            }

            return _return;
        }
        public static Config LineCharts_SegmentStyling(ChartFormat _chart)
        {
            //IMPLEMENTAR
            Config _return = ChartJSHelper.LineCharts(_chart);

            return _return;
        }

        public static Config OtherCharts_Bubble(ChartFormat _chart)
        {
            //IMPLEMENTAR
            Config _return = ChartJSHelper.Create(_chart);
            _return.type = "bubble";
            ChartJSHelper.ToDataSets(ref _return, _chart);

            return _return;
        }
        public static Config OtherCharts_Doughnut(ChartFormat _chart)
        {
            Config _return = ChartJSHelper.Create(_chart);
            _return.type = "doughnut";
            ChartJSHelper.ToDataSets(ref _return, _chart, true, false);
            ChartJSHelper.CleanScales(ref _return);

            return _return;
        }
        public static Config OtherCharts_Pie(ChartFormat _chart)
        {
            Config _return = ChartJSHelper.Create(_chart);
            _return.type = "pie";
            ChartJSHelper.ToDataSets(ref _return, _chart, true, false);
            ChartJSHelper.CleanScales(ref _return);

            return _return;
        }
        public static Config OtherCharts_PolarArea(ChartFormat _chart)
        {
            Config _return = ChartJSHelper.Create(_chart);
            _return.type = "polarArea";
            ChartJSHelper.ToDataSets(ref _return, _chart, true, false);
            ChartJSHelper.CleanScales(ref _return);

            return _return;
        }
        public static Config OtherCharts_Radar(ChartFormat _chart)
        {
            Config _return = ChartJSHelper.Create(_chart);
            _return.type = "radar";
            ChartJSHelper.ToDataSets(ref _return, _chart);
            ChartJSHelper.CleanScales(ref _return);

            return _return;
        }
        public static Config OtherCharts_Combo_BarLine(ChartFormat _chart)
        {
            Config _return = ChartJSHelper.BarCharts_Vertical(_chart);

            for (int _i = 0; _i < _return.data.datasets.Length; _i++)
            {
                _return.data.datasets[_i].type = _i % 2 == 0 ? "bar" : "line";
                _return.data.datasets[_i].order = _i % 2 == 0 ? 1 : 0;
            }

            return _return;
        }
        public static Config OtherCharts_Stacked_BarLine(ChartFormat _chart)
        {
            Config _return = ChartJSHelper.BarCharts_Vertical(_chart);

            if (_return.options.scales.y == null) _return.options.scales.y = new Y();

            _return.options.scales.y.stacked = true;

            for (int _i = 0; _i < _return.data.datasets.Length; _i++)
            {
                _return.data.datasets[_i].stack = "stacked";
                _return.data.datasets[_i].type = _i % 2 == 0 ? "bar" : "line";
            }

            return _return;
        }
    }
}
