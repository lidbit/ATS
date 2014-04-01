using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ats.Model;
using Newtonsoft.Json;

namespace Remote
{
    public class RemoteTestResultDAL
    {
        private static string ServiceURL = RemoteUtil.ConfigString("Service");

        public static int AddTestResult(int userid, int testid, DateTime date, int timetaken, int correct, int total, string testname)
        {
            string result;
            string data;
            int testresultid = -1;
            Dictionary<string, string> postData = new Dictionary<string, string>();
            Dictionary<string, string> dict = null;

            postData.Add("userid", userid.ToString());
            postData.Add("testid", testid.ToString());
            postData.Add("date", date.ToString());
            postData.Add("timetaken",timetaken.ToString());
            postData.Add("correct",correct.ToString());
            postData.Add("total",total.ToString());
            postData.Add("testname",testname);
            result = RemoteUtil.postSynchronous(ServiceURL, ServiceUtil.ServiceMethods.AddTestResult, postData, ServiceUtil.ServiceCallType.Post);

            if (result != null)
            {
                dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                dict.TryGetValue("d", out data);
                testresultid = JsonConvert.DeserializeObject<int>(data);

            }
            return testresultid;
        }

        public static object GetTestResult(int testresultid)
        {
            string result = null;
            string data;
            TestResult testResult = null;
            Dictionary<string, string> dict = null;
            Dictionary<string, string> postData = new Dictionary<string, string> { { "testresultid", testresultid.ToString() } };

            result = RemoteUtil.postSynchronous(ServiceURL, ServiceUtil.ServiceMethods.GetTestResult, postData, ServiceUtil.ServiceCallType.Post);
            if (result != null)
            {
                dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                dict.TryGetValue("d", out data);
                testResult = JsonConvert.DeserializeObject<TestResult>(data);
                return testResult;
            }
            else
                return null;
        }

        public static List<TestResult> GetTestResults()
        {
            string result = null;
            string data;
            List<TestResult> tests = null;
            Dictionary<string, string> dict = null;
            Dictionary<string, string> postData = new Dictionary<string, string>();

            result = RemoteUtil.postSynchronous(ServiceURL, ServiceUtil.ServiceMethods.GetTestResults, postData, ServiceUtil.ServiceCallType.Post);
            if (result != null)
            {
                dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                dict.TryGetValue("d", out data);
                tests = JsonConvert.DeserializeObject<List<TestResult>>(data);
                return tests;
            }
            else
                return null;
        }
    }
}
