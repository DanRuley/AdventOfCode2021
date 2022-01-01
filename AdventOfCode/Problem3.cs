using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Problem3
    {
        //public static void Main()
        //{
        //    string[] lines = System.IO.File.ReadAllLines(@"c:\users\drslc\downloads\input.txt");

        //    GetScrubbers(lines, out int oxGen, out int co2Gen);

        //    Console.WriteLine(oxGen * co2Gen);

        //    Console.ReadLine();
        //}

        private static void GetScrubbers(string[] lines, out int oxGen, out int co2Gen)
        {
            oxGen = 0;
            co2Gen = 0;

            HashSet<string> oxygenNumbers = lines.ToHashSet();
            HashSet<string> co2Numbers = lines.ToHashSet();

            char keep;
            HashSet<string> newSet;

            for (int i = 0; i < lines[0].Length; i++)
            {
                if (oxygenNumbers.Count > 1)
                {
                    if (CountOneBitsAtPosition(oxygenNumbers, i) < (oxygenNumbers.Count / 2 + (oxygenNumbers.Count % 2 != 0 ? 1 : 0)))
                        keep = '0';
                    else keep = '1';

                    newSet = new HashSet<string>();
                    foreach (string number in oxygenNumbers)
                        if (number[i] == keep)
                            newSet.Add(number);

                    oxygenNumbers = newSet;
                }

                if (co2Numbers.Count > 1)
                {
                    if (CountOneBitsAtPosition(co2Numbers, i) < (co2Numbers.Count / 2 + (co2Numbers.Count % 2 != 0 ? 1 : 0)))
                        keep = '1';
                    else keep = '0';

                    newSet = new HashSet<string>();
                    foreach (string number in co2Numbers)
                        if (number[i] == keep)
                            newSet.Add(number);

                    co2Numbers = newSet;
                }
            }

            Debug.Assert(oxygenNumbers.Count == 1 && co2Numbers.Count == 1);

            oxGen = Convert.ToInt32(oxygenNumbers.ToList()[0], 2);
            co2Gen = Convert.ToInt32(co2Numbers.ToList()[0], 2);
        }

        private static int CountOneBitsAtPosition(HashSet<string> numbers, int i)
        {
            int onesCount = 0;
            foreach (string n in numbers)
                onesCount += n[i] - '0';
            return onesCount;
        }

        private static void GetGammaEpsilon(string[] lines, out uint gamma, out uint epsilon)
        {
            uint[] tallies = new uint[lines[0].Length];

            foreach (string line in lines)
                for (int i = 0; i < line.Length; i++)
                    tallies[i] += (uint)(line[i] - '0');

            gamma = 0;
            epsilon = 0;

            for (int i = 0; i < tallies.Length; i++)
                if (tallies[i] > lines.Length / 2)
                    gamma |= (uint)1 << (tallies.Length - 1 - i);
                else
                    epsilon |= (uint)1 << (tallies.Length - 1 - i);
        }
    }
}
