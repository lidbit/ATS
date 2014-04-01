using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Remote;
using core;
using Ats.Model;
using Ats.DAL;
namespace atsUI
{
    public partial class TestFrame : Form
    {
        private TestHelper test;
        private System.Windows.Forms.Timer timer;
        private bool numberEntered;
        private StringBuilder userInput;
        private bool readyToRender;
        private TestFrame instance;
        private int userid;
        private BufferedGraphicsContext context;
        private BufferedGraphics grafx;
        private Box box;

        private Renderer renderer;

        public TestFrame(TestHelper t, int userid)
        {
            test = t;
            InitializeComponent();

            context = BufferedGraphicsManager.Current;
            context.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);
            grafx = context.Allocate(this.CreateGraphics(), new Rectangle(0, 0, this.Width, this.Height));
            renderer = new Renderer(grafx);
            
            box = new Box(renderer,20,20);
            box.X = 150;
            box.Y = 300;

            userInput = new StringBuilder();
            
            this.userid = userid;
            instance = this;
            numberEntered = true;
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(this.OnTimer);              
        }

        private void run()
        {
            if (test.Questions.Count > 0)
            {
                test.start(userid);
                timer.Start();
                readyToRender = true;
            }
            else
                this.Dispose();
            while (readyToRender && test.TestObj.Running)
            {
                render();
                Application.DoEvents();
            }
        }

        private void render()
        {
            renderer.fillRect(Brushes.White, this);
            if (test.nextQuestion(test.TestObj.CurrentQuestion) != null)
            {
                renderer.restoreFont();
                renderer.drawText("Seconds Elapsed", 10, 10);
                renderer.drawText(test.TestObj.SecondsElapsed.ToString(), 120, 10);
                renderer.drawText("Question", 10, 20);
                renderer.drawText((test.TestObj.CurrentQuestion + 1).ToString() + " /" + test.Questions.Count, 120, 20);
                renderer.SetFont(new System.Drawing.Font("Arial", 14));
                renderer.drawText("Question: " + test.nextQuestion(test.TestObj.CurrentQuestion), this.Width / 3, this.Height / 3);
            }

            renderer.drawText("Your answer: " + userInput.ToString(), 150, 250);
            box.draw();
            System.Threading.Thread.Sleep(1);
            Refresh();
        }

        private void OnTimer(object source, EventArgs e)
        {
            if (!test.TestObj.Running)
            {
                timer.Stop();
                test.stop();
                int testresultid = -1;
                int questions = RemoteQuestionDAL.GetQuestions(test.TestObj.Id).Length;
                testresultid = RemoteTestResultDAL.AddTestResult(test.TestObj.UserId, test.TestObj.Id, DateTime.Now,
                                                         test.TestObj.SecondsElapsed, test.TestObj.Correct, questions,
                                                         test.TestObj.Name);

                //TestResultDAL.AddTestResult(test.TestObj);
                MessageBox.Show(RemoteTestResultDAL.GetTestResult(testresultid).ToString());
                instance.Dispose();
                this.Refresh();
            }
            this.Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            grafx.Render(e.Graphics);  
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (test.TestObj.Running)
                checkInput(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            MessageBox.Show("X = " + e.X + " Y = " + e.Y);
        }

        private void checkInput(KeyEventArgs e)
        {
            numberEntered = true;
            //MessageBox.Show(this, "e.KeyCode = " + e.KeyCode + "\ne.KeyValue = " + e.KeyValue, "ATS", MessageBoxButtons.OK);
            if (e.KeyCode == Keys.Enter)
            {
                test.getQuestion(test.TestObj.CurrentQuestion).TimetoAnswer = test.TestObj.SecondsElapsed;
                test.checkAnswer(userInput.ToString().Trim());
                // get next question
                if (test.TestObj.CurrentQuestion >= test.Questions.Count)
                {
                    timer.Stop();
                    this.Text = "Test Finished";
                    test.stop();
                }
                else
                {
                    test.moveToNextQuestion();
                    box.X = box.X + 10;
                }
                // clear userInput
                userInput.Remove(0, userInput.Length);
            }
            if (e.KeyCode == Keys.Escape)
            {
                timer.Stop();
                test.stop();
                Dispose();
            }
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                if (userInput.ToString().Length > 0)
                {
                    userInput.Remove(userInput.Length - 1,1);
                }
            }
            if (numberEntered)
            {
                userInput.Append(Util.KeyToString(e));
            }
        }

        private void TestFrame_Shown(object sender, EventArgs e)
        {
            run();
        }
    }
}
