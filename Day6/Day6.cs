namespace AdventOfCode2022
{
    public class Day6 : DailyChallenge
    {
        public override void Solve()
        {
            string stream = File.ReadAllText("Day6/input.txt");

            int part1 = -1, part2 = -1;
            for (int i = 0; i < stream.Length; i++)
                if (part2 < 0 && stream.Skip(i).Take(14).Distinct().ToList().Count == 14)
                    part2 = i + 14;
                else if (part1 < 0 && stream.Skip(i).Take(4).Distinct().ToList().Count == 4)
                    part1 = i + 4;
                
            PrintResults(part1, part2);
        }
    }
}