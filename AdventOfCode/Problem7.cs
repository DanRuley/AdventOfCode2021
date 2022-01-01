using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Problem7
    {

        //public static void Main()
        //{
        //    var map = readLinesMakeMap(out int min, out int max);
        //    int res = solve(map, min, max);
        //    Console.WriteLine(res);
        //    Console.ReadLine();
        //}

        private static int solve(Dictionary<int, int> map, int min, int max)
        {
            int minDist = int.MaxValue;

            for (int x = min; x <= max; x++)
            {
                int currDist = 0;
                foreach (int n in map.Keys)
                    currDist += map[n] * getFirstNSum(Math.Abs(n - x));

                minDist = currDist < minDist ? currDist : minDist;
            }

            return minDist;
        }

        private static int getFirstNSum(int n)
        {
            return n * (n + 1) / 2;
        }

        private static Dictionary<int, int> readLinesMakeMap(out int min, out int max)
        {
            var ret = new Dictionary<int, int>();
            using (StreamReader sr = new StreamReader(@"c:\users\drslc\downloads\input.txt"))
            {
                string[] nums = sr.ReadLine().Split(',');
                min = max = int.Parse(nums[0]);
                foreach (string n in nums)
                {
                    int num = int.Parse(n);
                    min = num < min ? num : min;
                    max = num > max ? num : max;
                    if (ret.ContainsKey(num))
                        ret[num]++;
                    else
                        ret.Add(num, 1);
                }
            }
            return ret;
        }
    }
}
