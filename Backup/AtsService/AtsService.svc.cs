using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;

using System.Web.Script.Services;
using Newtonsoft.Json;
using Ats.Model;
using RemoteDAL;

namespace AtsWeb
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AtsService
    {
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        public string[] DoWork()
        {
            List<String> list = new List<string>();

            list.Add("1");
            list.Add("2");
            list.Add("3");
            
            return list.ToArray();
        }

        [OperationContract]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetQuestions(int testid)
        {
            List<Question> questions = new List<Question>();
            questions = QuestionDAL.GetQuestions(testid);
            return JsonConvert.SerializeObject(questions);
        }

        [OperationContract]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetQuestionsAll()
        {
            List<Question> questions = new List<Question>();
            questions = QuestionDAL.GetQuestionsAll();
            return JsonConvert.SerializeObject(questions);
        }

        [OperationContract]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AddQuestion(int testid, String question, String type, String answer)
        {
            QuestionDAL.AddQuestion(testid, question, type, answer);
        }

        [OperationContract]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ImportTest(int testid, string name, string type, int timelimit)
        {
            TestDAL.ImportTest(testid, name, type, timelimit);
        }

        [OperationContract]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int AddTest(string name, string type, int timelimit)
        {
            return TestDAL.AddTest(name, type, timelimit);
        }

        [OperationContract]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetTests()
        {
            return JsonConvert.SerializeObject(TestDAL.GetTests());
        }

        [OperationContract]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetTest(int testid)
        {
            return JsonConvert.SerializeObject(TestDAL.GetTest(testid));
        }

        [OperationContract]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetTestTypes()
        {
            return JsonConvert.SerializeObject(TestDAL.GetTestTypes());
        }

        [OperationContract]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DeleteTest(int testid)
        {
            TestDAL.DeleteTest(testid);
        }

        [OperationContract]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void UpdateQuestion(int questionid, string questionstring, string answer)
        {
            QuestionDAL.UpdateQuestion(questionid, questionstring, answer);
        }

        [OperationContract]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DeleteQuestion(int questionid)
        {
            QuestionDAL.DeleteQuestion(questionid);
        }

        [OperationContract]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SetTestTimelimit(int testid, int timelimit)
        {
            TestDAL.SetTestTimelimit(testid, timelimit);
        }

        [OperationContract]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string AddTestResult(int userid, int testid, string date, int timetaken, int correct, int total, string testname)
        {
            return JsonConvert.SerializeObject(TestResultDAL.AddTestResult(userid, testid, timetaken, correct, total, testname));
        }

        [OperationContract]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetTestResult(int testresultid)
        {
            return JsonConvert.SerializeObject(TestResultDAL.GetTestResult(testresultid));
        }

        [OperationContract]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetTestResults()
        {
            return JsonConvert.SerializeObject(TestResultDAL.GetTestResults());
        }

        [OperationContract]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string AddQuestionResult(int testid, int questionid, string useranswer, string answer, int correct, int responsetime, int result)
        {
            return JsonConvert.SerializeObject(QuestionResultDAL.AddQuestionResult(testid, questionid, useranswer, answer, correct,
                                                                responsetime, result));
        }
    }
}
