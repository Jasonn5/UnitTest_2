using Library.Data.Models;

namespace Library.Data.Services
{
    public interface IBookService
    {
        IEnumerable<Book> GetAll();

        Book Add(Book newBook);

        Book GetById(Guid id);

        void Remove(Guid id);
    }
}
