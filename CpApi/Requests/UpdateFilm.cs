﻿using System.ComponentModel.DataAnnotations;

namespace CpApi.Requests
{
    public class UpdateFilm
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Genre { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Range(0, 5)]
        public decimal Rating { get; set; }
    }
}
