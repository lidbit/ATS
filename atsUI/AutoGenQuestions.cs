using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using core;
using Ats.Model;

namespace atsUI
{
    public partial class AutoGenQuestions : Form
    {
        private Form form;

        public AutoGenQuestions()
        {
            InitializeComponent();
            groupBox1.Controls.Add(radioButton1);
            groupBox1.Controls.Add(radioButton2);
            groupBox1.Controls.Add(radioButton3);
            groupBox1.Controls.Add(radioButton4);
        }

        public void init(Form frm)
        {
            if (frm is AddTestFrame)
                form = (AddTestFrame)frm;
            if (frm is EditTestFrame)
                form = (EditTestFrame)frm;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Question> questions = new List<Question>();
            String selectedRadioButton = String.Empty;
            QuestionGenerator qgen = new QuestionGenerator();
            if (radioButton1.Checked)
                selectedRadioButton = "+";
            if (radioButton2.Checked)
                selectedRadioButton = "-"; 
            if (radioButton3.Checked)
                selectedRadioButton = "/";
            if (radioButton4.Checked)
                selectedRadioButton = "*";
            for (int i = 0; i < Convert.ToInt32(textBox1.Text); i++)
            {
                questions.Add(qgen.createQuestion(selectedRadioButton,Convert.ToInt32(textBox2.Text)));
            }

            if (form is AddTestFrame)
            {
                form = (AddTestFrame)form;
                ((AddTestFrame)form).updateQuestions(questions);
                Dispose();
            }
            if (form is EditTestFrame)
            {
                form = (EditTestFrame)form;
                ((EditTestFrame)form).updateQuestions(questions);
                Dispose();
            }
        }
    }
}
