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
    public partial class Spisok_klientov : Form
    {
        public Spisok_klientov()
        {
            InitializeComponent();
            
        }

        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\otel.mdf;Integrated Security=True;Connect Timeout=30";


        private void Spisok_klientov_Load(object sender, EventArgs e)
        {
            read_table();
        }

        private void read_table()                   
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
                    int id_klients = dataReader.GetInt32(0);
                    string family = dataReader.GetString(1);
                    string name = dataReader.GetString(2);
                    string surname = dataReader.GetString(3);
                    int id_number = dataReader.GetInt32(4);

                                    

                    dataGridView1.Rows.Add(id_klients, family, name, surname, id_number);      // Заполняем нашу таблицу

                }

                dataReader.Close();
                conn.Close();


            }
            catch
            {
                MessageBox.Show("Что-то пошло не так, проблема с базой данных, попробуйте еще раз", "Ошибка");
            }
        }


        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)  // При клике мыши на строчке клиента в таблицу
        {
            add_textbox();
        }

        private void add_textbox()                      // Метод добавления значений из таблицы в texbox
        {
            try
            {
                family.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                name.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                surname.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                id.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();

                label6.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand command = conn.CreateCommand();
                conn.Open();

                command.Transaction = conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
                command.CommandText = "SELECT Откуда_приехал FROM Клиенты_информация WHERE id_клиента = " + label6.Text;
                city.Text = command.ExecuteScalar().ToString();
                command.CommandText = "SELECT Серия_номер_паспорта FROM Клиенты_информация WHERE id_клиента = " + label6.Text;
                passport.Text = command.ExecuteScalar().ToString();
                command.CommandText = "SELECT Дата_заселения FROM Клиенты_информация WHERE id_клиента = " + label6.Text;
                date.Text = command.ExecuteScalar().ToString();

                command.Transaction.Commit();

                conn.Close();
            }
            catch
            {
                MessageBox.Show("Вы выделяете пустую сточку, необходимо кого-то заселить", "Ошибка");

            }
        }


        private void clear_klient()  // Отдельный метод для очищения texbox_сов после удаления клиента
        {
            family.Clear();
            name.Clear();
            surname.Clear();
            id.Clear();
            city.Clear();
            passport.Clear();
            date.Clear();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            del_klient();
        }

        private void del_klient()                   // Выселяем клиента
        {
            try
            {
                int number;
                int.TryParse((id.Text), out number);
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT Свободен_Занят FROM Номера_гостиницы WHERE id_номера = " + number;
                conn.Open();
                number = Convert.ToInt32(command.ExecuteScalar());
                conn.Close();

                switch (number)             // Подробное объяснение данной конструкции лежит в файле new_klient.cs
                {                           // Тут аналогично
                    case 11:
                        number = 10;
                        break;
                    case 21:
                        number = 20;
                        break;
                    case 22:
                        number = 21;
                        break;
                    case 31:
                        number = 30;
                        break;
                    case 32:
                        number = 31;
                        break;
                    case 33:
                        number = 32;
                        break;
                    default:
                        number = 0;
                        break;

                }

                if (number != 0)
                {
                    int id_room = Convert.ToInt32(id.Text);
                    int id_klient = Convert.ToInt32(label6.Text);
                    SqlCommand command_2 = conn.CreateCommand();
                    conn.Open();
                    command_2.Transaction = conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
                    command_2.CommandText = "DELETE FROM Клиенты WHERE [id_клиента] = " + id_klient;
                    command_2.ExecuteNonQuery();
                    command_2.CommandText = "DELETE FROM Клиенты_информация WHERE [id_клиента] = " + id_klient;
                    command_2.ExecuteNonQuery();
                    command_2.CommandText = "UPDATE Номера_гостиницы SET Свободен_Занят = " + number + "WHERE id_номера = " + id_room;
                    command_2.ExecuteNonQuery();

                    command_2.Transaction.Commit();
                    conn.Close();

                    read_table();                                    // После выселения сразу обновляем нашу таблицу
                    MessageBox.Show("Изменения успешно внесены!");
                    clear_klient();
                }
                else
                {
                    MessageBox.Show("Нельзя выселить клиента!");
                    conn.Close();
                }
            }
            catch
            {
                MessageBox.Show("Увы, нет подключения к базе данных");
            }
        }

        

        private void button3_Click(object sender, EventArgs e)      // Определяем кто убирает номер
        {
            vybor_r();
            
        }


        private void vybor_r()                      // Метод определения служащего, который убирает текущий номер
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand command = conn.CreateCommand();
                conn.Open();

                command.CommandText = "SELECT Фамилия FROM Список_работников inner join График_работы on Список_работников.id_работника = (select id_работника FROM График_работы WHERE Этаж = (SELECT Этаж FROM Этаж WHERE id_номера = " + id.Text + ") and День_недели = '" + comboBox1.Text + "')";
                textBox2.Text = command.ExecuteScalar().ToString();
                conn.Close();
            }
            catch
            {
                MessageBox.Show("Необходимо выбрать день недели, а также клиента из таблицы справа", "Ошибка");
            }
        }


    }
}



 
