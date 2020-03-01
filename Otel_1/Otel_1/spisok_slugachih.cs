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
    public partial class spisok_slugachih : Form
    {
        public spisok_slugachih()
        {
            InitializeComponent();
        }

        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\otel.mdf;Integrated Security=True;Connect Timeout=30";


        private void button2_Click(object sender, EventArgs e)
        {
            read_data();
        }


        private void read_data()           // Метод для чтения таблицы
        {
            string commandText = "SELECT * FROM Список_работников";
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(commandText, connectionString);
            adapter.Fill(table);
            dataGridView1.DataSource = table;

        }


        private void button4_Click(object sender, EventArgs e)
        {
            read_grafic();
        }

        private void read_grafic()
        {
            string commandText = "SELECT * FROM График_работы INNER JOIN Список_работников on График_работы.id_работника = Список_работников.id_работника";
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(commandText, connectionString);
            adapter.Fill(table);
            dataGridView2.DataSource = table;
        }



        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            vybor_rab();
        }

        private void vybor_rab()
        {
            try
            {
                box_1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                box_2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                box_3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            }
            catch
            {
                MessageBox.Show("Необходимо обновить базу служащих", "Ошибка");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            del_rab();
        }

        private void del_rab()
        {
            try
            {
                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\otel.mdf;Integrated Security=True;Connect Timeout=30";


                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand command = conn.CreateCommand();
                conn.Open();

                command.CommandText = "update Список_работников set Фамилия = '-', Имя = '-', Отчество = '-'  where id_работника = " + dataGridView1.CurrentRow.Cells[0].Value.ToString();

                command.ExecuteNonQuery();
                read_data();
                conn.Close();
            }
            catch
            {
                MessageBox.Show("Необходимо обновить список служащих слева, выбрать служащего, которого хотите удалить либо заменить, и только после этого нажмите на кнопку Удалить служащего", "Ошибка!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            add_rab();
        }

        private void add_rab()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand command = conn.CreateCommand();
                conn.Open();

                command.CommandText = "update Список_работников set Фамилия = '" + box_1.Text.ToString() + "', Имя = '" + box_2.Text.ToString() + "', Отчество = '" + box_3.Text.ToString() + "'  where id_работника = " + dataGridView1.CurrentRow.Cells[0].Value.ToString();

                command.ExecuteNonQuery();
                read_data();
                conn.Close();
            }
            catch
            {
                MessageBox.Show("Необходимо обновить список служащих слева, выбрать служащего, которого хотите удалить либо заменить, и только после этого нажмите на кнопку Добавить служащего", "Ошибка!");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            vybor_r();
        }

        private void vybor_r()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand command = conn.CreateCommand();
                conn.Open();

                command.CommandText = "select Фамилия from Список_работников inner join График_работы on Список_работников.id_работника = График_работы.id_работника where День_недели = '" + comboBox1.Text + "' and Этаж = " + comboBox2.Text;
                textBox1.Text = command.ExecuteScalar().ToString();
                conn.Close();
            }
            catch
            {
                MessageBox.Show("Необходимо выбрать день недели и этаж в выпадающих списках выше", "Ошибка!");
            }
        }


    }
}
