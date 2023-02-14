using AutoMapper;

namespace BookStoreApi.Models.Repositories
{
    public interface IBookRepository
    {
        public IEnumerable<BooksModel> GetBooks();
        public BookModel? GetBookById(int bookId);
        public bool DeleteBookById(int bookId);
        public bool UpdateBookById(int bookId, UpdateBookModel newBook);
        public bool AddBook(CreateBookModel newBook);
    }
}
