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
        private List<string> exchangeTriggerMailAdresses;

        public string DeployDirectory { get; set; }
        public string EwsUri { get; set; }
        public string ExchangeUsername { get; set; }
        public string ExchangePassword { get; set; }
        public List<string> ExchangeTriggerMailAdresses { get; set; }

        public void CheckInitDirect()
        {
            if (!File.Exists(deployDirectory + "conf.xml")) ;
            {
                
                InitCL();

                Directory.CreateDirectory(deployDirectory);
                new XDocument(new XElement("root", new XElement("link", ""))).Save(deployDirectory + "conf.xml");
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
            Console.WriteLine("***Accountdetails***\n\n");
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

            bool validChoice = false;
            Console.WriteLine("\n\n***Actions***\n\n");
            Console.WriteLine("What would you like to happen? Type the number and enter to continue.\n 1. Trigger robot upon new email \n");
            string choice = Console.ReadLine();
            do {
                switch (choice)
                {
                    case "1":
                        validChoice = true;
                        InitExchangeTrigger();
                        break;

                    default:
                        Console.WriteLine("Please select a valid choice.");
                        break;
                }
            }while (!validChoice);
        }
        
        public void InitExchangeTrigger()
        {
            bool validChoice = false;
            Console.WriteLine("\n\n***Mailtrigger***\n\n");
            Console.WriteLine("Would you like to specify mailadresses?\n 1. Yes \n 2. No\n");
            string choice = Console.ReadLine();
            do {
                switch (choice)
                {
                    case "1":
                        validChoice = true;
                        Console.WriteLine("Specify the mail adresses below by typing the mail and pressing enter. Type done to continue.\n");
                        while(true)
                        {
                            string input = Console.ReadLine().ToString();
                            if (input.ToLower().Equals("done"))
                                break;
                        } 
                        break;
                    case "2":
                        validChoice = true;

                        break;
                    default:
                        Console.WriteLine("Please select a valid choice.");
                        break;
                }
            } while (!validChoice);
        }
    }
}
