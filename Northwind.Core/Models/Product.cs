using System.ComponentModel.DataAnnotations;

namespace Northwind.Core.Models
{
    public partial class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Required]
        [Display(Name = "Supplier")]
        public int? SupplierID { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int? CategoryID { get; set; }

        [Required]
        [Display(Name = "Quantity per Unit")]
        public string QuantityPerUnit { get; set; }

        [Range(0, 999999999)]
        [Display(Name = "Unit Price")]
        public decimal? UnitPrice { get; set; }

        [Required]
        [Range(0, 999999)]
        [Display(Name = "Units In Stock")]
        public short? UnitsInStock { get; set; }

        [Required]
        [Range(0, 100)]
        [Display(Name = "Units On Order")]
        public short? UnitsOnOrder { get; set; }

        [Required]
        [Range(0, short.MaxValue)]
        [Display(Name = "Reorder Level")]
        public short? ReorderLevel { get; set; }

        [Display(Name = "Discontinued")]
        public bool Discontinued { get; set; }

        public Category Category { get; set; }
        public Supplier Supplier { get; set; }
    }
}
