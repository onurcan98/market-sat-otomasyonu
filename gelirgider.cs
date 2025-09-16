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
    public partial class gelirgider : Form
    {
        DataTable dt_gelirgider;
        BindingSource bs_gelirgider;
        OleDbDataAdapter a1;
        OleDbConnection con;
        OleDbCommand komut = new OleDbCommand();
        string sorgu;
        double toplam2, kar, satilanmiktar;
        int ek;
        public gelirgider()
        {
            InitializeComponent();
        }

        public void listele()
        {
            dt_gelirgider = new DataTable();
            a1 = new OleDbDataAdapter(sorgu, con);
            a1.Fill(dt_gelirgider);


            bs_gelirgider = new BindingSource();
            bs_gelirgider.DataSource = dt_gelirgider;
            dataGridView1.DataSource = bs_gelirgider;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Barkod Numarası";
            dataGridView1.Columns[2].HeaderText = "Ürünün İsmi";
            dataGridView1.Columns[3].HeaderText = "Alış Fiyatı";
            dataGridView1.Columns[4].HeaderText = "Satış Fiyatı";
            dataGridView1.Columns[5].HeaderText = "Satılan Miktar";
            dataGridView1.Columns[6].HeaderText = "Ürünün çeşidi";
            dataGridView1.Columns[7].HeaderText = "Satış Tarihi";
            dataGridView1.Columns[8].HeaderText = "Satıştan elde edilen kâr";
            dataGridView1.Columns[9].HeaderText = "Satış Saati";
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[5].Width = 150;
            dataGridView1.Columns[7].Width = 150;
            dataGridView1.Columns[8].Width = 150;
            dataGridView1.RowHeadersWidth = 15;

            textBox1.Text = dt_gelirgider.Rows.Count.ToString() + "  Adet";
        }

        public void hesapla()
        {
            toplam2 = 0;
            kar = 0;
            satilanmiktar = 0;
            for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
            {
                try
                {
                    toplam2 += Convert.ToDouble(dataGridView1.Rows[i].Cells["satisfiyati"].Value.ToString().Replace(".",","));
                }
                catch { ;}
            }
            for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
            {
                try
                {
                    kar += Convert.ToDouble(dataGridView1.Rows[i].Cells["kar"].Value.ToString().Replace(".", ","));
                }
                catch { ;}
            }
            for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
            {
                try
                {
                    satilanmiktar += Convert.ToDouble(dataGridView1.Rows[i].Cells["miktar"].Value.ToString());
                }
                catch { ;}
            }
            textBox2.Text = Math.Round(Convert.ToDecimal(toplam2), 2).ToString().Replace(".",",") + " TL";
            textBox3.Text = Math.Round(Convert.ToDecimal(kar), 2).ToString().Replace(".",",") + "  TL";
            textBox4.Text = satilanmiktar.ToString() + "  Adet";
        }

        private void gelirgider_Load(object sender, EventArgs e)
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
            sorgu = "Select * from satilanlar Order By id Asc ";
            listele();
            hesapla();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            sorgu = "select * from satilanlar Where satistarihi='" + dateTimePicker1.Value.ToShortDateString() + "'";
            listele();
            hesapla();
            if (dt_gelirgider.Rows.Count > 0)
            {
                ;
            }
            else
            {
                MessageBox.Show("Bu tarihte ürün satışı bulunmamaktadır.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            XlsFile excel = new XlsFile(true);
            excel.NewFile();

            excel.SetCellValue(1, 3, "   Gelir Gider Kontrol Listesi");  // BAŞLIK
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
                MessageBox.Show("Aktarma Başarısız..","UYARI",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }
            MessageBox.Show("Excele Aktarma İşlemi Bitti.","BİLGİLENDİRME",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CrystalReport1 rapor = new CrystalReport1();

            rapor.SetDataSource(dt_gelirgider);                    // hazırlanan data table  rpt1 in veri kaynağı olarak ayarla

            ReportDocument rapordoc1 = rapor;                // yeni bir rapor dokumanına yukarıdaki rpt1 i ata

            rapordoc1.ParameterFields["baslik1"].CurrentValues.AddValue("Satılan ürün sayısı: " + dt_gelirgider.Rows.Count.ToString() + "  Adet");
            rapordoc1.ParameterFields["baslik2"].CurrentValues.AddValue("Ürünlerin toplam fiyatı: " + Math.Round(Convert.ToDecimal(toplam2), 2).ToString() + "  TL");
            rapordoc1.ParameterFields["baslik3"].CurrentValues.AddValue("Ürünlerden elde edilen kâr: " + Math.Round(Convert.ToDecimal(kar), 2).ToString() + "  TL");
            rapordoc1.ParameterFields["baslik4"].CurrentValues.AddValue("Satılan ürünlerin toplam miktarı: " + satilanmiktar.ToString() + "  Adet");


            raporform raporform = new raporform();
            raporform.crystalReportViewer1.ReportSource = rapordoc1;
            raporform.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            enterexitlog enterexit = new enterexitlog();
            enterexit.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            eklenenurunler eklenenurunler = new eklenenurunler();
            eklenenurunler.Show();
        }
    }
}
