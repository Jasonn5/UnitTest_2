using Library.Data.Models;

namespace Library.Data.Services
{
    public class BookService : IBookService
    {
        private readonly List<Book> books;
        public BookService() 
        {
            books = new List<Book>()
            {
                new Book()
                {
                    Id = new Guid("1b4076b1-fe31-4161-af58-6b3f69d93e55"),
                    Title= "Mananing Onselft",
                    Description= "We live in an age of unprecedented oppotunity",
                    Author= "Peter Ducker"
                },
                new Book()
                {
                    Id = new Guid("dc37b345-ba55-453e-814d-5c0ce7112892"),
                    Title= "SUPERPATATA, ENREDO COSMICO",
                    Description= "We live in an age of unprecedented oppotunity",
                    Author= "ARTUR LAPERLA"
                },
                new Book()
                {
                    Id = new Guid("195b0680-b518-4776-b42a-eddd2765af5b"),
                    Title= "¡GARFUNKEL EN DIRECTO! BITMAX 7",
                    Description= "We live in an age of unprecedented oppotunity",
                    Author= "JAUME COPONS RAMON"
                },
                new Book()
                {
                    Id = new Guid("0823e7e1-8d14-47ca-a87b-8197787cfe53"),
                    Title= "BLUE LOCK",
                    Description= "We live in an age of unprecedented oppotunity",
                    Author= "YUSUKE NOMURA"
                },
                new Book()
                {
                    Id = new Guid("f8bdb121-7417-4025-bbe1-c1e69a748f39"),
                    Title= "UN LOBO LLAMADO WANDER",
                    Description= "We live in an age of unprecedented oppotunity",
                    Author= "ROSANNE PARRY"
                }
            };
        }
        public Book Add(Book newBook)
        {
            this.books.Add(newBook);
            return newBook;
        }

        public IEnumerable<Book> GetAll()
        {
            var books = this.books.ToList();
            return books;
        }

        public Book GetById(Guid id)
        {
            var book = this.books.Where(b => b.Id == id).FirstOrDefault();

            return book;
        }

        public void Remove(Guid id)
        {
            var book = this.books.Where(b => b.Id == id).FirstOrDefault();
            this.books.Remove(book);
        }
    }
}
