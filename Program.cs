using System;
using System.Collections;
using System.IO;

namespace SleepData
{
    class Program
    {

        static void Main(string[] args)
        {
            // ask for input
            Console.WriteLine("Enter 1 to create data file.");
            Console.WriteLine("Enter 2 to parse data.");
            Console.WriteLine("Enter anything else to quit.");
            // input response
            string resp = Console.ReadLine();

            if (resp == "1")
            {
                // create data file

                 // ask a question
                Console.WriteLine("How many weeks of data is needed?");
                // input the response (convert to int)
                int weeks = int.Parse(Console.ReadLine());

                 // determine start and end date
                DateTime today = DateTime.Now;
                // we want full weeks sunday - saturday
                DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
                // subtract # of weeks from endDate to get startDate
                DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));
                
                // random number generator
                Random rnd = new Random();

                // create file
                StreamWriter sw = new StreamWriter("data.txt");
                // loop for the desired # of weeks
                while (dataDate < dataEndDate)
                {
                    // 7 days in a week
                    int[] hours = new int[7];
                    for (int i = 0; i < hours.Length; i++)
                    {
                        // generate random number of hours slept between 4-12 (inclusive)
                        hours[i] = rnd.Next(4, 13);
                    }
                    // M/d/yyyy,#|#|#|#|#|#|#
                    //Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
                    sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");
                    // add 1 week to date
                    dataDate = dataDate.AddDays(7);
                }
                sw.Close();
            }
            else if (resp == "2")
            {
                StreamReader sr = new StreamReader("data.txt");
                while(!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    //M/d/yyyy,#|#|#|#|#|#|#
                    
                    //quick rip date out and hours into an array[date,[hours]]
                    string[] lineItems = line.Split(',');
                    
                    //cast date string to DateTime to help w/ output formatting
                    DateTime lineDate = DateTime.Parse(lineItems[0]);
                    
                    //grab hours and toss in an array
                    string[] lineHours = lineItems[1].Split('|');
                    int[] hours = new int[7];
                    
                    //iterate through array of string ints, add to total and save it, calc average and format it
                    int totalHrs = 0;
                    double avgHrs = 0;
                    for(int i = 0; i < hours.Length; i++)
                    {
                        totalHrs+=short.Parse(lineHours[i]);
                    }
                    avgHrs = totalHrs / 7;
                    string avg = $"{avgHrs:0.0}";

                    //output the week
                    Console.WriteLine($"Week of {lineDate:MMM}, {lineDate:dd}, {lineDate:yyyy}");
                    Console.WriteLine($"{"Su",3}{"Mo",3}{"Tu",3}{"We",3}{"Th",3}{"Fr",3}{"Sa",3}{"Tot",4}{"Avg",4}");
                    Console.WriteLine($"{"--",3}{"--",3}{"--",3}{"--",3}{"--",3}{"--",3}{"--",3}{"---",4}{"---",4}");                    
                    Console.WriteLine($"{lineHours[0],3}{lineHours[1],3}{lineHours[2],3}{lineHours[3],3}{lineHours[4],3}{lineHours[5],3}{lineHours[6],3}{totalHrs,4}{avg,4}"+"\r\n");
                }
                sr.Close();
            }
        }
    }
}
