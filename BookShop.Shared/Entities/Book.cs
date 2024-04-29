using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace BookShop.Shared.Entities;

public class Book
{
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public Author? Author { get; set; }
    public string Title { get; set; }
    public string Img { get; set; }
    public string Discription { get; set; }
    public double Price { get; set; }
    public double Discount { get; set; }

    [NotMapped]
    public double PriceAfterDiscount { get; set; }
}

public class BookDTO
{
    public Guid AuthorId { get; set; }
    public string? Title { get; set; }
    public string? Img { get; set; }
    public string? Discription { get; set; }
    public double Price { get; set; }
    public double Discount { get; set; }


    [NotMapped]
    public double PriceAfterDiscount { get; set; }
}
