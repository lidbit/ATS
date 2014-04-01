using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Ats.Model;

namespace Remote
{
    public class RemoteQuestionDAL
    {
        private static string ServiceURL = RemoteUtil.ConfigString("Service");

        public static Question[] GetQuestions()
        {
            string result = null;
            string data;
            List<Question> questions = null;
            Dictionary<string, string> dict = null;
            Dictionary<string, string> postData = new Dictionary<string, string>();

            result = RemoteUtil.postSynchronous(ServiceURL, ServiceUtil.ServiceMethods.GetQuestionsAll, postData, ServiceUtil.ServiceCallType.Post);
            if (result != null)
            {
                dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                dict.TryGetValue("d", out data);
                questions = JsonConvert.DeserializeObject<List<Question>>(data);
                return questions.ToArray<Question>();
            }
            else
                return null;
        }

        public static Question[] GetQuestions(int id)
        {
            string result = null;
            string data;
            List<Question> questions = null;
            Dictionary<string, string> dict = null;
            Dictionary<string, string> postData = new Dictionary<string, string>();

            postData.Add("testid",id.ToString());
            result = RemoteUtil.postSynchronous(ServiceURL, ServiceUtil.ServiceMethods.GetQuestions, postData, ServiceUtil.ServiceCallType.Post);
            if (result != null)
            {
                dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                dict.TryGetValue("d", out data);
                questions = JsonConvert.DeserializeObject<List<Question>>(data);
                return questions.ToArray<Question>();
            }
            else
                return null;
        }

        public static void AddQuestion(int testid, String question, String type, String answer)
        {
            string result;
            Dictionary<string, string> postData = new Dictionary<string,string>();
            postData.Add("testid", testid.ToString());
            postData.Add("question",question);
            postData.Add("type",type);
            postData.Add("answer",answer);
            result = RemoteUtil.postSynchronous(ServiceURL, ServiceUtil.ServiceMethods.AddQuestion, postData, ServiceUtil.ServiceCallType.Post);
        }   

        public static void UpdateQuestion(int questionid, string questionstring, string answer)
        {
            string result;
            Dictionary<string,string> postData = new Dictionary<string, string>();
            postData.Add("questionid",questionid.ToString());
            postData.Add("questionstring",questionstring);
            postData.Add("answer",answer);
            result = RemoteUtil.postSynchronous(ServiceURL, ServiceUtil.ServiceMethods.UpdateQuestion, postData,
                                                ServiceUtil.ServiceCallType.Post);
        }

        public static void DeleteQuestion(int quesitonid)
        {
            string result;
            Dictionary<string,string> postData = new Dictionary<string, string>();
            postData.Add("questionid",quesitonid.ToString());
            result = RemoteUtil.postSynchronous(ServiceURL, ServiceUtil.ServiceMethods.DeleteQuestion, postData,
                                                ServiceUtil.ServiceCallType.Post);
        }
    }
}
