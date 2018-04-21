using System;
using System.Collections.Generic;
using System.Linq;

namespace DecisionTree
{
    class Node
    {
        Node True = null;
        Node False = null;
        int attribute;
        double instancesentropy;
        Result? WhenTrue = null;
        Result? WhenFalse = null;

        public Node(List<Instance> Instances, List<int> Attributes)
        { 
            //calculate the entropy of all instances
            instancesentropy = Entropy(Instances);

            Dictionary<int, double> gain = new Dictionary<int, double>();

            //calculate gain for every  attribute available
            foreach(int i in Attributes)
            {
                gain.Add(i, Gain(i, Instances));
            }

            //choose attribute with highes gain as attribute used by this node
            attribute = gain.FirstOrDefault(x => x.Value == gain.Values.Max()).Key;

            //remove attribuite from list of available attributes
            Attributes.Remove(attribute);

            int[] ResultWhenFalse = new int[2];
            int[] ResultWhenTrue = new int[2];
            int[] Result = new int[2];
            List<Instance> InstancesTrue = new List<Instance>();
            List<Instance> InstancesFalse = new List<Instance>();

            //sort instances by value of choosen attribute
            //count occurences of results in both lists
            foreach (Instance i in Instances)
            {
                if (i[attribute])
                {
                    ResultWhenTrue[(int)i.Result]++;
                    InstancesTrue.Add(i);
                    Result[(int)i.Result]++;
                }
                else
                {
                    ResultWhenFalse[(int)i.Result]++;
                    InstancesFalse.Add(i);
                    Result[(int)i.Result]++;
                }
            }

            //if there are no available attributes left for next node a descision needs to be made in this node
            if (Attributes.Count == 0)
            {
                //choose result that is more likely when choosen attribute is false
                if (ResultWhenFalse[0] > ResultWhenFalse[1]) WhenFalse = (Result)0;
                else WhenFalse = (Result)1;

                //choose result that is more likely when choosen attribute is true
                if (ResultWhenTrue[0] > ResultWhenTrue[1]) WhenTrue = (Result)0;
                else WhenTrue = (Result)1;

                return;
            }

            //create next node for instances where choosen attribute is false
            //only needs to be made if descision is not already distinct by choosen attribute
            if (ResultWhenFalse[0] != 0 && ResultWhenFalse[1] != 0)
            {
                List<int> NewAttributes = new List<int>();
                foreach (int i in Attributes) NewAttributes.Add(i); //copy attributes list so it can be different in later nodes
                False = new Node(InstancesFalse, NewAttributes);
            }

            //create next node for instances where choosen attribute is true
            //only needs to be made if descision is not already distinct by choosen attribute
            if (ResultWhenTrue[0] != 0 && ResultWhenTrue[1] != 0)
            {
                List<int> NewAttributes = new List<int>();
                foreach (int i in Attributes) NewAttributes.Add(i); //copy attributes list so it can be different in later nodes
                True = new Node(InstancesTrue, Attributes);
            }

            //save result for instances with choosen attributes value false
            //if descision is already distinct by choosen attribute
            if (ResultWhenFalse[0] == 0 ^ ResultWhenFalse[1] == 0)
            {
                if (ResultWhenFalse[0] == 0) WhenFalse = (Result)1;
                else WhenFalse = (Result)0;
            }

            //save result for instances with choosen attributes value true
            //if descision is already distinct by choosen attribute
            if (ResultWhenTrue[0] == 0 ^ ResultWhenTrue[1] == 0)
            {
                if (ResultWhenTrue[0] == 0) WhenTrue = (Result)1;
                else WhenTrue = (Result)0;
            }

            //if there are no instances with choosen attribute being false
            //return result that is most likely in this node in general
            if (ResultWhenFalse[0] == 0 && ResultWhenFalse[1] == 0)
            {
                if (Result[0] > Result[1]) WhenFalse = (Result)0;
                else WhenFalse = (Result)1;
            }

            //if there are no instances with choosen attribute being true
            //return result that is most likely in this node in general
            if (ResultWhenTrue[0] == 0 && ResultWhenTrue[1] == 0)
            {
                if (Result[0] > Result[1]) WhenTrue = (Result)0;
                else WhenTrue = (Result)1;
            }


        }

        //classify a given instance
        public Result? Classify(Instance i)
        {
            if (i[attribute])
            {
                if (True != null) return True.Classify(i); //if descision cannot be made in this node
                return WhenTrue; //if descision can be made in this node
            }
            else
            {
                if (False != null) return False.Classify(i);
                return WhenFalse;
            }
        }

        //calculates the gain of an attribute for a given list of instances
        double Gain(int j, List<Instance> Instances)
        {
            //Gain(S,F) = Entropy(S) - sum(|Sf|/|S| * Entropy(Sf))
            double gain = instancesentropy; //Entropy(S)
            List<Instance> True = new List<Instance>();
            List<Instance> False = new List<Instance>();

            //sort all instances in list by the value of the attribute discribed by j
            for (int i = 0; i < Instances.Count; i++)
            {
                if (Instances[i][j])
                {
                    True.Add(Instances[i]);

                }
                else False.Add(Instances[i]);
            }

            //               |Sf|       +           |S|            *  Entropy(Sf)
            gain -= ((double)True.Count / (double)Instances.Count) * Entropy(True);
            gain -= ((double)False.Count / (double)Instances.Count) * Entropy(False);

            return gain;

        }

        //calculates the entropy in a list of instances by result
        double Entropy(List<Instance> Instances)
        {
            if (Instances.Count == 0) return 0; //if there are no instances entropy wont be needed anyway
            double entropy = 0;
            int[] ResultCount = new int[2];

            //count how often every result is represented in the list
            foreach(Instance i in Instances)
            {
                ResultCount[(int)i.Result]++;
            }

            //entropy = sum(- pi * ²log(pi))
            for(int i = 0; i < 2; i++)
            {
                double p = (double)ResultCount[i] / Instances.Count;
                if (p == 0) continue;
                double log = Math.Log(p, 2);
                entropy -= p * log;
            }

            return entropy;
        }
    }
}
