using System;
using System.Linq;

using imL.JavaScript.ChartJS.Schema;

namespace imL.JavaScript.ChartJS
{
    public static class ChartJSHelper
    {
        public static int[] numbers(int _length, int _min = -100, int _max = 100)
        {
            int[] _return = new int[_length];

            Random _r = new Random();
            for (int _i = 0; _i < _length; _i++)
            {
                _return[_i] = _r.Next(_min, _max);
            }

            return _return;
        }
        //public static string[] months(int _length)
        //{
        //    string[] _months = new string[] {
        //        "January",
        //        "February",
        //        "March",
        //        "April",
        //        "May",
        //        "June",
        //        "July",
        //        "August",
        //        "September",
        //        "October",
        //        "November",
        //        "December"
        //    };

        //    return _months.Take(_length).ToArray();
        //}

        public static Config Test()
        {
            string[] _labels = new string[] {
                    "Red",
                    "Blue",
                    "Yellow",
                    "Green",
                    "Purple",
                    "Orange" };
            string[] _backgroundColor = new string[] {
                    "rgba(255, 99, 132, 0.2)",
                    "rgba(54, 162, 235, 0.2)",
                    "rgba(255, 206, 86, 0.2)",
                    "rgba(75, 192, 192, 0.2)",
                    "rgba(153, 102, 255, 0.2)",
                    "rgba(255, 159, 64, 0.2)"
                };
            string[] _borderColor = new string[] {
                    "rgba(255, 99, 132, 1)",
                    "rgba(54, 162, 235, 1)",
                    "rgba(255, 206, 86, 1)",
                    "rgba(75, 192, 192, 1)",
                    "rgba(153, 102, 255, 1)",
                    "rgba(255, 159, 64, 1)"
                };


            Config _return = new Config();
            _return.data = new Data();
            _return.data.datasets = new Dataset[] { new Dataset() };
            _return.options = new Options();
            _return.options.scales = new Scales();
            _return.options.scales.y = new Y();


            _return.type = "bar";
            _return.data.labels = _labels;
            _return.data.datasets[0].label = "# of Votes";
            _return.data.datasets[0].data = ChartJSHelper.numbers(6, 0);
            _return.data.datasets[0].backgroundColor = _backgroundColor;
            _return.data.datasets[0].borderColor = _borderColor;
            _return.data.datasets[0].borderWidth = 1;
            _return.options.scales.y.beginAtZero = true;

            return _return;
        }

        //public static Config BarCharts_VerticalBarChart()
        //{
        //    Config _return = new Config();
        //    _return.data = new Data();
        //    _return.data.datasets = new Dataset[] { new Dataset(), new Dataset() };
        //    _return.options = new Options();
        //    _return.options.plugins = new Plugins();
        //    _return.options.plugins.legend = new Legend();
        //    _return.options.plugins.title = new Title();

        //    Color[] _colors = BO_ChartJS.colors(5);

        //    Color _red = _colors[0];
        //    Color _blue = _colors[1];


        //    _return.type = "bar";
        //    _return.data.labels = BO_ChartJS.months(7);

        //    _return.data.datasets[0].label = "Dataset 1";
        //    _return.data.datasets[0].data = BO_ChartJS.numbers(7);
        //    _return.data.datasets[0].borderColor = new string[] { _red.ToStringRGB() };
        //    _return.data.datasets[0].backgroundColor = new string[] { _red.ToStringRGBA() };
        //    _return.data.datasets[0].borderWidth = 4;
        //    _return.data.datasets[0].borderRadius = 100;

        //    _return.data.datasets[1].label = "Dataset 2";
        //    _return.data.datasets[1].data = BO_ChartJS.numbers(7);
        //    _return.data.datasets[1].borderColor = new string[] { _blue.ToStringRGB() };
        //    _return.data.datasets[1].backgroundColor = new string[] { _blue.ToStringRGBA() };
        //    _return.data.datasets[1].borderWidth = 2;
        //    _return.data.datasets[1].borderRadius = 5;

