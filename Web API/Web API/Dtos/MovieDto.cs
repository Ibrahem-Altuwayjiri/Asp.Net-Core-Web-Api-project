using System.ComponentModel.DataAnnotations;

namespace Web_API.Dtos
{
    public class MovieDto
    {
        [MaxLength(250)]

        public string Title { get; set; }

        public int Year { get; set; }

        public double Rate { get; set; }

        public byte GenreId { get; set; }
    }
}
