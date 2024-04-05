using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using static System.Reflection.Metadata.BlobBuilder;


namespace MusicTrax
{

    internal class Program
    {
        string filePath = @"C:\scripts\";
        string inFile = "";
        string script = "";
        DateTime logDate = DateTime.Now;
        List<string> Tracks = new List<string>();
        string tag2 = "The City of Sunshine's official radio station: Sunny 107; ";
        string tag1 = "Today's hits. Yesterday's favorites. Sunny 107; ";
        string tag3 = "Sunny 107: the Basin's soft rock station; ";

        [STAThread]
        private static void Main(string[] args)
        {
            Program VoiceTrack = new Program();
            VoiceTrack.RunMe();
        }

        private void RunMe()
        {
            bool keepTrying = true;
            string userResponse = "";

            while (keepTrying)
            {
                keepTrying = false;
                userResponse = GetLogDate();
                if (!DateTime.TryParse(userResponse, out logDate) ||
                    userResponse == "" ||
                    userResponse == null)
                {
                    keepTrying = true;
                    _ = GetLogDate();
                }
                else
                {
                    keepTrying = false;
                    inFile = filePath + logDate.ToString("yyMMdd") + ".ASC";
                    script = filePath + "__Script_" + logDate.ToString("M-d-yy") + ".txt";
                }

            }

            ProcessText();
        }

        private string GetLogDate()
        {
            Console.WriteLine("Log date = ");
            return Console.ReadLine()!;
        }

        public void ProcessText()
        {
            Random rando = new Random();
            StreamWriter trackWriter = new StreamWriter(script, false);
            trackWriter.WriteLine($"KKRB Script for {logDate.ToString("ddd")}, {logDate.ToString("M-d-yy")}");
            string whichTag = "";

            // To read a text file line by line 
            if (File.Exists(inFile))
            {
                // Store each line in array of strings 
                string[] lines = File.ReadAllLines(inFile);
                foreach (string line in lines)
                {
                    string[] fields = line.Split(',');
                    for (int i = 0; i < fields.Count(); i++)
                    {
                        fields[i] = fields[i].Replace('"', ' ');
                    }
                    if (!line.Contains("SWEEPER") &&
                       (line.StartsWith("06:") ||
                        line.StartsWith("07:") ||
                        line.StartsWith("08:") ||
                        line.StartsWith("09:") ||
                        line.StartsWith("10:") ||
                        line.StartsWith("11:") ||
                        line.StartsWith("12:") ||
                        line.StartsWith("13:") ||
                        line.StartsWith("14:") ||
                        line.StartsWith("15:") ||
                        line.StartsWith("16:") ||
                        line.StartsWith("17:")))
                    {
                        if (fields[1] == "*")
                        {
                            trackWriter.WriteLine($"");
                            trackWriter.WriteLine($"***************** {fields[0]} HOUR *****************");
                        }
                        else if (line.Contains("JV06") ||
                                 line.Contains("JV07") ||
                                 line.Contains("JV08") ||
                                 line.Contains("JV09") ||
                                 line.Contains("JV10") ||
                                 line.Contains("JV11") ||
                                 line.Contains("JV12") ||
                                 line.Contains("JV13") ||
                                 line.Contains("JV14") ||
                                 line.Contains("JV15") ||
                                 line.Contains("JV16") ||
                                 line.Contains("JV17") ||
                                 line.Contains("JN06") ||
                                 line.Contains("JN07") ||
                                 line.Contains("JN08") ||
                                 line.Contains("JN09") ||
                                 line.Contains("JN10") ||
                                 line.Contains("JN11") ||
                                 line.Contains("JN12") ||
                                 line.Contains("JN13") ||
                                 line.Contains("JN14") ||
                                 line.Contains("JN15") ||
                                 line.Contains("JN16") ||
                                 line.Contains("JN17") ||
                                 line.Contains("JNTIME"))
                        {
                            trackWriter.WriteLine($"");
                            trackWriter.WriteLine($"{fields[3]}");
                            if (!line.Contains("JV0620") &&
                                !line.Contains("JV0720") &&
                                !line.Contains("JV0820") &&
                                !line.Contains("JV0920") &&
                                !line.Contains("JV1020") &&
                                !line.Contains("JV1120") &&
                                !line.Contains("JV1220") &&
                                !line.Contains("JV1320") &&
                                !line.Contains("JV1420") &&
                                !line.Contains("JV1520") &&
                                !line.Contains("JV1620") &&
                                !line.Contains("JV1720"))
                            {
                                int randoNum = rando.Next();
                                if (randoNum % 3 == 0)
                                {
                                    // "Today's hits. Yesterday's favorites. Sunny 107; "
                                    whichTag = tag1;
                                }
                                else if (randoNum % 2 == 0)
                                {
                                    // "The City of Sunshine's official radio station: Sunny 107; "
                                    whichTag = tag2;
                                }
                                else
                                {
                                    // "Sunny 107: the Basin's soft rock station; "
                                    whichTag = tag3;
                                }
                                trackWriter.WriteLine($"----------------");
                                trackWriter.WriteLine(whichTag);
                                trackWriter.WriteLine();
                            }
                            else if (line.Contains("JV0620") ||
                                     line.Contains("JV0720") ||
                                     line.Contains("JV0820") ||
                                     line.Contains("JV0920") ||
                                     line.Contains("JV1020") ||
                                     line.Contains("JV1120") ||
                                     line.Contains("JV1220") ||
                                     line.Contains("JV1320") ||
                                     line.Contains("JV1420") ||
                                     line.Contains("JV1520") ||
                                     line.Contains("JV1620") ||
                                     line.Contains("JV1720"))
                            {
                                trackWriter.WriteLine("------------");
                                trackWriter.WriteLine(":20 BREAK TAGLINE HERE ");
                                trackWriter.WriteLine();
                            }
                        }
                        else if (line.Contains("STOPSET"))
                        {
                            trackWriter.WriteLine($"STOPSET");
                        }
                        else
                        {
                            trackWriter.WriteLine($"{fields[0]} {fields[3]} {fields[4]} {fields[5]} {fields[6]} {fields[7]}");
                        }
                    }

                }
            }
            trackWriter.WriteLine();
            trackWriter.WriteLine("*******************************************************");
            trackWriter.WriteLine("**************KATIE: TURN ON YOUR FRIDGE!**************");
            trackWriter.Flush();
            trackWriter.Close();
            trackWriter.Dispose();
            // Console.ReadLine();

/*
            if (File.Exists(inFile))
            {
                File.Delete(inFile);
            }
*/
        }
    } // class
} // namespace
