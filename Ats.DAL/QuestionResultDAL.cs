using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using foundation;
using Ats.Model;

namespace Ats.DAL
{
    public class QuestionResultDAL
    {
        private static IDatabase<DbDataReader, DbConnection> dbmanager;

        public static void AddQuestionResult(int testid, int questionid, string useranswer, string answer, int correct, int responsetime, int result)
        {
            dbmanager = dbManager.Instance();

            dbmanager.setCommandText("INSERT INTO question_results(test_id,question_id,useranswer,answer,correct,responsetime,result) VALUES(@pTestid,@pQuestionid,@pUseranswer,@pAnswer,@pCorrect,@pResponsetime,@pResult)");

            dbmanager.beginTransaction();

            dbmanager.addParam("pTestid", testid);
            dbmanager.addParam("pQuestionid", questionid);
            dbmanager.addParam("pUseranswer", useranswer);
            dbmanager.addParam("pAnswer", answer);
            dbmanager.addParam("pCorrect", correct);
            dbmanager.addParam("pResponsetime", responsetime);
            dbmanager.addParam("pResult", result);
            dbmanager.executeCommand();
            dbmanager.endTransaction();
        }
    }
}
