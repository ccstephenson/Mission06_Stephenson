using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mission06_Stephenson.Models;

namespace Mission06_Stephenson.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }
        
        public string? Title { get; set; }

        public string? Director { get; set; }
        
        [Range(1888, 2100, ErrorMessage = "Year must be between 1888 and the present.")]
        public int? Year { get; set; }
        
        public string? Rating { get; set; }

        public bool Edited { get; set; }

        public string? LentTo { get; set; } 
        
        public bool CopiedToPlex { get; set; }

        [StringLength(25)]
        public string? Notes { get; set; }
        
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        
    }
}