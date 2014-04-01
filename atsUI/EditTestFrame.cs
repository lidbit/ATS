using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Ats.Model;
using Ats.DAL;
using core;

namespace atsUI
{
    public partial class EditTestFrame : Form
    {
        private Test test;
        private Time time;

        public EditTestFrame()
        {
            InitializeComponent();
        }

        public void init(Test t)
        {
            //testcontroller = TestController.Instance();
            test = t;
            listBox1.Items.Clear();

            foreach (Question question in Remote.RemoteQuestionDAL.GetQuestions(test.Id))
            {
                listBox1.Items.Add(question);
            }
            time = new Time(test.TimeLimit);
            label2.Text = test.Name;
            richTextBox1.Clear();
            richTextBox2.Clear();
            comboBox1.SelectedItem = time.getHours().ToString();
            comboBox2.SelectedItem = time.getMinutes().ToString();
            comboBox3.SelectedItem = time.getSeconds().ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox2.Clear();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                richTextBox1.Text = ((Question)listBox1.Items[listBox1.SelectedIndex]).Quest;
                richTextBox2.Text = ((Question)listBox1.Items[listBox1.SelectedIndex]).Answer;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int questionid;
            String question = null;
            String answer = null;


            if (listBox1.SelectedIndex != -1 && (!richTextBox1.Text.Equals("") && !richTextBox2.Text.Equals("")))
            {
                // get question id		
                questionid = ((Question)listBox1.Items[listBox1.SelectedIndex]).Id;
                question = richTextBox1.Text;
                answer = richTextBox2.Text;
                // write question to database
                Remote.RemoteQuestionDAL.UpdateQuestion(questionid,question,answer);
                //QuestionDAL.UpdateQuestion(questionid, question, answer);

                // reload all tests from db
                //testcontroller.loadTests();
                //testcontroller.initTests();
                // reload the test
                init(Remote.RemoteTestDAL.GetTest(test.Id));
                Refresh();
            }
            else
            {
                MessageBox.Show(@"Please enter question and answer");
                richTextBox1.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // get form data make sure the fields are not empty
            if (richTextBox1.Text.Equals("") || richTextBox2.Text.Equals(""))
            {
                MessageBox.Show(@"Please enter question and answer part");
                richTextBox1.Focus();
            }
            else
            {
                int testid = test.Id;
                String question = richTextBox1.Text;
                String type = test.TestType;
                String answer = richTextBox2.Text;

                Remote.RemoteQuestionDAL.AddQuestion(testid,question,type,answer);
                //QuestionDAL.AddQuestion(testid, question, type, answer);
                // reload all tests from db
                //testcontroller.loadTests();
                //testcontroller.initTests();
                // reload the test
                init(TestDAL.GetTest(testid));
                richTextBox1.Focus();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // save all the stuff and close the form
		    System.Console.WriteLine((String) comboBox1.SelectedItem);
            time.setHours(Convert.ToInt32((String)comboBox1.SelectedItem));
            time.setMinutes(Convert.ToInt32((String)comboBox2.SelectedItem));
            time.setSeconds(Convert.ToInt32((String)comboBox3.SelectedItem));
            Remote.RemoteTestDAL.SetTestTimelimit(test.Id,time.timeToseconds());
		    //TestDAL.SetTimeLimit(test.Id, time.timeToseconds());

            ((ATSUI)this.Owner).init();
            ((ATSUI)this.Owner).Refresh();
		    Dispose();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // if there is something selected
            if (listBox1.SelectedIndex != -1)
            {
                while (listBox1.SelectedIndices.Count > 0)
                {
                    Remote.RemoteQuestionDAL.DeleteQuestion(((Question)listBox1.Items[listBox1.SelectedIndices[0]]).Id);
                    //QuestionDAL.DeleteQuestion(((Question)listBox1.Items[listBox1.SelectedIndices[0]]).Id);
                    listBox1.Items.RemoveAt(listBox1.SelectedIndices[0]);
                }
                // reload all tests from database
                //testcontroller.loadTests();
                //testcontroller.initTests();
                // reload the test
                init(Remote.RemoteTestDAL.GetTest(test.Id));
                Refresh();
            }
            else
            {
                MessageBox.Show(@"Please select a question to delete first");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AutoGenQuestions autoGen = new AutoGenQuestions();
            autoGen.init(this);
            autoGen.Show();
        }

        public void updateQuestions(List<Question> questions)
        {
            foreach (Question q in questions)
            {
                int testid = test.Id;
                String question = q.Quest;
                String type = test.TestType;
                String answer = q.Answer;

                Remote.RemoteQuestionDAL.AddQuestion(testid,question,type,answer);
                //QuestionDAL.AddQuestion(testid, question, type, answer);
                // reload all tests from db
                //testcontroller.loadTests();
                //testcontroller.initTests();
                // reload the test
                init(Remote.RemoteTestDAL.GetTest(testid));
                listBox1.Items.Add(q);
            }
        }
    }
}
