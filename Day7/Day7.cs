namespace AdventOfCode2022
{
    public class Day7 : DailyChallenge
    {
        public override void Solve()
        {
            List<string> lines = File.ReadAllLines("Day7/input.txt").ToList();
            Folder root = new("/");
            root.Run(lines);

            long part1 = root.AllSubdirs.Where(f => f.Size <= 100000)
                                       .Sum(f => f.Size);

            long missing = 30000000 - (70000000 - root.Size);
            long part2 = root.AllSubdirs.Where(f => f.Size >= missing)
                                       .OrderBy(f => f.Size)
                                       .First().Size;

            PrintResults(part1, part2);
        }
    }

    class Folder
    {
        public string Name { get; private set; }
        public List<Datafile> Files { get; init; } = new();
        public List<Folder> Subdirs { get; init; } = new();
        public List<Folder> AllSubdirs => new List<Folder>() { this }.Concat(Subdirs.SelectMany(s => s.AllSubdirs)).ToList();

        public Folder(string name) => Name = name;

        public List<string> Run(List<string> lines)
        {
            int i;
            for (i = 2; i < lines.Count && !lines[i].StartsWith('$'); i++)
            {
                if (lines[i].StartsWith("dir"))
                    Subdirs.Add(new(lines[i].Split("dir ")[1]));
                else
                    Files.Add(new(lines[i].Split(' ')[1], int.Parse(lines[i].Split(' ')[0])));
            }

            lines = lines.Skip(i).ToList();
            while (lines.Count > 0 && !lines[0].Equals("$ cd .."))
            {
                if (lines[0].StartsWith("$ cd"))
                {
                    string name = lines[0].Split("$ cd ")[1];
                    Folder dir = Subdirs.Find(s => s.Name.Equals(name))!;
                    lines = dir.Run(lines);
                }
            }
            return lines.Skip(1).ToList();
        }

        public long Size => Files.Sum(f => f.Size) + Subdirs.Sum(s => s.Size);
    }

    class Datafile
    {
        public string Name { get; init; }
        public long Size { get; init; }

        public Datafile(string name, int size)
        {
            Name = name;
            Size = size;
        }
    }
}