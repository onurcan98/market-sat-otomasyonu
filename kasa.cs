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
    public partial class kasa : Form
    {
        stoklar stok = new stoklar();
        DataTable dt_stok, dt_kasa;
        BindingSource bs_stok, bs_kasa;
        OleDbConnection con;
        OleDbDataAdapter a1, a2;
        OleDbCommand komut = new OleDbCommand();
        string sorgu, sorgu2;
        string yedek3, yedek4;
        double toplam1, odenenpara, paraustu, toplam2, kar, satisfiyati, toplamsatisfiyati, alisfiyati, toplamalisfiyati, yedek, yedek2;
        public static int a = 1;
        public kasa()
        {
            InitializeComponent();
        }

        public void listele()
        {
            dt_stok = new DataTable();
            a1 = new OleDbDataAdapter(sorgu2, con);
            a1.Fill(dt_stok);


            bs_stok = new BindingSource();
            bs_stok.DataSource = dt_stok;
            dataGridView1.DataSource = bs_stok;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Barkod Numarası";
            dataGridView1.Columns[2].HeaderText = "Ürünün İsmi";
            dataGridView1.Columns[3].HeaderText = "Alış Fiyatı";
            dataGridView1.Columns[4].HeaderText = "Satış Fiyatı";
            dataGridView1.Columns[5].HeaderText = "Ürünün Çeşidi";
            dataGridView1.Columns[6].HeaderText = "Mevcut Miktar";
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.RowHeadersWidth = 15;

            //textBox8.DataBindings.Clear(); textBox8.DataBindings.Add("text", bs_stok, "ismi");

        }

        public void listele_1()
        {
            dt_kasa = new DataTable();
            a2 = new OleDbDataAdapter(sorgu, con);
            a2.Fill(dt_kasa);


            bs_kasa = new BindingSource();
            bs_kasa.DataSource = dt_kasa;
            dataGridView2.DataSource = bs_kasa;
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[5].Visible = false;
            dataGridView2.Columns[1].Width = 120;
            dataGridView2.Columns[4].Width = 120;
            dataGridView2.Columns[1].HeaderText = "Barkod Numarası";
            dataGridView2.Columns[2].HeaderText = "Ürünün İsmi";
            dataGridView2.Columns[3].HeaderText = "Satış Fiyatı";
            dataGridView2.Columns[4].HeaderText = "Satılacak Miktar";
            dataGridView2.Columns[6].HeaderText = "Stoktaki Miktar";
            dataGridView2.Columns[7].HeaderText = "Ürünün Çeşidi";
            dataGridView2.RowHeadersWidth = 15;
        }

        public void satisonay()
        {
            if (dataGridView2.Rows.Count > 0) //SATILACAK ÜRÜN VARSA
            {
                for (int p = 0; p <= dt_kasa.Rows.Count - 1; p++)
                {

                    sorgu = "Select * from kasa";
                    listele_1();
                    double karhesap1 = Convert.ToDouble(dt_kasa.Rows[p]["satisfiyati"]);
                    double karhesap2 = Convert.ToDouble(dt_kasa.Rows[p]["alisfiyati"]);
                    double kartoplam = karhesap1 - karhesap2;

                    try
                    {
                        komut.Connection = con;
                        komut.CommandText = "INSERT INTO satilanlar(barkod,ismi,satisfiyati,miktar,urununcesidi,alisfiyati,satistarihi,kar,satissaati) VALUES ('" + dt_kasa.Rows[p]["barkod"] + "','" + dt_kasa.Rows[p]["ismi"] + "','" + dt_kasa.Rows[p]["satisfiyati"].ToString().Replace('.', ',') + "','" + dt_kasa.Rows[p]["miktar"].ToString().Replace(".",",") + "','"+dt_kasa.Rows[p]["cesit"]+"','" + dt_kasa.Rows[p]["alisfiyati"].ToString().Replace('.', ',') + "','" + DateTime.Now.ToShortDateString() + "','" + kartoplam.ToString().Replace('.', ',') + "','" + DateTime.Now.ToLongTimeString() + "')";
                        komut.ExecuteNonQuery();
                        komut.Dispose();
                    }
                    catch (Exception hata)
                    {
                        MessageBox.Show(hata.Message);
                        return;
                    }
                }
                for (int lale = 0; lale <= dt_kasa.Rows.Count-1; lale++)
                {
                    try
                    {
                        komut.Connection = con;
                        komut.CommandText = "Update stoklar Set miktar='" + dt_kasa.Rows[lale]["toplammiktar"].ToString() + "' Where ismi='" + dt_kasa.Rows[lale]["ismi"] + "'";
                        komut.ExecuteNonQuery();
                        komut.Dispose();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }

                try
                {
                    komut.Connection = con;
                    komut.CommandText = "delete from kasa";
                    komut.ExecuteNonQuery();
                    komut.Dispose();
                }
                catch (Exception hata)
                {
                    MessageBox.Show(hata.Message);
                    return;
                }
                sorgu = "Select * from kasa";
                listele_1();

                sorgu2 = "select * from stoklar";
                listele();
            }

            else // SATILACAK ÜRÜN YOKSA
            {
                MessageBox.Show("Lütfen kasada ürün yok iken satış yapmayınız.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void eklee()
        {
            try
            {
                sorgu2 = "Select * from stoklar Where barkod='" + textBox3.Text + "'";
                listele();
                //MessageBox.Show("Ürün başarıyla eklendi");

                yedek3 = dt_stok.Rows[0]["alisfiyati"].ToString();
                yedek4 = dt_stok.Rows[0]["satisfiyati"].ToString();
                yedek = Convert.ToDouble(yedek3);
                yedek2 = Convert.ToDouble(yedek4);
            }
            catch
            {
                ;
            }
                    sorgu = "Select * from kasa Where barkod='" + textBox3.Text + "'";
                    listele_1();
                    if (dt_kasa.Rows.Count > 0)  // aynı ürün kasada varsa
                    {
                        int sayi1 = Convert.ToInt32(textBox2.Text);
                        int sayi2 = Convert.ToInt32(dt_kasa.Rows[0]["miktar"]);
                        int toplam = sayi1 + sayi2;
                        satisfiyati = yedek2 * Convert.ToDouble(textBox2.Text);
                        alisfiyati = yedek * Convert.ToDouble(textBox2.Text);
                        toplamsatisfiyati = Convert.ToDouble(dt_kasa.Rows[0]["satisfiyati"]) + satisfiyati;
                        toplamalisfiyati = Convert.ToDouble(dt_kasa.Rows[0]["alisfiyati"]) + alisfiyati;
                        try
                        {
                            komut.Connection = con;
                            komut.CommandText = "Update kasa Set satisfiyati='" + toplamsatisfiyati.ToString().Replace(".", ",") + "',miktar='" + toplam.ToString() + "',alisfiyati='" + toplamalisfiyati.ToString().Replace(".", ",") + "',toplammiktar='" + (Convert.ToDouble(dt_kasa.Rows[0]["toplammiktar"]) - Convert.ToDouble(textBox2.Text)).ToString() + "' Where barkod='" + textBox3.Text + "'";
                            komut.ExecuteNonQuery();
                            komut.Dispose();
                        }
                        catch (Exception hata)
                        {
                            MessageBox.Show(hata.Message); return;
                        }
                        textBox2.Text = "1"; textBox3.Clear(); textBox5.Clear(); textBox3.Focus();
                    }
                    else // ürün kasada yoksa
                    {
                        try
                        {
                            sorgu2 = "Select * from stoklar Where barkod='" + textBox3.Text + "'";
                            listele();
                            //if (dt_stok.Rows.Count > 0)  // ürün stoklar da yoksavarsa
                            //{
                                string satisfiyati2 = dt_stok.Rows[0]["satisfiyati"].ToString();
                                string satisfiyati3 = dt_stok.Rows[0]["alisfiyati"].ToString();
                                double satisfiyati1 = Convert.ToDouble(satisfiyati2) * Convert.ToDouble(textBox2.Text);
                                double alisfiyati1 = Convert.ToDouble(satisfiyati3) * Convert.ToDouble(textBox2.Text);
                                try
                                {
                                    komut.Connection = con;
                                    komut.CommandText = "INSERT INTO kasa(barkod,ismi,satisfiyati,miktar,alisfiyati,toplammiktar,cesit) VALUES ('" + textBox3.Text + "','" + dt_stok.Rows[0]["ismi"] + "','" + satisfiyati1.ToString().Replace(".", ",") + "','" + textBox2.Text + "','" + alisfiyati1.ToString().Replace(".", ",") + "','" + (Convert.ToDouble(dt_stok.Rows[0]["miktar"]) - Convert.ToDouble(textBox2.Text)).ToString() + "','" + dt_stok.Rows[0]["cesit"] + "')";
                                    komut.ExecuteNonQuery();
                                    komut.Dispose();
                                }
                                catch (Exception hata)
                                {
                                    MessageBox.Show(hata.Message); return;
                                }
                                textBox2.Text = "1"; textBox3.Clear(); textBox5.Clear(); textBox3.Focus();
                            //}
                            //else
                            //{
                            //    MessageBox.Show("Böyle bir ürün stokta bulunmamaktadır lütfen barkodu doğru girdiğinizden emin olunuz!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Error); textBox2.Text = "1"; textBox3.Clear(); textBox3.Focus();
                            //}
                        }
                        catch(Exception hata)
                        {
                            MessageBox.Show(hata.Message);
                        }
                    }

                    sorgu = "select * from kasa";
                    listele_1();

                    toplam1 = 0;
                    toplam2 = 0;
                    for (int k = 0; k <= dataGridView2.Rows.Count - 1; k++)
                    {
                        try
                        {
                            toplam1 += Convert.ToDouble(dataGridView2.Rows[k].Cells["satisfiyati"].Value.ToString());
                        }
                        catch { ;}
                    }
                    textBox4.Text = toplam1.ToString();
                    for (int i = 0; i <= dataGridView2.Rows.Count - 1; i++)
                    {
                        try
                        {
                            toplam2 += Convert.ToDouble(dataGridView2.Rows[i].Cells["alisfiyati"].Value.ToString());
                        }
                        catch { ;}
                    }
                    kar = toplam1 - toplam2;
                    textBox7.Text = kar.ToString() + " TL";
                    satisfiyati = 0;
                    alisfiyati = 0;
            }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void kasa_Load(object sender, EventArgs e)
        {
            a = Convert.ToInt32(textBox2.Text);
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
            sorgu2 = "select * from stoklar ";
            listele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult sonuc = MessageBox.Show("Satışı iptal etmek istiyor musunuz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (sonuc == DialogResult.Yes)
            {
                try
                {
                    komut.Connection = con;
                    komut.CommandText = "delete from kasa";
                    komut.ExecuteNonQuery();
                    komut.Dispose();
                }
                catch (Exception hata)
                {
                    MessageBox.Show(hata.Message);
                    return;
                }
                sorgu = "Select * from kasa order by id Asc";
                listele_1();
                toplam1 = 0;
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
                textBox7.Clear();
                textBox3.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ara ara = new ara();
            ara.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            kasa kasa = new kasa();
            kasa.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                komut.Connection = con;
                komut.CommandText = "delete from kasa";
                komut.ExecuteNonQuery();
                komut.Dispose();
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
                return;
            }
            this.Close();
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                eklee();
            }
            if (e.KeyCode == Keys.Space)
            {
                MessageBox.Show("Lütfen boşluk tuşunu kullanmayınız !", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox3.Clear();
                textBox3.Focus();
                return;
            }
        }

        private void kasa_Shown(object sender, EventArgs e)
        {
          
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //miktarı arttır
            a++;
            textBox2.Text = a.ToString();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //miktarı azalt
            a--;
            textBox2.Text = a.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            sorgu2 = "Select * from stoklar Where ismi like'" + textBox1.Text + "%'";
            listele();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                textBox6.Clear();
                odenenpara = Convert.ToDouble(textBox5.Text);
                paraustu = odenenpara - toplam1;
                paraustu = Convert.ToDouble(Math.Round(Convert.ToDecimal(paraustu), 2));
                textBox6.Text = paraustu.ToString() + " TL";
            }
            catch { ;}
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                textBox7.Visible = true;
            }
            else
            {
                textBox7.Visible = false;
            }
        }

        private void seçilenÜrünüKaldırToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0) //ürün varsa
            {
                if (dataGridView2.SelectedRows.Count > 0) //ürün seçtiyse
                {
                    DialogResult sonuc = MessageBox.Show("Seçilen ürünü silmek ister misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (sonuc == DialogResult.Yes)
                    {
                        for (int k = 0; k <= dataGridView2.Rows.Count - 1; k++)
                        {
                            try
                            {
                                komut.Connection = con;
                                komut.CommandText = "delete from kasa Where barkod ='" + dataGridView2.SelectedRows[k].Cells["barkod"].Value.ToString() + "'";
                                komut.ExecuteNonQuery();
                                komut.Dispose();
                            }
                            catch
                            {
                                ;
                            }
                        }
                        sorgu = "Select * from kasa order by id Asc";
                        listele_1();
                        toplam1 = 0;
                        toplam2 = 0;
                        for (int k = 0; k <= dataGridView2.Rows.Count - 1; k++)
                        {
                            try
                            {
                                toplam1 += Convert.ToDouble(dataGridView2.Rows[k].Cells["satisfiyati"].Value.ToString());
                            }
                            catch { ;}
                        }
                        textBox4.Text = toplam1.ToString();
                        for (int i = 0; i <= dataGridView2.Rows.Count - 1; i++)
                        {
                            try
                            {
                                toplam2 += Convert.ToDouble(dataGridView2.Rows[i].Cells["alisfiyati"].Value.ToString());
                            }
                            catch { ;}
                        }
                        kar = toplam1 - toplam2;
                        textBox7.Text = kar.ToString() + " TL";
                        satisfiyati = 0;
                        alisfiyati = 0;
                    }
                }
                else //ürün seçmediyse
                {
                    MessageBox.Show("Lütfen ürün seçmeden ürün silme işlemi yapmayınız!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            else //yoksa
            {
                MessageBox.Show("Lütfen ürün eklemeden ürün silme işlemi yapmayınız!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            satisonay();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                sorgu = "select * from kasa where ismi='"+textBox8.Text+"'";
                listele_1();
                try
                {

                    sorgu2 = "Select * from stoklar Where ismi='" + textBox8.Text + "'";
                    listele();
                    //MessageBox.Show("Ürün başarıyla eklendi");
                    yedek3 = dt_stok.Rows[0]["alisfiyati"].ToString();
                    yedek4 = dt_stok.Rows[0]["satisfiyati"].ToString();
                    yedek = Convert.ToDouble(yedek3);
                    yedek2 = Convert.ToDouble(yedek4);
                }
                catch (Exception hata)
                {
                    MessageBox.Show(hata.Message);
                }
                sorgu = "Select * from kasa Where ismi='" + textBox8.Text + "'";
                listele_1();
                if (dt_kasa.Rows.Count > 0)  // aynı ürün kasada varsa
                {
                    int sayi1 = Convert.ToInt32(textBox2.Text);
                    int sayi2 = Convert.ToInt32(dt_kasa.Rows[0]["miktar"]);
                    int toplam = sayi1 + sayi2;
                    satisfiyati = yedek2 * Convert.ToDouble(textBox2.Text);
                    alisfiyati = yedek * Convert.ToDouble(textBox2.Text);
                    toplamsatisfiyati = Convert.ToDouble(dt_kasa.Rows[0]["satisfiyati"]) + satisfiyati;
                    toplamalisfiyati = Convert.ToDouble(dt_kasa.Rows[0]["alisfiyati"]) + alisfiyati;
                    try
                    {
                        komut.Connection = con;
                        komut.CommandText = "Update kasa Set satisfiyati='" + toplamsatisfiyati.ToString().Replace(".", ",") + "',miktar='" + toplam.ToString().Replace(".",",") + "',alisfiyati='" + toplamalisfiyati.ToString().Replace(".", ",") + "',toplammiktar='" + (Convert.ToDouble(dt_kasa.Rows[0]["toplammiktar"]) - Convert.ToDouble(textBox2.Text)).ToString().Replace(".",",") + "' Where ismi='" + textBox8.Text + "'";
                        komut.ExecuteNonQuery();
                        komut.Dispose();
                    }
                    catch (Exception hata)
                    {
                        MessageBox.Show(hata.Message); return;
                    }
                    textBox2.Text = "1"; textBox3.Clear(); textBox5.Clear(); textBox3.Focus();
                }
                else // ürün kasada yoksa
                {
                    try
                    {
                        sorgu2 = "select * from stoklar where ismi='" + textBox8.Text + "'";
                        //listele();
                        if (dt_stok.Rows.Count > 0)  // ürün stoklar da yoksavarsa
                        {
                            string satisfiyati2 = dt_stok.Rows[0]["satisfiyati"].ToString();
                            string satisfiyati3 = dt_stok.Rows[0]["alisfiyati"].ToString();
                            double satisfiyati1 = Convert.ToDouble(satisfiyati2) * Convert.ToDouble(textBox2.Text);
                            double alisfiyati1 = Convert.ToDouble(satisfiyati3) * Convert.ToDouble(textBox2.Text);
                            try
                            {
                                komut.Connection = con;
                                komut.CommandText = "INSERT INTO kasa(barkod,ismi,satisfiyati,miktar,alisfiyati,toplammiktar,cesit) VALUES ('" + dt_stok.Rows[0]["barkod"] + "','" + textBox8.Text + "','" + satisfiyati1.ToString().Replace(".", ",") + "','" + textBox2.Text.Replace(".",",") + "','" + alisfiyati1.ToString().Replace(".", ",") + "','" + (Convert.ToDouble(dt_stok.Rows[0]["miktar"]) - Convert.ToDouble(textBox2.Text)).ToString().Replace(".",",") + "','"+dt_stok.Rows[0]["cesit"]+"')";
                                komut.ExecuteNonQuery();
                                komut.Dispose();
                            }
                            catch (Exception hata)
                            {
                                MessageBox.Show(hata.Message); return;
                            }
                            textBox2.Text = "1"; textBox3.Clear(); textBox5.Clear(); textBox3.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Böyle bir ürün stokta bulunmamaktadır lütfen barkodu doğru girdiğinizden emin olunuz!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Error); textBox2.Text = "1"; textBox3.Clear(); textBox3.Focus();
                        }
                    }
                    catch (Exception hata)
                    {
                        MessageBox.Show(hata.Message);
                    }
                }

                sorgu = "select * from kasa";
                listele_1();

                toplam1 = 0;
                toplam2 = 0;
                for (int k = 0; k <= dataGridView2.Rows.Count - 1; k++)
                {
                    try
                    {
                        toplam1 += Convert.ToDouble(dataGridView2.Rows[k].Cells["satisfiyati"].Value.ToString());
                    }
                    catch { ;}
                }
                textBox4.Text = toplam1.ToString();
                for (int i = 0; i <= dataGridView2.Rows.Count - 1; i++)
                {
                    try
                    {
                        toplam2 += Convert.ToDouble(dataGridView2.Rows[i].Cells["alisfiyati"].Value.ToString());
                    }
                    catch { ;}
                }
                kar = toplam1 - toplam2;
                textBox7.Text = kar.ToString() + " TL";
                satisfiyati = 0;
                alisfiyati = 0;


                tabControl1.SelectedTab = tabPage1;
                textBox3.Focus();
                textBox1.Clear();
                sorgu2 = "select * from stoklar";
                listele();
            }
            else
            {
                MessageBox.Show("Lütfen boş alanlara tıklamayınız!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void kasa_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                komut.Connection = con;
                komut.CommandText = "delete from kasa";
                komut.ExecuteNonQuery();
                komut.Dispose();
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
                return;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
                SendKeys.Send("{ENTER}");
        }
    }
}
