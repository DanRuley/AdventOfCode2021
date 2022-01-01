using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{

    class Problem4
    {
        const int BoardSize = 5;
        Dictionary<int, List<Coord>> numbers;
        List<int[,]> boards;
        List<int> sums;

        //public static void Main(string[] args)
        //{
        //    Problem4 p4 = new Problem4();
        //    p4.readLinesSetUpBoards();
        //    int ans = p4.findLastWinner();
        //    Console.WriteLine(ans);
        //    Console.ReadLine();
        //}

        private int solve()
        {
            foreach (int num in numbers.Keys)
            {
                foreach (Coord c in numbers[num])
                {
                    boards[c.board][c.row, c.col] = -1;
                    sums[c.board] -= num;
                    bool winner = true;
                    for (int i = 0; i < BoardSize && winner; i++)
                        if (boards[c.board][i, c.col] >= 0)
                            winner = false;
                    if (winner)
                        return sums[c.board] * num;
                    winner = true;
                    for (int j = 0; j < BoardSize && winner; j++)
                        if (boards[c.board][c.row, j] >= 0)
                            winner = false;
                    if (winner)
                        return sums[c.board] * num;
                }
            }
            throw new Exception("No winner");
        }

        private int findLastWinner()
        {
            HashSet<int> winners = new HashSet<int>();
            foreach (int num in numbers.Keys)
            {
                foreach (Coord c in numbers[num])
                {
                    if (winners.Contains(c.board))
                        continue;

                    boards[c.board][c.row, c.col] = -1;
                    sums[c.board] -= num;
                    bool winner = true;
                    for (int i = 0; i < BoardSize && winner; i++)
                        if (boards[c.board][i, c.col] >= 0)
                            winner = false;
                    if (winner)
                    {
                        winners.Add(c.board);
                        if (winners.Count == boards.Count)
                            return sums[c.board] * num;
                        continue;
                    }
                    winner = true;
                    for (int j = 0; j < BoardSize && winner; j++)
                        if (boards[c.board][c.row, j] >= 0)
                            winner = false;
                    if (winner)
                    {
                        winners.Add(c.board);
                        if (winners.Count == boards.Count)
                            return sums[c.board] * num;
                    }
                }
            }
            throw new Exception("No winner");
        }

        private void readLinesSetUpBoards()
        {
            numbers = new Dictionary<int, List<Coord>>();
            boards = new List<int[,]>();
            sums = new List<int>();

            using (StreamReader sr = new StreamReader(@"c:\users\drslc\downloads\input.txt"))
            {
                var numStr = sr.ReadLine().Split(',');
                foreach (string n in numStr)
                    numbers.Add(int.Parse(n), new List<Coord>());
                sr.ReadLine();

                int boardNum = 0;
                while (!sr.EndOfStream)
                {
                    int[,] board = new int[BoardSize, BoardSize];
                    sums.Add(0);
                    for (int i = 0; i < BoardSize; i++)
                    {
                        var row = sr.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                        for (int j = 0; j < BoardSize; j++)
                        {
                            int n = int.Parse(row[j].Trim());
                            sums[boardNum] += n;
                            numbers[n].Add(new Coord(boardNum, i, j));
                            board[i, j] = n;
                        }
                    }
                    boards.Add(board);
                    boardNum++;
                    sr.ReadLine();
                }
            }
        }

        class Coord
        {
            public int board { get; set; }
            public int row { get; set; }
            public int col { get; set; }

            public Coord(int _board, int _row, int _col)
            {
                board = _board; row = _row; col = _col;
            }
        }
    }
}
