﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class ProductDto
    {

        [Required, MaxLength(100)]
        public string Name { get; set; } = "";

        [Required, MaxLength(100)]
        public string Brand { get; set; } = "";

        [Required, MaxLength(100)]
        public string Category { get; set; } = "";

        [Required]
        public string Price { get; set; }

        public string? Description { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}
