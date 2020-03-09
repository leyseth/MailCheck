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
        private string responseb;

        private string authb;

        public string authbody
        {
            get { return authb; }
            set { authb = value; }
        }

        public string responsebody {
            get { return responseb; }
            set { responseb = value; } 
        }


        public async void getRequest(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    using (HttpContent content = response.Content)
                    {
                        responseb = await content.ReadAsStringAsync();
                    }
                }                
            }
        }


        public async void postRequest(string url)
        {
            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<String, String>>()
            {
                new KeyValuePair<string, string>("test", "testest"),
            };
            HttpContent postcontent = new FormUrlEncodedContent(queries);
            var response = client.PostAsync(url, postcontent).Result;
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
        }

        public void Authorize(string url)
        {
            String querystring = "{\"username': 'string', 'password': 'string', 'apiKey': 'string', 'mfaCode': 0}";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"username\":\"aa_leyseth\",\"password\": \"gismo2340\", \"apiKey\": \"eu,Tkrd&+)/&UWcMava5V=DVh?6(!08^/w7WAamf\", \"mfaCode\": 0}";
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                JObject json = JObject.Parse(streamReader.ReadToEnd());
                string authKey = (string)json["token"];
                Console.WriteLine(authKey);
            }

        }
    }
}
