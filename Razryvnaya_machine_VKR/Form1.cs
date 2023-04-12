using ClassLibrary1;
using System;
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
