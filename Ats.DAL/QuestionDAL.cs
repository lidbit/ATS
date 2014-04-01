using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ats.Model;
using foundation;
using System.Data.Common;
using System.Collections;

namespace Ats.DAL
{
    public class QuestionDAL
    {
        private static IDatabase<DbDataReader, DbConnection> dbmanager;

        public static List<Question> GetQuestions(int testid)
        {
            List<Question> questions = new List<Question>();
            dbmanager = dbManager.Instance();
            string query = "select * from questions where testid=" + testid + ";";
            dbmanager.executeQuery(query);
            while (dbmanager.Reader.Read())
            {
                questions.Add(new Question
                {
                    Answer = dbmanager.Reader.GetString(4),
                    Quest = dbmanager.Reader.GetString(2),
                    Id = dbmanager.Reader.GetInt32(0),
                    Type = dbmanager.Reader.GetString(3),
                    TestId = dbmanager.Reader.GetInt32(1)
                });
            }
            return questions;
        }

        public static Question GetQuestion(int questionid)
        {
            Question question = null;

            string query = "select * from questions where questionid=" + questionid + ";";
            dbmanager.executeQuery(query);

            while (dbmanager.Reader.Read())
            {
                question = new Question
                {
                    Answer = dbmanager.Reader.GetString(4),
                    Quest = dbmanager.Reader.GetString(2),
                    Id = dbmanager.Reader.GetInt32(0),
                    Type = dbmanager.Reader.GetString(3),
                    TestId = dbmanager.Reader.GetInt32(1)
                };
            }

            return question;
        }

        public static void AddQuestion(int testid, String question, String type, String answer)
        {
            dbmanager.beginTransaction();
            dbmanager.setCommandText("INSERT INTO questions(testid,questionstring,type,answer) VALUES(@pTestid,@pQuestionstring,@pType,@pAnswer)");
            dbmanager.addParam("pTestid", testid);
            dbmanager.addParam("pQuestionstring", question);
            dbmanager.addParam("pType", type);
            dbmanager.addParam("pAnswer", answer);
            dbmanager.executeCommand();

            dbmanager.endTransaction();
        }

        public static void DeleteQuestion(int questionid)
        {
            dbmanager = dbManager.Instance();

            String query = "DELETE FROM QUESTIONS WHERE questionid=" + questionid.ToString() + ";";
            dbmanager.execute(query);
        }

        public static void UpdateQuestion(int questionid, String question, String answer)
        {
            dbmanager = dbManager.Instance();

            String updateQuestion = "update questions set questionstring='" + question + "' where questionid=" + questionid.ToString() + ";";
            String updateanswer = "update questions set answer='" + answer + "' where questionid=" + questionid.ToString() + ";";

            dbmanager.execute(updateQuestion);
            dbmanager.execute(updateanswer);
        }

        public static List<Question> GetQuestions()
        {
            dbmanager = dbManager.Instance();
            List<Question> questions = new List<Question>();

            string sql = "select * from questions;";
            dbmanager.executeQuery(sql);

            while (dbmanager.Reader.Read())
            {
                questions.Add(new Question
                {
                    Answer = dbmanager.Reader.GetString(4),
                    Quest = dbmanager.Reader.GetString(2),
                    Id = dbmanager.Reader.GetInt32(0),
                    Type = dbmanager.Reader.GetString(3),
                    TestId = dbmanager.Reader.GetInt32(1)
                });
            }
            return questions;
        }
    }
}
