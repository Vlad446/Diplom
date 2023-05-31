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
using System.Data.OleDb;
using System.Xml.Linq;
using Microsoft.SqlServer.Server;

namespace Razryvnaya_machine_VKR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            label26.Text = DateTime.Now.ToString("dd.MM.yyyy");

            dataGridView1.ColumnCount = 16;
            dataGridView1.Columns[0].Name = "ID";
            dataGridView1.Columns[1].Name = "Номер_испытания";
            dataGridView1.Columns[2].Name = "Дата_испытания";            
            dataGridView1.Columns[3].Name = "Относительное_удлиннение,%";
            dataGridView1.Columns[4].Name = "Относительное_сужение,%";
            dataGridView1.Columns[5].Name = "Предел_текучести_(условный),кгс/мм²";       
            dataGridView1.Columns[6].Name = "Временное_сопротивление,кгс/мм²";
            dataGridView1.Columns[7].Name = "Начальный_диаметр_сечения,мм";
            dataGridView1.Columns[8].Name = "Начальная_толщина_сечения,мм";
            dataGridView1.Columns[9].Name = "Начальная_ширина_сечения,мм";
            dataGridView1.Columns[10].Name = "Расчётная_длина,мм";
            dataGridView1.Columns[11].Name = "Рабочая_длина,мм";
            dataGridView1.Columns[12].Name = "Длина_после_разрыва,мм";
            dataGridView1.Columns[13].Name = "Диаметр_сечения_после_разрыва,мм";
            dataGridView1.Columns[14].Name = "Значение_максимума,кгс";
            dataGridView1.Columns[15].Name = "Значение_P02,кгс";

            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[11].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[12].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[13].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[14].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[15].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
                              
                int max = 0;
                int secMax = 0;
                if (Convert.ToInt16(numericUpDown1.Value) > max)
                {
                    max = Convert.ToInt16(numericUpDown1.Value);

                }
                secMax = max;
               
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

        private void button1_Click_1(object sender, EventArgs e)
        {           
            // Удаляем данные с ячейки
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            // Сверяем значения исходных данных
            if (textBox1.Text == "" ||
                textBox2.Text == "" ||
                textBox3.Text == "" ||
                textBox4.Text == "" ||
                textBox5.Text == "" ||
                textBox6.Text == "" ||
                textBox7.Text == "")
            {
                MessageBox.Show("Заполните исходные данные", "Внимание!");
                return;
            }
                  
            // Считывание значения исходных данных
            double d = double.Parse(textBox1.Text);
            double a0 = double.Parse(textBox2.Text);
            double b0 = double.Parse(textBox3.Text);
            double l0 = double.Parse(textBox4.Text);
            double dk = double.Parse(textBox5.Text);
            double lk = double.Parse(textBox6.Text);
            double d_ko = double.Parse(textBox7.Text);
            double p_max = double.Parse(textBox9.Text);
            double p_02 = Math.Round((l0 * 0.2), 2);
            textBox10.Text += Environment.NewLine + p_02.ToString();
            double Otn_udlin = Math.Round((100 * (lk - l0) / l0), 2);           
            textBox11.Text += Environment.NewLine + Otn_udlin.ToString();
            // Все ли поля заполнены
            if (textBox9.Text == "" ||
                textBox10.Text == "")
            {
                MessageBox.Show("Заполните значение максимума и P02", "Внимание!");
                return;
            }

            // Сверяем значения исходных данных
            if (d > 30 | d < 0)
            {
                MessageBox.Show("Начальный диаметр может быть в пределах от 0 до 30 мм", "Внимание!");
                return;
            }
            if (l0 > 300 | l0 < 0)
            {
                MessageBox.Show("Начальная расчетная длина образца образца может быть в пределах от 0 до 30 мм", "Внимание!");
                return;
            }
            if (a0 > 30 | a0 < 0)
            {
                MessageBox.Show("Начальная толщина образца может быть в пределах от 0 до 30 мм", "Внимание!");
                return;
            }
            if (b0 > 30 | b0 < 0)
            {
                MessageBox.Show("Начальная ширина образца может быть в пределах от 0 до 30 мм", "Внимание!");
                return;
            }

            // Вычисляем арифметическое выражение
            
                      
            if (radioButton3.Checked)
            {
                double Otn_suzh = Math.Round((100 * ((3.14 * Math.Pow(d, 2) / 4) - (3.14 * Math.Pow(d_ko, 2) / 4)) / (3.14 * Math.Pow(d_ko, 2) / 4)), 2);
                textBox12.Text += Environment.NewLine + Otn_suzh.ToString();
                double predel_tek = Math.Round((p_02 * 1000) / (3.14 * Math.Pow(d, 2) / 4), 2);
                textBox13.Text += Environment.NewLine + predel_tek.ToString();
                double vrem_sopr = Math.Round(p_max * 1000 / (3.14 * Math.Pow(d, 2) / 4), 2);
                textBox14.Text += Environment.NewLine + vrem_sopr.ToString();
            }
            if (radioButton4.Checked)
            {
                double Otn_suzh = Math.Round(100 * (dk * lk - a0 * b0) / (a0 * b0), 2);                
                textBox12.Text += Environment.NewLine + Otn_suzh.ToString();
                double predel_tek = Math.Round((p_02 * 1000) / (a0 * b0 ), 2);
                textBox13.Text += Environment.NewLine + predel_tek.ToString();
                double vrem_sopr = Math.Round(p_max * 1000 / (a0 * b0), 2);
                textBox14.Text += Environment.NewLine + vrem_sopr.ToString();
            }
            
           
            
        }
        private void button2_Click_1(object sender, EventArgs e)
        {       
            textBox1.Text = "6,01";
            textBox2.Text = "6,4";
            textBox3.Text = "20";
            textBox4.Text = "30";
            textBox5.Text = "60";
            textBox6.Text = "36,6";
            textBox7.Text = "4,61";
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
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

        private void radioButton3_CheckedChanged_1(object sender, EventArgs e)
        {        
            textBox1.Visible = true;
            textBox2.Visible = false;
            textBox3.Visible = false;
        }
        private void radioButton4_CheckedChanged_1(object sender, EventArgs e)
        {       
            textBox1.Visible = false;
            textBox2.Visible = true;
            textBox3.Visible = true;
        }   

        private void оПрограммеToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Users\burda\OneDrive\Рабочий стол\Razryvnaya_machine_VKR\app\Razryvnaya_machine_VKR\Folder\rukovodstvo.pdf");
        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Users\burda\OneDrive\Рабочий стол\Razryvnaya_machine_VKR\app\Razryvnaya_machine_VKR\Folder\rukovodstvo.pdf");

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Close();
        }
        int ID=1;
        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(ID, textBox8.Text, label26.Text,textBox11.Text, textBox12.Text, textBox13.Text, textBox14.Text, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text, textBox9.Text, textBox10.Text);
            ++ID;
        }

        private void button_download_Click(object sender, EventArgs e)
        {
            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\burda\\OneDrive\\Рабочий стол\\Razryvnaya_machine_VKR\\app\\Razryvnaya_machine_VKR\\Database.mdb";
            OleDbConnection dbConnection = new OleDbConnection(connectionString);
            dbConnection.Open();
            string query = "SELECT * FROM Report";
            OleDbCommand dbCommand = new OleDbCommand(query, dbConnection);
            OleDbDataReader dbReader = dbCommand.ExecuteReader();

            if (dbReader.HasRows == false)
            {
                MessageBox.Show("Данные не найдены!", "Ошибка!");
            }
            else
            {
                while (dbReader.Read())
                {
                    dataGridView1.Rows.Add(dbReader["ID"], dbReader["Номер_испытания"], dbReader["Дата_испытания"], dbReader["Относительное_удлиннение,%"], dbReader["Относительное_сужение,%"], dbReader["Предел_текучести_(условный),кгс/мм²"], dbReader["Временное_сопротивление,кгс/мм²"],dbReader["Начальный_диаметр_сечения,мм"], dbReader["Начальная_толщина_сечения,мм"], dbReader["Начальная_ширина_сечения,мм"], dbReader["Расчётная_длина,мм"], dbReader["Рабочая_длина,мм"], dbReader["Длина_после_разрыва,мм"], dbReader["Диаметр_сечения_после_разрыва,мм"],dbReader["Значение_максимума,кгс"], dbReader["Значение_P02,кгс"]);
                }
            }
            dbReader.Close();
            dbConnection.Close();
        }

       

        private void button_add_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count !=1)
            {
                MessageBox.Show("Выберите одну строку!", "Внимание!");
                return;
            }

            int index = dataGridView1.SelectedRows[0].Index;

            if (dataGridView1.Rows[index].Cells[0].Value==null ||
                dataGridView1.Rows[index].Cells[1].Value == null ||
                dataGridView1.Rows[index].Cells[2].Value == null ||
                dataGridView1.Rows[index].Cells[3].Value == null ||
                dataGridView1.Rows[index].Cells[4].Value == null ||
                dataGridView1.Rows[index].Cells[5].Value == null ||
                dataGridView1.Rows[index].Cells[6].Value == null ||
                dataGridView1.Rows[index].Cells[7].Value == null ||
                dataGridView1.Rows[index].Cells[8].Value == null ||
                dataGridView1.Rows[index].Cells[9].Value == null ||
                dataGridView1.Rows[index].Cells[10].Value == null ||
                dataGridView1.Rows[index].Cells[11].Value == null ||
                dataGridView1.Rows[index].Cells[12].Value == null ||
                dataGridView1.Rows[index].Cells[13].Value == null ||
                dataGridView1.Rows[index].Cells[14].Value == null ||
                dataGridView1.Rows[index].Cells[15].Value == null)
            {
                MessageBox.Show("Не все данные введены!", "Внимание!");
                return;
            }

            string id = dataGridView1.Rows[index].Cells[0].Value.ToString();
            string nomer = dataGridView1.Rows[index].Cells[1].Value.ToString();            
            string data = dataGridView1.Rows[index].Cells[2].Value.ToString();
            string otn1 = dataGridView1.Rows[index].Cells[3].Value.ToString();
            string otn2 =dataGridView1.Rows[index].Cells[4].Value.ToString();
            string predel= dataGridView1.Rows[index].Cells[5].Value.ToString();
            string vrem =dataGridView1.Rows[index].Cells[6].Value.ToString();
            string nach_d = dataGridView1.Rows[index].Cells[7].Value.ToString();
            string nach_t = dataGridView1.Rows[index].Cells[8].Value.ToString();
            string nach_sh = dataGridView1.Rows[index].Cells[9].Value.ToString();
            string rash_d = dataGridView1.Rows[index].Cells[10].Value.ToString();
            string rab_d = dataGridView1.Rows[index].Cells[11].Value.ToString();
            string konech_dlin = dataGridView1.Rows[index].Cells[12].Value.ToString();
            string konech_d = dataGridView1.Rows[index].Cells[13].Value.ToString();          
            string max_znach = dataGridView1.Rows[index].Cells[14].Value.ToString();
            string zn_p = dataGridView1.Rows[index].Cells[15].Value.ToString();         


            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\burda\\OneDrive\\Рабочий стол\\Razryvnaya_machine_VKR\\app\\Razryvnaya_machine_VKR\\Database.mdb";
            OleDbConnection dbConnection = new OleDbConnection(connectionString);
            dbConnection.Open();
            string query = $"INSERT INTO Report VALUES ("+id+",'"+nomer+"','"+data+ "','" + otn1+"','"+otn2+"','"+predel+"','"+vrem+ "','"+nach_d+ "','"+ nach_t + "','"+ nach_sh + "','"+ rash_d + "','"+ rab_d + "','" + konech_dlin + "','"+ konech_d + "','"+ max_znach + "','"+ zn_p + "')";
            OleDbCommand dbCommand = new OleDbCommand(query, dbConnection);
            if (dbCommand.ExecuteNonQuery() != 1)
                MessageBox.Show("Ошибка выполнения запроса!", "Ошибка!");
            else
            {
                MessageBox.Show("Данные добавлены!", "Внимание!"); 
            }
            dbConnection.Close();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1)
            {
                MessageBox.Show("Выберите одну строку!", "Внимание!");
                return;
            }

            int index = dataGridView1.SelectedRows[0].Index;

            if (dataGridView1.Rows[index].Cells[0].Value == null ||
                dataGridView1.Rows[index].Cells[1].Value == null ||
                dataGridView1.Rows[index].Cells[2].Value == null ||
                dataGridView1.Rows[index].Cells[3].Value == null ||
                dataGridView1.Rows[index].Cells[4].Value == null ||
                dataGridView1.Rows[index].Cells[5].Value == null ||
                dataGridView1.Rows[index].Cells[6].Value == null ||
                dataGridView1.Rows[index].Cells[7].Value == null ||
                dataGridView1.Rows[index].Cells[8].Value == null ||
                dataGridView1.Rows[index].Cells[9].Value == null ||
                dataGridView1.Rows[index].Cells[10].Value == null ||
                dataGridView1.Rows[index].Cells[11].Value == null ||
                dataGridView1.Rows[index].Cells[12].Value == null ||
                dataGridView1.Rows[index].Cells[13].Value == null ||
                dataGridView1.Rows[index].Cells[14].Value == null ||
                dataGridView1.Rows[index].Cells[15].Value == null)
            {
                MessageBox.Show("Не все данные введены!", "Внимание!");
                return;
            }

            string id = dataGridView1.Rows[index].Cells[0].Value.ToString();
            string nomer = dataGridView1.Rows[index].Cells[1].Value.ToString();
            string data = dataGridView1.Rows[index].Cells[2].Value.ToString();
            string otn1 = dataGridView1.Rows[index].Cells[3].Value.ToString();
            string otn2 = dataGridView1.Rows[index].Cells[4].Value.ToString();
            string predel = dataGridView1.Rows[index].Cells[5].Value.ToString();
            string vrem = dataGridView1.Rows[index].Cells[6].Value.ToString();
            string nach_d = dataGridView1.Rows[index].Cells[7].Value.ToString();
            string nach_t = dataGridView1.Rows[index].Cells[8].Value.ToString();
            string nach_sh = dataGridView1.Rows[index].Cells[9].Value.ToString();
            string rash_d = dataGridView1.Rows[index].Cells[10].Value.ToString();
            string rab_d = dataGridView1.Rows[index].Cells[11].Value.ToString();
            string konech_dlin = dataGridView1.Rows[index].Cells[12].Value.ToString();
            string konech_d = dataGridView1.Rows[index].Cells[13].Value.ToString();
            string max_znach = dataGridView1.Rows[index].Cells[14].Value.ToString();
            string zn_p = dataGridView1.Rows[index].Cells[15].Value.ToString();

            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\burda\\OneDrive\\Рабочий стол\\Razryvnaya_machine_VKR\\app\\Razryvnaya_machine_VKR\\Database.mdb";
            OleDbConnection dbConnection = new OleDbConnection(connectionString);
            dbConnection.Open();
            string query = "UPDATE Report SET [Номер_испытания] = '" + nomer + "', [Дата_испытания] = '" + data + "', [Относительное_удлиннение,%] = '" + otn1 + "', [Относительное_сужение,%] = '" + otn2 + "', [Предел_текучести_(условный),кгс/мм²] = '" + predel + "', [Временное_сопротивление,кгс/мм²] = '" + vrem + "',[Начальный_диаметр_сечения,мм] = '" + nach_d + "',[Начальная_толщина_сечения,мм] = '" + nach_t + "',[Начальная_ширина_сечения,мм] = '" + nach_sh + "',[Расчётная_длина,мм] = '" + rash_d + "',[Рабочая_длина,мм] = '" + rab_d + "',[Длина_после_разрыва,мм] = '" + konech_dlin + "',[Диаметр_сечения_после_разрыва,мм] = '" + konech_d + "',[Значение_максимума,кгс] = '" + max_znach + "',[Значение_P02,кгс] = '" + zn_p + "' WHERE ID = " + id;
            OleDbCommand dbCommand = new OleDbCommand(query, dbConnection);
            if (dbCommand.ExecuteNonQuery() != 1)
                MessageBox.Show("Ошибка выполнения запроса!", "Ошибка!");
            else
            {
                MessageBox.Show("Данные изменены!", "Внимание!");
            }
            dbConnection.Close();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1)
            {
                MessageBox.Show("Выберите одну строку!", "Внимание!");
                return;
            }

            int index = dataGridView1.SelectedRows[0].Index;

            if (dataGridView1.Rows[index].Cells[0].Value == null)
            {
                MessageBox.Show("Не все данные введены!", "Внимание!");
                return;
            }

            string id = dataGridView1.Rows[index].Cells[0].Value.ToString();           

            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\burda\\OneDrive\\Рабочий стол\\Razryvnaya_machine_VKR\\app\\Razryvnaya_machine_VKR\\Database.mdb";
            OleDbConnection dbConnection = new OleDbConnection(connectionString);
            dbConnection.Open();
            string query = "DELETE FROM Report WHERE ID = " + id;
            OleDbCommand dbCommand = new OleDbCommand(query, dbConnection);
            if (dbCommand.ExecuteNonQuery() != 1)
                MessageBox.Show("Ошибка выполнения запроса!", "Ошибка!");
            else
            { 
                MessageBox.Show("Данные удалены!", "Внимание!");
                dataGridView1.Rows.RemoveAt(index);
            }
            dbConnection.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Users\burda\OneDrive\Рабочий стол\Razryvnaya_machine_VKR\app\Razryvnaya_machine_VKR\Database.mdb");
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (radioButton3.Checked)
            {
                report1.Load("Report_params1.frx");
                report1.Show();
            }
            if (radioButton4.Checked)
            {
                report1.Load("Report_params2.frx");
                report1.Show();
            }
            
        }
    }
}
