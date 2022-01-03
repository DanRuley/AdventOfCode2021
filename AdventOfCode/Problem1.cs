namespace AdventOfCode
{
    class Problem1
    {
        //static void Main(string[] args)
        //{

        //    int inc = computeIncrease();
        //    Console.WriteLine(inc + "\n");
        //    Console.ReadLine();
        //}

        private static int computeIncrease()
        {
            string[] lines = System.IO.File.ReadAllLines(@"c:\users\drslc\downloads\input.txt");
            int[] nums = new int[lines.Length];
            for (int i = 0; i < lines.Length; i++)
                nums[i] = int.Parse(lines[i]);


            int prev = nums[0] + nums[1] + nums[2];
            int inc = 0;


            for (int i = 3; i < nums.Length; i++)
            {
                int curr = prev + nums[i] - nums[i - 3];
                if (curr > prev)
                    inc++;
                prev = curr;
            }
            return inc;
        }
    }
}
