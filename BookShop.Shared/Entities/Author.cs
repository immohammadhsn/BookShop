using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BookShop.Shared.Entities;

public class Author
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    [AllowNull]
    public List<Book>? Books { get; set; }
}


