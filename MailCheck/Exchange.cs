using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.WebServices.Data;

namespace MailCheck
{
    class Exchange
    {
        private Boolean mailHit = false;

        public Boolean MailHit { get { return mailHit; } set { mailHit = value; } }

        public void getEmailsAndSave(string uri, string username, string password, List<string> exchangeMailList, string savePath)
        {
            //Uri V = new Uri("https://outlook.office365.com/EWS/Exchange.asmx");
            
            ExchangeService exchange = new ExchangeService(ExchangeVersion.Exchange2007_SP1);
            Uri V = new Uri(uri);
            exchange.Url = V;
            exchange.Credentials = new WebCredentials(username, password);
            bool hit = false;
            int i = 0;

            if (exchange != null)
            {
                FindItemsResults<Item> result = exchange.FindItems(WellKnownFolderName.Inbox, new ItemView(1000));
                
                foreach(Item item in result)
                {
                   
                    EmailMessage message = EmailMessage.Bind(exchange, item.Id);
                    try
                    {
                        if(exchangeMailList.Contains(message.Sender.Address.ToString()) && message.IsRead == false)
                        {
                            i++;
                            Directory.CreateDirectory(savePath + "\\" + i);
                            File.WriteAllText(savePath + "\\" + i + @"\Subject.txt", message.Subject.ToString());
                            File.WriteAllText(savePath + "\\" + i + @"\Body.txt", message.Body.Text.ToString());


                            foreach (Attachment attachment in message.Attachments)
                            {
                                if (attachment is FileAttachment)
                                {
                                    FileAttachment fileAttachment = attachment as FileAttachment;
                                    // Load the attachment into a file.
                                    // This call results in a GetAttachment call to EWS.
                                    fileAttachment.Load(savePath + "\\" + i + "\\" + fileAttachment.Name);

                                    Console.WriteLine("File attachment name: " + fileAttachment.Name);
                                }
                                else // Attachment is an item attachment.
                                {
                                    ItemAttachment itemAttachment = attachment as ItemAttachment;
                                    // Load attachment into memory and write out the subject.
                                    // This does not save the file like it does with a file attachment.
                                    // This call results in a GetAttachment call to EWS.
                                    itemAttachment.Load();
                                    Console.WriteLine("Item attachment name: " + itemAttachment.Name);
                                }
                            }
                            hit = true;
                        }  
                    }
                    catch (System.NullReferenceException)
                    {
                        Console.WriteLine("This message does not containt a subject");
                    }
                }
            }

        }

    }
}
