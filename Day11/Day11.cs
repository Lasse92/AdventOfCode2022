using System.Numerics;

namespace AdventOfCode2022
{
    public class Day11 : DailyChallenge
    {
        public override void Solve()
        {
            List<string> lines = File.ReadAllLines("Day11/input.txt").ToList();

            for (int i = 0, j = 0; i < lines.Count; i = j + 1)
            {
                if (lines[i].StartsWith("Monkey "))
                {
                    for (int x = 0; x < 2; x++)
                    {
                        Monkey monkey = new(x == 0);
                        for (j = i; j < lines.Count && lines[j].Length > 0; j++)
                        {
                            if (lines[j].Contains("Starting items:"))
                                monkey.SetItems(lines[j].Split("Starting items: ")[1]);
                            else if (lines[j].Contains("Operation:"))
                                monkey.SetOperation(lines[j].Split("Operation: new = old ")[1]);
                            else if (lines[j].Contains("Test:"))
                                monkey.SetTest(lines.Skip(j).Take(3).Select(s => int.Parse(s.Substring(s.LastIndexOf(' ') + 1))).ToArray());
                        }
                    }
                }
            }

            for (int i = 0; i < 20; i++)
                Monkey.MonkeyList.Where(m => m.Relief).ToList().ForEach(m => m.DoTurn());

            for (int i = 0; i < 10000; i++)
                Monkey.MonkeyList.Where(m => !m.Relief).ToList().ForEach(m => m.DoTurn());

            long[] mBusiness = new[] {true, false} // part1, part2
                                        .Select(b => (Monkey.MonkeyList.Where(m => m.Relief == b)
                                        .OrderByDescending(m => m.Inspections)
                                        .Select(m => m.Inspections)
                                        .Take(2)
                                        .Aggregate((x, y) => x * y)))
                                        .ToArray();

            PrintResults(mBusiness[0], mBusiness[1]);
        }

        private class Monkey
        {
            public static List<Monkey> MonkeyList{ get; private set; } = new();
            private static int Mod { get; set; } = 1;
            private List<long> Items { get; init; } = new();
            private (char, int?) Operation { get; set; }
            private int[] Test { get; set; } = default!;
            public long Inspections { get; private set; }
            public bool Relief { get; init; }
            
            public Monkey(bool relief)
            {
                Relief = relief;
                MonkeyList.Add(this);
            }

            public void SetItems(string items)
            {
                Items.AddRange(items.Split(", ").Select(s => long.Parse(s)));
            }

            public void SetOperation(string operation)
            {
                char op = operation.Split(' ')[0].ElementAt(0);
                string value = operation.Split(' ')[1];
                Operation = (op, int.TryParse(value, out int val) ? val : null); // "old" -> null
            }

            public void SetTest(int[] throwTo)
            {
                Test = throwTo;
                if (Relief)
                    Mod *= Test[0];
            }

            public void DoTurn()
            {
                foreach (long item in Items.ToArray())
                {
                    long worry = Operation switch
                    {
                        ('*', null) => (item * item),
                        ('+', null) => item + item,
                        ('*', _)    => item * (int)Operation.Item2,
                        ('+', _)    => item + (int)Operation.Item2,
                        _           => throw new InvalidOperationException()
                    } / (Relief ? 3 : 1);
                    int to = worry % Test[0] == 0 ? Test[1] : Test[2];
                    MonkeyList.Where(m => m.Relief == Relief)
                              .ElementAt(to).Items.Add(worry % Mod);
                    Items.RemoveAt(0);
                    Inspections++;
                }
            }
        }
    }
}