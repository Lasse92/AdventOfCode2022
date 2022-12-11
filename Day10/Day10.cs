namespace AdventOfCode2022
{
    public class Day10 : DailyChallenge
    {
        public override void Solve()
        {
            List<string> lines = File.ReadAllLines("Day10/input.txt").ToList();

            int cycles = 0;
            int register = 1;
            List<int> signals = new();
            foreach (string line in lines)
            {
                if (line.Equals("noop"))
                {
                    cycles++;
                    if (((cycles - 20) % 40 == 0 && cycles > 0) || cycles == 20)
                        signals.Add(cycles * register);
                }
                else
                {
                    int value = int.Parse(line.Split(' ')[1]);
                    if ((cycles - 19) % 40 == 0 || cycles + 1 == 20)
                        signals.Add((cycles + 1) * register);
                    cycles += 2;
                    if (((cycles - 20) % 40 == 0) || cycles == 20)
                        signals.Add(cycles * register);
                    register += value;
                }
            }

            int part1 = signals.Sum();
            PrintResults(part1);
        }
    }
}