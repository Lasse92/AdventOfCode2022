namespace AdventOfCode2022
{
    public class Day8 : DailyChallenge
    {
        public override void Solve()
        {
            List<string> lines = File.ReadAllLines("Day8/input.txt").ToList();
            int[][] matrix = lines.Select(s => s.ToArray().Select(c => (int)char.GetNumericValue(c)).ToArray()).ToArray();

            int visible = 0;
            int highscore = 0;
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    int[] column = Enumerable.Range(0, matrix.Length).Select(t => matrix[t][j]).ToArray();
                    if (j == 0 || matrix[i].Take(j).Max() < matrix[i][j]) // left
                        visible++;
                    else if (j == matrix[i].Length -1 || matrix[i].Skip(j + 1).Max() < matrix[i][j]) // right
                        visible++;
                    else if (i == 0 || column.Take(i).Max() < matrix[i][j]) // up
                        visible++;
                    else if (i == matrix.Length - 1 || column.Skip(i + 1).Max() < matrix[i][j]) // down
                        visible++;
                    int[] scores = new int[4];
                    if (j > 0) // left
                    {
                        var treesVisible = matrix[i].Take(j).Reverse().TakeWhile(n => n < matrix[i][j]);
                        scores[0] = GetScore(matrix[i].Take(j), treesVisible);
                    }
                    if (j < matrix[i].Length - 1) // right
                    {
                        var treesVisible = matrix[i].Skip(j + 1).TakeWhile(n => n < matrix[i][j]);
                        scores[1] = GetScore(matrix[i].Skip(j + 1), treesVisible);
                    }
                    if (i > 0) // up
                    {
                        var treesVisible = column.Take(i).Reverse().TakeWhile(n => n < matrix[i][j]);
                        scores[2] = GetScore(column.Take(i), treesVisible);
                    }
                    if (i < matrix.Length - 1) // down
                    {
                        var treesVisible = column.Skip(i + 1).TakeWhile(n => n < matrix[i][j]);
                        scores[3] = GetScore(column.Skip(i + 1), treesVisible);
                    }
                    int score = scores.Aggregate((x, y) => x * y);
                    if (highscore < score)
                        highscore = score;
                }
            }

            PrintResults(visible, highscore);
        }

        private int GetScore(IEnumerable<int> treePath, IEnumerable<int> treesVisible)
        {
            return treesVisible.Count() + (treePath.Count() > treesVisible.Count() ? 1 : 0);
        }
    }
}