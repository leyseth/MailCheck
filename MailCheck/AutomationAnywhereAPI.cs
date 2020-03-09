using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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

        public async void Authorize(string url)
        {
            
            
        }
    }
}
