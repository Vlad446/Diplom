using ClassLibrary1;
using System;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Razryvnaya_machine_VKR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

        }

        private int _countSecond = 0;
        int limitNagruz = 1000;

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            panel1.BackColor = Color.Black;
            numericUpDown1.Maximum = 1000;
            numericUpDown1.Minimum = 0;

            chart1.ChartAreas[0].AxisY.Maximum = 1000;
            chart1.ChartAreas[0].AxisY.Minimum = 0;

            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "H:mm:ss";
            chart1.Series[0].XValueType = ChartValueType.DateTime;

            chart1.ChartAreas[0].AxisX.Minimum = DateTime.Now.ToOADate();
            chart1.ChartAreas[0].AxisX.Maximum = DateTime.Now.AddMinutes(1).ToOADate();

            chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Seconds;
            chart1.ChartAreas[0].AxisX.Interval = 6;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Random random = new Random(DateTime.Now.Millisecond);
            double ranNagruzka = random.Next(0, 1000);
            label19.Text = ranNagruzka.ToString();


            DateTime timeNow = DateTime.Now;
            int value = Convert.ToInt32(numericUpDown1.Value);

            chart1.Series[0].Points.AddXY(timeNow, value);
            chart1.Series[1].Points.AddXY(timeNow, ranNagruzka);

            _countSecond++;
            if (_countSecond == 60)
            {
                _countSecond = 0;
                chart1.ChartAreas[0].AxisX.Minimum = DateTime.Now.ToOADate();
                chart1.ChartAreas[0].AxisX.Maximum = DateTime.Now.AddMinutes(1).ToOADate();

                chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Seconds;
                chart1.ChartAreas[0].AxisX.Interval = 6;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Удаляем данные с ячейки
            textBox11.Text = "";
            textBox12.Text = "";

            // Считывание значения исходных данных
            double d = double.Parse(textBox1.Text);
            double a0 = double.Parse(textBox2.Text);
            double bo = double.Parse(textBox3.Text);
            double l0 = double.Parse(textBox4.Text);
            double dlina_raboch = double.Parse(textBox5.Text);
            double lk = double.Parse(textBox6.Text);
            double d_ko = double.Parse(textBox7.Text);
                      
            // Вычисляем арифметическое выражение
            double Otn_udlin = Math.Round((100 * (lk - l0) / l0),2);
            double Otn_suzh = Math.Round((100*((3.14*Math.Pow(d,2)/4)-(3.14* Math.Pow(d_ko, 2)/ 4))/(3.14 * Math.Pow(d_ko, 2) / 4)),2);

            // Выводим результат в окно
            textBox11.Text += Environment.NewLine+ Otn_udlin.ToString();
            textBox12.Text += Environment.NewLine + Otn_suzh.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "6,01";
            textBox2.Text = "10";
            textBox3.Text = "4";
            textBox4.Text = "30";
            textBox5.Text = "60";
            textBox6.Text = "36,6";
            textBox7.Text = "4,61";
        }

       
    }
}
