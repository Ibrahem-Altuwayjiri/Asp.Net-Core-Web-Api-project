using System.ComponentModel.DataAnnotations;

namespace Using_API.Models
{
    public class NewMovie
    {
        [MaxLength(250)]

        public string Title { get; set; }

        public int Year { get; set; }

        public double Rate { get; set; }

        public byte GenreId { get; set; }
    }
}
