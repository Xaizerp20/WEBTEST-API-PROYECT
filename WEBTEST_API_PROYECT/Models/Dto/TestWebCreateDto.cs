using System.ComponentModel.DataAnnotations;

namespace WEBTEST_API_PROYECT.Models.Dto
{
    public class TestWebCreateDto
    {

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public string Detail { get; set; }

        [Required]
        public double Fee { get; set; }

        public int Pages  { get; set; }

        public int SquareMeters { get; set; }

        public string ImageUrl { get; set; }

        public string Amenity { get; set; }
    }
}
