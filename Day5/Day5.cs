using System.Text.RegularExpressions;

namespace AdventOfCode2022
{
    public class Day5 : DailyChallenge
    {
        public override void Solve()
        {
            List<string> lines = File.ReadAllLines("Day5/input.txt").ToList();

            List<Stack> stacks = new();
            int index = lines.FindIndex(line => line.ElementAt(1) == '1');

            /* only works with a maximum of 9 stacks */
            for (int j = 1; j < lines[index].Length; j += 4)
            {
                Stack s = new();
                for (int k = index - 1; k >= 0 && char.IsLetter(lines[k].ElementAt(j)); k--)
                    s.Add(lines[k].ElementAt(j));
                stacks.Add(s);
            }

            for (int i = index + 2; i < lines.Count; i++)
            {
                int[] moves = Regex.Matches(lines[i], "[0-9]+").Select(m => int.Parse(m.Value)).ToArray();
                stacks[moves[1] - 1].MoveTo(stacks[moves[2] - 1], moves[0], true);
                stacks[moves[1] - 1].MoveTo(stacks[moves[2] - 1], moves[0], false);
            }
            string part1 = string.Join("", stacks.Select(s => s.Crates_FIFO.Last()));
            string part2 = string.Join("", stacks.Select(s => s.Crates.Last()));
            PrintResults(part1, part2);
        }
    }

    class Stack
    {
        public List<char> Crates { get; set; } = new();
        public List<char> Crates_FIFO { get; set; } = new();

        public void MoveTo(Stack dest, int amount, bool fifo)
        {
            if (fifo)
            {
                dest.Crates_FIFO.AddRange(Crates_FIFO.TakeLast(amount).Reverse());
                Crates_FIFO.RemoveRange(Crates_FIFO.Count - amount, amount);
            }
            else
            {
                dest.Crates.AddRange(Crates.TakeLast(amount));
                Crates.RemoveRange(Crates.Count - amount, amount);
            }
        }

        public void Add(char c)
        {
            Crates.Add(c);
            Crates_FIFO.Add(c);
        }
    }
}