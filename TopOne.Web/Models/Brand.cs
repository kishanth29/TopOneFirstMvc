using System.ComponentModel.DataAnnotations;

namespace TopOne.Web.Models
{
    public class Brand
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        public string Name { get; set; }

        [Display(Name = "Established Year")]
        public int EstablishedYear { get; set; }

        [Display(Name = "Brand Logo")]
        public string BrandLogo { get; set; }
    }
}
