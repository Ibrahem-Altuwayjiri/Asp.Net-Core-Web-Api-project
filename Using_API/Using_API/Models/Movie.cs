using System.ComponentModel.DataAnnotations;
using Web_API.Models;

namespace Using_API.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [MaxLength(250)]

        public string Title { get; set; }

        public int Year { get; set; }

        public double Rate { get; set; }

        public int GenreId { get; set; }

        public Genre Genre { get; set; }
    }
}
