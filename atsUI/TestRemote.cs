using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Remote;
using Ats.Model;
using System.Threading;
using AsyncDialog;
    
namespace atsUI
{
    public partial class TestRemote : AsyncBaseDialog
    {
        public TestRemote()
        {
            InitializeComponent();
        }

        public int TestId { get; set; }
        public string OldText { get; set; }


        private void button1_Click(object sender, EventArgs e)
        {
            OldText = button1.Text;
            button1.Enabled = false;
            button1.Text = "Working...";
            TestId = 1;// Get this from the main form
            AsyncProcessDelegate d = delegate()
            {
                GetQuestions(this);
                //ThreadPool.QueueUserWorkItem(new WaitCallback(GetQuestions), this);
            };
            RunAsyncOperation(d);
        }

        public void GetQuestions(Object stateinfo)
        {
            TestRemote tr = (TestRemote)stateinfo;

            var questions = RemoteQuestionDAL.GetQuestions(tr.TestId);
            if (questions != null)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    listBox1.DataSource = questions.ToList<Question>(); // runs on UI thread
                    button1.Enabled = true;
                    button1.Text = OldText;
                });
            }
            else
            {
                MessageBox.Show("No questions returned");
                this.Invoke((MethodInvoker)delegate
                {
                    button1.Enabled = true;
                    button1.Text = OldText;
                });
            }
        }
    }
}
