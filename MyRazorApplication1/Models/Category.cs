using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MyRazorApplication1.Models
{
    public class Category
    {       
            [Key]
            public int Id { get; set; }
            [Required]
            [MaxLength(30)]
            [DisplayName("Category Name")]
            public string? Name { get; set; }
            [DisplayName("Display order")]
            [Range(1, 100, ErrorMessage = "Display Order Must Be Between 1-100")]
            public int DisplayOrder { get; set; }
            //public DateTime CreatedTime { get; set; } = DateTime.Now;
       
    }
}
