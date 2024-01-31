var roles = new List<string>
{
    "Admin; User; Dev; Boss",
    "User; Boss; Dev",
    "User; Dev;",
    "User; Dev;",
};

var shared = roles
    .SelectMany(x => x.Split(";"))
    .Select(x => x.Trim())
    .GroupBy(x => x)
    .Where(x => x.Count() == roles.Count)
    .Select(x => x.Key)
    .ToList();

const string path = "common-roles.txt";
File.WriteAllLines(path, shared);
Console.WriteLine($"Saved {shared.Count} shared roles to:\n{Path.GetFullPath(path)}");
