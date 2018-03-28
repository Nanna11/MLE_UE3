using System;
using System.Collections.Generic;
using System.Linq;

namespace DecisionTree
{
    class Node
    {
        //static Dictionary<int, bool> UsedAttributes = null;
        List<Instance> instances;
        Node True = null;
        Node False = null;
        int attribute;
        double instancesentropy;
        Result? WhenTrue = null;
        Result? WhenFalse = null;

        public Node(List<Instance> Instances, List<int> Attributes)
        {
            instances = Instances;

            //if(UsedAttributes == null)
            //{
            //    Initialize();
            //}
            instancesentropy = Entropy(instances);

            Dictionary<int, double> gain = new Dictionary<int, double>();

            foreach(int i in Attributes)
            {
                gain.Add(i, Gain(i));
            }

            attribute = gain.FirstOrDefault(x => x.Value == gain.Values.Max()).Key;

            Attributes.Remove(attribute);

            int[] ResultWhenFalse = new int[2];
            int[] ResultWhenTrue = new int[2];
            List<Instance> InstancesTrue = new List<Instance>();
            List<Instance> InstancesFalse = new List<Instance>();
            foreach (Instance i in Instances)
            {
                if (i[attribute])
                {
                    ResultWhenTrue[(int)i.Result]++;
                    InstancesTrue.Add(i);
                }
                else
                {
                    ResultWhenFalse[(int)i.Result]++;
                    InstancesFalse.Add(i);
                }
            }

            if (Attributes.Count == 0)
            {
                if (ResultWhenFalse[0] > ResultWhenFalse[1]) WhenFalse = (Result)0;
                else WhenFalse = (Result)1;

                if (ResultWhenTrue[0] > ResultWhenTrue[1]) WhenTrue = (Result)0;
                else WhenTrue = (Result)1;

                return;
            }

            if (ResultWhenFalse[0] != 0 && ResultWhenFalse[1] != 0)
            {
                List<int> NewAttributes = new List<int>();
                foreach (int i in Attributes) NewAttributes.Add(i);
                False = new Node(InstancesFalse, NewAttributes);
            }

            if (ResultWhenTrue[0] != 0 && ResultWhenTrue[1] != 0)
            {
                List<int> NewAttributes = new List<int>();
                foreach (int i in Attributes) NewAttributes.Add(i);
                True = new Node(InstancesTrue, Attributes);
            }

            if (ResultWhenFalse[0] == 0 ^ ResultWhenFalse[1] == 0)
            {
                if (ResultWhenFalse[0] == 0) WhenFalse = (Result)1;
                else WhenFalse = (Result)0;
            }

            if (ResultWhenTrue[0] == 0 ^ ResultWhenTrue[1] == 0)
            {
                if (ResultWhenTrue[0] == 0) WhenTrue = (Result)1;
                else WhenTrue = (Result)0;
            }
        }

        public Result? Classify(Instance i)
        {
            if (i[attribute])
            {
                if (True != null) return True.Classify(i);
                else if (WhenTrue != null) return WhenTrue;
                else return Result.notdefined;
            }
            else
            {
                if (False != null) return False.Classify(i);
                else if (WhenFalse != null) return WhenFalse;
                else return Result.notdefined;
            }
        }

        double Gain(int j)
        {
            double gain = instancesentropy;
            List<Instance> True = new List<Instance>();
            List<Instance> False = new List<Instance>();

            for (int i = 0; i < instances.Count; i++)
            {
                if (instances[i][j])
                {
                    True.Add(instances[i]);

                }
                else False.Add(instances[i]);
            }

            gain -= ((double)True.Count / (double)instances.Count) * Entropy(True);
            gain -= ((double)False.Count / (double)instances.Count) * Entropy(False);

            return gain;

        }

        //void Initialize()
        //{
        //    if (instances.Count <= 0) throw new Exception();
        //    for (int i = 0; i < instances[0].Count; i++)
        //    {
        //        UsedAttributes.Add(i, false);
        //    }
        //}

        double Entropy(List<Instance> Instances)
        {
            if (Instances.Count == 0) return 0;
            double entropy = 0;
            int[] ResultCount = new int[2];
            foreach(Instance i in Instances)
            {
                ResultCount[(int)i.Result]++;
            }

            for(int i = 0; i < 2; i++)
            {
                double num = (double)ResultCount[i] / Instances.Count;
                if (num == 0) continue;
                double log = Math.Log(num, 2);
                entropy -= num * log;
            }

            return entropy;
        }
    }
}
