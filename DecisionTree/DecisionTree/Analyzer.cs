using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DecisionTree
{
    class Analyzer
    {
        int count = 0;

        public static Dictionary<int, bool> Analyze(string text)
        {
            Dictionary<int, bool> Attributes = new Dictionary<int, bool>();
            int index = 0;

            Attributes.Add(Index(ref index), XXX(text)); //0
            Attributes.Add(Index(ref index), Free(text)); //1
            Attributes.Add(Index(ref index), Apostrophe(text)); //2
            Attributes.Add(Index(ref index), Pound(text)); //3
            Attributes.Add(Index(ref index), Uppercase(text)); //4
            Attributes.Add(Index(ref index), WWW(text)); //5
            Attributes.Add(Index(ref index), Number(text)); //6

            return Attributes;
        }

        static bool XXX(string text)
        {
            if (text.ToLower().Contains("xxx")) return true;
            else return false;
        }

        static bool Free(string text)
        {
            if (text.ToLower().Contains("free")) return true;
            else return false;
        }

        static bool Apostrophe(string text)
        {
            if (text.ToLower().Contains("'")) return true;
            else return false;
        }

        static bool Pound(string text)
        {
            if (text.ToLower().Contains("£")) return true;
            else return false;
        }

        static bool Uppercase(string text)
        {
            string regex = @"(\b*[A-Z][A-Z]*\b)";
            MatchCollection matches = Regex.Matches(text, regex);
            if (matches.Count > 0) return true;
            else return false;
        }

        static bool WWW(string text)
        {
            if (text.ToLower().Contains("www")) return true;
            else return false;
        }

        static bool Number(string text)
        {
            string regex = @"(.*[0-9][0-9][0-9][0-9].*)";
            MatchCollection matches = Regex.Matches(text, regex);
            if (matches.Count > 0) return true;
            else return false;
        }

        static int Index(ref int i)
        {
            i = i + 1;
            return i - 1;
        }

    }
}
