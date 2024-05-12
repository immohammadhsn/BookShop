using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Shared.Entities
{
    public class SoldBook
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public BookStatus BookStatus { get; set; }
    }
    public class SoldBookDTO
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public BookStatus BookStatus { get; set; }
    }
}
