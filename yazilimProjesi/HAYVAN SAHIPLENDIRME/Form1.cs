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

namespace HAYVAN_SAHIPLENDIRME
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();//Menustripte formlar arası geçiş için kullanılır.
            
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 frm = new Form3();//Menustripte formlar arası geçiş için kullanılır.
            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 frm = new Form4();//Menustripte formlar arası geçiş için kullanılır.
            frm.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
