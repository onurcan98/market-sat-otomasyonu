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
    public partial class degistirmeekrani : Form
    {
        OleDbConnection con;
        DataTable dt_giris;
        OleDbDataAdapter a1;
        OleDbCommand komut = new OleDbCommand();
        string sorgu,tcnumarasi;
        public degistirmeekrani()
        {
            InitializeComponent();
        }

        void listele_giris()
        {
            dt_giris = new DataTable();
            a1 = new OleDbDataAdapter(sorgu, con);
            a1.Fill(dt_giris);
        }

        private void degistirmeekrani_Load(object sender, EventArgs e)
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (textBox1.UseSystemPasswordChar == true || textBox2.UseSystemPasswordChar == true)
            {
                textBox1.UseSystemPasswordChar = false;
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox1.UseSystemPasswordChar = true;
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sorgu = "select * from tcnolar ";
            listele_giris();
            
            tcnumarasi=dt_giris.Rows[0]["tcno"].ToString();

            if (textBox1.Text == textBox2.Text)
            {
                try
                {
                    
                    komut.Connection = con;
                    komut.CommandText = "Update giris Set Sifre='" + textBox2.Text + "' Where tcno='"+tcnumarasi+"'";
                    komut.ExecuteNonQuery();
                    komut.Dispose();
                }
                catch (Exception hata)
                {
                    MessageBox.Show(hata.Message);
                    return;
                }
                MessageBox.Show("Şifreniz başarıyla değiştirildi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Clear();
                textBox2.Clear();
                textBox1.Focus();
            }
            else
            {
                MessageBox.Show("Şifreler eşleşmiyor lütfen tekrar deneyiniz.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Clear();
                textBox2.Clear();
                textBox1.Focus();
            }
        }

        private void degistirmeekrani_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                komut.Connection = con;
                komut.CommandText = "delete from tcnolar";
                komut.ExecuteNonQuery();
                komut.Dispose();
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox2.Focus();
            }

            if (e.KeyCode == Keys.Space)
            {
                MessageBox.Show("Lütfen boşluk tuşunu kullanmayınız !", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox1.Clear();
                textBox1.Focus();
                return;
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }

            if (e.KeyCode == Keys.Space)
            {
                MessageBox.Show("Lütfen boşluk tuşunu kullanmayınız !", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox1.Clear();
                textBox1.Focus();
                return;
            }
        }
    }
}
