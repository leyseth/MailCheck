using System;
using System.Collections.Generic;
using System.IO;
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
            int i = 0;

            if (exchange != null)
            {
                FindItemsResults<Item> result = exchange.FindItems(WellKnownFolderName.Inbox, new ItemView(100));
                
                foreach(Item item in result)
                {
                    i++;
                    EmailMessage message = EmailMessage.Bind(exchange, item.Id);
                    try
                    {
                        if(message.Sender.Address.ToString() == "support@aveve.be" && message.IsRead == false)
                        {
                            Directory.CreateDirectory(@"C:\Users\leyseth\Desktop\INPUT\" + i);
                            //FileOps.CreateTextFile(@"C:\Users\leyseth\Desktop\INPUT\" + i , "Body");
                            //FileOps.CreateTextFile(@"C:\Users\leyseth\Desktop\INPUT\" + i, "Subject");

                            File.WriteAllText(@"C:\Users\leyseth\Desktop\INPUT\" + i + @"\Subject.txt", message.Subject.ToString());
                            File.WriteAllText(@"C:\Users\leyseth\Desktop\INPUT\" + i + @"\Body.txt", message.Body.Text.ToString());


                            foreach (Attachment attachment in message.Attachments)
                            {
                                if (attachment is FileAttachment)
                                {
                                    FileAttachment fileAttachment = attachment as FileAttachment;
                                    // Load the attachment into a file.
                                    // This call results in a GetAttachment call to EWS.
                                    fileAttachment.Load(@"C:\Users\leyseth\Desktop\INPUT\" + i + "\\" + fileAttachment.Name);

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

                if (hit)
                {
                    Console.WriteLine("\n\n\nBot can be launched");
                }
            }

        }

    }
}
