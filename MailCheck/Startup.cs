using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace MailCheck
{
    class Startup
    {
        private static readonly string username = Environment.UserName;

        private string deployDirectory = @"C:\Users\" + username + @"\Documents\LinQ";
        
        private bool exchangeEnable = false;
        private bool exchangeSpecificEmailTrigger = false;
        private bool exchangeSaveMail = false;

        private List<string> exchangeTriggerMailAdresses = new List<string>();
        private string exchangeSavePath;
        private string ewsUri;
        private string exchangeUsername;
        private string exchangePassword;

        private string controlURL;
        private string apiKey;
        private int botID;
        private int machineID;


        public string DeployDirectory { get { return deployDirectory; } set { deployDirectory = value; } }
        public string EwsUri { get { return ewsUri; } set { ewsUri = value; } }
        public string ExchangeUsername { get {return exchangeUsername; } set {exchangeUsername = value; } }
        public string ExchangePassword { get { return exchangePassword; } set {exchangePassword = value; } }
        public List<string> ExchangeTriggerMailAdresses { get { return exchangeTriggerMailAdresses; } set {exchangeTriggerMailAdresses = value; } }
        public string ExchangeSavePath { get {return exchangeSavePath; } set {exchangeSavePath = value; } }
        public bool ExchangeEnable { get {return exchangeEnable; } set {exchangeEnable = value; } }
        public bool ExchangeSpecificEmailTrigger { get {return exchangeSpecificEmailTrigger; } set {exchangeSpecificEmailTrigger = value; } }
        public bool ExchangeSaveMail { get { return exchangeSaveMail; } set {exchangeSaveMail = value; } }
        public string ControlURL { get { return controlURL; } set { controlURL = value; } }
        public string ApiKey { get { return apiKey; } set { apiKey = value; } }
        public int BotID { get { return botID; } set { botID = value; } }
        public int MachineID { get { return machineID;  } set { machineID = value; } }


        public void CheckInitDirect()
        {
            if (!File.Exists(deployDirectory + @"\conf.xml"))
            {
                InitCL();

                Directory.CreateDirectory(deployDirectory);
                new XDocument(
                    new XElement("root",
                        new XElement("link",
                            new XElement("exchangeEnable", exchangeEnable.ToString())
                        ),
                        new XElement("exchange",
                            new XElement("exchangeUsername", exchangeUsername),
                            new XElement("exchangePassword", exchangePassword),
                            new XElement("ewsUri", ewsUri),
                            new XElement("exchangeSpecificMailTrigger", exchangeSpecificEmailTrigger),
                            new XElement("exchangeTriggerEmailAdresses",
                                from email in exchangeTriggerMailAdresses
                                select new XElement("exchangeMailList", email)
                            ),
                            new XElement("exchangeSaveMail", exchangeSaveMail),
                            new XElement("ExchangeSavePath", exchangeSavePath)
                        ),
                        new XElement("AutomationAnywhereAPI",
                            new XElement("controlURL", controlURL),
                            new XElement("apiKey", apiKey),
                            new XElement("botID", botID),
                            new XElement("machineID", machineID)
                        )
                    )
                ).Save(deployDirectory + @"\conf.xml");
            }
            else
            {
                Console.WriteLine("Linq already configured, if you wish to change this configuration delete the conf.xml!\nExecuting...");
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
                        exchangeEnable = true;
                        InitExchange();
                        break;

                    default:
                        Console.WriteLine("Please select a valid choice.");
                        break;
                }
            } while (!validChoice);

            Console.WriteLine("\n\n\n\n\n\n===============================================\nAll done! Please schedule this application.\n===============================================\n\n");
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
            
            do {
                string choice = Console.ReadLine();
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
            string input = "";
            

            Console.WriteLine("\n\n***Mailtrigger***\n\n");
            Console.WriteLine("Would you like to specify mailadresses?\n 1. Yes \n 2. No\n");
            
            do {
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        validChoice = true;
                        exchangeSpecificEmailTrigger = true;
                        Console.WriteLine("\nSpecify the mail adresses below by typing the mail and pressing enter. Type done to continue.\n");
                        while(true)
                        {
                            try
                            {
                                input = Console.ReadLine().ToString().ToLower();
                            }
                            catch(Exception E)
                            {
                                Console.WriteLine(E.ToString() + "\n\n");
                                input = "";
                            }
                            
                            if (input.Equals("done"))
                                break;
                            else if(!input.Equals("") && input != null)
                                exchangeTriggerMailAdresses.Add(input);
                            else
                                Console.WriteLine("Please provide proper input\n");
                        } 
                        break;
                    case "2":
                        validChoice = true;

                        break;
                    default:
                        Console.WriteLine("Please select a valid choice.\n");
                        break;
                }
            } while (!validChoice);

            InitExchangeDownloadDirect();

        }

        public void InitExchangeDownloadDirect()
        {
            bool validChoice = false;

            Console.WriteLine("\n\n***Storage***\n\n");
            Console.WriteLine("Would you like to save the mails and attachments of these triggers?\n 1. Yes \n 2. No \n");

            do {
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        validChoice = true;
                        exchangeSaveMail = true;
                        Console.WriteLine("\nSpecify the location. \n");
                        exchangeSavePath = Console.ReadLine();
                        break;
                    case "2":
                        validChoice = true;
                        break;
                    default:
                        Console.WriteLine("\nPlease select a valid choice.");
                        break;
                }
            } while (!validChoice);


        }

        public void ConfigReadout()
        {
             

            XmlDocument doc = new XmlDocument();
            doc.Load(deployDirectory + @"\conf.xml");

            XmlNode nExchangeEnable = doc.DocumentElement.SelectSingleNode("/root/link/exchangeEnable");
            exchangeEnable = bool.Parse(nExchangeEnable.InnerText);

            if (exchangeEnable)
            {
                XmlNode nExchangeUsername = doc.DocumentElement.SelectSingleNode("/root/exchange/exchangeUsername");
                exchangeUsername = nExchangeUsername.InnerText;

                XmlNode nExchangePassword = doc.DocumentElement.SelectSingleNode("/root/exchange/exchangePassword");
                exchangePassword = nExchangePassword.InnerText;

                XmlNode nEwsUri = doc.DocumentElement.SelectSingleNode("/root/exchange/ewsUri");
                ewsUri = nEwsUri.InnerText;

                XmlNode nExchangeSpecificMailTrigger = doc.DocumentElement.SelectSingleNode("/root/exchange/exchangeSpecificMailTrigger");
                exchangeSpecificEmailTrigger = bool.Parse(nExchangeSpecificMailTrigger.InnerText);

                if (exchangeSpecificEmailTrigger)
                {
                    foreach (XmlNode node in doc.DocumentElement.SelectSingleNode("/root/exchange/exchangeTriggerEmailAdresses").ChildNodes)
                    {
                        string text = node.InnerText;
                        exchangeTriggerMailAdresses.Add(text);
                    }
                }

                XmlNode nExchangeSaveMail = doc.DocumentElement.SelectSingleNode("/root/exchange/exchangeSaveMail");
                exchangeSaveMail = bool.Parse(nExchangeSaveMail.InnerText);

                if (exchangeSaveMail)   
                {
                    XmlNode nExchangeSavePath = doc.DocumentElement.SelectSingleNode("/root/exchange/ExchangeSavePath");
                    exchangeSavePath = nExchangeSavePath.InnerText;
                }
            }
        }
    }
}
