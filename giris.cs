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
    public partial class giris : Form
    {
        public static string a,b;
        int capslock = 1,klavyedurum=0,imlecnerdexd=0;
        OleDbConnection con;
        DataTable dt_giris;
        OleDbDataAdapter a1;
        OleDbCommand komut = new OleDbCommand();
        string sorgu;
        public giris()
        {
            InitializeComponent();
        }

        void listele_giris()
        {
            dt_giris = new DataTable();
            a1 = new OleDbDataAdapter(sorgu, con);
            a1.Fill(dt_giris);
        }

        private void ekran_klavyesicapslockacik()
        {
            //ilk sıra
            button15.Text = "q"; button16.Text = "w"; button17.Text = "e"; button18.Text = "r"; button19.Text = "t"; button20.Text = "y";
            button21.Text = "u"; button22.Text = "ı"; button23.Text = "o"; button24.Text = "p"; button25.Text = "ğ"; button26.Text = "ü";
        //

            //ikinci sıra
            button27.Text = "a"; button28.Text = "s"; button29.Text = "d"; button30.Text = "f"; button31.Text = "g"; button32.Text = "h";
            button33.Text = "j"; button34.Text = "k"; button35.Text = "l"; button36.Text = "ş"; button37.Text = "i";
            //

            //üçüncü sıra
            button38.Text = "z"; button39.Text = "x"; button40.Text = "c"; button41.Text = "v"; button42.Text = "b"; button43.Text = "n";
            button44.Text = "m"; button45.Text = "ö"; button46.Text = "ç";
            //
        }

        private void ekran_klavyesicapslockkapali()
        {
            //ilk sıra
            button15.Text = "Q"; button16.Text = "W"; button17.Text = "E"; button18.Text = "R"; button19.Text = "T"; button20.Text = "Y";
            button21.Text = "U"; button22.Text = "I"; button23.Text = "O"; button24.Text = "P"; button25.Text = "Ğ"; button26.Text = "Ü";
            //

            //ikinci sıra
            button27.Text = "A"; button28.Text = "S"; button29.Text = "D"; button30.Text = "F"; button31.Text = "G"; button32.Text = "H";
            button33.Text = "J"; button34.Text = "K"; button35.Text = "L"; button36.Text = "Ş"; button37.Text = "İ";
            //

            //üçüncü sıra
            button38.Text = "Z"; button39.Text = "X"; button40.Text = "C"; button41.Text = "V"; button42.Text = "B"; button43.Text = "N";
            button44.Text = "M"; button45.Text = "Ö"; button46.Text = "Ç";
            //
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                b = textBox1.Text;
                sorgu = "select * from giris Where Kullaniciadi ='" + textBox1.Text + "' and Sifre = '" + textBox2.Text + "'";
                listele_giris();
                if (dt_giris.Rows.Count > 0)  // doğru girildi 
                {
                    sorgu = "select * from giris Where Kullaniciadi ='" + textBox1.Text + "'";
                    listele_giris();
                    if (dt_giris.Rows[0]["anaformgiris"].ToString() == "1")
                    {
                        a = "1";
                        Form1 form1 = new Form1();
                        form1.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Anaformu görücek yetkiye sahip değilsiniz lütfen market sahibi ile iletişime geçiniz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    // ana formu aktif edecek
                }
                else
                {
                    a = "0";
                    MessageBox.Show("Kullanıcı Adınızı veya Şifrenizi doğru giriniz!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox1.Focus();
                }
            }
            catch
            {
                MessageBox.Show("Beklenmedik bir hata meydana geldi lütfen programı yeniden başlatınız.","UYARI",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }

        }

        private void giris_Load(object sender, EventArgs e)
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

        private void giris_Shown(object sender, EventArgs e)
        {
            textBox1.Focus();
            this.Height = 225;
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            klavye.Enabled = true;
            //ekran klavyesi için
        }

        private void klavye_Tick(object sender, EventArgs e)
        {
            if (klavyedurum == 0)
            {
                if (this.Height >= 396)
                {
                    klavyedurum = 1;
                    klavye.Stop();
                }
                else
                {
                    this.Height += 1;
                }
            }
            else
            {
                if (this.Height <= 225)
                {
                    klavyedurum = 0;
                    klavye.Stop();
                }
                else
                {
                    this.Height -= 1;
                }
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (capslock == 1)
            {
                ekran_klavyesicapslockacik();
                capslock = 0;
            }
            else
            {
                ekran_klavyesicapslockkapali();
                capslock = 1;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (imlecnerdexd == 0)
            {
                textBox1.Text += "1";
            }
            else
            {
                textBox2.Text += "1";
            }
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            imlecnerdexd = 0;
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            imlecnerdexd = 1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (imlecnerdexd == 0)
            {
                textBox1.Text += "2";
            }
            else
            {
                textBox2.Text += "2";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (imlecnerdexd == 0)
            {
                textBox1.Text += "3";
            }
            else
            {
                textBox2.Text += "3";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (imlecnerdexd == 0)
            {
                textBox1.Text += "4";
            }
            else
            {
                textBox2.Text += "4";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (imlecnerdexd == 0)
            {
                textBox1.Text += "5";
            }
            else
            {
                textBox2.Text += "5";
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (imlecnerdexd == 0)
            {
                textBox1.Text += "6";
            }
            else
            {
                textBox2.Text += "6";
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (imlecnerdexd == 0)
            {
                textBox1.Text += "7";
            }
            else
            {
                textBox2.Text += "7";
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (imlecnerdexd == 0)
            {
                textBox1.Text += "8";
            }
            else
            {
                textBox2.Text += "8";
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (imlecnerdexd == 0)
            {
                textBox1.Text += "9";
            }
            else
            {
                textBox2.Text += "9";
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (imlecnerdexd == 0)
            {
                textBox1.Text += "0";
            }
            else
            {
                textBox2.Text += "0";
            }
        }

        private void button47_Click(object sender, EventArgs e)
        {
            if (imlecnerdexd == 0)
            {
                try
                {
                    string degisken;
                    degisken = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
                    textBox1.Text = degisken;
                }
                catch
                {
                    return;
                }
            }
            else
            {
                try
                {
                    string degisken;
                    degisken = textBox2.Text.Substring(0, textBox2.Text.Length - 1);
                    textBox2.Text = degisken;
                }
                catch
                {
                    return;
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                if(imlecnerdexd==0)
                {
                    textBox1.Text += button15.Text;
                }

                else
                {
                    textBox2.Text += button15.Text;
                }
            }
            catch
            {
                return;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                if (imlecnerdexd == 0)
                {
                    textBox1.Text += button16.Text;
                }

                else
                {
                    textBox2.Text += button16.Text;
                }
            }
            catch
            {
                return;
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            try
            {
                if (imlecnerdexd == 0)
                {
                    textBox1.Text += button17.Text;
                }

                else
                {
                    textBox2.Text += button17.Text;
                }
            }
            catch
            {
                return;
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            try
            {
                if (imlecnerdexd == 0)
                {
                    textBox1.Text += button18.Text;
                }

                else
                {
                    textBox2.Text += button18.Text;
                }
            }
            catch
            {
                return;
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            try
            {
                if (imlecnerdexd == 0)
                {
                    textBox1.Text += button19.Text;
                }

                else
                {
                    textBox2.Text += button19.Text;
                }
            }
            catch
            {
                return;
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            try
            {
                if (imlecnerdexd == 0)
                {
                    textBox1.Text += button20.Text;
                }

                else
                {
                    textBox2.Text += button20.Text;
                }
            }
            catch
            {
                return;
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            try
            {
                if (imlecnerdexd == 0)
                {
                    textBox1.Text += button21.Text;
                }

                else
                {
                    textBox2.Text += button21.Text;
                }
            }
            catch
            {
                return;
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            try
            {
                if (imlecnerdexd == 0)
                {
                    textBox1.Text += button22.Text;
                }

                else
                {
                    textBox2.Text += button22.Text;
                }
            }
            catch
            {
                return;
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            try
            {
                if (imlecnerdexd == 0)
                {
                    textBox1.Text += button23.Text;
                }

                else
                {
                    textBox2.Text += button23.Text;
                }
            }
            catch
            {
                return;
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            try
            {
                if (imlecnerdexd == 0)
                {
                    textBox1.Text += button24.Text;
                }

                else
                {
                    textBox2.Text += button24.Text;
                }
            }
            catch
            {
                return;
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            try
            {
                if (imlecnerdexd == 0)
                {
                    textBox1.Text += button25.Text;
                }

                else
                {
                    textBox2.Text += button25.Text;
                }
            }
            catch
            {
                return;
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            try
            {
                if (imlecnerdexd == 0)
                {
                    textBox1.Text += button26.Text;
                }

                else
                {
                    textBox2.Text += button26.Text;
                }
            }
            catch
            {
                return;
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            try
            {
                if (imlecnerdexd == 0)
                {
                    textBox1.Text += button27.Text;
                }

                else
                {
                    textBox2.Text += button27.Text;
                }
            }
            catch
            {
                return;
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            try
            {
                if (imlecnerdexd == 0)
                {
                    textBox1.Text += button28.Text;
                }

                else
                {
                    textBox2.Text += button28.Text;
                }
            }
            catch
            {
                return;
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            try
            {
                if (imlecnerdexd == 0)
                {
                    textBox1.Text += button29.Text;
                }

                else
                {
                    textBox2.Text += button29.Text;
                }
            }
            catch
            {
                return;
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            try
            {
                if (imlecnerdexd == 0)
                {
                    textBox1.Text += button30.Text;
                }

                else
                {
                    textBox2.Text += button30.Text;
                }
            }
            catch
            {
                return;
            }
        }

        private void button31_Click(object sender, EventArgs e)
        {
            try
            {
                if (imlecnerdexd == 0)
                {
                    textBox1.Text += button31.Text;
                }

                else
                {
                    textBox2.Text += button31.Text;
                }
            }
            catch
            {
                return;
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            try
            {
                if (imlecnerdexd == 0)
                {
                    textBox1.Text += button32.Text;
                }

                else
                {
                    textBox2.Text += button32.Text;
                }
            }
            catch
            {
                return;
            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            try
            {
                if (imlecnerdexd == 0)
                {
                    textBox1.Text += button33.Text;
                }

                else
                {
                    textBox2.Text += button33.Text;
                }
            }
            catch
            {
                return;
            }
        }

        private void button34_Click(object sender, EventArgs e)
        {
            try
            {
                if (imlecnerdexd == 0)
                {
                    textBox1.Text += button34.Text;
                }

                else
                {
                    textBox2.Text += button34.Text;
                }
            }
            catch
            {
                return;
            }
        }

        private void button35_Click(object sender, EventArgs e)
        {
            try
            {
                if (imlecnerdexd == 0)
                {
                    textBox1.Text += button35.Text;
                }

                else
                {
                    textBox2.Text += button35.Text;
                }
            }
            catch
            {
                return;
            }
        }

        private void button36_Click(object sender, EventArgs e)
        {
            try
            {
                if (imlecnerdexd == 0)
                {
                    textBox1.Text += button36.Text;
                }

                else
                {
                    textBox2.Text += button36.Text;
                }
            }
            catch
            {
                return;
            }
        }

        private void button37_Click(object sender, EventArgs e)
        {
            try
            {
                if (imlecnerdexd == 0)
                {
                    textBox1.Text += button37.Text;
                }

                else
                {
                    textBox2.Text += button37.Text;
                }
            }
            catch
            {
                return;
            }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            try
            {
                if (imlecnerdexd == 0)
                {
                    textBox1.Text += button38.Text;
                }

                else
                {
                    textBox2.Text += button38.Text;
                }
            }
            catch
            {
                return;
            }
        }

        private void button39_Click(object sender, EventArgs e)
        {
            try
            {
                if (imlecnerdexd == 0)
                {
                    textBox1.Text += button39.Text;
                }

                else
                {
                    textBox2.Text += button39.Text;
                }
            }
            catch
            {
                return;
            }
        }

        private void button40_Click(object sender, EventArgs e)
        {
            try
            {
                if (imlecnerdexd == 0)
                {
                    textBox1.Text += button40.Text;
                }

                else
                {
                    textBox2.Text += button40.Text;
                }
            }
            catch
            {
                return;
            }
        }

        private void button41_Click(object sender, EventArgs e)
        {
            try
            {
                if (imlecnerdexd == 0)
                {
                    textBox1.Text += button41.Text;
                }

                else
                {
                    textBox2.Text += button41.Text;
                }
            }
            catch
            {
                return;
            }
        }

        private void button42_Click(object sender, EventArgs e)
        {
            try
            {
                if (imlecnerdexd == 0)
                {
                    textBox1.Text += button42.Text;
                }

                else
                {
                    textBox2.Text += button42.Text;
                }
            }
            catch
            {
                return;
            }
        }

        private void button43_Click(object sender, EventArgs e)
        {
            try
            {
                if (imlecnerdexd == 0)
                {
                    textBox1.Text += button43.Text;
                }

                else
                {
                    textBox2.Text += button43.Text;
                }
            }
            catch
            {
                return;
            }
        }

        private void button44_Click(object sender, EventArgs e)
        {
            try
            {
                if (imlecnerdexd == 0)
                {
                    textBox1.Text += button44.Text;
                }

                else
                {
                    textBox2.Text += button44.Text;
                }
            }
            catch
            {
                return;
            }
        }

        private void button45_Click(object sender, EventArgs e)
        {
            try
            {
                if (imlecnerdexd == 0)
                {
                    textBox1.Text += button45.Text;
                }

                else
                {
                    textBox2.Text += button45.Text;
                }
            }
            catch
            {
                return;
            }
        }

        private void button46_Click(object sender, EventArgs e)
        {
            try
            {
                if (imlecnerdexd == 0)
                {
                    textBox1.Text += button46.Text;
                }

                else
                {
                    textBox2.Text += button46.Text;
                }
            }
            catch
            {
                return;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (textBox2.UseSystemPasswordChar == true)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sifremiunuttum unuttum = new sifremiunuttum();
            unuttum.Show();
        }

        private void giris_FormClosing(object sender, FormClosingEventArgs e)
        {

            Application.Exit();
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
                textBox2.Clear();
                textBox2.Focus();
                return;
            }
        }
    }
}
