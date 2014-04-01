using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ats.Model;
using Ats.DAL;
using core;

namespace atsUI
{
    public partial class AddTestFrame : Form
    {
        public AddTestFrame()
        {
            InitializeComponent();
        }

        public void init()
        {
            var questions = Remote.RemoteQuestionDAL.GetQuestions();
            if (questions != null)
            {
                foreach (Question q in questions)
                {
                    listBox1.Items.Add(q);
                }
            }

            var testtypes = Remote.RemoteTestDAL.GetTestTypes();
            if (testtypes != null)
            {
                foreach (String type in testtypes)
                {
                    comboBox1.Items.Add(type);
                }
            }
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                for (int i = 0; i < listBox1.SelectedIndices.Count; i++)
                {
                    String question = String.Empty, answer = String.Empty;
                    question = (((Question)listBox1.Items[listBox1.SelectedIndices[i]]).Quest);
                    answer = (((Question)listBox1.Items[listBox1.SelectedIndices[i]]).Answer);
                    listBox2.Items.Add(new Question(question, answer));
                }                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ((ATSUI)this.Owner).init();
            ((ATSUI)this.Owner).Refresh();
            Dispose();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
                while (listBox2.SelectedIndices.Count > 0)
                {
                    listBox2.Items.RemoveAt(listBox2.SelectedIndices[0]);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Equals("") || richTextBox2.Text.Equals(""))
            {
                MessageBox.Show("Please enter Question and Answer parts");
                richTextBox1.Focus();
            }
            else
            {
                listBox2.Items.Add(new Question(richTextBox1.Text, richTextBox2.Text));
                richTextBox1.Clear();
                richTextBox2.Clear();
                richTextBox1.Focus();
            }
        }

        private int getTimelimit()
        {
            Time time = new Time();

            if (comboBox2.SelectedIndex != -1 && comboBox3.SelectedIndex != -1 && comboBox4.SelectedIndex != -1)
            {
                time.setHours(Convert.ToInt32((String)comboBox2.Items[comboBox2.SelectedIndex]));
                time.setMinutes(Convert.ToInt32((String)comboBox3.Items[comboBox3.SelectedIndex]));
                time.setSeconds(Convert.ToInt32((String)comboBox4.Items[comboBox4.SelectedIndex]));
            }
            else
            {
                time.setHours(0);
                time.setMinutes(0);
                time.setSeconds(0);
            }

            return time.timeToseconds();
        }

        private bool isFormValid()
        {
            bool valid = true;
            if (textBox1.Text.Equals(""))
            {
                MessageBox.Show("Please enter a test name");
                valid = false;
                textBox1.Focus();
            }
            else if (comboBox1.Text.Equals(""))
            {
                MessageBox.Show("Please select a test type");
                valid = false;
                comboBox1.Focus();
            }
            else if (getTimelimit() <= 0)
            {
                MessageBox.Show("Please enter a timelimit greater than zero");
                comboBox2.Focus();
                valid = false;
            }
            return valid;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String testname = textBox1.Text;
            String testtype;
            int timelimit = getTimelimit();

            if (isFormValid())
            {
                int testid = -1;
                testtype = (string)comboBox1.Text;
                testid = Remote.RemoteTestDAL.AddTest(testname,testtype,timelimit);
                //TestDAL.AddTest(testname, testtype, timelimit);
                for (int i = 0; i < listBox2.Items.Count; i++)
                {
                    Remote.RemoteQuestionDAL.AddQuestion(testid, ((Question)listBox2.Items[i]).Quest, testtype, ((Question)listBox2.Items[i]).Answer);
                    //QuestionDAL.AddQuestion(testid, ((Question)listBox2.Items[i]).Quest, testtype, ((Question)listBox2.Items[i]).Answer);
                }
                textBox1.Clear();
                listBox2.Items.Clear();
                MessageBox.Show("Test successfully added");
                Refresh();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AutoGenQuestions autoGen = new AutoGenQuestions();
            autoGen.init(this);
            autoGen.Show();
        }

        public void updateQuestions(List<Question> questions)
        {
            foreach(Question q in questions)
            {
                listBox2.Items.Add(q);
            }
        }
    }
}
