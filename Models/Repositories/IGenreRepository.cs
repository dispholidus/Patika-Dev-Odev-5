using AutoMapper;

namespace BookStoreApi.Models.Repositories
{
    public interface IGenreRepository
    {
        public IEnumerable<GenreModel> GetGenres();
        public GenreModel? GetGenreById(int genreId);
        public bool DeleteGenreById(int genreId);
        public bool UpdateGenreById(int genreId, GenreModel genreModel);
        public bool AddGenre(GenreModel genreModel);
    }
}
