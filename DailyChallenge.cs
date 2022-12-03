namespace AdventOfCode2022
{
    public abstract class DailyChallenge
    {
        private string Day => GetType().Name;

        public abstract void Solve();
        
        protected void PrintResults(params object[] results)
        {
            Console.WriteLine($"{Day}:");
            for (int i = 0; i < results.Length; i++)
                Console.WriteLine($" => Part {i + 1}: {results[i]}");
            Console.WriteLine();
        }
    }
}