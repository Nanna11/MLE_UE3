using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionTree
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Tree t = new Tree("SMSSpamCollection");

                Instance i = new Instance("068790", Result.notdefined);
                Console.WriteLine(t.Classify(i));
                t.Test(10);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Please take a look at the filename in the Tree constructor.");
            }

            Console.WriteLine("Press any key to quit.");
            Console.ReadKey();
        }
    }
}
