﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SampleProject1.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public string? Name { get; set; }
        [DisplayName("Dispaly order")]
        [Range(1, 100, ErrorMessage="Display Order Must Be Between 1-100")]
        public int DisplayOrder { get; set; }
        //public DateTime CreatedTime { get; set; } = DateTime.Now;
    }
}
