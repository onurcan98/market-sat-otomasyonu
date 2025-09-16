using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using CrystalDecisions.CrystalReports.Engine;
using FlexCel.XlsAdapter;
using FlexCel.Core;


namespace Onurcan_CALISKAN_12_I__12749
{
    public partial class enterexitlog : Form
    {
        DataTable dt_giriscikis;
        BindingSource bs_giriscikis;
        OleDbDataAdapter a1;
        OleDbConnection con;
        OleDbCommand komut = new OleDbCommand();
        string sorgu;
        int ek;
        public enterexitlog()
        {
            InitializeComponent();
        }

        public void listele_1()
        {
            dt_giriscikis = new DataTable();
            a1 = new OleDbDataAdapter(sorgu, con);
            a1.Fill(dt_giriscikis);

            bs_giriscikis = new BindingSource();
            bs_giriscikis.DataSource = dt_giriscikis;
            dataGridView1.DataSource = bs_giriscikis;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Giriş Tarihi";
            dataGridView1.Columns[2].HeaderText = "Giriş Saati";
            dataGridView1.Columns[3].HeaderText = "Çıkış Tarihi";
            dataGridView1.Columns[4].HeaderText = "Çıkış Saati";
            dataGridView1.Columns[5].HeaderText = "Giriş Yapan Kullanıcı";
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[3].Width = 200;
            dataGridView1.Columns[4].Width = 150;
            dataGridView1.Columns[5].Width = 150;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            giriscikis giriscikisrpr = new giriscikis();

            giriscikisrpr.SetDataSource(dt_giriscikis);

            ReportDocument rapordoc2 = giriscikisrpr;

            cikisgiris cikisgiris = new cikisgiris();
            cikisgiris.crystalReportViewer1.ReportSource = rapordoc2;
            cikisgiris.Show();
        }

        private void enterexitlog_Load(object sender, EventArgs e)
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
            sorgu = "Select * from giriscikis Order By id Asc ";
            listele_1();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            sorgu = "select * from giriscikis Where giristarihi='" + dateTimePicker1.Value.ToLongDateString() + "'";
            listele_1();
            if (dt_giriscikis.Rows.Count > 0)
            {
                ;
            }
            else
            {
                MessageBox.Show("Bu tarihte kullanıcı girişi bulunmamaktadır.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            XlsFile excel = new XlsFile(true);
            excel.NewFile();

            excel.SetCellValue(1, 3, "   Giriş Çıkış Log Raporu");  // BAŞLIK
            ek = 4;
            for (int i = 1; i <= dataGridView1.ColumnCount - 1; i++)
            {
                excel.SetCellValue(3, i, dataGridView1.Columns[i].HeaderText);  //  SÜTUNLARI EXCELE YAZ
            }

            for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
            {

                for (int k = 2; k <= dataGridView1.ColumnCount; k++)
                {

                    excel.SetCellValue(i + ek, k - 1, dataGridView1[k - 1, i].Value);        // İÇİÇE İKİ DÖNGÜ İLE GRİD İÇİNDEN VERİLERİ EXCELE ... 

                }

            }
            saveFileDialog1.Filter = "*.xls|*.xls";
            saveFileDialog1.ShowDialog();
            string yol2 = saveFileDialog1.FileName;

            try
            {
                excel.Save("" + yol2 + "");
            }
            catch
            {
                MessageBox.Show("Aktarma Başarısız..", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            MessageBox.Show("Excele Aktarma İşlemi Bitti.", "BİLGİLENDİRME", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sorgu = "Select * from giriscikis Order By id Asc ";
            listele_1();
        }
    }
}
