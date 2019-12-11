using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventManager.Utility
{
    public class Logger
    {
        public static string lockingObject = "";
        /// <summary>
        /// Logger to log the exceptions that occur during the application runtime
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="showError"></param>
        public static void LogException(Exception ex, bool showError)
        {
           
            String workingDir = Directory.GetCurrentDirectory();
            string filePath = workingDir + @"\" + DateTime.Now.ToString("MM-dd-yyyy-h-mm-tt") + ex.GetType().Name + ".txt";

            lock (lockingObject)
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine("Date : " + DateTime.Now.ToString());
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine();

                    while (ex != null)
                    {
                        writer.WriteLine("----------------------------------Name-------------------------------------------");
                        writer.WriteLine(ex.GetType().FullName);
                        writer.WriteLine("-----------------------------------------------------------------------------");
                        writer.WriteLine();
                        writer.WriteLine("----------------------------------Message-------------------------------------------");
                        writer.WriteLine("Message : " + ex.Message);
                        writer.WriteLine("-----------------------------------------------------------------------------");
                        writer.WriteLine();
                        writer.WriteLine("----------------------------------Cause-------------------------------------------");
                        writer.WriteLine("StackTrace : " + ex.StackTrace);
                        writer.WriteLine("-----------------------------------------------------------------------------");
                        writer.WriteLine();
                        writer.WriteLine("----------------------------------Source-------------------------------------------");
                        writer.WriteLine("StackTrace : " + ex.Source);
                        writer.WriteLine("-----------------------------------------------------------------------------");
                        writer.WriteLine();

                        ex = ex.InnerException;
                    }
                }
            }
            if (showError)
            {
                MessageBox.Show("Something went wrong.");
            }
        }
    }

}
