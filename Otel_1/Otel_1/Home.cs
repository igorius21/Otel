using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otel_1
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Spisok_nomerov_1 table = new Spisok_nomerov_1();
            table.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Spisok_klientov klients = new Spisok_klientov();
            klients.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new_klient new_Klient = new new_klient();
            new_Klient.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            spisok_slugachih rabochie = new spisok_slugachih();
            rabochie.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            finance fin = new finance();
            fin.ShowDialog();
        }
    }
}
