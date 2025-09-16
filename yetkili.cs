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
    public partial class yetkili : Form
    {
        OleDbConnection con;
        DataTable dt_giris;
        BindingSource bs_giris;
        OleDbDataAdapter a1;
        string sorgu;
        string ch1, ch2, ch3, ch4, ch5, ch6;
        OleDbCommand komut = new OleDbCommand();
        public yetkili()
        {
            InitializeComponent();
        }

        void listele_giris()
        {
            dt_giris = new DataTable();
            a1 = new OleDbDataAdapter(sorgu, con);
            a1.Fill(dt_giris);
        }

        void listele_giris_1()
        {
            dt_giris = new DataTable();
            a1 = new OleDbDataAdapter(sorgu, con);
            a1.Fill(dt_giris);

            bs_giris = new BindingSource();
            bs_giris.DataSource = dt_giris;
            dataGridView1.DataSource = bs_giris;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[3].Width = 150;
            dataGridView1.Columns[1].HeaderText = "Kullanıcı Adı";
            dataGridView1.Columns[2].HeaderText = "Şifre";
            dataGridView1.Columns[3].HeaderText = "TC Numarası";

            textBox1.DataBindings.Clear(); textBox1.DataBindings.Add("text", bs_giris, "Kullaniciadi");
            yetkilii();
        }

        void yetkilii()
        {

                checkBox1.DataBindings.Clear();
                Binding bind = new Binding("Checked", bs_giris, "anaformgiris");
                bind.Format += (s, e) => { e.Value = (int)e.Value == 1; };
                checkBox1.DataBindings.Add(bind);

                checkBox2.DataBindings.Clear();
                Binding bind1 = new Binding("Checked", bs_giris, "urunekle");
                bind1.Format += (d, e) => { e.Value = (int)e.Value == 1; };
                checkBox2.DataBindings.Add(bind1);

                checkBox3.DataBindings.Clear();
                Binding bind2 = new Binding("Checked", bs_giris, "urunsat");
                bind2.Format += (s, e) => { e.Value = (int)e.Value == 1; };
                checkBox3.DataBindings.Add(bind2);

                checkBox4.DataBindings.Clear();
                Binding bind3 = new Binding("Checked", bs_giris, "sifredegis");
                bind3.Format += (s, e) => { e.Value = (int)e.Value == 1; };
                checkBox4.DataBindings.Add(bind3);

                checkBox5.DataBindings.Clear();
                Binding bind4 = new Binding("Checked", bs_giris, "yetkiver");
                bind4.Format += (s, e) => { e.Value = (int)e.Value == 1; };
                checkBox5.DataBindings.Add(bind4);

                checkBox6.DataBindings.Clear();
                Binding bind5 = new Binding("Checked", bs_giris, "kullaniciekle");
                bind5.Format += (s, e) => { e.Value = (int)e.Value == 1; };
                checkBox6.DataBindings.Add(bind5);
        }
        private void yetkili_Load(object sender, EventArgs e)
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
            sorgu = "Select * from giris ";
            listele_giris_1();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
               
                    sorgu = "select * from giris Where tcno ='" + textBox8.Text + "'";
                    listele_giris();
                    if (dt_giris.Rows.Count > 0) //tcno doğru ise
                    {
                        if (textBox9.Text == textBox10.Text)
                        {
                            try
                            {
                                komut.Connection = con;
                                komut.CommandText = "Update giris Set Sifre='" + textBox10.Text + "' Where tcno='" + textBox8.Text + "'";
                                komut.ExecuteNonQuery();
                                komut.Dispose();
                            }
                            catch (Exception hata)
                            {
                                MessageBox.Show(hata.Message);
                                return;
                            }
                            MessageBox.Show("Şifreniz başarıyla değiştirildi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            textBox8.Clear();
                            textBox9.Clear();
                            textBox10.Clear();
                            textBox8.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Şifreleriniz birbiriyle eşleşmiyor lütfen tekrar deneyiniz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Lütfen  TC No'yu doğru giriniz !", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        textBox8.Clear();
                        textBox8.Focus();
                        return;
                    }
               
            }
            catch
            {
                MessageBox.Show("Beklenmedik bir hata meydana geldi lütfen programı yeniden başlatınız.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
              sorgu = "select * from giris Where Kullaniciadi ='" + giris.b + "'";
                listele_giris();
                if (dt_giris.Rows[0]["sifredegis"].ToString() == "1")
                {
                sorgu = "select * from giris Where tcno ='" + textBox11.Text + "'";
                listele_giris();
                if (dt_giris.Rows.Count > 0) //tcno doğru ise
                {
                    if (textBox12.Text == textBox13.Text)
                    {
                         sorgu = "select * from giris Where Kullaniciadi ='" + textBox12.Text + "'";
                         listele_giris();
                         if (dt_giris.Rows.Count > 0) //aynı kullanıcı adı varsa
                         {
                             MessageBox.Show("Bu kullanıcı adı kullanılmaktadır lütfen başka bir isim deneyiniz.","UYARI",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                             textBox12.Clear();
                             textBox13.Clear();
                             textBox12.Focus();
                             return;
                         }
                         else
                         {
                             try
                             {
                                 komut.Connection = con;
                                 komut.CommandText = "Update giris Set Kullaniciadi='" + textBox12.Text + "' Where tcno='" + textBox11.Text + "'";
                                 komut.ExecuteNonQuery();
                                 komut.Dispose();
                             }
                             catch (Exception hata)
                             {
                                 MessageBox.Show(hata.Message);
                                 return;
                             }
                             MessageBox.Show("Kullanıcı adınız başarıyla değiştirildi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                             textBox11.Clear();
                             textBox12.Clear();
                             textBox13.Clear();
                             textBox11.Focus();
                         }
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı adlarınız birbiriyle eşleşmiyor lütfen tekrar deneyiniz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen  TC No'yu doğru giriniz !", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    textBox11.Clear();
                    textBox11.Focus();
                    return;
                }
            }  
                else
                {
                    MessageBox.Show("Kullanıcı adınızı değiştirebilecek yetkiye sahip değilsiniz lütfen market sahibi ile iletişime geçiniz.", "BİLGİLENDİRME", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
        }
            catch
            {
                MessageBox.Show("Beklenmedik bir hata meydana geldi lütfen programı yeniden başlatınız.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                sorgu = "select * from giris Where Kullaniciadi ='" + giris.b + "'";
                listele_giris();
                if (dt_giris.Rows[0]["kullaniciekle"].ToString() == "1")
                {
                    if (textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "")
                    {
                        if (textBox2.Text == textBox3.Text) //kullanıcı adı filtre
                        {
                            if (textBox4.Text == textBox5.Text) //şifre filtre
                            {
                                if (textBox6.Text == textBox7.Text) //tcno filtre
                                {
                                    sorgu = "select * from giris Where Kullaniciadi ='" + textBox2.Text + "'";
                                    listele_giris();
                                    if (dt_giris.Rows.Count > 0) //aynı kullanıcı adı varsa
                                    {
                                        MessageBox.Show("Bu kullanıcı adı kullanılmaktadır lütfen başka bir isim deneyiniz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        textBox2.Clear();
                                        textBox3.Clear();
                                        textBox2.Focus();
                                        return;
                                    }
                                    else
                                    {
                                        sorgu = "select * from giris Where tcno ='" + textBox6.Text + "'";
                                        listele_giris();
                                        if (dt_giris.Rows.Count > 0) //aynı kullanıcı adı varsa
                                        {
                                            MessageBox.Show("Bu TC No kullanılmaktadır lütfen başka bir TC No deneyiniz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                            textBox6.Clear();
                                            textBox7.Clear();
                                            textBox6.Focus();
                                            return;
                                        }
                                        else
                                        {
                                            if (textBox6.Text.Length == 11 || textBox7.Text.Length == 11)
                                            {
                                                try
                                                {
                                                    komut.Connection = con;
                                                    komut.CommandText = "INSERT INTO giris(Kullaniciadi,Sifre,tcno,anaformgiris,urunekle,urunsat,sifredegis,yetkiver,kullaniciekle) VALUES ('" + textBox2.Text + "','" + textBox4.Text + "','" + textBox6.Text + "'," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + ")";
                                                    komut.ExecuteNonQuery();
                                                    komut.Dispose();
                                                }
                                                catch (Exception hata)
                                                {
                                                    MessageBox.Show(hata.Message);
                                                    return;
                                                }
                                                MessageBox.Show("Kullanıcı başarıyla eklendi.", "BİLGİLENDİRME", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                textBox2.Clear();
                                                textBox3.Clear();
                                                textBox4.Clear();
                                                textBox5.Clear();
                                                textBox6.Clear();
                                                textBox7.Clear();
                                                textBox2.Focus();
                                            }
                                            else
                                            {
                                                MessageBox.Show("TC No uzunluğu minumum 11 rakamdan oluşmalıdır.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                textBox6.Clear();
                                                textBox7.Clear();
                                                textBox6.Focus();
                                                return;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("TC Numaraları eşleşmiyor lütfen tekrar deneyiniz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    textBox6.Clear();
                                    textBox7.Clear();
                                    textBox6.Focus();
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Şifreleriniz eşleşmiyor lütfen tekrar deneyiniz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                textBox4.Clear();
                                textBox5.Clear();
                                textBox4.Focus();
                                return;
                            }
                            }
                            else
                            {
                                MessageBox.Show("Kullanıcı adları eşleşmiyor lütfen tekrar deneyiniz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                textBox2.Clear();
                                textBox3.Clear();
                                textBox2.Focus();
                                return;
                            }
                        }
                    else
                    {
                        MessageBox.Show("Lütfen boş alan bırakmayınız.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                     }
                 else
                    {
                        MessageBox.Show("Kullanıcı ekleyebilecek yetkiye sahip değilsiniz lütfen market sahibi ile iletişime geçiniz.", "BİLGİLENDİRME", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
            }
            catch
            {
                MessageBox.Show("Beklenmedik bir hata meydana geldi lütfen programı yeniden başlatınız.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            sorgu = "select * from giris";
            listele_giris();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                sorgu = "select * from giris Where Kullaniciadi ='" + giris.b + "'";
                listele_giris();
                if (dt_giris.Rows[0]["yetkiver"].ToString() == "1")
                {
                    sorgu = "select * from giris Where Kullaniciadi ='" + textBox1.Text + "'";
                    listele_giris();
                    if (dt_giris.Rows.Count > 0) //böyle kullanıcı varsa
                    {
                        try
                        {
                            komut.Connection = con;
                            komut.CommandText = "Update giris Set anaformgiris='" + ch1 + "', urunekle='" + ch2 + "',urunsat='" + ch3 + "',sifredegis='" + ch4 + "',yetkiver='" + ch5 + "',kullaniciekle='" + ch6 + "' Where Kullaniciadi='" + textBox1.Text + "'";
                            komut.ExecuteNonQuery();
                            komut.Dispose();
                        }
                        catch (Exception hata)
                        {
                            MessageBox.Show(hata.Message);
                            return;
                        }
                        MessageBox.Show("Yetkilendirme başarıyla sonuçlanmıştır.", "BİLGİLENDİRME", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Böyle bir kullanıcı bulunmamaktadır lütfen kullanıcı adını doğru yazdığınızdan emin olunuz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        textBox1.Clear();
                        textBox1.Focus();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Yetki verebilecek yetkiye sahip değilsiniz lütfen market sahibi ile iletişime geçiniz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    textBox1.Clear();
                    textBox1.Focus();
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Beklenmedik bir hata meydana geldi lütfen programı yeniden başlatınız.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                ch1 = "1";
            }
            else
            {
                ch1 = "0";
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                ch2 = "1";
            }
            else
            {
                ch2 = "0";
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                ch3 = "1";
            }
            else
            {
                ch3 = "0";
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                ch4 = "1";
            }
            else
            {
                ch4 = "0";
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                ch5 = "1";
            }
            else
            {
                ch5 = "0";
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked == true)
            {
                ch6 = "1";
            }
            else
            {
                ch6 = "0";
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox3.Focus();
            }
            if (e.KeyCode == Keys.Space)
            {
                MessageBox.Show("Lütfen boşluk tuşunu kullanmayınız !", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox2.Clear();
                textBox2.Focus();
                return;
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
                textBox6.Focus();
            }
            if (e.KeyCode == Keys.Space)
            {
                MessageBox.Show("Lütfen boşluk tuşunu kullanmayınız !", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox5.Clear();
                textBox5.Focus();
                return;
            }
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox7.Focus();
            }
            if (e.KeyCode == Keys.Space)
            {
                MessageBox.Show("Lütfen boşluk tuşunu kullanmayınız !", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox6.Clear();
                textBox6.Focus();
                return;
            }
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2.PerformClick();
            }
            if (e.KeyCode == Keys.Space)
            {
                MessageBox.Show("Lütfen boşluk tuşunu kullanmayınız !", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox7.Clear();
                textBox7.Focus();
                return;
            }
        }

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox9.Focus();
            }
            if (e.KeyCode == Keys.Space)
            {
                MessageBox.Show("Lütfen boşluk tuşunu kullanmayınız !", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox8.Clear();
                textBox8.Focus();
                return;
            }
        }

        private void textBox9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox10.Focus();
            }
            if (e.KeyCode == Keys.Space)
            {
                MessageBox.Show("Lütfen boşluk tuşunu kullanmayınız !", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox9.Clear();
                textBox9.Focus();
                return;
            }
        }

        private void textBox10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button3.PerformClick();
            }
            if (e.KeyCode == Keys.Space)
            {
                MessageBox.Show("Lütfen boşluk tuşunu kullanmayınız !", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox10.Clear();
                textBox10.Focus();
                return;
            }
        }

        private void textBox11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox12.Focus();
            }
            if (e.KeyCode == Keys.Space)
            {
                MessageBox.Show("Lütfen boşluk tuşunu kullanmayınız !", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox11.Clear();
                textBox11.Focus();
                return;
            }
        }

        private void textBox12_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox13.Focus();
            }
            if (e.KeyCode == Keys.Space)
            {
                MessageBox.Show("Lütfen boşluk tuşunu kullanmayınız !", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox12.Clear();
                textBox12.Focus();
                return;
            }
        }

        private void textBox13_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button4.PerformClick();
            }
            if (e.KeyCode == Keys.Space)
            {
                MessageBox.Show("Lütfen boşluk tuşunu kullanmayınız !", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox13.Clear();
                textBox13.Focus();
                return;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
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

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                textBox8.Focus();
            }
            if (tabControl1.SelectedTab == tabPage2)
            {
                textBox1.Focus();
            }
            if (tabControl1.SelectedTab == tabPage3)
            {
                textBox2.Focus();
            }
        }

        private void yetkili_Shown(object sender, EventArgs e)
        {
            textBox8.Focus();
        }
    }
}
