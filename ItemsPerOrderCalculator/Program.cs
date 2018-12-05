using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemsPerOrderCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the full path to your text file of transaction ids. Example: C:\\Users\\bob\\Desktop\\domains.txt");
            Console.WriteLine("One id per line, like this:");
            Console.WriteLine("fdgh45yjdt");
            Console.WriteLine("zdgfhe4567");
            Console.WriteLine("Please do not close the program until you see: Done!");
            string userInput = Console.ReadLine();
            string[] listOfIds = File.ReadAllLines(userInput);
            Console.WriteLine(listOfIds.Length);
            var csv = new StringBuilder();
            var firstLine = string.Format("Trans ID,Items");
            csv.AppendLine(firstLine);
            var IdInstances = new ConcurrentDictionary<string, int>();

            foreach (var line in listOfIds)
            {
                IdInstances.AddOrUpdate(line, 1, (key, value) => value + 1);
            }

            foreach (var trans in IdInstances)
            {
                var newLine = string.Format("{0},{1}", trans.Key, trans.Value);
                csv.AppendLine(newLine);
            }

            File.WriteAllText("ssl-checker-output.csv", csv.ToString());

        }
    }
}
