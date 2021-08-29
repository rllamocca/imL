using System;
using System.Drawing;
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
        public static string[] months(int _length)
        {
            string[] _months = new string[] {
                "January",
                "February",
                "March",
                "April",
                "May",
                "June",
                "July",
                "August",
                "September",
                "October",
                "November",
                "December"
            };

            return _months.Take(_length).ToArray();
        }
        public static Color[] colors()
        {
            byte _por = Convert.ToByte(1 * 255);
            Color[] _colors;

            //_colors = new Color[] {
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#0d6efd")),
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#6610f2")),
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#6f42c1")),
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#d63384")),
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#dc3545")),
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#fd7e14")),
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#ffc107")),
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#198754")),
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#20c997")),
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#0dcaf0"))
            //};

            //_colors = new Color[] {
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#4dc9f6")),
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#f67019")),
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#f53794")),
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#537bc4")),
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#acc236")),
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#166a8f")),
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#00a950")),
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#58595b")),
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#8549ba"))
            //};

            _colors = new Color[] {
                Color.FromArgb(_por, 255, 99, 132),
                Color.FromArgb(_por, 255, 159, 64),
                Color.FromArgb(_por, 255, 205, 86),
                Color.FromArgb(_por, 75, 192, 192),
                Color.FromArgb(_por, 54, 162, 235),
                Color.FromArgb(_por, 153, 102, 255),
                Color.FromArgb(_por, 201, 203, 207)
            };

            //return _colors.Take(_length).ToArray();


            //_colors = new Color[] {
            //    Color.FromArgb(_por, Color.Red),
            //    Color.FromArgb(_por, Color.Green),
            //    Color.FromArgb(_por, Color.Blue),

            //    Color.FromArgb(_por, new Color[] { Color.Red, Color.Green }.Blend()),
            //    Color.FromArgb(_por, new Color[] { Color.Green, Color.Blue }.Blend()),
            //    Color.FromArgb(_por, new Color[] { Color.Blue, Color.Red }.Blend()),

            //    Color.FromArgb(_por, new Color[] { Color.Red, Color.Green, Color.Blue }.Blend())
            //};

            //Color _red = Color.FromArgb(255, 0, 0);
            //Color _green = Color.FromArgb(0, 255, 0);
            //Color _blue = Color.FromArgb(0, 0, 255);

            //_colors = new Color[] {
            //    Color.FromArgb(_por, _red),
            //    Color.FromArgb(_por, _green),
            //    Color.FromArgb(_por, _blue),

            //    Color.FromArgb(_por, new Color[] { _red, _green }.Blend()),
            //    Color.FromArgb(_por, new Color[] { _green, _blue }.Blend()),
            //    Color.FromArgb(_por, new Color[] { _blue, _red }.Blend()),

            //    Color.FromArgb(_por, new Color[] { _red, _green, _blue }.Blend())
            //};


            //_colors = new Color[] {
            //    Color.FromArgb(_por, Color.Cyan),
            //    Color.FromArgb(_por, Color.Magenta),
            //    Color.FromArgb(_por, Color.Yellow),

            //    Color.FromArgb(_por, new Color[] { Color.Cyan, Color.Magenta }.Blend()),
            //    Color.FromArgb(_por, new Color[] { Color.Magenta, Color.Yellow }.Blend()),
            //    Color.FromArgb(_por, new Color[] { Color.Yellow, Color.Cyan }.Blend()),

            //    Color.FromArgb(_por, new Color[] { Color.Cyan, Color.Magenta, Color.Yellow }.Blend())
            //};

            //Color _cyan = Color.FromArgb(0, 176, 246);
            //Color _magenta = Color.FromArgb(245, 0, 135);
            //Color _yellow = Color.FromArgb(255, 233, 0);
            //Color _cyan = Color.FromArgb(0, 235, 245);
            //Color _magenta = Color.FromArgb(245, 0, 235);
            //Color _yellow = Color.FromArgb(245, 235, 0);

            //_colors = new Color[] {
            //    Color.FromArgb(_por, _cyan),
            //    Color.FromArgb(_por, _magenta),
            //    Color.FromArgb(_por, _yellow),

            //    Color.FromArgb(_por, new Color[] { _cyan, _magenta }.Blend()),
            //    Color.FromArgb(_por, new Color[] { _magenta, _yellow }.Blend()),
            //    Color.FromArgb(_por, new Color[] { _yellow, _cyan }.Blend()),

            //    Color.FromArgb(_por, new Color[] { _cyan, _magenta, _yellow }.Blend())
            //};

            return _colors;
        }

        public static string ToStringRGB(this Color _value)
        {
            return string.Format("rgb({0},{1},{2})", _value.R, _value.G, _value.B);
        }
        public static string ToStringRGBA(this Color _value)
        {
            return string.Format("rgba({0},{1},{2},{3})", _value.R, _value.G, _value.B, Math.Round(_value.A / 255.0, 4));
        }
        public static Color Blend(this Color _value, Color _value2, double _por = 0.5)
        {
            double _por2 = 1.0 - _por;

            double _a = _value.A * _por + _value2.A * _por2;
            double _r = _value.R * _por + _value2.R * _por2;
            double _g = _value.G * _por + _value2.G * _por2;
            double _b = _value.B * _por + _value2.B * _por2;

            return Color.FromArgb(Convert.ToByte(_a), Convert.ToByte(_r), Convert.ToByte(_g), Convert.ToByte(_b));
        }
        public static Color Blend(this Color[] _array)
        {
            if (_array == null)
                return default;

            double _por = 1.0 / _array.Length;
            double _a = 0;
            double _r = 0;
            double _g = 0;
            double _b = 0;

            foreach (Color _item in _array)
            {
                _a += _item.A;
                _r += _item.R;
                _g += _item.G;
                _b += _item.B;
            }

            _a = _por * _a;
            _r = _por * _r;
            _g = _por * _g;
            _b = _por * _b;

            return Color.FromArgb(Convert.ToByte(_a), Convert.ToByte(_r), Convert.ToByte(_g), Convert.ToByte(_b));
        }
        public static Color Blend_CMYK(this Color[] _array)
        {
            if (_array == null)
                return default;

            double _por = 1.0 / _array.Length;
            double _a = 0;
            double _r = 0;
            double _g = 0;
            double _b = 0;

            foreach (Color _item in _array)
            {
                _a += _item.A;
                _r += _item.R;
                _g += _item.G;
                _b += _item.B;
            }

            _a = _por * _a;
            _r = _por * _r;
            _g = _por * _g;
            _b = _por * _b;

            return Color.FromArgb(Convert.ToByte(_a), Convert.ToByte(_r), Convert.ToByte(_g), Convert.ToByte(_b));
        }

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

        public static Config BarCharts_VerticalBarChart()
        {
            Color[] _colors = ChartJSHelper.colors();

            Config _return = new Config();
            _return.data = new Data();
            _return.data.datasets = new Dataset[_colors.Length];
            _return.options = new Options();
            _return.options.plugins = new Plugins();
            _return.options.plugins.legend = new Legend();
            _return.options.plugins.title = new Title();

            _return.type = "bar";
            _return.data.labels = _colors.Select(_s => _s.ToString()).ToArray();

            for (int _i = 0; _i < _colors.Length; _i++)
            {
                Color _color = _colors[_i];
                _return.data.datasets[_i] = new Dataset();

                _return.data.datasets[_i].label = "Dataset +" + _color.ToString();
                _return.data.datasets[_i].data = ChartJSHelper.numbers(_colors.Length);
                _return.data.datasets[_i].borderColor = new string[] { _color.ToStringRGB() };
                _return.data.datasets[_i].backgroundColor = new string[] { _color.ToStringRGBA() };
                _return.data.datasets[_i].borderWidth = 2;
            }

            _return.options.responsive = true;
            _return.options.plugins.legend.position = "top";
            _return.options.plugins.title.display = true;
            _return.options.plugins.title.text = "Chart.js Bar Chart";

            return _return;
        }

        public static Config LineCharts_LineChart()
        {
            Color[] _colors = ChartJSHelper.colors();

            Config _return = new Config();
            _return.data = new Data();
            _return.data.datasets = new Dataset[_colors.Length];
            _return.options = new Options();
            _return.options.plugins = new Plugins();
            _return.options.plugins.legend = new Legend();
            _return.options.plugins.title = new Title();

            _return.type = "line";
            _return.data.labels = _colors.Select(_s => _s.ToString()).ToArray();

            for (int _i = 0; _i < _colors.Length; _i++)
            {
                Color _color = _colors[_i];
                _return.data.datasets[_i] = new Dataset();

                _return.data.datasets[_i].label = "Dataset +" + _color.ToString();
                _return.data.datasets[_i].data = ChartJSHelper.numbers(_colors.Length);
                _return.data.datasets[_i].borderColor = new string[] { _color.ToStringRGB() };
                _return.data.datasets[_i].backgroundColor = new string[] { _color.ToStringRGBA() };
            }

            _return.options.responsive = true;
            _return.options.plugins.legend.position = "top";
            _return.options.plugins.title.display = true;
            _return.options.plugins.title.text = "Chart.js Line Chart";


            return _return;
        }
        public static Config LineCharts_SteppedLineCharts()
        {
            Color[] _colors = ChartJSHelper.colors();

            Config _return = new Config();
            _return.data = new Data();
            _return.data.datasets = new Dataset[_colors.Length];
            _return.options = new Options();
            _return.options.interaction = new Interaction();
            _return.options.plugins = new Plugins();
            _return.options.plugins.title = new Title();

            _return.type = "line";
            _return.data.labels = _colors.Select(_s => _s.ToString()).ToArray();

            for (int _i = 0; _i < _colors.Length; _i++)
            {
                Color _color = _colors[_i];
                _return.data.datasets[_i] = new Dataset();

                _return.data.datasets[_i].label = "Dataset +" + _color.ToString();
                _return.data.datasets[_i].data = ChartJSHelper.numbers(_colors.Length);
                _return.data.datasets[_i].borderColor = new string[] { _color.ToStringRGB() };
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
