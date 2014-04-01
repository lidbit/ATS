using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Remote;
using System.Threading;

namespace atsUI
{
    public partial class UsersForm : Form
    {
        public UsersForm()
        {
            InitializeComponent();
            ThreadPool.QueueUserWorkItem(new WaitCallback(GetQuestions), this);
        }

        private void GetQuestions(Object stateInfo)
        {
            UsersForm tr = (UsersForm)stateInfo;

            var questions = Remote.RemoteQuestionDAL.GetQuestions(1);

            if (questions != null)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    listBox1.DataSource = questions;
                });
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
