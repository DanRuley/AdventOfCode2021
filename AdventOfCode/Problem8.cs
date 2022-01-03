using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace AdventOfCode
{

    class Problem8
    {
        static Dictionary<int, int> uqNums;
        //public static void Main()
        //{
        //    List<List<string>> inputs;
        //    List<List<string>> outputs;
        //    readProblem(out inputs, out outputs);

        //    uqNums = new Dictionary<int, int>();
        //    uqNums.Add(2, 1);
        //    uqNums.Add(4, 4);
        //    uqNums.Add(3, 7);
        //    uqNums.Add(7, 8);

        //    solve(inputs, outputs);

        //    Console.ReadLine();
        //}

        private static void solve(List<List<string>> inputs, List<List<string>> outputs)
        {
            Dictionary<Code, int> map;

            int sum = 0;

            for (int i = 0; i < inputs.Count; i++)
            {
                string partialSum = "";
                map = new Dictionary<Code, int>();
                parseNumbersUpdateMap(map, inputs[i]);

                foreach (string s in outputs[i])
                {
                    Code temp = new Code(s);
                    partialSum += map[temp];
                }
                sum += int.Parse(partialSum);
            }

            Console.WriteLine(sum);
        }

        private static void parseNumbersUpdateMap(Dictionary<Code, int> map, List<string> nums)
        {
            Dictionary<int, Code> backMap = new Dictionary<int, Code>();
            List<string> sixes = new List<string>();
            List<string> fives = new List<string>();
            foreach (string s in nums)
            {
                int length = s.Length;
                if (uqNums.ContainsKey(length))
                {
                    Code c = new Code(s);
                    map.Add(c, uqNums[length]);
                    backMap.Add(map[c], c);
                }
                else if (length == 5)
                    fives.Add(s);
                else
                    sixes.Add(s);
            }
            foreach (string s in fives)
            {
                Code c = new Code(s);
                if (c.intersectCount(backMap[4]) == 2)
                    map.Add(c, 2);
                else if (c.intersectCount(backMap[1]) == 2 && c.intersectCount(backMap[7]) == 3)
                    map.Add(c, 3);
                else
                    map.Add(c, 5);
            }
            foreach (string s in sixes)
            {
                Code c = new Code(s);
                if (c.intersectCount(backMap[4]) == 4)
                    map.Add(c, 9);
                else if (c.intersectCount(backMap[1]) == 2 && c.intersectCount(backMap[7]) == 3)
                    map.Add(c, 0);
                else
                    map.Add(c, 6);
            }
        }

        private static void readProblem(out List<List<string>> inputs, out List<List<string>> outputs)
        {
            inputs = new List<List<string>>();
            outputs = new List<List<string>>();

            using (StreamReader sr = new StreamReader(@"c:/users/drslc/downloads/input.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Match m = Regex.Match(line, @"((?:[a-z]+\s*){10})\|\s((?:[a-z]+\s*){4})");

                    var g = m.Groups;
                    inputs.Add(g[1].ToString().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList());
                    outputs.Add(g[2].ToString().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList());
                }
            }
        }
        class Code
        {
            public string val;
            public HashSet<char> hs;

            public Code(string _val)
            {
                var ls = _val.ToList();
                ls.Sort();
                val = "";
                foreach (char x in ls)
                    val += x;
                hs = val.ToHashSet();
            }

            public override int GetHashCode()
            {
                return val.GetHashCode();
            }

            public override string ToString()
            {
                return val;
            }

            public override bool Equals(object obj)
            {
                if (!(obj is Code))
                    return false;

                return val == ((Code)obj).val;
            }

            public int intersectCount(Code other)
            {
                return hs.Intersect(other.hs).Count();
            }
        }
    }
}
