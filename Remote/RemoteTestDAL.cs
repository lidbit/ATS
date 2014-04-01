using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ats.Model;
using Newtonsoft.Json;

namespace Remote
{
    public class RemoteTestDAL
    {
        private static string ServiceURL = RemoteUtil.ConfigString("Service");

        public static void ImportTest(int testid, string name, string type, int timelimit)
        {
            string result;
            Dictionary<string, string> postData = new Dictionary<string, string>();
            postData.Add("testid",testid.ToString());
            postData.Add("name", name);
            postData.Add("type", type);
            postData.Add("timelimit", timelimit.ToString());
            result = RemoteUtil.postSynchronous(ServiceURL, ServiceUtil.ServiceMethods.ImportTest, postData, ServiceUtil.ServiceCallType.Post);
        }

        public static int AddTest(string name, string type, int timelimit)
        {
            string result;
            string data;
            int testid = -1;
            Dictionary<string, string> postData = new Dictionary<string, string>();
            Dictionary<string, string> dict = null;
            postData.Add("name", name);
            postData.Add("type", type);
            postData.Add("timelimit", timelimit.ToString());
            result = RemoteUtil.postSynchronous(ServiceURL, ServiceUtil.ServiceMethods.AddTest, postData, ServiceUtil.ServiceCallType.Post);

            if (result != null)
            {
                dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                dict.TryGetValue("d", out data);
                testid = JsonConvert.DeserializeObject<int>(data);
                
            }
            return testid;
        }

        public static List<Test> GetTests()
        {
            string result = null;
            string data;
            List<Test> tests = null;
            Dictionary<string, string> dict = null;
            Dictionary<string, string> postData = new Dictionary<string, string>();

            result = RemoteUtil.postSynchronous(ServiceURL, ServiceUtil.ServiceMethods.GetTests, postData, ServiceUtil.ServiceCallType.Post);
            if (result != null)
            {
                dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                dict.TryGetValue("d", out data);
                tests = JsonConvert.DeserializeObject<List<Test>>(data);
                return tests;
            }
            else
                return null;
        }

        public static List<string> GetTestTypes()
        {
            string result = null;
            string data;
            List<string> testTypes = null;
            Dictionary<string, string> dict = null;
            Dictionary<string, string> postData = new Dictionary<string, string>();

            result = RemoteUtil.postSynchronous(ServiceURL, ServiceUtil.ServiceMethods.GetTestTypes, postData, ServiceUtil.ServiceCallType.Post);
            if (result != null)
            {
                dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                dict.TryGetValue("d", out data);
                testTypes = JsonConvert.DeserializeObject<List<string>>(data);
                return testTypes;
            }
            else
                return null;
        }

        public static void DeleteTest(int testid)
        {
            string result;
            Dictionary<string, string> postData = new Dictionary<string, string>();
            postData.Add("testid", testid.ToString());
            result = RemoteUtil.postSynchronous(ServiceURL, ServiceUtil.ServiceMethods.DeleteTest, postData, ServiceUtil.ServiceCallType.Post);
        }

        public static Test GetTest(int testid)
        {
            string result = null;
            string data;
            Test test = null;
            Dictionary<string, string> dict = null;
            Dictionary<string, string> postData = new Dictionary<string, string> {{"testid", testid.ToString()}};

            result = RemoteUtil.postSynchronous(ServiceURL, ServiceUtil.ServiceMethods.GetTest, postData, ServiceUtil.ServiceCallType.Post);
            if (result != null)
            {
                dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                dict.TryGetValue("d", out data);
                test = JsonConvert.DeserializeObject<Test>(data);
                return test;
            }
            else
                return null;
        }

        public static void SetTestTimelimit(int testid, int timelimit)
        {
            string result;
            Dictionary<string, string> postData = new Dictionary<string, string>();
            postData.Add("testid", testid.ToString());
            postData.Add("timelimit",timelimit.ToString());
            result = RemoteUtil.postSynchronous(ServiceURL, ServiceUtil.ServiceMethods.SetTestTimelimit, postData, ServiceUtil.ServiceCallType.Post);
        }
    }
}
