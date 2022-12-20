using System.ComponentModel.DataAnnotations;

namespace Web_API.Dtos
{
    public class GenreDto
    {
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
