using System;
using System.Threading.Tasks;

namespace MailCheck
{
    class Program
    {

        

        static void Main(string[] args)
        {
            Init();
            Launch();
        }

        static void Init()
        {
            Startup s = new Startup();
            s.CheckInitDirect();
        }


        static void Launch()
        {
            Startup s = new Startup();
            s.ConfigReadout();

            if (s.ExchangeEnable)
            {
                Exchange e = new Exchange();
                e.getEmailsAndSave(s.EwsUri, s.ExchangeUsername, s.ExchangePassword, s.ExchangeTriggerMailAdresses, s.ExchangeSavePath);
                if (e.MailHit)
                {
                    LaunchBot(5,12);
                }
            }
        }


        static void LaunchBot(int botId, int machineId)
        {
            string mainUrl = "http://cbkonw-dyn-aa.iconos.be/";
            string apiKey = "3*gd>BW;9DJp,3wBCbNRil]jwPzwc.FBU1y<6mZ3";

            try
            {
                AutomationAnywhereAPI aaapi = new AutomationAnywhereAPI();
                aaapi.Authorize(mainUrl, apiKey);
                aaapi.getFileList(mainUrl);
                aaapi.deployBot(mainUrl, machineId, botId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
