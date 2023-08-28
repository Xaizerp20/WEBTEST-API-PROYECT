using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEBTEST_API_PROYECT.Models
{
    public class TestWeb
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //auto increment
        public int Id { get; set; }
        public string Name { get; set; }

        public string Detail { get; set; }

        [Required]
        public double Fee { get; set; }

        public int Pages { get; set; }

        public int SquareMeters { get; set; }

        public string ImageUrl { get; set; }

        public string Amenity { get; set; }

        public DateTime DateCreation { get; set; }

        public DateTime DateUpdating { get; set; }
    }
}
