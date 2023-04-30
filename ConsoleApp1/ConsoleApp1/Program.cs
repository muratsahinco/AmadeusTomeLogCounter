using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
        public static string RegexSeparator = @"\?.+?@@\?\?T[1-3]";

        public static string filePath = @"C:\log_1.txt";

        public static List<string> req = new List<string>();
        public static List<string> res = new List<string>();
        public static List<string> resError = new List<string>();

        static void Main(string[] args)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    int count = 0;

                    while ((line = sr.ReadLine()) != "SONS")
                    {
                        if (!line.Contains("rr : info"))
                        {
                            count++;

                            if (line.Contains(": Error")) { resError.Add(line); }
                            else if (line.Contains("rs: --Big Data"))
                            {
                                BigData(line, "rs");
                            }
                            else if (line.Contains("rq: --Big Data"))
                            {
                                BigData(line, "rq");
                            }
                            else if (line.Contains(": rq -")) { req.Add(line); }
                            else if (line.Contains(": rs -")) { res.Add(line); }
                        }
                    }
                    Console.WriteLine($"total log row count : {count}");
                    Console.WriteLine($"req : {req.Count + 7}");
                    Console.WriteLine($"res : {res.Count}");

                    Console.WriteLine($"static error : 7");

                    Console.WriteLine($"Total Error : {req.Count - res.Count + resError.Count}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("-------------------------------");
                Console.WriteLine("-------------------------------");
                Console.WriteLine("-------------------------------");
                Console.WriteLine("Hata: " + e.Message);
            }

            Console.ReadLine();
        }

        private static void BigData(string line, string Type)
        {
            var matchFlat = 0;
            var splitListToList = line.Split(new string[] { "??@@??" }, StringSplitOptions.None).ToList();
            //var splitListToList = Regex.Split(line, RegexSeparator).Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            var title = splitListToList.First();
            if (title.Contains("2023-04-28")) { splitListToList.Remove(splitListToList.First()); }
            var splitList = splitListToList.ToArray();
            for (int i = 0; i < splitList.Length; i++)
            {
                var row = splitList[i];
                if (Type == "rq")
                {
                    req.Add(row);
                }
                else if (Type == "rs")
                {
                    res.Add(row);
                }
                else
                {
                    Console.Write("error");
                }
            }
        }
    }
}