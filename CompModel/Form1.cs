using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CompModel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitFields();
        }

        private ChartSeries series;

        public void InitFields()
        {
            series = new ChartSeries(chart1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var x0 = 1.0;
            var y0 = 0.0;
            var step = 0.0;
            var iterations = 0;
            var parameters = GetParameters();
            var chart = GetChartType();
            if (textBox3.Text.Length != 0 && textBox2.Text.Length != 0)
            {
                if (!double.TryParse(textBox3.Text, out x0) || !double.TryParse(textBox2.Text, out y0))
                {
                    MessageBox.Show("Недопустимые координаты.");
                    return;
                }
            }

            if (textBox4.Text.Length != 0 && !double.TryParse(textBox4.Text, out step))
            {
                MessageBox.Show("Недопустимое значение шага.");
                return;
            }

            if (textBox4.Text.Length != 0 && !int.TryParse(textBox5.Text, out iterations)
                || iterations < 0)
            {
                MessageBox.Show("Недопустимое количество итераций.");
                return;
            }

            if (x0 < 0 || y0 < 0)
            {
                MessageBox.Show("Отрицательные значения недопустимы");
                return;
            }

            series.Draw(chart, x0, y0, step, iterations, parameters);
        }

        private Base GetChartType()
        {
            return new Task();
        }

        private double[] GetParameters()
        {
            var parameters = new List<double>();
            var splitResult = textBox1.Text.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (splitResult.Length == 0)
                return null;

            var parameter = 0.0;

            for (int i = 0; i < splitResult.Length; i++)
            {
                if (!double.TryParse(splitResult[i], out parameter))
                {
                    MessageBox.Show("Один или несколько параметров введены неверно.");
                    return null;
                }
                else
                    parameters.Add(parameter);
            }

            return parameters.ToArray();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            series.Clear();
        }
    }
}
