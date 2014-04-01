using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Remote
{
    public class RemoteQuestionResultDAL
    {
        private static string ServiceURL = RemoteUtil.ConfigString("Service");

        public static int AddQuestionResult(int testid, int questionid, string useranswer, string answer, int correct, int responsetime, int result)
        {
            string Quesryresult;
            string data;
            int questionresultid = -1;
            Dictionary<string, string> postData = new Dictionary<string, string>();
            Dictionary<string, string> dict = null;

            postData.Add("testid", testid.ToString());
            postData.Add("questionid", questionid.ToString());
            postData.Add("useranswer", useranswer);
            postData.Add("answer", answer);
            postData.Add("correct", correct.ToString());
            postData.Add("responsetime", responsetime.ToString());
            postData.Add("result",result.ToString());
            Quesryresult = RemoteUtil.postSynchronous(ServiceURL, ServiceUtil.ServiceMethods.AddQuestionResult, postData, ServiceUtil.ServiceCallType.Post);

            if (Quesryresult != null)
            {
                dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(Quesryresult);
                dict.TryGetValue("d", out data);
                questionresultid = JsonConvert.DeserializeObject<int>(data);
            }
            return questionresultid;
        }
    }
}
