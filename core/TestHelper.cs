using System;
using System.Timers;
using Ats.DAL;
using Ats.Model;
using System.Collections.Generic;
using Remote;

namespace core
{
    public class TestHelper
    {
        private Timer timer;
        private List<Question> questions;

        public Test TestObj { get; set; }
        

        public TestHelper(Test test)
        {
            TestObj = test;
            questions = new List<Question>(RemoteQuestionDAL.GetQuestions(test.Id));
            test.CurrentQuestion = 0;
            test.SecondsElapsed = 0;
            timer = new Timer();
            timer.Interval = 1000; // one second interval
            timer.Enabled = true;
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            test.Running = false;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            TestObj.SecondsElapsed++;
            if (TestObj.CurrentQuestion == questions.Count || TestObj.SecondsElapsed >= TestObj.TimeLimit)
            {
                stop();
            }
        }

        public void init()
        {     
            questions.Clear();

            questions = new List<Question>(Remote.RemoteQuestionDAL.GetQuestions(TestObj.Id));        
        }

        public void start(int _userId)
        {
            TestObj.CurrentQuestion = 0;
            TestObj.SecondsElapsed = 0;
            TestObj.Correct = 0;
            TestObj.UserId = _userId;
            TestObj.Running = true;
            timer.Start();
        }

        public void stop()
        {
            timer.Stop();
            TestObj.Running = false;
        }

        public Question getQuestion(int index)
        {
            if ((index >= 0) && (index < questions.Count))
                return (Question)questions[index];
            else
                return null;
        }

        //TODO should probably have a present() method instead

        public Question nextQuestion(int current)
        {
            if (current < questions.Count)
                return (Question)questions[current];
            else
                return null;
        }

        public bool moveToNextQuestion()
        {
            if (TestObj.CurrentQuestion == questions.Count)
            {
                return false;
            }
            else
            {
                TestObj.CurrentQuestion++;
                return true;
            }
        }

        //TODO check answer depends on what test this is
        public void checkAnswer(String answer)
        {
            if (TestObj.Running)
            {
                if (((Question)questions[TestObj.CurrentQuestion]).Answer.Trim().Equals(answer.Trim()))
                {
                    ((Question)questions[TestObj.CurrentQuestion]).Correct = 1;
                    ((Question)questions[TestObj.CurrentQuestion]).Useranswer = answer;
                    TestObj.Correct++;
                    Remote.RemoteQuestionResultDAL.AddQuestionResult(TestObj.Id,((Question) questions[TestObj.CurrentQuestion]).Id,answer,((Question) questions[TestObj.CurrentQuestion]).Answer, 1, 0, 1);
                    //QuestionResultDAL.AddQuestionResult(TestObj.Id, ((Question)questions[TestObj.CurrentQuestion]).Id, answer, ((Question)questions[TestObj.CurrentQuestion]).Answer, 1, 0, 1);
                }
                else
                {
                    Remote.RemoteQuestionResultDAL.AddQuestionResult(TestObj.Id, ((Question)questions[TestObj.CurrentQuestion]).Id, answer, ((Question)questions[TestObj.CurrentQuestion]).Answer, 0, 0, 0);
                    //QuestionResultDAL.AddQuestionResult(TestObj.Id, ((Question)questions[TestObj.CurrentQuestion]).Id, answer, ((Question)questions[TestObj.CurrentQuestion]).Answer, 0, 0, 0);
                }
            }
        }

        public override string ToString()
        {
            return TestObj.Name;
        }

        public List<Question> Questions
        {
            get { return questions; }
        }
    }
}
