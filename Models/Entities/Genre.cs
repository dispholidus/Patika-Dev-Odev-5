using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreApi.Models.Entities
{
    public class Genre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GenreId { get; set; }
        public string GenreName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
