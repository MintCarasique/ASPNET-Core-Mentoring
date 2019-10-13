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
        public string ProductName { get; set; }

        [Required]
        public int? SupplierID { get; set; }

        [Required]
        public int? CategoryID { get; set; }

        [Required]
        public string QuantityPerUnit { get; set; }

        [Required]
        public decimal? UnitPrice { get; set; }

        [Required]
        public short? UnitsInStock { get; set; }

        [Required]
        public short? UnitsOnOrder { get; set; }

        [Required]
        public short? ReorderLevel { get; set; }

        public bool Discontinued { get; set; }

        public Categories Category { get; set; }
        public Suppliers Supplier { get; set; }
    }
}
