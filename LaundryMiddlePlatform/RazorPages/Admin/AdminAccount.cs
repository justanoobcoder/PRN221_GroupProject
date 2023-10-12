namespace RazorPages.Admin;

public sealed class AdminAccount
{
    public static string Username
    {
        get => GetAdminInfoFromJson("Username");
    }

    public static string Password
    {
        get => GetAdminInfoFromJson("Password");
    }

    public static bool IsAdmin(string username, string password)
    {
        return username == Username && password == Password;
    }

    private static string GetAdminInfoFromJson(string key)
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
        var info = config[$"AdminAccount:{key}"];
        return info is null ? throw new Exception($"Admin account info {key} is not found in appsettings.json") : info;
    }
}