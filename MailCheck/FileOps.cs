using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MailCheck
{
    class FileOps
    {
        public static void CreateTextFile(string path, string filename)
        {
            string fileName = path + "\\" + filename + ".txt";

            try
            {
                // Check if file already exists. If yes, delete it.     
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                // Create a new file     
                using (FileStream fs = File.Create(fileName));
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }


    }
}
