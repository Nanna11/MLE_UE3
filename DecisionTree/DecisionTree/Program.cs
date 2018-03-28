using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionTree
{
    class Program
    {
        static void Main(string[] args)
        {
            Tree t = new Tree("SMSSpamCollection");

            Instance i = new Instance("AAAA XXX Free 0890", Result.notdefined);
            Console.WriteLine(t.Classify(i));
            t.Test();
            Console.ReadKey();
        }
    }
}
