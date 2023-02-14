using AutoMapper;
using BookStoreApi.DbOperations;
using BookStoreApi.Models.Entities;
using System.Net;

namespace BookStoreApi.Models.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly BookStoreDbContext _bookStoreDbContext;
        private readonly IMapper _mapper;

        public GenreRepository(BookStoreDbContext bookStoreDbContext, IMapper mapper)
        {
            _bookStoreDbContext = bookStoreDbContext;
            _mapper = mapper;
        }

        public bool AddGenre(GenreModel genreModel)
        {
            Genre? genre = _bookStoreDbContext.Genres.FirstOrDefault(g => g.GenreName == genreModel.GenreName);
            if (genre != null)
            {
                return false;
            }
            genre = _mapper.Map<Genre>(genreModel);

            _bookStoreDbContext.Add(genre);
            _bookStoreDbContext.SaveChanges();

            return true;
        }

        public bool DeleteGenreById(int genreId)
        {
            Genre? genre = _bookStoreDbContext.Genres.FirstOrDefault(g => g.GenreId == genreId);
            if (genre != null)
            {
                _bookStoreDbContext.Genres.Remove(genre);
                _bookStoreDbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public GenreModel? GetGenreById(int genreId)
        {
            Genre? genre = _bookStoreDbContext.Genres.FirstOrDefault(g => g.GenreId == genreId);

            if (genre != null)
            {
                GenreModel genreModel = _mapper.Map<GenreModel>(genre);
                return genreModel;
            }
            return null;
        }

        public IEnumerable<GenreModel> GetGenres()
        {
            IEnumerable<Genre> genres = _bookStoreDbContext.Genres.OrderBy(g => g.GenreId).ToList();
            List<GenreModel> genresModel = new();
            foreach (var genre in genres)
            {
                genresModel.Add(_mapper.Map<GenreModel>(genre));
            }
            return genresModel;
        }

        public bool UpdateGenreById(int genreId, GenreModel genreModel)
        {
            Genre? genre = _bookStoreDbContext.Genres.FirstOrDefault(b => b.GenreId == genreId);
            if (genre != null)
            {
                genre.GenreName = genreModel.GenreName != default ? genreModel.GenreName : genre.GenreName;
                _bookStoreDbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
    public class GenreModel
    {
        public string GenreName { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = true;
    }
}
