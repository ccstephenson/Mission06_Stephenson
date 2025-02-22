using System.Collections.Generic;
using Mission06_Stephenson.Models;

namespace Mission06_Stephenson.Models
{
    public class Category
    {
        public int CategoryId { get; set; }  // Primary Key
        public string CategoryName { get; set; }  // Category name (e.g., Action, Comedy)

        public ICollection<Movie> Movies { get; set; }  // Navigation property
    }
}