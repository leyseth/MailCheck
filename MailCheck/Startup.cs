using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace MailCheck
{
    class Startup
    {
        private string deployDirectory;
        private string ewsUri;
        private string exchangeUsername;
        private string exchangePassword;

        public string DeployDirectory { get; set; }
        public string EwsUri { get; set; }
        public string ExchangeUsername { get; set; }
        public string ExchangePassword { get; set; }


        public void CheckInitDirect()
        {
            if (!Directory.Exists(deployDirectory))
            {
                Directory.CreateDirectory(deployDirectory);
                new XDocument(new XElement("root",new XElement("link", ""))).Save(DeployDirectory + "conf.xml");

                InitCL();
            }
        }

        public void InitCL()
        {
            bool validChoice = false;            
            Console.WriteLine(" _     _       _____ \n| |   (_)     |  _  |\n| |    _ _ __ | | | |\n| |   | | '_ \\| | | |\n| |___| | | | \\ \\/' /\n\\_____/_|_| |_|\\_/\\_\\\n");
            do
            {
                Console.WriteLine("What would you like to link to? Type the number and enter to continue.\n 1. Outlook \n");
                string choice = Console.ReadLine();
                Console.WriteLine("\n\n");
                switch (choice)
                {
                    case "1":
                        validChoice = true;
                        InitExchange();
                        break;

                    default:
                        Console.WriteLine("Please select a valid choice.");
                        break;
                }
            } while (!validChoice);
        }

        public void InitExchange()
        {
            Console.WriteLine("===============================================\n                    Outlook                   \n===============================================\n\n");

            Console.Write("EWS URI: ");
            ewsUri = Console.ReadLine();

            Console.Write("Mailadress: ");
            exchangeUsername = Console.ReadLine();

            Console.Write("Password: ");
            exchangePassword = null;
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                    break;
                exchangePassword += key.KeyChar;
            }
        }
    }
}
