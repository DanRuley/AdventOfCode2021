using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Problem6
    {
        //public static void Main()
        //{
        //    //readTestAndSort();
        //    Console.WriteLine("\n+++++++++++++++++++++++++++++\n");
        //    solve();
        //}

        public static void readTestAndSort()
        {
            string line;
            using (var sr = new StreamReader(@"c:/users/drslc/downloads/test.txt"))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    var start = line.Split(',').ToList();
                    start.Sort();
                    for (int i = 0; i < start.Count; i++)
                        Console.Write(start[i] + (i == start.Count - 1 ? "\n" : ","));
                }
            }
        }

        public static void solve()
        {
            var fish = new Dictionary<int, long>();

            for (int i = 0; i <= 8; i++)
                fish.Add(i, 0);

            using (var sr = new StreamReader(@"c:/users/drslc/downloads/input.txt"))
            {
                string[] start = sr.ReadToEnd().Split(',');

                foreach (string n in start)
                    fish[int.Parse(n)]++;
            }

            long ans;
            for (int i = 0; i < 256; i++)
            {
                ans = 0;
                foreach (int n in fish.Keys)
                    ans += fish[n];
                Console.WriteLine(ans);
                //string p = "";
                //for (int ii = 0; ii <= 8; ii++)
                //    if (fish.ContainsKey(ii))
                //        for (int j = 0; j < fish[ii]; j++)
                //            p += ii + ",";

                //Console.WriteLine(p.Substring(0, p.Length - 1));

                fish = updateGeneration(fish);
            }

            ans = 0;

            foreach (int n in fish.Keys)
                ans += fish[n];
            Console.WriteLine(ans);
            Console.ReadLine();
        }

        private static Dictionary<int, long> updateGeneration(Dictionary<int, long> fish)
        {
            var nextGen = new Dictionary<int, long>();
            for (int i = 0; i <= 8; i++)
                nextGen.Add(i, 0);

            for (int i = 1; i <= 8; i++)
                nextGen[i - 1] = fish[i];

            nextGen[6] += fish[0];
            nextGen[8] += fish[0];

            return nextGen;
        }
    }
}
