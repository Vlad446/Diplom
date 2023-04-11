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
            // Считывание значения исходных данных
            double d = double.Parse(textBox1.Text);
            double a0 = double.Parse(textBox2.Text);
            double bo = double.Parse(textBox3.Text);
            double l0 = double.Parse(textBox4.Text);
            double dlina_raboch = double.Parse(textBox5.Text);
            double lk = double.Parse(textBox6.Text);
            double d_ko = double.Parse(textBox7.Text);

            // Вывод значения в окно формулы

            // Вычисляем арифметическое выражение
            double Otn_udlin = 100 * (lk - l0) / l0;

            // Выводим результат в окно
            textBox11.Text += Environment.NewLine+ Otn_udlin.ToString();
        }
    }
}
