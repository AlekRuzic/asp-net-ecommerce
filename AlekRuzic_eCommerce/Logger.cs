using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace AlekRuzic_eCommerce
{
    public class Logger
    {
        public Logger()
        {

        }

        // ***********************************************************************************
        // public method serializes all updates of the application logfile for the current day
        public void LogMessage(string logFile, string logMessage)
        {
            lock (this)
            {
                string lPath = HttpContext.Current.Server.MapPath(".") + @"\logs\";

                //DateTime lStamp = DateTime.Now.ToUniversalTime();
                DateTime lStamp = DateTime.Now;
                string lStampString = lStamp.ToString();
                string fName = String.Format("{0:D2}", lStamp.Year) + String.Format("{0:D2}", lStamp.Month) + String.Format("{0:D2}", lStamp.Day) + ".txt";

                if (logFile == "MessageStream" || logFile == "NewLine")
                    fName = "O" + fName;
                else if (logFile == "EventStream")
                    fName = "E" + fName;
                else if (logFile == "FeedStream")
                    fName = "F" + fName;
                else
                    fName = "I" + fName;

                lPath += fName;
                //lStampString += " " + String.Format("{0:D2}", lStamp.Hour) + ":" + String.Format("{0:D2}", lStamp.Minute) + ":" + String.Format("{0:D2}", lStamp.Second);

                try
                {
                    StreamWriter output = new StreamWriter(lPath, true);

                    if (logFile == "NewLine")
                        output.WriteLine();
                    else
                        output.WriteLine(lStampString + " " + logMessage);

                    output.Close();
                }
                catch (Exception ex)
                {
                    lPath = HttpContext.Current.Server.MapPath(".") + @"\logs\metalog.txt";
                    StreamWriter outputx = new StreamWriter(lPath, true);
                    outputx.WriteLine(lStampString + " " + ex.Message);
                    outputx.Close();
                }
            }
        }
    }
}