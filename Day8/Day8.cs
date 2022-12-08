namespace AdventOfCode2022
{
    public class Day8 : DailyChallenge
    {
        public override void Solve()
        {
            List<string> lines = File.ReadAllLines("Day8/input.txt").ToList();
            int[][] matrix = lines.Select(s => s.ToArray().Select(c => (int)char.GetNumericValue(c)).ToArray()).ToArray();

            int visible = 0;
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    int[] column = Enumerable.Range(0, matrix.Length).Select(t => matrix[t][j]).ToArray();
                    if (j == 0 || matrix[i].Take(j).Max() < matrix[i][j])
                        visible++;
                    else if (j == matrix[i].Length -1 || matrix[i].Skip(j + 1).Max() < matrix[i][j])
                        visible++;
                    else if (i == 0 || column.Take(i).Max() < matrix[i][j])
                        visible++;
                    else if (i == matrix.Length - 1 || column.Skip(i + 1).Max() < matrix[i][j])
                        visible++;
                }
            }

            PrintResults(visible);
        }
    }
}