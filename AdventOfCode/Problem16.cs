using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode
{
    class Problem16
    {

        static Dictionary<int, string> typeOps = new Dictionary<int, string>()
        {
            {0, "sum"},
            {1, "product"},
            {2, "minimum"},
            {3, "maximum"},
            {4, "literal"},
            {5, "greater than"},
            {6, "less than"},
            {7, "equal to"}
        };

        //public static void Main()
        //{
        //    string hex = readLines();
        //    string binary = parseHex(hex);

        //    long res = solve(binary);

        //    Console.WriteLine("version sum: " + res);

        //    Console.ReadLine();
        //}

        private static long solve(string binary)
        {
            int cursor = 0;
            int indent = 0;
            Operator op = parsePackets(indent, ref cursor, binary);
            return op.val;
        }

        private static Operator parsePackets(int indent, ref int cursor, string binary)
        {

            int vLen, tLen;
            vLen = tLen = 3;
            const int literal = 4;

            int version = Convert.ToInt32(binary.Substring(cursor, vLen), 2);
            cursor += vLen;
            int type = Convert.ToInt32(binary.Substring(cursor, tLen), 2);
            cursor += tLen;

            string indentString = "";
            for (int i = 0; i < indent; i++)
                indentString += "    ";

            Operator op = new Operator(type);

            switch (type)
            {
                case literal:
                    op.val = parseLiteral(ref cursor, binary);
                    Console.WriteLine(indentString + "Literal value packet (" + op.val + ") type: " + type);
                    return op;
                default:
                    Console.WriteLine(indentString + "Operator packet LT " + binary[cursor] + " type: " + type);
                    if (binary[cursor] == '1')
                        return parseLengthTypeOneOp(op, indent, ref cursor, binary);
                    else
                        return parseLengthTypeZeroOp(op, indent, ref cursor, binary);
            }
        }

        private static long parseLiteral(ref int cursor, string binary)
        {
            //length starts at 6 due to type and version.
            StringBuilder sb = new StringBuilder();
            while (binary[cursor] == '1')
            {
                sb.Append(binary.Substring(cursor + 1, 4));
                cursor += 5;
            }

            //append final '0' group
            sb.Append(binary.Substring(cursor + 1, 4));
            cursor += 5;

            long ret = Convert.ToInt64(sb.ToString(), 2);

            return ret;
        }

        private static Operator parseLengthTypeZeroOp(Operator op, int indent, ref int cursor, string binary)
        {
            cursor++;
            int subPacketLength = Convert.ToInt32(binary.Substring(cursor, 15), 2);

            cursor += 15;
            int oldCursor = cursor;
            while (cursor < oldCursor + subPacketLength)
                op.children.Add(parsePackets(indent + 1, ref cursor, binary));

            performOps(op, indent);

            return op;
        }

        private static Operator parseLengthTypeOneOp(Operator op, int indent, ref int cursor, string binary)
        {
            cursor++;
            int packets = Convert.ToInt32(binary.Substring(cursor, 11), 2);
            cursor += 11;

            for (int i = 0; i < packets; i++)
                op.children.Add(parsePackets(indent + 1, ref cursor, binary));

            performOps(op, indent);

            return op;
        }

        private static void performOps(Operator o, int indent)
        {
            string indentStr = "";
            for (int i = 0; i < indent; i++)
                indentStr += "    ";

            if (o.type == 5)
                o.val = o.children[0].val > o.children[1].val ? 1 : 0;
            else if (o.type == 6)
                o.val = o.children[0].val < o.children[1].val ? 1 : 0;
            else if (o.type == 7)
                o.val = o.children[0].val == o.children[1].val ? 1 : 0;

            else
            {
                o.val = o.type == 0 ? 0 : o.type == 1 ? 1 : o.children[0].val;

                foreach (Operator c in o.children)
                {
                    switch (o.type)
                    {
                        case 0:
                            o.val += c.val;
                            break;
                        case 1:
                            o.val *= c.val;
                            break;
                        case 2:
                            o.val = Math.Min(o.val, c.val);
                            break;
                        case 3:
                            o.val = Math.Max(o.val, c.val);
                            break;
                    }
                }
            }
            Console.WriteLine(indentStr + "type: " + o.type + " (" + typeOps[o.type] + "), val: " + o.val);
        }


        private static string readLines()
        {
            string line;
            using (StreamReader sr = new StreamReader(@"c:/users/drslc/downloads/input.txt"))
                line = sr.ReadLine();
            return line;
        }

        class Operator
        {
            public long val;
            public int type;
            public List<Operator> children;

            public Operator(int type)
            {
                this.type = type;
                children = new List<Operator>();
            }
        }

        private static string parseHex(string hex)
        {
            Dictionary<char, string> hexToBinary = new Dictionary<char, string>()
            { {'0', "0000"},
                {'1', "0001" },
                {'2', "0010" },
                {'3', "0011" },
                {'4', "0100" },
                {'5', "0101" },
                {'6', "0110" },
                {'7', "0111" },
                {'8', "1000" },
                {'9', "1001" },
                {'A', "1010" },
                {'B', "1011" },
                {'C', "1100" },
                {'D', "1101" },
                {'E', "1110" },
                {'F', "1111" },
            };

            StringBuilder binary = new StringBuilder();

            for (int i = 0; i < hex.Length; i++)
                binary.Append(hexToBinary[hex[i]]);

            return binary.ToString();
        }
    }
}
