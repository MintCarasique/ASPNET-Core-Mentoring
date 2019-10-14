using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Introduction.Models
{
    public partial class Products
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }

        [Required]
        public int? SupplierID { get; set; }

        [Required]
        public int? CategoryID { get; set; }

        [Required]
        public string QuantityPerUnit { get; set; }

        [Range(0, 999999999)]
        public decimal? UnitPrice { get; set; }

        [Required]
        [Range(0, 999999)]
        public short? UnitsInStock { get; set; }

        [Required]
        [Range(0, 100)]
        public short? UnitsOnOrder { get; set; }

        [Required]
        [Range(0, short.MaxValue)]
        public short? ReorderLevel { get; set; }

        public bool Discontinued { get; set; }

        public Categories Category { get; set; }
        public Suppliers Supplier { get; set; }
    }
}
