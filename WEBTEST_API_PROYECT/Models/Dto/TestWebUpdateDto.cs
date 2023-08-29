using System.ComponentModel.DataAnnotations;

namespace WEBTEST_API_PROYECT.Models.Dto
{
    public class TestWebUpdateDto
    {

        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public string Detail { get; set; }

        [Required]
        public double Fee { get; set; }

        [Required]
        public int Pages  { get; set; }

        [Required]
        public int SquareMeters { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public string Amenity { get; set; }
    }
}
