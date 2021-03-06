﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionTree
{
    class Instance
    {
        string text;
        Result result;
        Dictionary<int, bool> Attributes;

        //represents an spam or ham message including results and the attributes defined in the analyzer
        public Instance(string Text, Result Result)
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

        public Result Result
        {
            get
            {
                return result;
            }
        }

        //returns the value of the attribute discribed by i
        public bool this[int i]
        {
            get
            {
                if (Attributes.ContainsKey(i)) return Attributes[i];
                else throw new ArgumentOutOfRangeException();
            }
        }

        //returns the number of attributes
        public int AttributeCount
        {
            get
            {
                return Attributes.Count;
            }
        }
    }
}
