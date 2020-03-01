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
    public partial class new_klient : Form
    {
        public new_klient()
        {
            InitializeComponent();
            button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)   // Добавить нового клиента
        {

            new_klients();
            
        }

        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\otel.mdf;Integrated Security=True;Connect Timeout=30";


        private void new_klients()                               // Добавить нового клиента
        {
            try
            {
                string family = Convert.ToString(this.family.Text);     // Инициализируем переменные из texbox_сов.
                string name = Convert.ToString(this.name.Text);
                string surname = Convert.ToString(this.surname.Text);

                string city = Convert.ToString(this.city.Text);
                long passport = long.Parse(this.passport.Text);
                string date = Convert.ToString(this.date.Text);

                int number = int.Parse(this.number.Text);
                double oplata = double.Parse(this.oplata.Text);
                int id_klients = int.Parse(this.id_klients.Text);

                double oplata_2 = 0.00;     // Переменная для записи в БД начальной суммы, которую оплатил клиент, она равна нулю



                SqlConnection conn = new SqlConnection(connectionString);   
                

                SqlCommand myCommand = conn.CreateCommand();

                myCommand.Parameters.Add("@family", SqlDbType.NVarChar).Value = family;     //  создаем параметры


                myCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;


                myCommand.Parameters.Add("@surname", SqlDbType.NVarChar).Value = surname;


                myCommand.Parameters.Add("@city", SqlDbType.NVarChar, 50).Value = city;


                myCommand.Parameters.Add("@passport", SqlDbType.BigInt, 10).Value = passport;


                myCommand.Parameters.Add("@date", SqlDbType.Date).Value = date;


                myCommand.Parameters.Add("@number", SqlDbType.Int).Value = number;


                myCommand.Parameters.Add("@oplata", SqlDbType.Money).Value = oplata;

                myCommand.Parameters.Add("@oplata_2", SqlDbType.Decimal).Value = oplata_2;


                myCommand.Parameters.Add("@id_klients", SqlDbType.Int, 4).Value = id_klients;


                SqlCommand command_2 = conn.CreateCommand();        // Создадим новую команду

                command_2.CommandText = "SELECT Свободен_Занят FROM Номера_гостиницы WHERE id_номера = " + number;

                conn.Open();

                int chislo = Convert.ToInt32(command_2.ExecuteScalar());

                conn.Close();


                switch (chislo)         // Пояснение конструкции: БД в разделе "Свободен_Занят" спроектирована в виде чисел, первая цифра в числе обозначает количество комнат в номере, вторая цифра в числе обозначает количество человек, живущих в номере поэтому 
                {                       // создаем переменную chislo, в которую записываем то, что было в БД на текущий момент
                                        // Далее изменяем переменную chislo в зависимости от того, скольки комнатный номер и сколько человек уже живет в нем
                    case 10:            // 10 - Свободен
                        chislo = 11;    // 11 - Занят
                        break;
                    case 20:            // 20 - Свободен
                        chislo = 21;    // 21 - Свободно_1_место
                        break;
                    case 21:            // 21 - Свободно_1_место
                        chislo = 22;    // 22 - Занят
                        break;
                    case 30:            //30 - Свободен
                        chislo = 31;    // 31 - Свободно_2_места
                        break;
                    case 31:            // 31 - Свободно_2_места
                        chislo = 32;    // 32 - Свободно_1_место
                        break;
                    case 32:            // 32 - Свободно_1_место
                        chislo = 33;    // 33 - Занят
                        break;
                    default:
                        chislo = 0;
                        break;

                }

                if (chislo != 0)        // Проверка на состояние гостиничноо номера, которое нас устраивает
                {
                    conn.Open();

                    myCommand.Transaction = conn.BeginTransaction(System.Data.IsolationLevel.Serializable);     // Создаем транзакцию

                    myCommand.CommandText = "INSERT INTO Клиенты (id_клиента, Фамилия, Имя, Отчество, id_номера) VALUES (@id_klients, @family, @name, @surname, @number)";

                    myCommand.ExecuteNonQuery();

                    myCommand.CommandText = "UPDATE Номера_гостиницы SET Свободен_Занят = " + chislo + " WHERE id_номера = " + number;

                    myCommand.ExecuteNonQuery();

                    myCommand.CommandText = "INSERT INTO Клиенты_информация (id_клиента, Серия_номер_паспорта, Откуда_приехал, Дата_заселения, Необходимая_оплата, Оплатил) VALUES (@id_klients, @passport, @city, @date, @oplata, @oplata_2)";

                    myCommand.ExecuteNonQuery();

                    myCommand.Transaction.Commit();     // Завершаем тразакцию

                    conn.Close();

                    MessageBox.Show("Изменения успешно внесены!");

                    Close();

                }

                else
                {
                    MessageBox.Show("К сожалению, данный номер имеет максимальное количество проживающих!");
                    
                }

            }
            catch
            {
                MessageBox.Show("Скорее всего Вы ввели что-то не так, не вводите цифры вместо букв и наоборот, проверьте формат даты. Также все поля должны быть заполнены!");

                method_clear();
            }

        }

        private void button2_Click(object sender, EventArgs e)  // Стоимость номера в сутки заполняется автоматически после нажатия кнопки
        {
            data_set();
            
        }

        private void data_set()
        {
            try
            {
                double a = Convert.ToDouble(number.Text);
                if (a > 50)
                {
                    MessageBox.Show("Такого номера в гостинице нет", "Ошибка");
                    oplata.Text = "Такого номера нет";
                    button1.Enabled = false;
                }
                else
                {
                    SqlConnection conn = new SqlConnection(connectionString);

                    SqlCommand command = conn.CreateCommand();
                    command.CommandText = "SELECT Цена_номера_в_сутки FROM Номера_гостиницы WHERE id_номера = " + a;
                    conn.Open();
                    a = Convert.ToDouble(command.ExecuteScalar());
                    oplata.Text = a.ToString();
                    conn.Close();
                    comparison_id();                // если проверка номера прошла успешно, запускаем метод проверки валидности id_клиента
                }
                
            }
            catch
            {
                MessageBox.Show("Необходимо ввести номер комнаты", "Ошибка");
            }

        }

        private void comparison_id()        // Метод для проверки валидности id
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);

                SqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT id_клиента FROM Клиенты WHERE id_клиента = " + Convert.ToInt32(id_klients.Text);
                conn.Open();

                int a = Convert.ToInt32(command.ExecuteScalar());
                conn.Close();

                if (a == 0)
                    button1.Enabled = true;
                else
                    MessageBox.Show("Клиент с таким id уже существует, необходимо указать другой id", "Ошибка");
            }
            catch
            {
                MessageBox.Show("Необходимо заполнить поле id_клиента", "Ошибка");
            }

        }


        private void number_TextChanged(object sender, EventArgs e)     // метод для блокировки кнопки в случае смены значений комнаты и id_клиента
        {
            button1.Enabled = false;
        }


        private void method_clear()                                     // Метод очистки данных
        {
            family.Clear();
            name.Clear();
            surname.Clear();
            passport.Clear();
            city.Clear();
            date.Clear();
            number.Clear();
            id_klients.Clear();
            button1.Enabled = false;
        }

        
    }
}
