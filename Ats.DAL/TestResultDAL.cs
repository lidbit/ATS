using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using foundation;
using System.Data.Common;
using Ats.Model;

namespace Ats.DAL
{
    public class TestResultDAL
    {
        private static IDatabase<DbDataReader, DbConnection> dbmanager;

        public static TestResult GetTestResult(int testResultId)
        {
            dbmanager = dbManager.Instance();

            int testresultid = testResultId;
            int testid = 0;
            int userid = 0;
            int score = 0;
            int totalquestions = 0;
            int timetaken = 0;
            String description = null;

            String query = "SELECT * FROM test_results WHERE test_result_id=" + testResultId.ToString() + ";";
            dbmanager.executeQuery(query);

            while (dbmanager.Reader.Read())
            {
                userid = dbmanager.Reader.GetInt32(1);
                testid = dbmanager.Reader.GetInt32(2);
                score = dbmanager.Reader.GetInt32(5);
                totalquestions = dbmanager.Reader.GetInt32(6);
                timetaken = dbmanager.Reader.GetInt32(4);
                description = dbmanager.Reader.GetString(7);
            }
            return new TestResult(testresultid, testid, userid, score, totalquestions, timetaken, description);
        }

        public static List<TestResult> GetAllTestResults()
        {
            dbmanager = dbManager.Instance();

            List<TestResult> results = new List<TestResult>();

            String query = "SELECT * FROM test_results ORDER BY DATE DESC;";

            dbmanager.executeQuery(query);

            while (dbmanager.Reader.Read())
            {
                TestResult testResult = new TestResult();
                testResult.TestResultId = dbmanager.Reader.GetInt32(0);
                testResult.UserId = dbmanager.Reader.GetInt32(1);
                testResult.TestId = dbmanager.Reader.GetInt32(2);
                testResult.DateTaken = Convert.ToDateTime(dbmanager.Reader.GetString(3));
                testResult.Score = dbmanager.Reader.GetInt32(5);
                testResult.TotalQuestions = dbmanager.Reader.GetInt32(6);
                testResult.TimeTaken = dbmanager.Reader.GetInt32(4);
                testResult.Description = dbmanager.Reader.GetString(7);
                results.Add(testResult);
            }
            foreach (TestResult tr in results)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select questions.questionid, questions.questionstring, question_results.useranswer, question_results.answer, question_results.responsetime ");
                sb.Append("from question_results, questions ");
                sb.Append("where questions.testid = question_results.test_id AND questions.questionid = question_results.question_id AND questions.testid =" + tr.TestId);
                sb.Append(" Order by questions.questionid");
                String query2 = sb.ToString();
                dbmanager.executeQuery(query2);

                while (dbmanager.Reader.Read())
                {
                    QuestionResult qr = new QuestionResult();
                    qr.Question = dbmanager.Reader.GetString(1);
                    qr.UserAnswer = dbmanager.Reader.GetString(2);
                    qr.Answer = dbmanager.Reader.GetString(3);
                    qr.ResponseTime = dbmanager.Reader.GetInt32(4);
                    tr.addQuestionResult(qr);
                }
            }
            return results;
        }

        public static void AddTestResult(Test test)
        {
            dbmanager = dbManager.Instance();

            int questions = QuestionDAL.GetQuestions(test.Id).Count;

            dbmanager.beginTransaction();
            dbmanager.setCommandText("INSERT INTO test_results(userid,testid,date,timetaken,correct,total,testname) VALUES(@pUserid,@pTestid,@pDate,@pTimetaken,@pCorrect,@pTotal,@pTestname)");
            dbmanager.addParam("pUserid", test.UserId);
            dbmanager.addParam("pTestid", test.Id);
            dbmanager.addParam("pDate", DateTime.Now.ToString());
            dbmanager.addParam("pTimetaken", test.SecondsElapsed);
            dbmanager.addParam("pCorrect", test.Correct);
            dbmanager.addParam("pTotal", questions);
            dbmanager.addParam("pTestname", test.Name);

            dbmanager.executeCommand();
            dbmanager.endTransaction();
        }
    }
}
