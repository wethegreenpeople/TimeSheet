using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Diagnostics;

namespace TimeSheet
{
    public partial class FormUpdate : Form
    {
        public FormUpdate()
        {
            InitializeComponent();
            this.TopMost = true;
            this.Activate();
            richTextBoxChangelog.Text = File.ReadAllText("newestversion.ini");
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile("http://uraqt.xyz/uselessprograms/TimeSheet.exe", "Update.exe");
                Process.Start("Update.exe");
                Application.Exit();
            }
        }

        private void buttonSkip_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
