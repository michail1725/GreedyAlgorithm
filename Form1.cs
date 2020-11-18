using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HungryAlgorithm
{
    public partial class Form1 : Form
    {
        DataTable dt = new DataTable();
        public Form1()
        {
            InitializeComponent();
            dt.Columns.Add("Название");
            dt.Columns.Add("Цена(руб.)");
            dt.Columns.Add("Оценка");
            dt.Columns.Add("Отношение");
            //dt.Rows.Add("Хлеб", 50.0, "", "");
            //dt.Rows.Add("Проездной", 1075.0, "", "");
            //dt.Rows.Add("Интернет", 550.0, "", "");
            //dt.Rows.Add("Колбаса", 300.0, "", "");
            dt.Rows.Add("Сыр", 200.0, "", "");
            //dt.Rows.Add("Масло", 200.0, "", "");
            //dt.Rows.Add("Молоко", 60.0, "", "");
            //dt.Rows.Add("Моб. связь", 350.0, "", "");
            //dt.Rows.Add("Туалетная бумага", 150.0, "", "");
            //dt.Rows.Add("Канцелярия", 240.0, "", "");
            //dt.Rows.Add("Чай", 200.0, "", "");
            //dt.Rows.Add("Кофе", 250.0, "", "");
            //dt.Rows.Add("Общежитие", 850.0, "", "");
            //dt.Rows.Add("Шампунь", 100.0, "", "");
            //dt.Rows.Add("Зубная паста", 100.0, "", "");
            //dt.Rows.Add("Пряники", 60.0, "", "");
            //dt.Rows.Add("Сок", 90.0, "", "");
            //dt.Rows.Add("Лимонад", 70.0, "", "");
            //dt.Rows.Add("Сушки", 40.0, "", "");
            dt.Rows.Add("Чипсы", 160.0, "", "");
            dt.Rows.Add("Мясо", 250.0, "", "");
            dt.Rows.Add("Греча", 100.0, "", "");
            dt.Rows.Add("Спички", 5, "", "");
            dataGridView1.DataSource = dt;
        }

        private void Simulate_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            for (int i = 0; i < dt.Rows.Count; i++) {
                if (dt.Rows[i][2].ToString() == "") {
                    dt.Rows[i][2] = rnd.Next(10, 100);
                }
                dt.Rows[i][3] = Math.Round(Convert.ToDouble(dt.Rows[i][2]) / Convert.ToDouble(dt.Rows[i][1]),3);
            }
            dataGridView1.DataSource = dt;
            
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            DataView dv = dt.DefaultView;
            dv.Sort = "Отношение";
            dt = dv.ToTable();
            int bank = Convert.ToInt32(comboBox1.Text);
            int count = dt.Rows.Count;
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("Название");
            dt1.Columns.Add("Цена(руб.)");
            dt1.Columns.Add("Оценка");
            dt1.Columns.Add("Отношение");
            while (bank!=0 && count!=0) {
                if ((bank - Convert.ToInt32(dt.Rows[count - 1][1])) >= 0)
                {
                    bank -= Convert.ToInt32(dt.Rows[count - 1][1]);
                    dt1.Rows.Add(dt.Rows[count - 1][0], dt.Rows[count - 1][1], dt.Rows[count - 1][2], dt.Rows[count - 1][3]);
                    count -= 1;
                }
                else {
                    count -= 1;
                }
            }
            dataGridView2.DataSource = dt1;
            label1.Text = "На " + comboBox1.Text + " руб.(ост. " + bank +" руб.)" + Environment.NewLine +"мы можем купить " + dt1.Rows.Count + " позиций:";
            dv = dt1.DefaultView;
            dv.Sort = "Отношение DESC";
            dt1 = dv.ToTable();
        }
    }
}
