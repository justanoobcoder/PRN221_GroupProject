using RazorPages.Constants;

namespace RazorPages.Models;

public sealed class CurrentUser
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public Role Role { get; set; }
}
