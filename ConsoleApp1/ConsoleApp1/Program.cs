var rawRoles = new List<RawUser>
{
    new("Admin; User; Dev; Boss"),
    new("User; Boss; Dev"),
    new("User; Dev;"),
    new("User; Dev;"),
};

var parsedRoles = new List<User>();
foreach (var rawUser in rawRoles)
{
    var roles = rawUser.Roles
        .Split(";")
        .Select(r => r.Trim())
        .ToList();

    parsedRoles.Add(new User(roles));
}

var commonRoles = parsedRoles
    .SelectMany(u => u.Roles)
    .GroupBy(r => r)
    .Where(g => g.Count() == parsedRoles.Count)
    .Select(g => g.Key)
    .ToList();

await File.WriteAllLinesAsync("common-roles.txt", commonRoles);
Console.WriteLine($"Saved {commonRoles.Count} common roles to:\n{Path.GetFullPath("common-roles.txt")}");

public record RawUser(string Roles);

public record User(List<string> Roles);