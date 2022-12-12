namespace AdventOfCode2022
{
    public class Day4 : DailyChallenge
    {
        public override void Solve()
        {
            List<string> lines = File.ReadAllLines("Day04/input.txt").ToList();

            int part1 = 0, part2 = 0;
            foreach (string line in lines)
            {
                List<int[]> elves = line.Split(',').Select(s => Enumerable.Range(int.Parse(s.Split('-')[0]), int.Parse(s.Split('-')[1]) - int.Parse(s.Split('-')[0]) + 1).ToArray()).ToList();
                int intersect = elves[0].Intersect(elves[1]).ToList().Count;
                part1 += intersect >= elves[0].Length || intersect >= elves[1].Length ? 1 : 0;
                part2 += intersect > 0 ? 1 : 0;
            }
            PrintResults(part1, part2);
        }
    }
}