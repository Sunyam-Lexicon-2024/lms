using Microsoft.AspNetCore.Identity;

namespace LMS.Core.Entities;

public abstract class User : IdentityUser
{
    public string Name { get; set; }
    public ICollection<Document> Documents { get; } = [];
}