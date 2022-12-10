namespace AdventOfCode2022
{
    public class Day9 : DailyChallenge
    {
        public override void Solve()
        {
            List<string> lines = File.ReadAllLines("Day9/input.txt").ToList();
            Map map = new();

            foreach(string line in lines)
                map.Move((char)line.Split(' ')[0][0], int.Parse(line.Split(' ')[1]));

            int part1 = map.GetVisitedCount();
            PrintResults(part1);
        }


        private class Map
        {
            private int Tail_X { get; set; }
            private int Tail_Y { get; set; }
            private int Head_X { get; set; }
            private int Head_Y { get; set; }
            private List<(int, int)> Visited { get; init; } = new();

            public Map()
            {
                Visited.Add((0, 0));
            }

            public void Move(char direction, int steps)
            {
                for (int i = 0; i < steps; i++)
                {
                    if      (direction == 'L') Head_X--;
                    else if (direction == 'R') Head_X++;
                    else if (direction == 'U') Head_Y++;
                    else if (direction == 'D') Head_Y--;

                    foreach (char tailDirection in GetNextTailMoves())
                    {
                        if (tailDirection != 'W')
                        {
                            if (tailDirection == 'L')      Tail_X--;
                            else if (tailDirection == 'R') Tail_X++;
                            else if (tailDirection == 'U') Tail_Y++;
                            else if (tailDirection == 'D') Tail_Y--;
                        }
                    }
                    if (!Visited.Exists(v => v.Item1 == Tail_X && v.Item2 == Tail_Y))
                        Visited.Add((Tail_X, Tail_Y));
                }
            }

            public int GetVisitedCount() => Visited.Count;
            
            private char[] GetNextTailMoves()
            {
                // Horizontal + Vertical
                if (Tail_X + 2 == Head_X && Tail_Y == Head_Y)
                    return new char[] {'R'};
                if (Tail_X - 2 == Head_X && Tail_Y == Head_Y)
                    return new char[] {'L'};
                if (Tail_X == Head_X && Tail_Y + 2 == Head_Y)
                    return new char[] {'U'};
                if (Tail_X == Head_X && Tail_Y - 2 == Head_Y)
                    return new char[] {'D'};

                // Diagonal width
                if (Tail_X + 2 == Head_X && Tail_Y + 1 == Head_Y)
                    return new char[] { 'R', 'U' };
                if (Tail_X - 2 == Head_X && Tail_Y + 1 == Head_Y)
                    return new char[] { 'L', 'U' };
                if (Tail_X + 2 == Head_X && Tail_Y - 1 == Head_Y)
                    return new char[] { 'R', 'D' };
                if (Tail_X - 2 == Head_X && Tail_Y - 1 == Head_Y)
                    return new char[] { 'L', 'D' };

                // Diagonal height
                if (Tail_X + 1 == Head_X && Tail_Y + 2 == Head_Y)
                    return new char[] { 'R', 'U' };
                if (Tail_X - 1 == Head_X && Tail_Y + 2 == Head_Y)
                    return new char[] { 'L', 'U' };
                if (Tail_X + 1 == Head_X && Tail_Y - 2 == Head_Y)
                    return new char[] { 'R', 'D' };
                if (Tail_X - 1 == Head_X && Tail_Y - 2 == Head_Y)
                    return new char[] { 'L', 'D' };

                // Wait
                return new char[] { 'W' };
            }
        }
    }
}