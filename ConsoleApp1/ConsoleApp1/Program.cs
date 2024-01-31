var rawUsers = new List<RawUser>
{
    new() { Name = "John", Roles = "Admin; User; Dev; Boss" },
    new() { Name = "Jane", Roles = "User; Boss; Dev" },
    new() { Name = "Jack", Roles = "User; Dev;" },
    new() { Name = "Jill", Roles = "User; Dev;" },
};

var users = new List<User>();
foreach (var rawUser in rawUsers)
{
    var user = new User
    {
        Name = rawUser.Name,
        Roles = rawUser.Roles.Split(";").Select(r => r.Trim()).ToList()
    };
    users.Add(user);
}

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

Console.WriteLine($"Found {commonRoles.Count} common roles");

foreach (var user in users)
{
    var missingRoles = commonRoles.Except(user.Roles).ToList();
    Console.WriteLine($"{user.Name} is missing {missingRoles.Count} roles");
}

public class User
{
    public required string Name { get; set; }
    public required List<string> Roles { get; set; }
}

public class RawUser
{
    public required string Name { get; set; }
    public required string Roles { get; set; }
}