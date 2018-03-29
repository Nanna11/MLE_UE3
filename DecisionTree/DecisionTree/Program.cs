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

            Instance i = new Instance("068790", Result.notdefined);
            Console.WriteLine(t.Classify(i));
            t.Test(10);
            Console.ReadKey();
        }
    }
}
