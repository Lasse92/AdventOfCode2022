namespace AdventOfCode2022
{
    public class Day3 : DailyChallenge
    {
        public override void Solve()
        {
            List<string> lines = File.ReadAllLines("Day03/input.txt").ToList();

            int part1 = 0;
            int part2 = 0;
            for (int i = 0; i < lines.Count; i++)
            {
                string com1 = lines[i].Substring(0, lines[i].Length / 2);
                string com2 = lines[i].Substring(lines[i].Length / 2);

                part1 += GetValue(com1.First(c => com2.Contains(c)));
                if (i % 3 == 0)
                    part2 += GetValue(lines[i].First(c => lines[i + 1].Contains(c) && lines[i + 2].Contains(c)));
            }
            PrintResults(part1, part2);
        }

        private int GetValue(char c) => ((int)c) - (char.IsLower(c) ? 96 : 38);
    }
}