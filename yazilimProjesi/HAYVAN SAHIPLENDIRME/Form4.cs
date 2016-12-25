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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            //turgetir();
            //durumgetir();
            goster3();
            goster4();
        }
        static string baglanti = "Server=localhost; Data Source=EMIR-PC; Initial Catalog=hayvansahiplendirme; User Id=; password=;";
        SqlConnection baglan = new SqlConnection(baglanti);
        void turgetir()//Veri tabanındaki Bütün Hayvan Türlerini Combobox'a getirir.
        {
            SqlDataAdapter verial = new SqlDataAdapter("Select distinct tur from hayvanlar", baglan);
            DataSet dset = new DataSet();
            verial.Fill(dset);
            foreach (DataRow dr in dset.Tables[0].Rows)
            {
                comboBox3.Items.Add(dr["tur"].ToString());
            }
        }
        void durumgetir()//Veri tabanındaki durumları Combobox'a getirir.
        {
            SqlDataAdapter verial = new SqlDataAdapter("Select distinct durum from hayvanlar", baglan);
            DataSet dset = new DataSet();
            verial.Fill(dset);
            foreach (DataRow dr in dset.Tables[0].Rows)
            {
                comboBox4.Items.Add(dr["durum"].ToString());
            }
        }
        SqlConnection baglan2 = new SqlConnection(baglanti);
        void goster3() //Veritabanıntaki kayıtları datagridview de göstermek için kullanılır.   (barinma)
        {
            string sorgu;
            sorgu = "SELECT * FROM barinma";
            SqlDataAdapter getir = new SqlDataAdapter(sorgu, baglan2);
            baglan2.Open();
            DataSet goster = new DataSet();
            getir.Fill(goster, "barinma");
            DataTable dt = new DataTable();
            getir.Fill(dt);
            dataGridView2.DataSource = dt;
            getir.Dispose();
            baglan2.Close();
        }
        SqlConnection baglan3 = new SqlConnection(baglanti);
        void goster4() //Veritabanıntaki kayıtları datagridview de göstermek için kullanılır.    (hayvanlar)
        {
            string sorgu;
            sorgu = "SELECT * FROM hayvanlar";
            SqlDataAdapter getir = new SqlDataAdapter(sorgu, baglan3);
            baglan3.Open();
            DataSet goster = new DataSet();
            getir.Fill(goster, "hayvanlar");
            DataTable dt = new DataTable();
            getir.Fill(dt);
            dataGridView6.DataSource = dt;
            getir.Dispose();
            baglan3.Close();
        }
        private void Form4_Load_1(object sender, EventArgs e)
        {
            goster3();
            goster4();
            turgetir();
            durumgetir();
        }
        static string baglanti2 = "Server=localhost; Data Source=EMIR-PC; Initial Catalog=hayvansahiplendirme; User Id=; password=;";
        SqlConnection baglan1 = new SqlConnection(baglanti2);
        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                string a;
                baglan1.Open();
                SqlCommand kaydet3 = new SqlCommand("Insert into barinma(tur,cins,isim,sahiplenmetarihi,barinmasuresi,ad,soyad,adres,telno,durum) values(@tur,@cins,@isim,@sahiplenmetarihi,@barinmasuresi,@ad,@soyad,@adres,@telno,@durum)", baglan1);
                kaydet3.Parameters.AddWithValue("@tur", comboBox3.Text);
                kaydet3.Parameters.AddWithValue("@cins", textBox5.Text);
                kaydet3.Parameters.AddWithValue("@isim", textBox1.Text);
                kaydet3.Parameters.AddWithValue("@sahiplenmetarihi", dateTimePicker10.Text);
                kaydet3.Parameters.AddWithValue("@barinmasuresi", textBox11.Text);
                kaydet3.Parameters.AddWithValue("@ad", textBox2.Text);
                kaydet3.Parameters.AddWithValue("@soyad", textBox3.Text);
                kaydet3.Parameters.AddWithValue("@adres", textBox4.Text);
                kaydet3.Parameters.AddWithValue("@telno", textBox10.Text);
                kaydet3.Parameters.AddWithValue("@durum", comboBox4.Text);
                kaydet3.ExecuteNonQuery();
                string guncelle = "update hayvanlar set durum@durum where tur=@tur";
                SqlCommand guncelle2 = new SqlCommand(guncelle, baglan1);
                guncelle2.Parameters.AddWithValue("@durum", comboBox4.Text);
                guncelle2.ExecuteNonQuery();
                MessageBox.Show("Hayvan Sahiplendirildi. Teşekkürler.");
                a = Convert.ToString(comboBox4.Text);
                baglan1.Close();
                goster3();
                goster4();
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu" + hata.Message);
            }
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            turgetir();
            durumgetir();
            gez();
            goster3();
            goster4();
            comboBox3.Text = "Tür Seçiniz";
            textBox1.Hide();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            // Barınma İptal Etmek İçin kullanılır.
            DialogResult durum;
            durum = MessageBox.Show("Barınmayı İptal Etmek İstediğinizden Emin misiniz?.");
            if (durum == DialogResult.Yes)
            {
                baglan.Open();
                SqlCommand sil = new SqlCommand("DELETE From barinma Where tur", baglan);
                sil.Parameters.AddWithValue("tur", comboBox3.Text);
                sil.ExecuteNonQuery();
                sil.Dispose();
                MessageBox.Show("Barınma İptal Edildi.");
                baglan.Close();
                goster3();
                goster4();
            }
        }
        SqlConnection kayıt2 = new SqlConnection(baglanti);
        private void button3_Click_1(object sender, EventArgs e)
        {   //Barınmaları Günceller.
            kayıt2.Open();
            string gunclle2 = "update barinma SET tur=@tur,cins=@cins,isim=@isim,sahiplenmetarihi=@sahiplenmetarihi,barinmasuresi=@barinmasuresi,ad=@ad,soyad=@soyad,adres=@adres,telno=@telno,durum=@durum Where tur=@tur";
            SqlCommand guncelle2 = new SqlCommand(gunclle2, kayıt2);
            guncelle2.Parameters.AddWithValue("@tur", comboBox3.Text);
            guncelle2.Parameters.AddWithValue("@cins", textBox5.Text);
            guncelle2.Parameters.AddWithValue("@isim", textBox1.Text);
            guncelle2.Parameters.AddWithValue("@sahiplenmetarihi", dateTimePicker10.Text);
            guncelle2.Parameters.AddWithValue("@barinmasuresi", textBox11.Text);
            guncelle2.Parameters.AddWithValue("@ad", textBox2.Text);
            guncelle2.Parameters.AddWithValue("@soyad", textBox3.Text);
            guncelle2.Parameters.AddWithValue("@adres", textBox4.Text);
            guncelle2.Parameters.AddWithValue("@telno", textBox10.Text);
            guncelle2.Parameters.AddWithValue("@durum", comboBox4.Text);
            guncelle2.ExecuteNonQuery();
            MessageBox.Show("Kayıt Güncellendi");
            kayıt2.Close();
            goster3();
            goster4();
        }
        SqlConnection kayıt = new SqlConnection("Data Source=EMIR-PC; Initial Catalog=hayvansahiplendirme; User Id=; password=;");
         void gez()
        {
            kayıt.Open();
            SqlCommand sorgu = new SqlCommand("SELECT * FROM hayvanlar order by isim", kayıt);
            SqlDataReader datare;
            datare = sorgu.ExecuteReader();
            while (datare.Read())
            {
                string a = Convert.ToString(comboBox3.Text);
                if (a == datare[0].ToString())
                {
                    textBox1.Text = datare[4].ToString();
                    }
            }
            datare.Close();
            kayıt.Close();
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int secili = dataGridView2.SelectedCells[0].RowIndex;
            comboBox3.Text = dataGridView2.Rows[secili].Cells[1].Value.ToString();          //tür
            textBox5.Text = dataGridView2.Rows[secili].Cells[2].Value.ToString();           //cins
            textBox1.Text = dataGridView2.Rows[secili].Cells[3].Value.ToString();           //isim
            dateTimePicker10.Text = dataGridView2.Rows[secili].Cells[4].Value.ToString();    //sahiplenmetarihi
            textBox11.Text = dataGridView2.Rows[secili].Cells[5].Value.ToString();          //barinma süresi
            textBox2.Text = dataGridView2.Rows[secili].Cells[6].Value.ToString();           //ad
            textBox3.Text = dataGridView2.Rows[secili].Cells[7].Value.ToString();           //soyad
            textBox4.Text = dataGridView2.Rows[secili].Cells[8].Value.ToString();           //adres
            textBox10.Text = dataGridView2.Rows[secili].Cells[9].Value.ToString();          //telno
            comboBox4.Text = dataGridView2.Rows[secili].Cells[10].Value.ToString();         //durum   
         }

        private void dataGridView6_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int secili = dataGridView6.SelectedCells[0].RowIndex;
            comboBox3.Text = dataGridView6.Rows[secili].Cells[1].Value.ToString();          //tür
            textBox5.Text = dataGridView6.Rows[secili].Cells[2].Value.ToString();           //cins
            textBox1.Text = dataGridView6.Rows[secili].Cells[5].Value.ToString();           //isim
            comboBox4.Text = dataGridView6.Rows[secili].Cells[10].Value.ToString();          //durum
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
 