using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEBTEST_API_PROYECT.Models
{
    public class NumberTestWeb
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TestWebNo { get; set; }

        [Required]
        public int TestWebId { get; set; }

        [ForeignKey("TestWebId")]
        public TestWeb testWeb { get; set; }


        public string SpecialDetails { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime UpdatingTime { get; set;}


    }
}
