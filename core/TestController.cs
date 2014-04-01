using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using Ats.DAL;
using Ats.Model;

namespace core
{
    public class TestController
    {
        private List<Test> tests;

        protected TestController()
        {
            tests = new List<Test>();
            loadTests();
            initTests();
        }

        public void initTests()
        {
            foreach (Test t in tests)
            {
                //TestHelper.init();
            }
        }

        public void loadTests()
        {
            tests.Clear();
            tests = TestDAL.GetTests();
        }

        public Test getTest(int testid)
        {
            return TestDAL.GetTest(testid);
        }

        public TestResult getTestResult(int testResultId)
        {
            return TestResultDAL.GetTestResult(testResultId);
        }

        public String getLastTestId()
        {
            return TestDAL.GetLastTestId();
        }

        public String getLastTestResultId()
        {
            return TestDAL.GetLastTestResultId();
        }

        public TestResult getLastTestResult()
        {
            return getTestResult(Convert.ToInt32(getLastTestResultId()));
        }

        public void addTest(String name, String type, int timelimit)
        {
            TestDAL.AddTest(name, type, timelimit);
        }

        public void deleteTest(int testid)
        {
            TestDAL.DeleteTest(testid);
        }

        public void setTestTimelimit(int testid, int timelimit)
        {
            TestDAL.SetTimeLimit(testid, timelimit);
        }

        public void addTestResult(Test test)
        {
            TestResultDAL.AddTestResult(test);
        }

        public void addQuestion(int testid, String question, String type, String answer)
        {
            QuestionDAL.AddQuestion(testid, question, type, answer);
        }

        public void deleteQuestion(int questionid)
        {
            QuestionDAL.DeleteQuestion(questionid);
        }

        public void updateQuestion(int questionid, String question, String answer)
        {
            QuestionDAL.UpdateQuestion(questionid,question,answer);
        }

        public List<Question> getAllQuestions()
        {
            return QuestionDAL.GetQuestions();
        }

        public List<String> getTestTypes()
        {
            return TestDAL.GetTestTypes();   
        }
    }
}
