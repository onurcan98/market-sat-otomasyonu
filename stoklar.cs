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
    public partial class stoklar : Form
    {
        OleDbConnection con;
        DataTable dt_stok;
        BindingSource bs_stok;
        OleDbDataAdapter a1;
        string sorgu;
        OleDbCommand komut = new OleDbCommand();
        public stoklar()
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
            dataGridView1.DataSource = bs_stok;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Barkod Numarası";
            dataGridView1.Columns[2].HeaderText = "Ürünün İsmi";
            dataGridView1.Columns[3].HeaderText = "Alış Fiyatı";
            dataGridView1.Columns[4].HeaderText = "Satış Fiyatı";
            dataGridView1.Columns[5].HeaderText = "Ürünün Çeşidi";
            dataGridView1.Columns[6].HeaderText = "Mevcut Miktar";
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.RowHeadersWidth = 15;
            nesneler();
        }

        void nesneler()
        {
            textBox1.DataBindings.Clear(); textBox1.DataBindings.Add("text", bs_stok, "barkod");
            textBox2.DataBindings.Clear(); textBox2.DataBindings.Add("text", bs_stok, "ismi");
            textBox3.DataBindings.Clear(); textBox3.DataBindings.Add("text", bs_stok, "alisfiyati");
            textBox4.DataBindings.Clear(); textBox4.DataBindings.Add("text", bs_stok, "satisfiyati");
            textBox5.DataBindings.Clear(); textBox5.DataBindings.Add("text", bs_stok, "miktar");
            comboBox1.DataBindings.Clear(); comboBox1.DataBindings.Add("text", bs_stok, "cesit");
            textBox7.DataBindings.Clear(); textBox7.DataBindings.Add("text", bs_stok, "id");
        }

        private void stoklar_Load(object sender, EventArgs e)
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
            sorgu = "Select * from stoklar Order By id Asc ";
            listele_giris();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && comboBox1.Text != "")
            {
                DialogResult sonuc = MessageBox.Show("Ürünün bilgilerini güncellemek istiyor musunuz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (sonuc == DialogResult.Yes)
                {
                    //try
                    //{
                        komut.Connection = con;
                        komut.CommandText = "Update stoklar Set barkod='" + textBox1.Text + "',ismi = '" + textBox2.Text + "',alisfiyati='" + textBox3.Text +"',satisfiyati='" + textBox4.Text + "',cesit='" + comboBox1.Text + "',miktar='" + textBox5.Text + "' Where id=" + textBox7.Text + "";
                        komut.ExecuteNonQuery();
                        komut.Dispose();
                    //}
                    //catch (Exception hata)
                    //{
                    //    MessageBox.Show(hata.Message);
                    //    return;
                    //}
                    sorgu = "Select * from stoklar order by id Asc";
                    listele_giris();
                }
            }
            else
            {
                MessageBox.Show("Lütfen boş alan bırakmayınız !","UYARI",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult sonuc = MessageBox.Show("Seçilen ürünü silmek ister misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (sonuc == DialogResult.Yes)
            {
                try
                {
                    komut.Connection = con;
                    komut.CommandText = "delete from stoklar Where id =" + textBox7.Text + "";
                    komut.ExecuteNonQuery();
                    komut.Dispose();
                }
                catch (Exception hata)
                {
                    MessageBox.Show(hata.Message);
                    return;
                }
                sorgu = "Select * from stoklar order by id Asc";
                listele_giris();
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            sorgu = "Select * from stoklar Where ismi like'" + textBox6.Text + "%'";
            listele_giris();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                MessageBox.Show("Lütfen boşluk tuşunu kullanmayınız !", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox1.Clear();
                textBox1.Focus();
                nesneler();
                return;
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                MessageBox.Show("Lütfen boşluk tuşunu kullanmayınız !", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox3.Clear();
                textBox3.Focus();
                nesneler();
                return;
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                MessageBox.Show("Lütfen boşluk tuşunu kullanmayınız !", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox4.Clear();
                textBox4.Focus();
                nesneler();
                return;
            }
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                MessageBox.Show("Lütfen boşluk tuşunu kullanmayınız !", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox5.Clear();
                textBox5.Focus();
                nesneler();
                return;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
             && !char.IsSeparator(e.KeyChar);
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
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.';
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
            && !char.IsSeparator(e.KeyChar);
        }

        private void stoklar_Shown(object sender, EventArgs e)
        {
            textBox6.Focus();
        }
    }
}
