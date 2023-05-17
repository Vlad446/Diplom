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
            dataGridView1.ColumnCount = 7;
            dataGridView1.Columns[0].Name = "ID";
            dataGridView1.Columns[1].Name = "Номер испытания";
            dataGridView1.Columns[2].Name = "Дата испытания";
            dataGridView1.Columns[3].Name = "Относительное удлиннение, %";
            dataGridView1.Columns[4].Name = "Относительное сужение, %";
            dataGridView1.Columns[5].Name = "Предел текучести (условный), кгс/мм²";
            dataGridView1.Columns[6].Name = "Временное сопротивление, кгс/мм²";

            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private int _countSecond = 0;
        int limitNagruz = 1000;

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.РасчётыTableAdapter.Fill(this.dataSet1.Расчёты);
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

                //if (Convert.ToInt16(numericUpDown1.Value) > 690)
                //{
                   

                //}

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

            // Считывание значения исходных данных
            double d = double.Parse(textBox1.Text);
            double a0 = double.Parse(textBox2.Text);
            double b0 = double.Parse(textBox3.Text);
            double l0 = double.Parse(textBox4.Text);
            double dk = double.Parse(textBox5.Text);
            double lk = double.Parse(textBox6.Text);
            double d_ko = double.Parse(textBox7.Text);
            double p_max = double.Parse(textBox9.Text);

            // Вычисляем арифметическое выражение
            double Otn_udlin = Math.Round((100 * (lk - l0) / l0),2);
            double p_02 = Math.Round((l0 * 0.2), 2);
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
                //double Otn_suzh = Math.Round(100*(a0*b0-dk*lk)/(dk * lk), 2);
                textBox12.Text += Environment.NewLine + Otn_suzh.ToString();
                double predel_tek = Math.Round((p_02 * 1000) / (a0 * b0 ), 2);
                textBox13.Text += Environment.NewLine + predel_tek.ToString();
                double vrem_sopr = Math.Round(p_max * 1000 / (a0 * b0), 2);
                textBox14.Text += Environment.NewLine + vrem_sopr.ToString();
            }
            
            // Выводим результат в окно            
            textBox10.Text += Environment.NewLine + p_02.ToString();
            textBox11.Text += Environment.NewLine + Otn_udlin.ToString();
            //textBox12.Text += Environment.NewLine + Otn_suzh.ToString();
            //textBox13.Text += Environment.NewLine + predel_tek.ToString();
            //textBox14.Text += Environment.NewLine + vrem_sopr.ToString();
        }
        private void button2_Click_1(object sender, EventArgs e)
        {       
            textBox1.Text = "6,01";
            textBox2.Text = "64";
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

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Users\burda\OneDrive\Рабочий стол\Razryvnaya_machine_VKR\Razryvnaya_machine_VKR\Folder\rukovodstvo.pdf");
        } 

       

        private void button3_Click_1(object sender, EventArgs e)
        {            
                //DataRow row = _1DataSet.Эмиграция_населения.NewRow();
                //row[1] = textBox1.Text;
                //row[2] = int.Parse(textBox2.Text);
                //row[3] = textBox3.Text;
                //row[4] = textBox4.Text;
                //row[5] = textBox5.Text;
                //row[6] = textBox6.Text;
                //row[7] = textBox7.Text;
                //row[8] = textBox8.Text;
                //_1DataSet.Эмиграция_населения.Rows.Add(row);
                //эмиграция_населенияTableAdapter.Update(_1DataSet.Эмиграция_населения);
                //textBox1.Clear();
                //textBox2.Clear();
                //textBox3.Clear();
                //textBox4.Clear();
                //textBox5.Clear();
                //textBox6.Clear();
                //textBox7.Clear();
                //textBox8.Clear();
            
        }        

        private void оПрограммеToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Users\burda\OneDrive\Рабочий стол\Razryvnaya_machine_VKR\Razryvnaya_machine_VKR\Folder\rukovodstvo.pdf");
        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Users\burda\OneDrive\Рабочий стол\Razryvnaya_machine_VKR\Razryvnaya_machine_VKR\Folder\rukovodstvo.pdf");

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Close();
        }
        int ID=1;
        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(ID, textBox8.Text, label26.Text, textBox11.Text, textBox12.Text, textBox13.Text, textBox14.Text);
            ++ID;
        }

        private void button_download_Click(object sender, EventArgs e)
        {
            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";
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
                    dataGridView1.Rows.Add(dbReader["ID"], dbReader["Номер_испытания"], dbReader["Дата_испытания"], dbReader["Относительное_удлиннение,%"], dbReader["Относительное_сужение,%"], dbReader["Предел_текучести_(условный),кгс/мм²"], dbReader["Временное_сопротивление,кгс/мм²"]);
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
                dataGridView1.Rows[index].Cells[6].Value == null)
            {
                MessageBox.Show("Не все данные введены!", "Внимание!");
                return;
            }

            string id = dataGridView1.Rows[index].Cells[0].Value.ToString();
            string nomer = dataGridView1.Rows[index].Cells[1].Value.ToString();            
            string data = dataGridView1.Rows[index].Cells[2].Value.ToString();            
            string otn1 =dataGridView1.Rows[index].Cells[3].Value.ToString();
            string otn2 =dataGridView1.Rows[index].Cells[4].Value.ToString();
            string predel= dataGridView1.Rows[index].Cells[5].Value.ToString();
            string vrem =dataGridView1.Rows[index].Cells[6].Value.ToString();

            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";
            OleDbConnection dbConnection = new OleDbConnection(connectionString);
            dbConnection.Open();
            string query = $"INSERT INTO Report VALUES ("+id+",'"+nomer+"','"+data+"','"+otn1+"','"+otn2+"','"+predel+"','"+vrem+"')";
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
                dataGridView1.Rows[index].Cells[6].Value == null)
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

            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";
            OleDbConnection dbConnection = new OleDbConnection(connectionString);
            dbConnection.Open();
            string query = "UPDATE Report SET [Номер_испытания] = '" + nomer + "', [Дата_испытания] = '" + data + "', [Относительное_удлиннение,%] = '" + otn1 + "', [Относительное_сужение,%] = '" + otn2 + "', [Предел_текучести_(условный),кгс/мм²] = '" + predel + "', [Временное_сопротивление,кгс/мм²] = '" + vrem + "' WHERE ID = " + id;
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

            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";
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
            System.Diagnostics.Process.Start(@"C:\Users\burda\OneDrive\Рабочий стол\Razryvnaya_machine_VKR\Razryvnaya_machine_VKR\bin\Debug\Database.mdb");
        }
    }
}
