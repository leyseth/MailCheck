using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.WebServices.Data;

namespace MailCheck
{
    class Exchange
    {
        Uri V = new Uri("https://outlook.office365.com/EWS/Exchange.asmx");

        private List<Email> mails = new List<Email>();


        public void getEmails()
        {
            ExchangeService exchange = new ExchangeService(ExchangeVersion.Exchange2007_SP1);
            exchange.Credentials = new WebCredentials("supportdynatos@cronos.be", "-Dynat0s-");
            exchange.Url = V;
            bool hit = false;

            if (exchange != null)
            {
                FindItemsResults<Item> result = exchange.FindItems(WellKnownFolderName.Inbox, new ItemView(100));

                foreach(Item item in result)
                {
                    EmailMessage message = EmailMessage.Bind(exchange, item.Id);
                    try
                    {
                        if(message.Sender.Address.ToString() == "support@aveve.be" && message.IsRead == false)
                        {
                            Console.WriteLine(message.Subject.ToString());
                            Console.WriteLine(message.Sender.Address.ToString());
                            hit = true;
                            break;
                        }  
                    }
                    catch (System.NullReferenceException)
                    {
                        Console.WriteLine("This message does not containt a subject");
                    }
                }

                if (hit)
                {
                    Console.WriteLine("\n\n\nBot can be launched");
                }
            }

        }

    }
}
