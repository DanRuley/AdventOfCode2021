using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Problem2
    {


        private static int GetVertTimeHorizDisplacement(string[] lines)
        {
            int horiz = 0;
            int vert = 0;

            foreach (string line in lines)
            {
                string[] split = line.Split(' ');
                int scalar = int.Parse(split[1]);
                switch (split[0])
                {
                    case "up":
                        vert -= scalar;
                        break;
                    case "down":
                        vert += scalar;
                        break;
                    default:
                        horiz += scalar;
                        break;
                }
            }

            return horiz * vert;
        }
        private static int GetVertHorizAimDisplacementValue(string[] lines)
        {
            int horiz = 0;
            int vert = 0;
            int aim = 0;

            foreach (string line in lines)
            {
                string[] split = line.Split(' ');
                int scalar = int.Parse(split[1]);
                switch (split[0])
                {
                    case "up":
                        aim -= scalar;
                        break;
                    case "down":
                        aim += scalar;
                        break;
                    default:
                        horiz += scalar;
                        vert += aim * scalar;
                        break;
                }
            }

            return horiz * vert;
        }
    }

   
}
