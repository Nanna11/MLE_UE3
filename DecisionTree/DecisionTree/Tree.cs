using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DecisionTree
{
    class Tree
    {
        Node node;
        List<Instance> instances;

        public Tree(string filename)
        {
            string deploypath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string filepath = Path.Combine(deploypath, filename);
            FileStream file = new FileStream(filepath, FileMode.Open);
            StreamReader sr = new StreamReader(file);

            instances = new List<Instance>();
            string f;
            while ((f = sr.ReadLine()) != null)
            {
                string[] result = f.Split("\t".ToArray<char>());
                instances.Add(new Instance(result[1], (Result)System.Enum.Parse(typeof(Result), result[0])));
            }

            List<int> attributes = new List<int>();
            for(int i = 0; i < instances[0].Count; i++)
            {
                attributes.Add(i);
            }

            node = new Node(instances, attributes);
        }

        public Result? Classify(Instance i)
        {
            return node.Classify(i);
        }

        public void Test()
        {
            double sum = 0;
            double correct = 0;
            int[,] Confusion = new int[3, 3];

            foreach(Instance i in instances)
            {
                Result? res = Classify(i);
                Confusion[(int)i.Result, (int)res]++;
            }

            Console.WriteLine("\nConfusion Matrix:");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    BeautifulConfusionMatrix.AddBlankSpaces(Confusion, Confusion[i, j], j);
                    Console.Write("{0} ", Confusion[i, j]);
                    
                    //add to correct count if actual and calculated result were the same
                    if (i == j) correct += Confusion[i, j];
                    sum += Confusion[i, j];
                }
                Console.WriteLine("");
            }

            Console.WriteLine("");
            //calculate accuracy
            double acc = correct / sum;
            Console.WriteLine("Accuracy: {0}", acc);
        }
        
    }
}
