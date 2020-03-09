using System;
using System.Threading.Tasks;

namespace MailCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        static async Task MainAsync()
        {
            try
            {
                AutomationAnywhereAPI aaapi = new AutomationAnywhereAPI();
                //aaapi.getRequest("http://www.google.com");
                aaapi.Authorize("http://cbkonw-dyn-aa.iconos.be/v1/authentication");
                //aaapi.postRequest("http://ptsv2.com/t/s7oqt-1583748337/post");
                Console.WriteLine(aaapi.authbody);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
