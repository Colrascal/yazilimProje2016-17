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
    public partial class Form3 : Form
    {
        public Form3()
        {
           InitializeComponent();
            goster();
            goster2();
            //turgetir();
            //durumgetir();
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
        void durumgetir()//Veri tabanındaki Bütün durumları Combobox'a getirir.
        {
            SqlDataAdapter verial = new SqlDataAdapter("Select distinct durum from hayvanlar", baglan);
            DataSet dset = new DataSet();
            verial.Fill(dset);
            foreach (DataRow dr in dset.Tables[0].Rows)
            {
                comboBox4.Items.Add(dr["durum"].ToString());
            }
        }
       SqlConnection baglaan = new SqlConnection(baglanti);
        void goster()//Yapılan İşlemleri Program Çalışırken Gösterir. (sahiplenilenler)
        {
            string sorgu;
            sorgu = "SELECT * FROM sahiplenilenler";
            SqlDataAdapter getir = new SqlDataAdapter(sorgu, baglaan);
            baglaan.Open();
            DataSet goster = new DataSet();
            getir.Fill(goster,"sahiplenilenler");
            DataTable dt = new DataTable();
            getir.Fill(dt);
            dataGridView4.DataSource = dt;
            getir.Dispose();
            baglaan.Close();
        }
        void goster2()//Yapılan İşlemleri Program Çalışırken Gösterir.  (hayvanlar)
        {
            string sorgu;
            sorgu = "SELECT * FROM hayvanlar";
            SqlDataAdapter getir = new SqlDataAdapter(sorgu, baglaan);
            baglaan.Open();
            DataSet goster2 = new DataSet();
            getir.Fill(goster2, "hayvanlar");
            DataTable dt2 = new DataTable();
            getir.Fill(dt2);
            dataGridView1.DataSource = dt2;
            getir.Dispose();
            baglaan.Close();
        }

        private void Form3_Load_1(object sender, EventArgs e)
        {
            goster();
            goster2();
            turgetir();
            durumgetir();
        }
        static string kayit2 = "Server=localhost; Data Source=EMIR-PC; Initial Catalog=hayvansahiplendirme; User Id=; password=";
        SqlConnection baglann = new SqlConnection(kayit2);
        SqlConnection kaydett = new SqlConnection();
        SqlConnection guncelle = new SqlConnection();
        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                baglann.Open();//hayvan sahiplendirir.
                SqlCommand kaydett = new SqlCommand("insert into sahiplenilenler(hayvan,Sahiplenenad,Sahiplenensoyad,barinmasuresi,tutar,telno,durum,barinmatarihi) values (@hayvan,@Sahiplenenad,@Sahiplenensoyad,@barinmasuresi,@tutar,@telno,@durum,@barinmatarihi)", baglann);
                kaydett.Parameters.AddWithValue("@hayvan", comboBox3.Text);
                kaydett.Parameters.AddWithValue("@Sahiplenenad", textBox17.Text);
                kaydett.Parameters.AddWithValue("@Sahiplenensoyad", textBox18.Text);
                kaydett.Parameters.AddWithValue("@barinmasuresi", textBox7.Text);
                kaydett.Parameters.AddWithValue("@tutar", textBox8.Text);
                kaydett.Parameters.AddWithValue("@telno", textBox9.Text);
                kaydett.Parameters.AddWithValue("@durum", comboBox4.Text);
                kaydett.Parameters.AddWithValue("@barinmatarihi", dateTimePicker1.Text);
                kaydett.ExecuteNonQuery();
                goster();
                string gunclle = "update hayvanlar SET tur=@tur,cins=@cins,renk=@renk,isim=@isim,kilo=@kilo,beslenmetipi=@beslenmetipi,yas=@yas,barinmaucreti=@barinmaucreti,durum=@durum Where tur=@tur";
                SqlCommand guncelle = new SqlCommand(gunclle, baglann);
                guncelle.Parameters.AddWithValue("@tur", comboBox3.Text);               //tur
                guncelle.Parameters.AddWithValue("@cins", textBox12.Text);              //cins
                guncelle.Parameters.AddWithValue("@dogumyili", textBox16.Text);         //dogumyili
                guncelle.Parameters.AddWithValue("@renk", textBox5.Text);                //renk
                guncelle.Parameters.AddWithValue("@isim", textBox13.Text);              //isim
                guncelle.Parameters.AddWithValue("@kilo", textBox16.Text);           //kilo
                guncelle.Parameters.AddWithValue("@beslenmetipi", textBox3.Text);           //beslenmetipi
                guncelle.Parameters.AddWithValue("@yas", textBox15.Text);                 //yas
                guncelle.Parameters.AddWithValue("@barinmaucreti", textBox10.Text);         //barinmaucret
                guncelle.Parameters.AddWithValue("@durum", comboBox4.Text);              //durum
                
                guncelle.ExecuteNonQuery();
                MessageBox.Show("Hayvan Sahiplendirildi");
                comboBox4.Text = "Sahiplendirildi";
                baglann.Close();
                goster();
                goster2();
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu" + hata.Message);
            }
        }       
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            goster();
            goster2();
            turgetir();
            durumgetir();
            }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)    //HAYVANLAR TABLOSU
        {            //DatagridView 'e tıklayınca textboxları doldurmak için .
            int secili = dataGridView1.SelectedCells[0].RowIndex;
            comboBox3.Text = dataGridView1.Rows[secili].Cells[1].Value.ToString();
            textBox12.Text = dataGridView1.Rows[secili].Cells[2].Value.ToString();      //tür
            textBox19.Text = dataGridView1.Rows[secili].Cells[3].Value.ToString();      //isim
            textBox5.Text = dataGridView1.Rows[secili].Cells[4].Value.ToString();       //yaş
            textBox13.Text = dataGridView1.Rows[secili].Cells[5].Value.ToString();      //beslenmetipi
            textBox16.Text = dataGridView1.Rows[secili].Cells[6].Value.ToString();      //doğum yılı
            textBox3.Text = dataGridView1.Rows[secili].Cells[7].Value.ToString();       //renk
            textBox15.Text = dataGridView1.Rows[secili].Cells[8].Value.ToString();      //beslenme tipi
            textBox10.Text = dataGridView1.Rows[secili].Cells[9].Value.ToString();      // barınma ücreti
            comboBox4.Text = dataGridView1.Rows[secili].Cells[10].Value.ToString();     // durum
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult durum;
            durum = MessageBox.Show("Sahiplenmeyi İptal Etmek İstediğinizden Emin misiniz?.", "HAYVAN SAHIPLENDIRME", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
            if (durum == DialogResult.Yes)
            {
                baglan.Open();
                SqlCommand sil = new SqlCommand("DELETE From sahiplenilenler Where hayvan=@hayvan", baglan);
                sil.Parameters.AddWithValue("@hayvan", comboBox3.Text);
                sil.ExecuteNonQuery();
                sil.Dispose();
                MessageBox.Show("İptal Edildi.");
                baglan.Close();
                goster();
                goster2();
            }
        }

        private void comboBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
