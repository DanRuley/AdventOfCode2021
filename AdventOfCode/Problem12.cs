using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Problem12
    {
        //public static void Main()
        //{
        //    Vertex start = readLinesMakeGraph();
        //    solve(start);
        //}

        private static void solve(Vertex start)
        {
            Queue<Path> q = new Queue<Path>();
            q.Enqueue(new Path(null, start));
            int paths = 0;

            while (q.Count > 0)
            {
                Path p = q.Dequeue();

                if (p.getMostRecentVertex().val == "end")
                {
                    Console.WriteLine(p.ToString());
                    paths++;
                    continue;
                }
                foreach (Vertex v in p.getMostRecentVertex().edges)
                {
                    if (Path.isLegalNextStep(p, v))
                        q.Enqueue(new Path(p, v));
                }
            }

            Console.WriteLine(paths);
            Console.ReadLine();
        }

        private static Vertex readLinesMakeGraph()
        {
            Dictionary<string, Vertex> nodes = new Dictionary<string, Vertex>();

            using (StreamReader sr = new StreamReader(@"c:/users/drslc/downloads/input.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] pair = line.Split('-');
                    foreach (string v in pair)
                    {
                        Vertex vv = new Vertex(v);
                        if (!nodes.ContainsKey(v))
                            nodes.Add(v, vv);
                    }
                    nodes[pair[0]].edges.Add(nodes[pair[1]]);
                    nodes[pair[1]].edges.Add(nodes[pair[0]]);
                }
            }

            return nodes["start"];
        }

        class Path
        {
            List<Vertex> sequence;
            HashSet<Vertex> disallowed;
            bool exception;

            public Path(Path concat, Vertex next)
            {
                sequence = new List<Vertex>();
                disallowed = new HashSet<Vertex>();
                exception = true;

                if (concat != null)
                {
                    sequence.AddRange(concat.sequence);
                    disallowed.UnionWith(concat.disallowed);
                    exception = concat.exception;
                }
                sequence.Add(next);
                if (next.val.ToLower() == next.val)
                {
                    if (disallowed.Contains(next) && exception)
                        exception = false;
                    disallowed.Add(next);
                }
            }

            public static bool isLegalNextStep(Path p, Vertex v)
            {
                return !p.disallowed.Contains(v) || (p.exception && v.val != "start");
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < sequence.Count; i++)
                    sb.Append(sequence[i] + (i == sequence.Count - 1 ? "" : ","));

                return sb.ToString();
            }

            public Vertex getMostRecentVertex()
            {
                return sequence[sequence.Count - 1];
            }
        }

        class Vertex
        {
            public List<Vertex> edges;
            public string val;

            public Vertex(string _val)
            {
                val = _val;
                edges = new List<Vertex>();
            }

            public override bool Equals(object obj)
            {
                if (!(obj is Vertex))
                    return false;
                var v = (Vertex)obj;
                return val == v.val;
            }

            public override int GetHashCode()
            {
                return val.GetHashCode();
            }

            public override string ToString()
            {
                return val;
            }
        }
    }
}
