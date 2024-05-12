using BookShop.Server.Data;
using BookShop.Shared.Entities;
using Generic.Repositories;

namespace BookShop.Server.Reopsitories
{
    public class BookRepository(AppDbContext context):BaseRepository<Book>(context)
    {
    }
    public class SoldBookRepository(AppDbContext context) : BaseRepository<SoldBook>(context)
    {
    }
}
