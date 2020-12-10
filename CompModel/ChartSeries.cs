using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace CompModel
{
    class ChartSeries
    {
        private Chart chart1;
        private int _iterations;
        private double _step;
        private double defStep = 0.01;
        private int defIterations = 5000;

        public ChartSeries(Chart chart)
        {
            chart1 = chart;
        }

        public void Draw(Base chartt, double startX, double startY, double step, int iterations, double[] parameters)
        {
            _step = step == 0 ? defStep : step;
            _iterations = iterations == 0 ? defIterations : iterations;

            string fullname = GetFullName(chartt.BaseName, startX, startY, parameters);
                if (chart1.Series.FindByName(fullname) == null)
                SetupSeries(fullname);
            RungeKutt(chartt, chart1.Series[fullname], startX, startY, parameters);
        }

        private string GetFullName(string baseName, double x, double y, params double[] parameters)
        {
            var builder = new StringBuilder();
            builder.Append(baseName).Append("(X = ").Append(x).Append("; Y = ").Append(y).Append(")");
            if (parameters != null && parameters.Length != 0)
            {
                builder.Append(" (");
                for (int i = 0; i < parameters.Length; i++)
                    builder.Append("K = " + parameters[i]);
                builder.Append(")");
            }

            return builder.ToString().Trim();
        }

        private void SetupSeries(string seriesName)
        {
            chart1.Series.Add(new Series(seriesName));
            chart1.Legends.Add(new Legend(seriesName));
            chart1.Series[seriesName].ChartType = SeriesChartType.Spline;
            chart1.Legends[seriesName].Font = new Font("TimesNewRoman", 10);
            var rnd = new Random();
            chart1.Series[seriesName].Color = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
        }

        public void Clear()
        {
            chart1.Series.Clear();
            chart1.Legends.Clear();
        }

        private void RungeKutt(Base chart, Series series, double x, double y, double[] parameters)
        {
            for (int i = 0; i < _iterations; i++)
            {
                double k1 = _step * chart.f(x, y, parameters);
                double l1 = _step * chart.g(x, y, parameters);

                double k2 = _step * chart.f(x + k1 / 2, y + l1 / 2, parameters);
                double l2 = _step * chart.g(x + k1 / 2, y + l1 / 2, parameters);

                double k3 = _step * chart.f(x + k2 / 2, y + l2 / 2, parameters);
                double l3 = _step * chart.g(x + k2 / 2, y + l2 / 2, parameters);

                double k4 = _step * chart.f(x + k3, y + l3, parameters);
                double l4 = _step * chart.g(x + k3, y + l3, parameters);

                series.Points.AddXY(x, y);

                x += 1.0/6 * Math.Round(k1 + 2 * k2 + 2 * k3 + k4, 5);
                y += 1.0/6 * Math.Round(l1 + 2 * l2 + 2 * l3 + l4, 5);
            }
        }
    }
}
