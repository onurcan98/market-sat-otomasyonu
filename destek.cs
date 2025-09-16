using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;

namespace Onurcan_CALISKAN_12_I__12749
{
    public partial class destek : Form
    {
        public destek()
        {
            InitializeComponent();
        }

        private void destek_Load(object sender, EventArgs e)
        {
            foreach (IPAddress adres in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                toolStripStatusLabel1.Text = "Ip Adresiniz: " + adres;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(@"C:\Program Files\TeamViewer\TeamViewer.exe");
            }
            catch
            {
                try
                {
                    System.Diagnostics.Process.Start(@"C:\Program Files (x86)\TeamViewer\TeamViewer.exe");
                }
                catch
                {
                    try
                    {
                        System.Diagnostics.Process.Start(@"C:\Program Files (x64)\TeamViewer\TeamViewer.exe");
                    }
                    catch
                    {
                        try
                        {
                            MessageBox.Show("Bilgisayarınız da TeamViewer bulunmamaktadır lütfen TeamViewer birazdan otomatik yüklenecektir.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            System.Diagnostics.Process.Start(@"C:\Onurcan_CALISKAN-12-I -12749\Onurcan_CALISKAN-12-I -12749\bin\x86\Release\tw");
                        }
                        catch
                        {
                            return;
                        }
                    }
                }
            }
        }
    }
}
