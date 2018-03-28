using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionTree
{
    class Instance
    {
        string text;
        Result? result;
        Dictionary<int, bool> Attributes;

        public Instance(string Text, Result? Result)
        {
            text = Text;
            result = Result;
            Attributes = Analyzer.Analyze(text);
        }

        public string Text
        {
            get
            {
                return text;
            }
        }

        public Result? Result
        {
            get
            {
                return result;
            }
        }

        public bool this[int i]
        {
            get
            {
                if (Attributes.ContainsKey(i)) return Attributes[i];
                else throw new ArgumentOutOfRangeException();
            }
        }

        public int Count
        {
            get
            {
                return Attributes.Count;
            }
        }
    }
}
