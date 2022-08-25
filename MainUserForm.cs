using System;
using System.Data;
using System.Windows.Forms;

namespace HungryAlgorithm
{
    public partial class MainUserForm : Form
    {
        DataTable dt = new DataTable();
        public MainUserForm()
        {
            InitializeComponent();
            FillProductTabel();
        }

        private void FillProductTabel() { //заполнение списка потребительской корзины
            dt.Columns.Add("Название");
            dt.Columns.Add("Цена(руб.)", typeof(int));
            dt.Columns.Add("Оценка");
            dt.Columns.Add("Отношение");
            dt.Rows.Add("Хлеб", 50, "", "");
            dt.Rows.Add("Проездной", 1200, "", "");
            dt.Rows.Add("Интернет", 550, "", "");
            dt.Rows.Add("Колбаса", 300, "", "");
            dt.Rows.Add("Сыр", 200, "", "");
            dt.Rows.Add("Масло", 200, "", "");
            dt.Rows.Add("Молоко", 60, "", "");
            dt.Rows.Add("Моб. связь", 500, "", "");
            dt.Rows.Add("Туалетная бумага", 200, "", "");
            dt.Rows.Add("Канцелярия", 240, "", "");
            dt.Rows.Add("Чай", 300, "", "");
            dt.Rows.Add("Кофе", 250, "", "");
            dt.Rows.Add("Общежитие", 1100, "", "");
            dt.Rows.Add("Шампунь", 180, "", "");
            dt.Rows.Add("Зубная паста", 100, "", "");
            dt.Rows.Add("Пряники", 60, "", "");
            dt.Rows.Add("Сок", 90, "", "");
            dt.Rows.Add("Лимонад", 70, "", "");
            dt.Rows.Add("Кроссовки", 3500, "", "");
            dt.Rows.Add("Лекарства", 800, "", "");
            dt.Rows.Add("Сушки", 40, "", "");
            dt.Rows.Add("Чипсы", 160, "", "");
            dt.Rows.Add("Мясо", 340, "", "");
            dt.Rows.Add("Греча", 100, "", "");
            dt.Rows.Add("Спички", 5, "", "");
            dataGridView1.DataSource = dt;
        }

        private void Simulate_Click(object sender, EventArgs e)//симуляция приоритетов
        {
            Random rnd = new Random();
           
            for (int i = 0; i < dt.Rows.Count; i++) {
                dt.Rows[i][2] = rnd.Next(10, 100);
                dt.Rows[i][3] = Math.Round(Convert.ToDouble(dt.Rows[i][2]) / Convert.ToDouble(dt.Rows[i][1]),3);
            }
            dataGridView1.DataSource = dt;
        }

        private void Maximize_Click(object sender, EventArgs e)// максимизация выбора продуктов на основании расчитанных коэффициентов 
        {
            if (StipendValue.SelectedIndex != -1)
            {
                label1.Visible = true;
                DataView dv = dt.DefaultView;
                dv.Sort = "Отношение";
                dt = dv.ToTable();
                int bank = Convert.ToInt32(StipendValue.Text);
                int count = dt.Rows.Count;
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("Название");
                dt1.Columns.Add("Цена(руб.)", typeof(int));
                dt1.Columns.Add("Оценка");
                dt1.Columns.Add("Отношение");
                while (bank != 0 && count != 0)
                {
                    if ((bank - Convert.ToInt32(dt.Rows[count - 1][1])) >= 0)
                    {
                        bank -= Convert.ToInt32(dt.Rows[count - 1][1]);
                        dt1.Rows.Add(dt.Rows[count - 1][0], dt.Rows[count - 1][1], dt.Rows[count - 1][2], dt.Rows[count - 1][3]);
                    }
                    count -= 1;
                }
                dataGridView2.DataSource = dt1;
                label1.Text = "На " + StipendValue.Text + " руб.(ост. " + bank + " руб.)" +  " мы можем купить " + dt1.Rows.Count + " позиций:";
                dv = dt1.DefaultView;
                dv.Sort = "Отношение DESC";
                dt1 = dv.ToTable();
            }
            else {
                MessageBox.Show("Значение стипендии не выбрано!");
            }
            
        }
    }
}
