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
            //read all instances from a file
            string deploypath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string filepath = Path.Combine(deploypath, filename);

            if (File.Exists(filepath))
            {
                FileStream file = new FileStream(filepath, FileMode.Open);
                StreamReader sr = new StreamReader(file);

                instances = new List<Instance>();
                string f;
                while ((f = sr.ReadLine()) != null)
                {
                    string[] result = f.Split("\t".ToArray<char>());
                    instances.Add(new Instance(result[1], (Result)System.Enum.Parse(typeof(Result), result[0])));
                }

                //initialize a list with numbers of all attributes
                List<int> attributes = new List<int>();
                for (int i = 0; i < instances[0].AttributeCount; i++)
                {
                    attributes.Add(i);
                }

                //make first node of tree
                node = new Node(instances, attributes);
            }
            else
            {
                throw new FileNotFoundException("File could not be found!", filename);
            }
        }

        public Result? Classify(Instance i)
        {
            return node.Classify(i);
        }

        public void Test(int kfc)
        {
            int[,] Confusion = new int[2, 2];
            //get kfc packages
            List<List<Instance>> packages = new List<List<Instance>>();
            for(int i = 0; i < kfc; i++)
            {
                packages.Add(new List<Instance>());
            }

            //divide instances into packages
            for(int i = 0; i < instances.Count; i++)
            {
                packages[i % kfc].Add(instances[i]);
            }

            //test for every package
            for(int i = 0; i < kfc; i++)
            {
                TestPackage(i, packages, Confusion);
            }

            //print confusion matrix
            double sum = 0;
            double correct = 0;

            Console.WriteLine("\nConfusion Matrix:");
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
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

        void TestPackage(int i, List<List<Instance>> packages, int[,] Confusion)
        {
            //create lists for learn and test data
            List<Instance> ToTest = packages[i];
            List<Instance> ToLearn = new List<Instance>();
            for(int j = 0; j < packages.Count; j++)
            {
                if (j == i) continue;
                ToLearn = ToLearn.Concat<Instance>(packages[j]).ToList<Instance>();
            }

            //create list of attributes
            List<int> attributes = new List<int>();
            for (int j = 0; j < ToLearn[0].AttributeCount; j++)
            {
                attributes.Add(j);
            }

            //new node only with learning data
            Node node = new Node(ToLearn, attributes);
            //test each instance from test package
            foreach (Instance instance in ToTest)
            {
                Result? res = Classify(instance);
                Confusion[(int)instance.Result, (int)res]++;
            }
        }
        
    }
}
