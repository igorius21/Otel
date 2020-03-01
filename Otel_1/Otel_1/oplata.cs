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
    public partial class oplata : Form
    {

        public oplata(finance f)            // Объединяем обе формы, родительскую и дочернюю
        {
            InitializeComponent();
            string a = f.id_klients.Text;
            label3.Text = a.ToString();
            
        }

        

        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\otel.mdf;Integrated Security=True;Connect Timeout=30";

        

        private void OK_Click(object sender, EventArgs e)
        {
            oplata_klient();
        }

        private void oplata_klient()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand command = conn.CreateCommand();


                conn.Open();
                command.CommandText = "select Оплатил from Клиенты_информация where id_клиента = " + label3.Text;

                double sum = Convert.ToDouble(command.ExecuteScalar());

                sum = sum + Convert.ToDouble(textBox1.Text);

                SqlCommand command_2 = conn.CreateCommand();
                command_2.CommandText = "update Клиенты_информация set Оплатил = " + sum + " where id_клиента = " + label3.Text;
                command_2.ExecuteNonQuery();

                conn.Close();


                DialogResult = DialogResult.OK;
            }
            catch
            {
                MessageBox.Show("Необходимо ввести сумму, либо Вы забыли выделить клиента в таблице слева в предыдущем окне","Ошибка");
            }
        }

       
    }
}
