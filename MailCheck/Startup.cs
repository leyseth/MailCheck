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

        public string DeployDirectory { get; set; }


        public void CheckInitDirect()
        {
            if (!Directory.Exists(deployDirectory))
            {
                Directory.CreateDirectory(deployDirectory);
                new XDocument(new XElement("root",new XElement("link", ""))).Save("foo.xml");
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
                        break;

                    default:
                        Console.WriteLine("Please select a valid choice.");
                        break;
                }
            } while (!validChoice);
        }

    }
}
