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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

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
            if (radioButton1.Checked)
            {
                panel1.BackColor = Color.Green;
                Random random = new Random(DateTime.Now.Millisecond);

                double ranNagruzka = random.Next(0, 1000);
                label19.Text = ranNagruzka.ToString();



                DateTime timeNow = DateTime.Now;
                int value = Convert.ToInt32(numericUpDown1.Value);

                chart1.Series[0].Points.AddXY(timeNow, value);
                chart1.Series[1].Points.AddXY(timeNow, ranNagruzka);

                if (Convert.ToInt16(numericUpDown1.Value) > 690)
                {
                   

                }

                ////if (Convert.ToInt16(value) > max)
                ////{                
                ////    max = value;

                ////}
                ////textBox9.Text = max.ToString();
                //max = Math.Max(0, value);

                //if (max> secMax)
                //{
                //    secMax=max;
                //}
                //else { textBox9.Text = value.ToString(); }
                //int[] array = new int[0];
                //NumericUpDown[] nud = new NumericUpDown[] {numericUpDown1};
                //for (int i = 0; i <= nud.Length; i++)
                //{
                //    array[i] = Convert.ToInt32(nud[i].Value);
                //}
                //int maxValue = array.Max();
                //textBox9.Text = array.ToString();
                //textBox9.Text = Math.Max(0, array).ToString();
                //textBox9.Text = Math.Max(0, numericUpDown1.Value).ToString();
                int max = 0;
                int secMax = 0;
                if (Convert.ToInt16(numericUpDown1.Value) > max)
                {
                    max = Convert.ToInt16(numericUpDown1.Value);

                }
                secMax = max;
                //else
                //{
                //    secMax = max;
                //}
                textBox9.Text = secMax.ToString();





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
           else
            { panel1.BackColor = Color.Red; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Удаляем данные с ячейки
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";

            // Считывание значения исходных данных
            double d = double.Parse(textBox1.Text);
            double a0 = double.Parse(textBox2.Text);
            double bo = double.Parse(textBox3.Text);
            double l0 = double.Parse(textBox4.Text);
            double dlina_raboch = double.Parse(textBox5.Text);
            double lk = double.Parse(textBox6.Text);
            double d_ko = double.Parse(textBox7.Text);
            double p_max = double.Parse(textBox9.Text);

            // Вычисляем арифметическое выражение
            double Otn_udlin = Math.Round((100 * (lk - l0) / l0),2);
            double Otn_suzh = Math.Round((100*((3.14*Math.Pow(d,2)/4)-(3.14* Math.Pow(d_ko, 2)/ 4))/(3.14 * Math.Pow(d_ko, 2) / 4)),2);
            double p_02 = Math.Round((l0 * 0.2), 2);
            double predel_tek = Math.Round((p_02*1000)/ (3.14 * Math.Pow(d, 2) / 4), 2);
            double vrem_sopr = Math.Round(p_max*1000/(3.14 * Math.Pow(d, 2) / 4), 2);
            // Выводим результат в окно            
            textBox10.Text += Environment.NewLine + p_02.ToString();
            textBox11.Text += Environment.NewLine + Otn_udlin.ToString();
            textBox12.Text += Environment.NewLine + Otn_suzh.ToString();
            textBox13.Text += Environment.NewLine + predel_tek.ToString();
            textBox14.Text += Environment.NewLine + vrem_sopr.ToString();
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.Clear(panel1.Parent.BackColor);
            Control control = panel1;
            int radius =64;
            using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
            {
                path.AddLine(radius, 0, control.Width - radius, 0);
                path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
                path.AddLine(control.Width, radius, control.Width, control.Height - radius);
                path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
                path.AddLine(control.Width - radius, control.Height, radius, control.Height);
                path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
                path.AddLine(0, control.Height - radius, 0, radius);
                path.AddArc(0, 0, radius, radius, 180, 90);
                using (SolidBrush brush = new SolidBrush(control.BackColor))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            textBox2.Visible = false;
            textBox3.Visible = false;
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            textBox2.Visible = true;
            textBox3.Visible = true;
        }

        
    }
}
