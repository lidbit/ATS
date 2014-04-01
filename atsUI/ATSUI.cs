using System;
using System.Windows.Forms;
using core;
using Ats.DAL;
using Ats.Model;
using System.Collections.Generic;
using System.Threading;

namespace atsUI
{
    public partial class ATSUI : Form
    {
        private UserManager usermanager;


        public ATSUI()
        {
            InitializeComponent();
        }

        public void init()
        {
            label6.Text = "";
            label7.Text = "";
            label8.Text = "";

            //usermanager = new UserManager();
            //usermanager.initUsers();
            listBox1.Items.Clear();

            // Sync with remote db. Just restore the remote from local actually for now.

            //List<Test> testsLocal = new List<Test>();
            //testsLocal = TestDAL.GetTests();
            //foreach (Test t in testsLocal)
            //{
            //    Remote.RemoteTestDAL.ImportTest(t.Id, t.Name, t.TestType, t.TimeLimit);
            //}
            //List<Question> quesitonsLocal = new List<Question>();
            //quesitonsLocal = QuestionDAL.GetQuestions();
            //foreach (Question q in quesitonsLocal)
            //{
            //    Remote.RemoteQuestionDAL.AddQuestion(q.TestId, q.Quest, q.Type, q.Answer);
            //}

            ThreadPool.QueueUserWorkItem(new WaitCallback(GetTests), this);
        }

        private void GetTests(Object stateInfo)
        {
            ATSUI ui = (ATSUI)stateInfo;

            var tests = Remote.RemoteTestDAL.GetTests();

            if (tests != null)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    foreach (Test t in tests)
                    {
                        listBox1.Items.Add(t);
                    }
                });
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                Time time = new Time(((Test)listBox1.Items[listBox1.SelectedIndex]).TimeLimit);
                label6.Text = ((Test)listBox1.Items[listBox1.SelectedIndex]).TestType;
                label7.Text = time.ToString();
                int questions = Remote.RemoteQuestionDAL.GetQuestions(((Test)listBox1.Items[listBox1.SelectedIndex]).Id).Length;
                //var n = QuestionDAL.GetQuestions(((Test)listBox1.Items[listBox1.SelectedIndex]).Id).Count;
                label8.Text = questions.ToString();
            }
            else
            {
                MessageBox.Show("Please select a test from the list");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                Test t = (Test)listBox1.Items[(listBox1.SelectedIndex)];
                TestHelper testHelper = new TestHelper(t);
                TestFrame testframe = new TestFrame(testHelper, usermanager.getUser("andrew").Id);
                testframe.Show();
            }
            else
            {
                MessageBox.Show("Please select a test from the list");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                // edit test frame
                EditTestFrame editframe = new EditTestFrame();
                editframe.init((Test)listBox1.Items[listBox1.SelectedIndex]);
                editframe.Show(this);
            }
            else
            {
                MessageBox.Show("Please select a test from the list");
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            try
            {
                about.ShowDialog();
            }
            finally
            {
                if (about != null)
                    about.Dispose();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                if (MessageBox.Show(this, "Are you sure you want to delete this test?", "ATS", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    Remote.RemoteTestDAL.DeleteTest(((Test)listBox1.Items[listBox1.SelectedIndex]).Id);
                    //TestDAL.DeleteTest(((Test)listBox1.Items[listBox1.SelectedIndex]).Id);
                    init();
                    Refresh();
                }
            }
            else
            {
                MessageBox.Show(this, "Please select a test from the list");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddTestFrame addtestframe = new AddTestFrame();
            addtestframe.init();
            addtestframe.Show(this);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TestReports reports = new TestReports();
            reports.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                TestImporterExporter.exportTest(((Test)listBox1.Items[listBox1.SelectedIndex]));
            }
            else
            {
                MessageBox.Show("Please select a test from the list");
            }
        }

        private void exportTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                TestImporterExporter.importTest(openFileDialog1.FileName);
            }
        }

        private void manageUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UsersForm usersForm = new UsersForm();
            usersForm.Show();
        }

        private void testRemoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestRemote tr = new TestRemote();
            tr.Show();
        }
    }
}
