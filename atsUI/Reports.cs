using System;
using System.IO;
using System.Windows.Forms;

namespace atsUI
{
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
            string currentDir = Directory.GetCurrentDirectory();
            currentDir = currentDir.Replace("\\","/");

            this.webBrowser1.Url = new System.Uri("file:///"+currentDir+"/Reports/Reports.htm" + "", System.UriKind.Absolute);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void Reports_Load(object sender, EventArgs e)
        {

        }
    }
}
