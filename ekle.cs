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
    public partial class ekle : Form
    {
        OleDbConnection con;
        DataTable dt_stok;
        BindingSource bs_stok;
        OleDbDataAdapter a1;
        string sorgu;
        OleDbCommand komut = new OleDbCommand();
        public ekle()
        {
            InitializeComponent();
        }

        void listele_giris()
        {
            dt_stok = new DataTable();
            a1 = new OleDbDataAdapter(sorgu, con);
            a1.Fill(dt_stok);

            bs_stok = new BindingSource();
            bs_stok.DataSource = dt_stok;
        }

        void listele_1()
        {
            dt_stok = new DataTable();
            a1 = new OleDbDataAdapter(sorgu, con);
            a1.Fill(dt_stok);

            bs_stok = new BindingSource();
            bs_stok.DataSource = dt_stok;
            nesneler();
        }

        void nesneler()
        {
            textBox2.DataBindings.Clear(); textBox2.DataBindings.Add("text", bs_stok, "ismi");
            textBox3.DataBindings.Clear(); textBox3.DataBindings.Add("text", bs_stok, "alisfiyati");
            textBox4.DataBindings.Clear(); textBox4.DataBindings.Add("text", bs_stok, "satisfiyati");
            comboBox1.DataBindings.Clear(); comboBox1.DataBindings.Add("text", bs_stok, "cesit");
            textBox7.DataBindings.Clear(); textBox7.DataBindings.Add("text", bs_stok, "miktar");
        }

        private void ekle_Load(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text!="" && textBox2.Text !="" && textBox3.Text !="" && textBox4.Text !="" && textBox5.Text!="" && comboBox1.Text!="")
            {
            string alis = textBox3.Text.Replace(".", ",");
            string satis = textBox4.Text.Replace(".", ",");

            sorgu = "select * from stoklar Where barkod ='" + textBox1.Text + "' ";
            listele_1();
            if (dt_stok.Rows.Count > 0)  //aynı barkod da ürün varsa
            {
                double sayi1 = Convert.ToDouble(textBox5.Text);
                double sayi2 = Convert.ToDouble(textBox7.Text);
                double toplam = sayi1 + sayi2;
             
                try
                {
                    komut.Connection = con;
                    komut.CommandText = "Update stoklar Set miktar='" + toplam.ToString() + "'Where barkod='" + textBox1.Text + "'";
                    komut.ExecuteNonQuery();
                    komut.Dispose();
                }
                catch (Exception hata)
                {
                    MessageBox.Show(hata.Message); return;
                }
                try
                {
                    komut.Connection = con;
                    komut.CommandText = "INSERT INTO logstok(kullaniciadi,barkod,urunismi,eklenenmiktar,cesidi,tarih,saat) VALUES ('" + giris.b + "','" + textBox1.Text + "','" + textBox2.Text + "','" + textBox5.Text + "','" + comboBox1.Text + "','" + DateTime.Now.ToLongDateString() + "','" + DateTime.Now.ToLongTimeString() + "')";
                    komut.ExecuteNonQuery();
                    komut.Dispose();
                }
                catch (Exception hata)
                {
                    MessageBox.Show(hata.Message); return;
                }
                MessageBox.Show("Ürün ekleme işleminiz başarıyla sonuçlandı.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                comboBox1.Text = "";
                textBox1.Focus();
                return;
            }
            else //aynı barkod da ürün yoksa
            {
                try
                {
                    komut.Connection = con;
                    komut.CommandText = "INSERT INTO stoklar(barkod,ismi,alisfiyati,satisfiyati,cesit,miktar) VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + alis.Replace(".",",") + "','" + satis.Replace(".",",") + "','"+comboBox1.Text+"','" + textBox5.Text + "')";
                    komut.ExecuteNonQuery();
                    komut.Dispose();
                }
                catch (Exception hata)
                {
                    MessageBox.Show(hata.Message); return;
                }

                try
                {
                    komut.Connection = con;
                    komut.CommandText = "INSERT INTO logstok(kullaniciadi,barkod,urunismi,eklenenmiktar,cesidi,tarih,saat) VALUES ('" + giris.b + "','" + textBox1.Text + "','" + textBox2.Text + "','" + textBox5.Text + "','" + comboBox1.Text + "','" + DateTime.Now.ToLongDateString() + "','"+DateTime.Now.ToLongTimeString()+"')";
                    komut.ExecuteNonQuery();
                    komut.Dispose();
                }
                catch (Exception hata)
                {
                    MessageBox.Show(hata.Message); return;
                }

                MessageBox.Show("Ürün ekleme işleminiz başarıyla sonuçlandı.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                comboBox1.Text = "";
                textBox1.Focus();
            }

            }
            else
            {
                MessageBox.Show("Lütfen boş alan bırakmayınız!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox2.Clear(); textBox3.Clear(); textBox4.Clear(); comboBox1.Text = ""; textBox7.Clear();
            sorgu = "Select * from stoklar Where barkod='" + textBox1.Text + "'";
            listele_1();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            stoklar stok = new stoklar();
            stok.Show();
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
                textBox3.Focus();
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox4.Focus();
            }
            if (e.KeyCode == Keys.Space)
            {
                MessageBox.Show("Lütfen boşluk tuşunu kullanmayınız !", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox3.Clear();
                textBox3.Focus();
                return;
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox5.Focus();
            }
            if (e.KeyCode == Keys.Space)
            {
                MessageBox.Show("Lütfen boşluk tuşunu kullanmayınız !", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox4.Clear();
                textBox4.Focus();
                return;
            }
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboBox1.Focus();
            }
            if (e.KeyCode == Keys.Space)
            {
                MessageBox.Show("Lütfen boşluk tuşunu kullanmayınız !", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox5.Clear();
                textBox5.Focus();
                return;
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',';
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',';
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void ekle_Shown(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
            && !char.IsSeparator(e.KeyChar);
        }
    }
}
