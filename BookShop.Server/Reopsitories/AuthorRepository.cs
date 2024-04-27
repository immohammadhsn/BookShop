using BookShop.Server.Data;
using BookShop.Shared.Entities;
using Generic.Repositories;

namespace BookShop.Server.Reopsitories
{
    public class AuthorRepository(AppDbContext context) : BaseRepository<Author>(context)
    {
    }
}
