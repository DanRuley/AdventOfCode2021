using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
    class Problem18
    {

        public static void Main()
        {
            List<Pair> pairs = parseInput();
            //Pair additionResult = addPairs(pairs);
            //int mag = magnitude(additionResult);
            //Console.WriteLine(mag);
            int largestMag = largestMagnitude(pairs);
            Console.WriteLine(largestMag);
            Console.ReadLine();
        }

        private static int magnitude(Pair p)
        {
            if (p.leaf)
                return p.val;
            else
                return 3 * magnitude(p.left) + 2 * magnitude(p.right);
        }

        public static int largestMagnitude(List<Pair> pairs)
        {
            int largestMag = int.MinValue;
            for (int i = 0; i < pairs.Count; i++)
            {
                for (int j = 0; j < pairs.Count; j++)
                {
                    if (j == i)
                        continue;
                    Pair added = new Pair(parsePairFromString(pairs[i].ToString()), parsePairFromString(pairs[j].ToString()));
                    reduce(added);
                    largestMag = Math.Max(largestMag, magnitude(added));
                }
            }
            return largestMag;
        }

        private static Pair addPairs(List<Pair> pairs)
        {
            Pair added = new Pair(pairs[0], pairs[1]);
            reduce(added);
            for (int i = 2; i < pairs.Count; i++)
            {
                Console.WriteLine(added);
                added = new Pair(added, pairs[i]);
                reduce(added);
            }
            Console.WriteLine(added);
            return added;
        }

        private static void reduce(Pair p)
        {
            while (explode(p, null, 0) || split(p, null, false))
                ;
        }

        private static bool split(Pair p, Pair parent, bool left)
        {
            if (p.leaf)
            {
                if (p.val >= 10)
                {
                    Pair split = new Pair(parent);
                    split.left = new Pair(p.val / 2, split);
                    split.right = new Pair(p.val % 2 == 0 ? p.val / 2 : p.val / 2 + 1, split);
                    addPair(split, parent, left);
                    return true;
                }
                else
                    return false;
            }

            return split(p.left, p, true) || split(p.right, p, false);
        }

        private static bool explode(Pair p, Pair parent, int depth)
        {
            if (p.leaf)
                return false;

            if (p.left.leaf && p.right.leaf && depth >= 4)
            {
                addExplosionVal(p, p.left.val, true);
                addExplosionVal(p, p.right.val, false);
                p.left = null;
                p.right = null;
                p.leaf = true;
                p.val = 0;
                return true;
            }

            return explode(p.left, p, depth + 1) || explode(p.right, p, depth + 1);
        }

        static void addExplosionVal(Pair start, int val, bool left)
        {
            Pair curr = start;
            HashSet<Pair> disallowed = new HashSet<Pair>();
            disallowed.Add(curr);

            while (curr.parent != null && ((left && curr.parent.left == curr) || (!left && curr.parent.right == curr)))
            {
                disallowed.Add(curr.parent);
                curr = curr.parent;
            }

            curr = curr.parent;
            if (curr != null)
                start = curr;
            else
                return;

            Stack<Pair> st = new Stack<Pair>();
            st.Push(start);

            while (st.Count > 0)
            {
                curr = st.Pop();
                if (disallowed.Contains(curr))
                    continue;
                if (curr.leaf)
                {
                    curr.val += val;
                    return;
                }
                if (left)
                {
                    st.Push(curr.left);
                    st.Push(curr.right);
                }
                else
                {
                    st.Push(curr.right);
                    st.Push(curr.left);
                }
            }
        }

        private static List<Pair> parseInput()
        {
            List<Pair> snails = new List<Pair>();
            string line;
            using (StreamReader sr = new StreamReader(@"c:/users/drslc/downloads/input.txt"))
                while ((line = sr.ReadLine()) != null)
                    snails.Add(parsePairFromString(line));
            return snails;
        }

        private static Pair parsePairFromString(string line)
        {
            Pair curr = null;
            bool addLeft = true;
            Stack<Pair> st = new Stack<Pair>();
            int cursor = 0;
            while (cursor < line.Length)
            {
                if (line[cursor] == '[')
                {
                    Pair p = new Pair(st.Count == 0 ? null : st.Peek());
                    if (st.Count > 0)
                        addPair(p, st.Peek(), addLeft);

                    st.Push(p);
                    addLeft = true;
                }
                else if (char.IsDigit(line[cursor]))
                {
                    int numEnd = cursor;

                    while (char.IsDigit(line[numEnd]))
                        numEnd++;

                    int val = int.Parse(line.Substring(cursor, numEnd - cursor));
                    addPair(new Pair(val, st.Peek()), st.Peek(), addLeft);
                    cursor = numEnd - 1;
                }
                else if (line[cursor] == ',')
                    addLeft = false;
                else
                    curr = st.Pop();

                cursor++;
            }
            return curr;
        }

        private static void addPair(Pair p, Pair parent, bool addLeft)
        {
            if (addLeft)
                parent.left = p;
            else
                parent.right = p;
        }
    }

    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    internal class Pair
    {
        internal Pair left;
        internal Pair right;
        internal Pair parent;
        internal bool leaf;
        internal int val;

        public Pair(Pair p)
        {
            parent = p;
        }

        public Pair(Pair left, Pair right)
        {
            this.left = left;
            this.right = right;
            left.parent = right.parent = this;
            this.parent = null;
        }
        public Pair(int val, Pair p)
        {
            parent = p;
            leaf = true;
            this.val = val;
        }

        public override string ToString()
        {
            if (leaf)
                return "" + val;
            else return "[" + (this.left != null ? this.left.ToString() : "null") + "," + (this.right != null ? this.right.ToString() : "null") + "]";
        }
    }
}
