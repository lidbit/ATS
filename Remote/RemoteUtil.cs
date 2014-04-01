using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;
using System.Web;
using System.Net;
using System.IO;

namespace Remote
{
    public class RemoteUtil
    {
        public static string postSynchronous(string serviceUrl, ServiceUtil.ServiceMethods method, Dictionary<string, string> postVariables, ServiceUtil.ServiceCallType requestType)
        {
            string result = null;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                string json = serializer.Serialize((object)postVariables);

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(serviceUrl + method);
                webRequest.Method = requestType.ToString();
                webRequest.ContentType = "application/json; charset=utf-8";
                webRequest.ContentLength = json.Length;

                Stream postStream = webRequest.GetRequestStream();
                postStream.Write(Encoding.UTF8.GetBytes(json), 0, json.Length);
                postStream.Close();

                HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();

                Stream responseStream = webResponse.GetResponseStream();
                StreamReader responseStreamReader = new StreamReader(responseStream);
                result = responseStreamReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        public static string getSynchronous(string url, ServiceUtil.ServiceCallType requestType)
        {
            string result = null;
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);

                HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
                Stream responseStream = webResponse.GetResponseStream();
                StreamReader responseStreamReader = new StreamReader(responseStream);
                result = responseStreamReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }
        public static Dictionary<string, string> postStringToDictionary(string postString)
        {
            char[] delimiters = { '&' };
            string[] postPairs = postString.Split(delimiters);

            Dictionary<string, string> postVariables = new Dictionary<string, string>();
            foreach (string pair in postPairs)
            {
                char[] keyDelimiters = { '=' };
                string[] keyAndValue = pair.Split(keyDelimiters);
                if (keyAndValue.Length > 1)
                {
                    postVariables.Add(HttpUtility.UrlDecode(keyAndValue[0]),
                        HttpUtility.UrlDecode(keyAndValue[1]));
                }
            }
            return postVariables;
        }

        public static string dictionaryToPostString(Dictionary<string, string> postVariables)
        {
            string postString = "";
            foreach (KeyValuePair<string, string> pair in postVariables)
            {
                postString += HttpUtility.UrlEncode(pair.Key) + "=" +
                    HttpUtility.UrlEncode(pair.Value) + "&";
            }

            return postString;
        }

        public static string ConfigString(string key)
        {
            return Remote.Settings.Default.Service;
        }
    }
}
