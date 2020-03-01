using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Otel_1
{
    public partial class finance : Form
    {
        public finance()
        {
            InitializeComponent();

        }

        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\otel.mdf;Integrated Security=True;Connect Timeout=30";



        private void finance_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();

                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand mycommannd = conn.CreateCommand();
                mycommannd.CommandText = "SELECT * FROM Клиенты";
                conn.Open();
                SqlDataReader dataReader;
                dataReader = mycommannd.ExecuteReader();

                while (dataReader.Read())
                {
                    int id = dataReader.GetInt32(0);
                    string family = dataReader.GetString(1);
                    string name = dataReader.GetString(2);
                    string surname = dataReader.GetString(3);
                    int id_number = dataReader.GetInt32(4);


                    dataGridView1.Rows.Add(id, family, name, surname, id_number);

                }
                dataReader.Close();
                conn.Close();
            }
            catch
            {
                MessageBox.Show("Увы, не удалось подключиться к базе данных");
            }
        }



        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            cell_items();
        }

        public void cell_items()
        {
            try
            {
                id_klients.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                family.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                name.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                surname.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                id_number.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            }
            catch
            {
                MessageBox.Show("Необходимо кого-то заселить! Вы выделяете пустую строчку", "Ошибка");
            }
        }


        private int how_many_days(int count)
        {
            

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand command = conn.CreateCommand();
            conn.Open();
            command.CommandText = "select Дата_заселения from Клиенты_информация where id_клиента =" + id_klients.Text;



            string date_1 = command.ExecuteScalar().ToString();
            conn.Close();



            DateTime dateTime_1 = new DateTime();
            dateTime_1 = Convert.ToDateTime(date_1);

            try
            {

                string date_2 = date_finish.Text;
                DateTime dateTime_2 = new DateTime();
                dateTime_2 = Convert.ToDateTime(date_2);



                while (dateTime_1 != dateTime_2)            // Определяем сколько дней жил клиент в номере
                {
                    dateTime_1 = dateTime_1.AddDays(1);
                    count++;
                }

                days.Text = count.ToString();

                return count;
            }
            catch
            {
                MessageBox.Show("Увы, скорее всего неверно введена дата", "Ошибка");
                return count - 1;
            }

            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            all_oplata();
        }

        private void all_oplata()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "select Цена_номера_в_сутки from Номера_гостиницы where id_номера = " + id_number.Text;
                conn.Open();
                double stoimost = Convert.ToDouble(command.ExecuteScalar());

                SqlCommand command_2 = conn.CreateCommand();
                command_2.CommandText = "select Оплатил from Клиенты_информация where id_клиента = " + id_klients.Text;

                double oplata = Convert.ToDouble(command_2.ExecuteScalar());

                conn.Close();

                int count = 1;

                count = how_many_days(count);

                if (count != 0)                     // Если мы не вошли в catch в методе подсчета количества дней
                {
                    double rezult = (count * stoimost) - oplata;            // Считаем конечную задолженность клиента с учетом оплаченных денег

                    money_klient.Text = rezult.ToString();
                }

                
            }
            catch
            {
                MessageBox.Show("Увы, не выделен клиент слева в таблице", "Ошибка");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            oplata oplata_klienta = new oplata(this);
            oplata_klienta.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "select Оплатил from Клиенты_информация where id_клиента = " + id_klients.Text;
                conn.Open();
                string qwe = command.ExecuteScalar().ToString();
                uge_oplacheno.Text = qwe.ToString();
                conn.Close();
            }
            catch
            {
                MessageBox.Show("Увы, не выделен клиент слева в таблице","Ошибка");
            }
        }
    }
}
