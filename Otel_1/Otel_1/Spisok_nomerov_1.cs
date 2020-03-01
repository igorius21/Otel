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
    public partial class Spisok_nomerov_1 : Form
    {
        public Spisok_nomerov_1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)      // Обновить значения из БД
        {
            read_data();
        }

        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\otel.mdf;Integrated Security=True;Connect Timeout=30";

        public void read_data()                             // Метод для вывода значений из БД в таблицу
        {
            try
            {
                dataGridView1.Rows.Clear();

                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand mycommannd = conn.CreateCommand();
                mycommannd.CommandText = "SELECT * FROM Номера_гостиницы";
                conn.Open();
                SqlDataReader dataReader;
                dataReader = mycommannd.ExecuteReader();

                while (dataReader.Read())
                {
                    int id = dataReader.GetInt32(0);
                    string tip = dataReader.GetString(1);
                    decimal cena = dataReader.GetDecimal(2);
                    int chislo = int.Parse(dataReader.GetString(3));

                    string status = null;                           // Создаем переменную status изначально как null
                    status = method_status(chislo, status);                   // Передаем status и chislo в метод (БД в разделе "Свободен_Занят" спроектирована в виде чисел)

                    dataGridView1.Rows.Add(id, tip, cena, status);      // Заполняем нашу таблицу

                }

                dataReader.Close();
                conn.Close();

            }
            catch
            {
                MessageBox.Show("Увы, нет соединения с базой данных");
            }
            
        }

        private string method_status(int chislo, string status)       // Метод для конвертации цисла из БД в разделе Свободен_Занят в строку в зависимости от количества клиентов в номере
        {                                                   
            switch (chislo)                                 // Пояснение конструкции: БД в разделе "Свободен_Занят" спроектирована в виде чисел, первая цифра в числе обозначает количество комнат в номере, вторая цифра в числе обозначает количество человек, живущих в номере поэтому 
            {                                               // переменную status изменяем в зависимости от условий, а именно от количества проживающих человек на текущий момент в номере
                                                           
                case 10:
                    status = "Свободен";
                    break;
                case 11:
                    status = "Занят";
                    break;
                case 20:
                    status = "Свободен";
                    break;
                case 21:
                    status = "Свободно_1_место";
                    break;
                case 22:
                    status = "Занят";
                    break;
                case 30:
                    status = "Свободен";
                    break;
                case 31:
                    status = "Свободно_2_места";
                    break;
                case 32:
                    status = "Свободно_1_место";
                    break;
                case 33:
                    status = "Занят";
                    break;

            }

            return status;                              //  и возвращаем переменную status в таблицу
        }

        
    }
}