        //    _return.options.responsive = true;
        //    _return.options.plugins.legend.position = "top";
        //    _return.options.plugins.title.display = true;
        //    _return.options.plugins.title.text = "Chart.js Bar Chart";

        //    return _return;
        //}

        public static Config BarCharts_VerticalBarChart(string[] _rgb, string[] _rgba)
        {
            Config _return = new Config();
            _return.data = new Data();
            _return.data.datasets = new Dataset[_rgb.Length];
            _return.options = new Options();
            _return.options.plugins = new Plugins();
            _return.options.plugins.legend = new Legend();
            _return.options.plugins.title = new Title();

            _return.type = "bar";
            _return.data.labels = _rgb.Select(_s => _s.ToString()).ToArray();

            for (int _i = 0; _i < _rgb.Length; _i++)
            {
                _return.data.datasets[_i] = new Dataset();

                _return.data.datasets[_i].label = "Dataset +" + _rgb[_i].ToString();
                _return.data.datasets[_i].data = ChartJSHelper.numbers(_rgb.Length);
                _return.data.datasets[_i].borderColor = new string[] { _rgb[_i] };
                _return.data.datasets[_i].backgroundColor = new string[] { _rgba[_i] };
                _return.data.datasets[_i].borderWidth = 2;
            }

            _return.options.responsive = true;
            _return.options.plugins.legend.position = "top";
            _return.options.plugins.title.display = true;
            _return.options.plugins.title.text = "Chart.js Bar Chart";

            return _return;
        }

        public static Config LineCharts_LineChart(string[] _rgb, string[] _rgba)
        {
            Config _return = new Config();
            _return.data = new Data();
            _return.data.datasets = new Dataset[_rgb.Length];
            _return.options = new Options();
            _return.options.plugins = new Plugins();
            _return.options.plugins.legend = new Legend();
            _return.options.plugins.title = new Title();

            _return.type = "line";
            _return.data.labels = _rgb.Select(_s => _s.ToString()).ToArray();

            for (int _i = 0; _i < _rgb.Length; _i++)
            {
                _return.data.datasets[_i] = new Dataset();

                _return.data.datasets[_i].label = "Dataset +" + _rgb[_i].ToString();
                _return.data.datasets[_i].data = ChartJSHelper.numbers(_rgb.Length);
                _return.data.datasets[_i].borderColor = new string[] { _rgb[_i] };
                _return.data.datasets[_i].backgroundColor = new string[] { _rgba[_i] };
            }

            _return.options.responsive = true;
            _return.options.plugins.legend.position = "top";
            _return.options.plugins.title.display = true;
            _return.options.plugins.title.text = "Chart.js Line Chart";


            return _return;
        }
        public static Config LineCharts_SteppedLineCharts(string[] _rgb)
        {
            Config _return = new Config();
            _return.data = new Data();
            _return.data.datasets = new Dataset[_rgb.Length];
            _return.options = new Options();
            _return.options.interaction = new Interaction();
            _return.options.plugins = new Plugins();
            _return.options.plugins.title = new Title();

            _return.type = "line";
            _return.data.labels = _rgb.Select(_s => _s.ToString()).ToArray();

            for (int _i = 0; _i < _rgb.Length; _i++)
            {
                _return.data.datasets[_i] = new Dataset();

                _return.data.datasets[_i].label = "Dataset +" + _rgb[_i].ToString();
                _return.data.datasets[_i].data = ChartJSHelper.numbers(_rgb.Length);
                _return.data.datasets[_i].borderColor = new string[] { _rgb[_i] };
                _return.data.datasets[_i].fill = false;
                _return.data.datasets[_i].stepped = true;
            }

            _return.options.responsive = true;
            _return.options.interaction.intersect = false;
            _return.options.interaction.axis = "x";
            _return.options.plugins.title.display = true;
            _return.options.plugins.title.text = "Step xXxXx Interpolation";


            return _return;
        }
    }
}
