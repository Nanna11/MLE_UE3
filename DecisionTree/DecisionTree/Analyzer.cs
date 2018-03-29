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
        //analyzes the text an returns a dictionary with ascending, ongoing numbers of attributes and
        //correspoding bool indicating if attribute is true or false
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

        //analyzes if text contains substring "xxx" - upper/lowercase is not considered
        static bool XXX(string text)
        {
            if (text.ToLower().Contains("xxx")) return true;
            else return false;
        }

        //analyzes if text contains substring "free" - upper/lowercase is not considered
        static bool Free(string text)
        {
            if (text.ToLower().Contains("free")) return true;
            else return false;
        }

        //analyzes if text contains apostrophes
        static bool Apostrophe(string text)
        {
            if (text.ToLower().Contains("'")) return true;
            else return false;
        }

        //analyzes if text contains the "£" symbol
        static bool Pound(string text)
        {
            if (text.ToLower().Contains("£")) return true;
            else return false;
        }

        //analyzes if text contains a word with two or more uppercase letters in a row
        static bool Uppercase(string text)
        {
            string regex = @"(\b*[A-Z][A-Z]*\b)";
            MatchCollection matches = Regex.Matches(text, regex);
            if (matches.Count > 0) return true;
            else return false;
        }

        //analyzes if text contains substring "www" - upper/lowercase is not considered
        static bool WWW(string text)
        {
            if (text.ToLower().Contains("www")) return true;
            else return false;
        }

        //analyzes if text contains more than four numeric characters in a row
        static bool Number(string text)
        {
            string regex = @"(.*[0-9][0-9][0-9][0-9].*)";
            MatchCollection matches = Regex.Matches(text, regex);
            if (matches.Count > 0) return true;
            else return false;
        }

        //returns the unincremented input i - then increments the reference of i by one
        static int Index(ref int i)
        {
            i = i + 1;
            return i - 1;
        }

    }
}
