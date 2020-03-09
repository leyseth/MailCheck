using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace MailCheck
{
    class AutomationAnywhereAPI
    {
        HttpClient client = new HttpClient();
        
        private string responsebody;
        private string authbody;
        private string authkey;
        private string fileList;


        public string Authbody
        {
            get { return authbody; }
            set { authbody = value; }
        }

        public string Responsebody {
            get { return responsebody; }
            set { responsebody = value; } 
        }

        public string Authkey
        {
            get { return authkey; }
            set { authkey = value; }
        }


        //public async void getRequest(string url)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        using (HttpResponseMessage response = await client.GetAsync(url))
        //        {
        //            using (HttpContent content = response.Content)
        //            {
        //                responseb = await content.ReadAsStringAsync();
        //            }
        //        }                
        //    }
        //}
        //
        //
        //public async void postRequest(string url)
        //{
        //    IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<String, String>>()
        //    {
        //        new KeyValuePair<string, string>("test", "testest"),
        //    };
        //    HttpContent postcontent = new FormUrlEncodedContent(queries);
        //    var response = client.PostAsync(url, postcontent).Result;
        //    if (response.StatusCode == HttpStatusCode.InternalServerError)
        //    {
        //        Console.WriteLine(response.Content.ReadAsStringAsync().Result);
        //    }
        //    Console.WriteLine(response.Content.ReadAsStringAsync().Result);
        //}

        public void Authorize(string url, string APIkey)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "v1/authentication");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"username\":\"aa_api\",\"password\": \"WhyRUTheWayUR\", \"apiKey\": \"" + APIkey + "\", \"mfaCode\": 0}";
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                JObject json = JObject.Parse(streamReader.ReadToEnd());
                authkey = (string)json["token"];
                
            }
            
        }

        public void getFileList(string url)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "v2/repository/file/list");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("X-Authorization",authkey);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"filter\": {\"operator\": \"substring\",\"field\": \"path\",\"value\": \"SDDYN\"}}";
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                Console.WriteLine(streamReader.ReadToEnd());

            }
        }

        public void deployBot(string url, int ID, int device)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "v2/automations/deploy");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("X-Authorization",authkey);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"fileId\" :\"" + ID + "\", \"deviceIds\":[\"" + device + "\"]}";
                Console.WriteLine(url + "/v2/automations/deploy");
                Console.WriteLine(json);
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                Console.WriteLine(streamReader.ReadToEnd());

            }
        }
    }
}
