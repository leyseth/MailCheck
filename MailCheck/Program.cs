using System;
using System.Threading.Tasks;

namespace MailCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            //Init();
            //Launch();
            Exchange e = new Exchange();
            e.moveEmail();
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
                    LaunchBot(s.BotID,s.MachineID, s.ControlURL, "3*gd>BW;9DJp,3wBCbNRil]jwPzwc.FBU1y<6mZ3");
                    //LaunchBot(5,12, "http://cbkonw-dyn-aa.iconos.be/", "3*gd>BW;9DJp,3wBCbNRil]jwPzwc.FBU1y<6mZ3");
                }
            }
        }


        static void LaunchBot(int botId, int machineId, string apiKey, string mainUrl)
        {
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
