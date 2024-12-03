using System.ComponentModel.DataAnnotations;

namespace TopOne.Web.Models
{
    public class Brand
    {
        [Key]
        public Guid id  { get; set; }
        [Required]
        public string? Name { get; set; }
        // = string.Empty;

        public int EstablishedYear { get; set; }

        public string? BrandLogo { get; set; }
        // 39:000

    }
}
