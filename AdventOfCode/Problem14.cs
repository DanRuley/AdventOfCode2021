using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Problem14
    {

        //public static void Main()
        //{
        //    Model m = readLines();


        //    for (int i = 1; i <= 75; i++)
        //    {
        //        m = readLines();
        //        Console.WriteLine(m.solve(i));
        //    }
        //    Console.ReadLine();
        //}

        private static Model readLines()
        {
            Dictionary<string, char> transitions = new Dictionary<string, char>();
            Dictionary<char, long> charCounts = new Dictionary<char, long>();
            Dictionary<string, long> pairCounts = new Dictionary<string, long>();

            Model model = new Model();
            using (StreamReader sr = new StreamReader(@"c:/users/drslc/downloads/input.txt"))
            {
                string line;
                line = sr.ReadLine();

                for (int i = 0; i < line.Length; i++)
                {
                    updateDictionary(charCounts, line[i]);

                    if (i < line.Length - 1)
                        updateDictionary(pairCounts, "" + line[i] + line[i + 1]);
                }

                sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    Match m = Regex.Match(line, @"([a-zA-Z]{2})\s->\s([A-Z])");
                    string pair = m.Groups[1].Value;
                    char ins = m.Groups[2].Value[0];

                    if (!charCounts.ContainsKey(ins))
                        charCounts.Add(ins, 0);

                    transitions.Add(pair, ins);
                }
            }

            model.transitions = transitions;
            model.charCounts = charCounts;
            model.pairCounts = pairCounts;
            return model;
        }

        public static void updateDictionary<K>(Dictionary<K, long> dict, K key)
        {
            if (dict.ContainsKey(key))
                dict[key]++;
            else
                dict.Add(key, 1);
        }

        class Model
        {
            public Dictionary<string, char> transitions { get; set; }
            public Dictionary<char, long> charCounts { get; set; }
            public Dictionary<string, long> pairCounts { get; set; }

            public void update()
            {
                Dictionary<string, long> updatedPairCounts = new Dictionary<string, long>();
                foreach (string key in transitions.Keys)
                {
                    if (pairCounts.ContainsKey(key))
                    {
                        charCounts[transitions[key]] += pairCounts[key];
                        string t1 = "" + key[0] + transitions[key];
                        string t2 = "" + transitions[key] + key[1];

                        if (updatedPairCounts.ContainsKey(t1))
                            updatedPairCounts[t1] += pairCounts[key];
                        else
                            updatedPairCounts.Add(t1, pairCounts[key]);

                        if (updatedPairCounts.ContainsKey(t2))
                            updatedPairCounts[t2] += pairCounts[key];
                        else
                            updatedPairCounts.Add(t2, pairCounts[key]);
                    }
                }
                pairCounts = updatedPairCounts;
            }

            public long solve(int N)
            {
                for (int i = 0; i < N; i++)
                    update();

                long min, max;
                min = long.MaxValue;
                max = long.MinValue;

                foreach (char c in charCounts.Keys)
                {
                    min = Math.Min(min, charCounts[c]);
                    max = Math.Max(max, charCounts[c]);
                }

                return max - min;
            }
        }


    }
}

/*
 * 
 * Garbage from brute force simulating the problem.
 * 
 * 
class LinkedList
{
    public Node head;
    public Node tail;

    public Dictionary<char, int> counts;
    public Dictionary<string, char> map { get; set; }

    public LinkedList(string start)
    {
        counts = new Dictionary<char, int>();

        head = new Node(start[0]);
        counts.Add(start[0], 1);
        Node n = head;
        for (int i = 1; i < start.Length; i++)
        {
            if (counts.ContainsKey(start[i]))
                counts[start[i]]++;
            else
                counts.Add(start[i], 1);
            n.next = new Node(start[i]);
            n = n.next;
        }
        tail = n;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        Node n = head;
        while (n != null)
        {
            sb.Append(n.val);
            n = n.next;
        }
        return sb.ToString();
    }

    internal void update()
    {
        Node n1 = head;
        Node n2 = head.next;

        while (n1 != tail)
        {
            string key = "" + n1.val + n2.val;
            char val = map[key];
            if (counts.ContainsKey(val))
                counts[val]++;
            else
                counts.Add(val, 1);
            n1.next = new Node(val);
            n1.next.next = n2;
            n1 = n1.next.next;
            n2 = n2.next;
        }
    }

    internal int getPartOne()
    {
        int max, min;
        max = min = counts[head.val];
        foreach (int n in counts.Values)
        {
            min = Math.Min(min, n);
            max = Math.Max(max, n);
        }

        return max - min;
    }

    internal void printCounts()
    {
        foreach (char k in counts.Keys)
        {
            if (k == 'H' || k == 'V')
                Console.WriteLine("" + k + ":" + counts[k]);
        }
        Console.WriteLine();
    }
}

class Node
{
    public char val;
    public Node next;

    public Node(char val)
    {
        this.val = val;
    }

    public override string ToString()
    {
        return "" + val;
    }
}

}
}
*/