using AutoMapper;
using BookStoreApi.Common;
using BookStoreApi.DbOperations;
using BookStoreApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Models.Repositories
{
    // CRUD operations for Book.
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreDbContext _bookStoreDbContext;
        private readonly IMapper _mapper;

        public BookRepository(BookStoreDbContext bookStoreDbContext, IMapper mapper)
        {
            _bookStoreDbContext = bookStoreDbContext;
            _mapper = mapper;
        }
        public bool AddBook(CreateBookModel createBookModel)
        {
            Book? book = _bookStoreDbContext.Books.FirstOrDefault(b => b.BookTitle == createBookModel.BookTitle);
            if (book != null)
            {
                return false;
            }
            Book newBook = _mapper.Map<Book>(createBookModel);

            _bookStoreDbContext.Books.Add(newBook);
            _bookStoreDbContext.SaveChanges();
            return true;
        }

        public bool DeleteBookById(int bookId)
        {
            Book? book = _bookStoreDbContext.Books.FirstOrDefault(b => b.BookId == bookId);
            if (book != null)
            {
                _bookStoreDbContext.Remove(book);
                _bookStoreDbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public BookModel? GetBookById(int bookId)
        {
            Book? book = _bookStoreDbContext.Books.Include(x => x.Genre).FirstOrDefault(b => b.BookId == bookId);
            if (book != null)
            {
                BookModel bookModel = _mapper.Map<BookModel>(book);
                return bookModel;
            }
            return null;

        }

        public IEnumerable<BooksModel> GetBooks()
        {
            IEnumerable<Book> books = _bookStoreDbContext.Books.Include(x => x.Genre).OrderBy(b => b.GenreId).ToList();
            List<BooksModel> booksViewModel = new();
            foreach (var book in books)
            {
                booksViewModel.Add(_mapper.Map<BooksModel>(book));
            }
            return booksViewModel;
        }

        public bool UpdateBookById(int bookId, UpdateBookModel newBook)
        {
            Book? book = _bookStoreDbContext.Books.FirstOrDefault(b => b.BookId == bookId);
            if (book != null)
            {
                book.BookTitle = newBook.BookTitle != default ? newBook.BookTitle : book.BookTitle;
                book.BookPublishDate = newBook.BookPublishDate != default ? newBook.BookPublishDate : book.BookPublishDate;
                book.GenreId = newBook.GenreId != default ? newBook.GenreId : book.GenreId;
                _bookStoreDbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
    public class BooksModel
    {
        public string BookTitle { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int BookPageCount { get; set; }
        public string BookPublishDate { get; set; } = string.Empty;
    }
    public class CreateBookModel
    {
        public string BookTitle { get; set; } = string.Empty;
        public int GenreId { get; set; }
        public int BookPageCount { get; set; }
        public DateTime BookPublishDate { get; set; }
    }
    public class BookModel
    {
        public string BookTitle { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int BookPageCount { get; set; }
        public string BookPublishDate { get; set; } = string.Empty;
    }
    public class UpdateBookModel
    {
        public string BookTitle { get; set; } = string.Empty;
        public int GenreId { get; set; }
        public int BookPageCount { get; set; }
        public DateTime BookPublishDate { get; set; }
    }

}
