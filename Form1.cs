using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Onurcan_CALISKAN_12_I__12749
{
    public partial class Form1 : Form
    {
        OleDbConnection con;
        OleDbCommand komut = new OleDbCommand();
        OleDbDataAdapter a1;
        DataTable dt_giris;
        string gtarih, gsaati,sorgu;
        public Form1()
        {
            InitializeComponent();
        }

        void listele_giris()
        {
            dt_giris = new DataTable();
            a1 = new OleDbDataAdapter(sorgu, con);
            a1.Fill(dt_giris);
        }

        private void Form1_Load(object sender, EventArgs e)
        {   
            this.Text=DateTime.Now.ToLongDateString();
            try
            {
                con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=database.mdb; Jet OLEDB:Database Password=");
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
            }
            catch
            {
                MessageBox.Show("Veritabanı ile bağlantı sağlanamadı!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void kontrol_Tick(object sender, EventArgs e)
        {
            giris giris = new giris();
            this.Text = "Market                                                                                                                "+"Kullanıcı : "+giris.b+"                                                                                               "+DateTime.Now.ToLongDateString();
            if (giris.a == "1")
            {
                this.Enabled = true;
            }
            if (giris.a == "0")
            {
                this.Enabled = false;
            }
        }

        private void saat_Tick(object sender, EventArgs e)
        {
            toolStripLabel1.Text = DateTime.Now.ToLongTimeString();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            giris giris = new giris();
            try
            {
                komut.Connection = con;
                komut.CommandText = "INSERT INTO giriscikis(giristarihi,girissaati,cikistarihi,cikissaati,kullaniciadi) VALUES ('" + gtarih + "','" + gsaati + "','" + DateTime.Now.ToLongDateString() + "','" + DateTime.Now.ToLongTimeString() + "','"+giris.b+"')";
                komut.ExecuteNonQuery();
                komut.Dispose();
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message); return;
            }

            
            giris.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                sorgu = "select * from giris Where Kullaniciadi ='" + giris.b + "'";
                listele_giris();
                if (dt_giris.Rows[0]["urunekle"].ToString() == "1")
                {
                    ekle ekle = new ekle();
                    ekle.Show();
                }
                else
                {
                    MessageBox.Show("Bu yetkiye sahip değilsiniz lütfen market sahibi ile iletişime geçiniz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Beklenmedik bir hata meydana geldi lütfen programı yeniden başlatınız.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
          
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            gtarih = DateTime.Now.ToLongDateString();
            gsaati = DateTime.Now.ToLongTimeString();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            stoklar stok = new stoklar();
            stok.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //try
            //{
                sorgu = "select * from giris Where Kullaniciadi ='" + giris.b + "'";
                listele_giris();
                if (dt_giris.Rows[0]["urunsat"].ToString() == "1")
                {
                    kasa kasa = new kasa();
                    kasa.Show();
                }
                else
                {
                    MessageBox.Show("Bu yetkiye sahip değilsiniz lütfen market sahibi ile iletişime geçiniz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            //}
            //catch
            //{
            //    MessageBox.Show("Beklenmedik bir hata meydana geldi lütfen programı yeniden başlatınız.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            yetkili yetkiliayarlari = new yetkili();
            yetkiliayarlari.Show();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            destek destek = new destek();
            destek.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            gelirgider gelirgider = new gelirgider();
            gelirgider.Show();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
