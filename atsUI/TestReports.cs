using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using core;

namespace atsUI
{
    public partial class TestReports : Form
    {
        public TestReports()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReportGenerator repGen = new ReportGenerator();
            repGen.createReport();
            Reports reports = new Reports();
            reports.Show();
            System.GC.Collect();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
