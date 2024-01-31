var users = new List<User>
{
    new User { Name = "John", Roles = new List<string> { "Admin", "User", "Dev", "Boss" } },
    new User { Name = "Jane", Roles = new List<string> { "User", "Boss", "Dev" } },
    new User { Name = "Jack", Roles = new List<string> { "User", "Dev" } },
    new User { Name = "Jill", Roles = new List<string> { "User", "Dev" } },
};

var commonRoles = users
    .SelectMany(u => u.Roles)
    .GroupBy(r => r)
    .Where(g => g.Count() == users.Count)
    .Select(g => g.Key)
    .ToList();

Console.WriteLine("Common roles:");
foreach (var role in commonRoles)
{
    Console.WriteLine(role);
}

await File.WriteAllLinesAsync("common-roles.txt", commonRoles);

public class User
{
    public required string Name { get; set; }
    public required List<string> Roles { get; set; }
}