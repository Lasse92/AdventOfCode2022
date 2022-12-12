namespace AdventOfCode2022
{
    public class Day2 : DailyChallenge
    {
        public override void Solve()
        {
            List<string> lines = File.ReadAllLines("Day02/input.txt").ToList();

            int part1 = 0;
            int part2 = 0;
            foreach (string line in lines)
            {
                if (line.Length < 1) continue;

                char[] strat = line.Split(' ').Select(s => char.Parse(s)).ToArray();
                part1 += GetValue(strat[0], strat[1]);
                part2 += GetValue(strat[0], GetPlayerStrategy(strat[0], strat[1]));
            }

            PrintResults(part1, part2);
        }

        private int GetValue(char opponent, char player)
        {
            return (opponent, player) switch
            {
                ('A', 'X') => 4,
                ('A', 'Y') => 8,
                ('A', 'Z') => 3,
                ('B', 'X') => 1,
                ('B', 'Y') => 5,
                ('B', 'Z') => 9,
                ('C', 'X') => 7,
                ('C', 'Y') => 2,
                ('C', 'Z') => 6,
                _ => 0
            };
        }

        private char GetPlayerStrategy(char opponent, char output)
        {
            return (opponent, output) switch
            {
                ('A', 'X') => 'Z',
                ('A', 'Y') => 'X',
                ('A', 'Z') => 'Y',
                ('B', 'X') => 'X',
                ('B', 'Y') => 'Y',
                ('B', 'Z') => 'Z',
                ('C', 'X') => 'Y',
                ('C', 'Y') => 'Z',
                ('C', 'Z') => 'X',
                _ => ' '
            };
        }
    }
}