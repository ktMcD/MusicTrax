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
        string tag4 = "Today's hits; Timeless favorites. Sunny 107;";

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
            int iHour;

            // To read a text file line by line 
            if (File.Exists(inFile))
            {
                // Store each line in array of strings 
                string[] lines = File.ReadAllLines(inFile);
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] fields = lines[i].Split(',');
                    iHour = GetHour(lines[i]);
                    for (int j = 0; j < fields.Count(); j++)
                    {
                        fields[j] = fields[j].Replace('"', ' ');
                    }
                    if (!lines[i].Contains("SWEEPER") &&
                       (iHour >= 6 && iHour < 18))
                    {
                        if (fields[1] == "*")
                        {
                            trackWriter.WriteLine($"");
                            trackWriter.WriteLine($"***************** {fields[0]} HOUR *****************");
                        }
                        if (lines[i].Contains("VOICETRACK") ||
                             lines[i].Contains("Voice Track"))
                        {
                            whichTag = SelectTag(rando.Next(7, 2501));
                            trackWriter.WriteLine($"");
                            trackWriter.WriteLine($"{fields[3]}");
                            trackWriter.WriteLine($"----------------");
                            trackWriter.WriteLine(whichTag);
                            trackWriter.WriteLine();

                        }
                        else if (lines[i].Contains("STOPSET"))
                        {
                            trackWriter.WriteLine($"STOPSET");
                        }
                        else
                        {
                            if (!lines[i].Contains("Legal ID"))
                            {
                                trackWriter.WriteLine($"{fields[0]} {fields[3]} {fields[4]} {fields[5]} {fields[6]} {fields[7]}");
                            }
                        }
                    }

                }
            }
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
        } // Process Text
        private string SelectTag(int randomTag)
        {

            bool lIsPrime = IsPrime(randomTag);
            if (lIsPrime)
            {
                return tag4;
            }
            else if (randomTag % 2 == 0)
            {
                return tag2;
            }
            else if (randomTag % 3 == 0)
            {
                return tag3;
            }
            return tag1;
        } // Select Tag

        private bool IsPrime(int whatNumber)
        {
            for (int i = 8; i < whatNumber; i++)
                if (whatNumber % i == 0) return false;
            return true;
        } // IsPrime

        private int GetHour(string line)
        {
            int hour = 0;
            string subst = "";
            if (line != null &&
                line.Length > 0)
            {
                subst = line.Substring(0, 2);
            }

            if (int.TryParse(subst, out hour))
            {
                return hour;
            }
            return 0;
        } // GetHour
    } // class
} // namespace
