using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Northwind.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public int CategoryID { get; set; }

        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        public byte[] Picture { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
