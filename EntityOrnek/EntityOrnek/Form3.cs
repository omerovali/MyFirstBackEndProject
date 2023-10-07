using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityOrnek
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        DbSinavOgrenciEntities db = new DbSinavOgrenciEntities();
        private void BtnLinqEntity_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                var degerler = db.TBLNOTLAR.Where(x => x.SINAV1 < 50);
                dataGridView1.DataSource = degerler.ToList();
            }

            if (radioButton2.Checked == true)
            {
                var ali = db.TBLOGRENCI.Where(x => x.AD == "ali");
                dataGridView1.DataSource = ali.ToList();
            }
            if (radioButton3.Checked == true)
            {
                var degerler = db.TBLOGRENCI.Where(x => x.AD == textBox1.Text || x.SOYAD == textBox1.Text);
                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton5.Checked == true)
            {
                var degerler = db.TBLOGRENCI.Select(x => new { soyadı = x.SOYAD });
                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton4.Checked == true)
            {
                var degerler = db.TBLOGRENCI.Select(x => new { ad = x.AD.ToUpper(), soyadı = x.SOYAD.ToLower() });
                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton6.Checked == true)
            {
                var degerler = db.TBLOGRENCI.Select(x => new { ad = x.AD.ToUpper(), soyadı = x.SOYAD.ToLower() }).Where(x => x.ad != "ali");
                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton7.Checked == true)
            {
                var degerler = db.TBLNOTLAR.Select(x =>
                new
                {
                    OgrenciAd = x.OGR,
                    Ortalaması = x.ORTALAMA,
                    Durum = x.DURUM == true ? "Geçti" : "Kaldı"


                });
                dataGridView1.DataSource = degerler.ToList();
            }

            if (radioButton8.Checked == true)
            {
                var degerler = db.TBLNOTLAR.SelectMany(x => db.TBLOGRENCI.Where(y => y.ID == x.OGR), (x, y) => new
                {
                    y.AD,
                    x.ORTALAMA,
                    Durum = x.DURUM == true ? "geçti" : "kaldı"



                });
                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton9.Checked)
            {
                var degerler = db.TBLOGRENCI.OrderBy(x => x.ID).Take(3);
                dataGridView1.DataSource=degerler.ToList();
            }

            if(radioButton10.Checked == true)
            {
                var degerler = db.TBLOGRENCI.OrderByDescending(x => x.ID).Take(3);
                dataGridView1.DataSource = degerler.ToList();
            }

            if(radioButton11.Checked == true)
            {
                var degerler = db.TBLOGRENCI.OrderBy(x => x.AD);
                dataGridView1.DataSource = degerler.ToList();
            }
            if(radioButton12.Checked == true)
            {
                var degerler = db.TBLOGRENCI.OrderBy(x =>x.ID).Skip(5);
                dataGridView1.DataSource = degerler.ToList();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
