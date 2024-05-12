using BookShop.Shared.Entities;
using BookShop.Server.Reopsitories;
using Generic.Controllers;
using Generic.Repositories;
using BookShop.Shared;

namespace BookShop.Server.Controllers
{
    public class BookController (IBaseRepository<Book> baseRepository):BaseController<Book,BookDTO>(baseRepository)
    {
    }
    public class SoldBookController(IBaseRepository<SoldBook> baseRepository) : BaseController<SoldBook, SoldBookDTO>(baseRepository)
    {
    }
    public class AuthorController(IBaseRepository<Author> baseRepository) : BaseController<Author,AuthorDTO>(baseRepository)
    {
    }
}
