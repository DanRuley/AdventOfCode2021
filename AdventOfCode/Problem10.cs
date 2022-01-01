using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
    class Problem10
    {
        //public static void Main()
        //{
        //    List<string> lines = readLines();

        //    var score = findCompletedLineErrorScores(lines);
        //    Console.WriteLine(score);
        //    Console.ReadLine();
        //}

        private static long findCompletedLineErrorScores(List<string> lines)
        {

            List<long> completedScores = new List<long>();

            Dictionary<char, int> scores = new Dictionary<char, int>();
            scores.Add(')', 1);
            scores.Add(']', 2);
            scores.Add('}', 3);
            scores.Add('>', 4);

            Dictionary<char, char> pairs = new Dictionary<char, char>();
            pairs.Add('(', ')');
            pairs.Add('<', '>');
            pairs.Add('{', '}');
            pairs.Add('[', ']');

            foreach (string line in lines)
            {
                Stack<char> st = new Stack<char>();
                bool err = false;

                //find errors or build up a stack of unfinished chars
                for (int i = 0; i < line.Length; i++)
                {
                    char c = line[i];
                    if (c == '(' || c == '<' || c == '{' || c == '[')
                        st.Push(c);
                    else
                    {
                        char o = st.Pop();
                        if (c != pairs[o])
                        {
                            err = true;
                            break;
                        }
                    }
                }

                if (err)
                    continue;

                long score = 0;

                while (st.Count > 0)
                {
                    score *= 5;
                    score += scores[pairs[st.Pop()]];
                }

                completedScores.Add(score);
            }

            completedScores.Sort();

            return completedScores[completedScores.Count / 2];
        }

        private static List<string> readLines()
        {
            List<string> lines = new List<string>();

            using (StreamReader sr = new StreamReader(@"c:/users/drslc/downloads/input.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                    lines.Add(line);
            }
            return lines;
        }
    }
}
