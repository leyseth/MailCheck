﻿using System;
using System.Threading.Tasks;

namespace MailCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" _     _       _____ \n| |   (_)     |  _  |\n| |    _ _ __ | | | |\n| |   | | '_ \\| | | |\n| |___| | | | \\ \\/' /\n\\_____/_|_| |_|\\_/\\_\\\n");
            //Init();
            //LaunchBot();

            Exchange e = new Exchange();
            e.getEmails();
        }

        static void Init()
        {
            Startup s = new Startup();
            s.InitCL();

        }

        static void LaunchBot()
        {
            string mainUrl = "http://cbkonw-dyn-aa.iconos.be/";
            string apiKey = "3*gd>BW;9DJp,3wBCbNRil]jwPzwc.FBU1y<6mZ3";

            try
            {
                AutomationAnywhereAPI aaapi = new AutomationAnywhereAPI();
                aaapi.Authorize(mainUrl, apiKey);
                aaapi.getFileList(mainUrl);
                aaapi.deployBot(mainUrl, 12, 5);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
