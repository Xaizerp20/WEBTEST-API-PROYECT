using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WEBTEST_API_PROYECT.Models.Dto
{
    public class NumberTestWebDto
    {

        [Required]
        public int TestWebNo { get; set; }

        [Required]
        public int TestWebId { get; set; }

        public string SpecialDetails { get; set; }

    }
}
