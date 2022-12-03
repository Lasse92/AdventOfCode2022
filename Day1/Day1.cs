namespace AdventOfCode2022
{
    public class Day1 : DailyChallenge
    {
        public override void Solve()
        {
            List<string> lines = File.ReadAllLines("Day1/input.txt").ToList();
            List<int[]> elves = new();
            List<string> calories = new();
            for (int i = 0; i <= lines.Count; i++)
            {
                if (i == lines.Count || lines[i].Equals(string.Empty))
                {
                    elves.Add(calories.Select(x => int.Parse(x)).ToArray());
                    calories = new();
                    continue;
                }
                calories.Add(lines[i]);
            }

            int highest = elves.OrderByDescending(e => e.Sum(c => c))
                               .First()
                               .Sum(c => c);

            int top3 = elves.OrderByDescending(e => e.Sum(c => c))
                            .Take(3)
                            .Sum(e => e.Sum(c => c));

            PrintResults(highest, top3);
        }
    }
}