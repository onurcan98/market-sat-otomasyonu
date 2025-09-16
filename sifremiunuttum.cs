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
    public partial class sifremiunuttum : Form
    {
        OleDbConnection con;
        DataTable dt_giris;
        OleDbDataAdapter a1;
        string sorgu;
        public string adana;
        OleDbCommand komut = new OleDbCommand();
        public sifremiunuttum()
        {
            InitializeComponent();
        }

        void listele_giris()
        {
            dt_giris = new DataTable();
            a1 = new OleDbDataAdapter(sorgu, con);
            a1.Fill(dt_giris);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sorgu = "select * from giris Where tcno = '" + textBox1.Text + "'";
            listele_giris();

            if (dt_giris.Rows.Count > 0)
            {
                try
                {
                    komut.Connection = con;
                    komut.CommandText = "INSERT INTO tcnolar(tcno) VALUES ('" + textBox1.Text + "')";
                    komut.ExecuteNonQuery();
                    komut.Dispose();
                }
                catch (Exception hata)
                {
                    MessageBox.Show(hata.Message); return;
                }

                degistirmeekrani ekran = new degistirmeekrani();
                ekran.Show();
                //MessageBox.Show("Şifreniz: " + dt_giris.Rows[0]["Şifre"], "BİLGİLENDİRME", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show("Lütfen TC numaranızı doğru giriniz! Eğer onu bilmiyorsanız üretici ile iletişime geçiniz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void sifremiunuttum_Load(object sender, EventArgs e)
        {
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

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }
    }
}
