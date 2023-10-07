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

namespace EntityOrnek
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DbSinavOgrenciEntities db = new DbSinavOgrenciEntities();
        private void button1_Click(object sender, EventArgs e)
        {

            dataGridView1.DataSource = db.TBLOGRENCI.ToList();
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnderslistesi_Click(object sender, EventArgs e)
        {
            SqlConnection baglantı = new SqlConnection(@"Data Source=DESKTOP-D6TPT2C\SQLEXPRESS;Initial Catalog=DbSinavOgrenci;Integrated Security=True");
            SqlCommand komut = new SqlCommand("Select * from TBLDERSLER", baglantı);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void btnnotlistesi_Click(object sender, EventArgs e)
        {
            var querry = from item in db.TBLNOTLAR
                         select new
                         {
                             item.NOTID,
                             item.TBLOGRENCI.AD,
                             item.TBLOGRENCI.SOYAD,
                             item.TBLDERSLER.DERSAD,
                             item.SINAV1,
                             item.SINAV2,
                             item.SINAV3,
                             item.ORTALAMA,
                             item.DURUM
                         };
            dataGridView1.DataSource = querry.ToList();
            //dataGridView1.DataSource = db.TBLNOTLAR.ToList();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            TBLOGRENCI t = new TBLOGRENCI();
            t.AD = TxtOgrencıAd.Text;
            t.SOYAD = TxtOgrencıSoyad.Text;
            db.TBLOGRENCI.Add(t);
            db.SaveChanges();
            MessageBox.Show("ogrenci listeye eklenmıstır");

            TBLDERSLER D = new TBLDERSLER();

            D.DERSAD = TxtDersAd.Text;
            db.TBLDERSLER.Add(D);
            db.SaveChanges();
            MessageBox.Show("Ders eklenmıstır");

        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(TxtOgrencıID.Text);
            var x = db.TBLOGRENCI.Find(id);
            db.TBLOGRENCI.Remove(x);
            db.SaveChanges();
            MessageBox.Show("ogrencı lısteden sılındı");
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(TxtOgrencıID.Text);
            var x = db.TBLOGRENCI.Find(id);
            x.AD = TxtOgrencıAd.Text;
            x.SOYAD = TxtOgrencıSoyad.Text;
            x.Fotograf = TxtOgrencıFoto.Text;
            db.SaveChanges();
            MessageBox.Show("ogrenci bilgileri başarıyla guncellendi");
        }

        private void BtnProsedur_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.NOTLISTESI();
        }

        private void btnbul_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.TBLOGRENCI.Where(x => x.AD == TxtOgrencıAd.Text | x.SOYAD == TxtOgrencıSoyad.Text).ToList();
        }

        private void TxtOgrencıAd_TextChanged(object sender, EventArgs e)
        {
            string aranan = TxtOgrencıAd.Text;
            var degerler = from item in db.TBLOGRENCI
                           where item.AD.Contains(aranan)
                           select item;
            dataGridView1.DataSource = degerler.ToList();
        }

        private void BtnLinqEntity_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                // ASC BUYUKTEN KUCUGE ALFABDEN ADAN ZYE
                List<TBLOGRENCI> liste1 = db.TBLOGRENCI.OrderBy(p => p.AD).ToList();
                dataGridView1.DataSource = liste1;
            }
            if (radioButton2.Checked == true)
            {
                // DESC DESCENDING AZALAN 
                List<TBLOGRENCI> liste2 = db.TBLOGRENCI.OrderByDescending(p => p.AD).ToList();
                dataGridView1.DataSource = liste2;
            }
            if (radioButton3.Checked == true)
            {
                List<TBLOGRENCI> liste3 = db.TBLOGRENCI.OrderBy(p => p.AD).Take(3).ToList();
                dataGridView1.DataSource = liste3;
            }
            if (radioButton4.Checked == true)
            {
                List<TBLOGRENCI> liste4 = db.TBLOGRENCI.Where(p => p.ID == 5).ToList();
                dataGridView1.DataSource = liste4;
            }
            if (radioButton5.Checked == true)
            {
                List<TBLOGRENCI> liste5 = db.TBLOGRENCI.Where(p => p.AD.StartsWith("A")).ToList();
                dataGridView1.DataSource = liste5;

            }
            if (radioButton6.Checked == true)
            {
                List<TBLOGRENCI> liste6 = db.TBLOGRENCI.Where(p => p.AD.EndsWith("A")).ToList();
                dataGridView1.DataSource = liste6;
            }
            if (radioButton7.Checked == true)
            {
                bool deger = db.TBLKULUPLER.Any();
                MessageBox.Show(deger.ToString(), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (radioButton8.Checked == true)
            {
                int toplam = db.TBLOGRENCI.Count();
                MessageBox.Show(toplam.ToString(), "Toplam ogrenci sayisi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            if (radioButton9.Checked == true)
            {
                var toplam = db.TBLNOTLAR.Sum(p => p.SINAV1);
                MessageBox.Show("Toplam SINAV1 PUANI: " + toplam.ToString());
            }
            if (radioButton10.Checked == true)
            {
                var ortalama = db.TBLNOTLAR.Average(p => p.SINAV1);
                MessageBox.Show("SINAV1 ORTALAMASI: " + ortalama.ToString());
            }
            if (radioButton11.Checked == true)
            {
                var enyuksek = db.TBLNOTLAR.Max(p => p.SINAV1);
                MessageBox.Show("SINAV1 en yuksek: " + enyuksek.ToString());
            }
            if (radioButton12.Checked)
            {
                var endusuk = db.TBLNOTLAR.Min(p => p.SINAV1);
                MessageBox.Show("een dusuk not: " + endusuk.ToString());
            }

            if (radioButton13.Checked == true)
            {
                var enyuksek = db.TBLNOTLAR.Average(p => p.SINAV1);
                List<TBLNOTLAR> liste11 = db.TBLNOTLAR.Where(p => p.SINAV1 > enyuksek).ToList();
                dataGridView1.DataSource = liste11;
            }
            if (radioButton14.Checked == true)
            {
                var enyukseksinav = db.TBLNOTLAR.Max(p => p.SINAV1);
                dataGridView1.DataSource = db.NOTLISTESI().Where(p => p.SINAV1 == enyukseksinav).ToList();
            }

        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton13_CheckedChanged(object sender, EventArgs e)
        {

        }



        private void BtnJoin_Click(object sender, EventArgs e)
        {
            var sorgu = from d1 in db.TBLNOTLAR
                        join d2 in db.TBLOGRENCI
                        on d1.OGR equals d2.ID
                        join d3 in db.TBLDERSLER
                        on d1.DERS equals d3.DERSID

                        select new
                        {
                            OGRENCI = d2.AD + " " + d2.SOYAD,
                            DERS = d3.DERSAD,
                            //SOYAD = d2.SOYAD,
                            SINAV1 = d1.SINAV1,
                            SINAV2 = d1.SINAV2,
                            SINAV3 = d1.SINAV3,
                            ORTALAMA = d1.ORTALAMA
                        };
            dataGridView1.DataSource = sorgu.ToList();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form3 omer = new Form3();
            omer.ShowDialog(); // showdialogda arka sekmeyı kapatamıyoruz normal showda kapattıgımızda ıkısıde kapanıyor
        }
    }
}
