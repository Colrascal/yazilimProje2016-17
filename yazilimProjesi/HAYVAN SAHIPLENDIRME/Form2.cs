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
        public partial class Form2 : Form
        {
            public Form2()
            {
                InitializeComponent();
                goster();
               // beslenmetipigetir();
                //renkgetir();
                //kilogetir();
               // durumgetir();
            }
        static string baglanti = "Server=localhost;Data Source=EMIR-PC; Initial Catalog=hayvansahiplendirme; User Id=; password=;";
        SqlConnection baglan = new SqlConnection(baglanti);
        void beslenmetipigetir()//Veri tabanındaki Bütün Hayvan Türlerini Combobox'a getirir.
        {
            SqlDataAdapter verial = new SqlDataAdapter("Select distinct beslenmetipi from hayvanlar", baglan);
            DataSet dset = new DataSet();
            verial.Fill(dset);
            foreach (DataRow dr in dset.Tables[0].Rows)
            {
                comboBox4.Items.Add(dr["beslenmetipi"].ToString());
            }
        }
        void renkgetir()//Veri tabanındaki Bütün hayvan renklerini Combobox'a getirir.
        {
            SqlDataAdapter verial = new SqlDataAdapter("Select distinct renk from hayvanlar", baglan);
            DataSet dset = new DataSet();
            verial.Fill(dset);
            foreach (DataRow dr in dset.Tables[0].Rows)
            {
                comboBox1.Items.Add(dr["renk"].ToString());
            }
        }
        void durumgetir()//Veri tabanındaki Bütün Hayvan durumlarını Combobox'a getirir.
        {
            SqlDataAdapter verial = new SqlDataAdapter("Select distinct durum from hayvanlar", baglan);
            DataSet dset = new DataSet();
            verial.Fill(dset);
            foreach (DataRow dr in dset.Tables[0].Rows)
            {
                comboBox3.Items.Add(dr["durum"].ToString());
            }
        }
        void kilogetir()//Veri tabanındaki Bütün kilolarını Combobox'a getirir.
        {
            SqlDataAdapter verial = new SqlDataAdapter("Select distinct kilo from hayvanlar", baglan);
            DataSet dset = new DataSet();
            verial.Fill(dset);
            foreach (DataRow dr in dset.Tables[0].Rows)
            {
                comboBox5.Items.Add(dr["kilo"].ToString());
            }
        }
        void goster()//Veritabanıntaki değişiklikleri program çalışırken göstermek için kullanılır.
            {
            string sorgu, baglanti;
            baglanti = "Server=localhost;Data Source=EMIR-PC; Initial Catalog=hayvansahiplendirme; User Id=; password=; ";   
            sorgu = "SELECT * FROM hayvanlar";
            SqlConnection baglan = new SqlConnection(baglanti);  
            SqlDataAdapter getir = new SqlDataAdapter(sorgu, baglan);
            baglan.Open();
            DataSet goster = new DataSet();
            getir.Fill(goster, "hayvanlar");
            DataTable dt = new DataTable();
            getir.Fill(dt);
            dataGridView1.DataSource = dt;
            getir.Dispose();
            beslenmetipigetir();
            renkgetir();
            kilogetir();
            durumgetir();
            baglan.Close();
             }
        // Veri Tabanına Yeni Hayvan Eklemek İçin Kullanılır
        static string kayit = "Server=localhost; Data Source=EMIR-PC; Initial Catalog=hayvansahiplendirme; User Id=; password=";
        SqlConnection kaydt = new SqlConnection(kayit);
        SqlConnection kaydet = new SqlConnection();
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                kaydt.Open();
                SqlCommand kaydet = new SqlCommand("Insert into hayvanlar(tur,cins,dogumyili,renk,isim,kilo,beslenmetipi,yas,barinmaucreti,durum) values (@tur,@cins,@dogumyili,@renk,@isim,@kilo,@beslenmetipi,@yas,@barinmaureti,@durum)",kaydt);
                kaydet.Parameters.AddWithValue("@tur", textBox1.Text);
                kaydet.Parameters.AddWithValue("@cins", textBox2.Text);
                kaydet.Parameters.AddWithValue("@dogumyili", textBox7.Text);
                kaydet.Parameters.AddWithValue("@renk", comboBox1.Text);
                kaydet.Parameters.AddWithValue("@isim", textBox5.Text);
                kaydet.Parameters.AddWithValue("@kilo", comboBox5.Text);
                kaydet.Parameters.AddWithValue("@beslenmetipi", comboBox4.Text);
                kaydet.Parameters.AddWithValue("@yas", textBox4.Text);
                kaydet.Parameters.AddWithValue("@barinmaucreti", textBox6.Text);
                kaydet.Parameters.AddWithValue("@durum", comboBox3.Text); 
                kaydet.ExecuteNonQuery();
                MessageBox.Show("Hayvan Eklendi");
                kaydt.Close();
                goster();
                textBox1.Clear();
                textBox2.Clear();
                textBox5.Clear();
                textBox4.Clear();
                textBox6.Clear();
                textBox7.Clear();
                comboBox1.Text = "";
                comboBox3.Text = "";
                comboBox4.Text = "";
                comboBox5.Text = "";
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu" + hata.Message);
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            goster();
            beslenmetipigetir();
            renkgetir();
            kilogetir();
            durumgetir();
        }
        static string kayitt = "Server=localhost; Data Source=EMIR-PC; Initial Catalog=hayvansahiplendirme; User Id=; password=";
        SqlConnection gkayit = new SqlConnection(kayitt);
        SqlConnection guncelle = new SqlConnection();

        private void button5_Click_1(object sender, EventArgs e)
        {   // Veritabanındaki kayıtları güncellemek için kullanılır.
            gkayit.Open();
            SqlCommand guncelle = new SqlCommand("update hayvanlar SET tur=@tur,cins=@cins,dogumyili=@dogumyili,renk=@renk,isim=@isim,kilo=@kilo,beslenmetipi=@beslenmetipi,yas=@yas,barinmaucreti=@barinmaucreti,durum=@durum Where hayvanlar_id=@id", gkayit);
            guncelle.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[0].Value);
            guncelle.Parameters.AddWithValue("@tur", textBox1.Text);
            guncelle.Parameters.AddWithValue("@cins", textBox2.Text);
            guncelle.Parameters.AddWithValue("@dogumyili", textBox7.Text);
            guncelle.Parameters.AddWithValue("@renk", comboBox1.Text);
            guncelle.Parameters.AddWithValue("@isim", textBox5.Text);
            guncelle.Parameters.AddWithValue("@kilo", comboBox5.Text);
            guncelle.Parameters.AddWithValue("@beslenmetipi", comboBox4.Text);
            guncelle.Parameters.AddWithValue("@yas", textBox4.Text);
            guncelle.Parameters.AddWithValue("@barinmaucreti", textBox6.Text);
            guncelle.Parameters.AddWithValue("@durum", comboBox3.Text);
            guncelle.ExecuteNonQuery(); 
            MessageBox.Show("Kayıt Güncellendi");
            gkayit.Close();
            goster();
        }
        private void button4_Click_1(object sender, EventArgs e)
        {
            // Veritabanındaki kayıtları silmek için kullanılır.
            SqlConnection kayıt = new SqlConnection("Server=localhost; Data Source=EMIR-PC; Initial Catalog=hayvansahiplendirme; User Id=; password=;");
            kayıt.Open();
            SqlCommand sil = new SqlCommand("DELETE From hayvanlar Where [isim]=@isim", kayıt);
            sil.Parameters.AddWithValue("@isim", textBox5.Text);
            sil.ExecuteNonQuery();
            sil.Dispose();
            MessageBox.Show("Kayıt Silindi");
            kayıt.Close();
            goster();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int secili = dataGridView1.SelectedCells[0].RowIndex;//DatagridView 'e tıklayınca textboxları doldurmak için kullanılır .
            textBox1.Text = dataGridView1.Rows[secili].Cells[1].Value.ToString();      //tür
            textBox2.Text = dataGridView1.Rows[secili].Cells[2].Value.ToString();      //cins
            textBox7.Text = dataGridView1.Rows[secili].Cells[3].Value.ToString();      //doğumyılı
            comboBox1.Text = dataGridView1.Rows[secili].Cells[4].Value.ToString();     //renk
            textBox5.Text = dataGridView1.Rows[secili].Cells[5].Value.ToString();      //isim
            comboBox5.Text = dataGridView1.Rows[secili].Cells[6].Value.ToString();     //kilo
            comboBox4.Text = dataGridView1.Rows[secili].Cells[7].Value.ToString();     //beslenme tipi
            textBox4.Text = dataGridView1.Rows[secili].Cells[8].Value.ToString();      //yaş
            textBox6.Text = dataGridView1.Rows[secili].Cells[9].Value.ToString();      //barınma ücreti
            comboBox3.Text = dataGridView1.Rows[secili].Cells[10].Value.ToString();     //durum
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

            private void label2_Click(object sender, EventArgs e)
            {
            
            }
    }
}
