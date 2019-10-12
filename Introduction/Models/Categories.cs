using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Introduction.Models
{
    public partial class Categories
    {
        public Categories()
        {
            Products = new HashSet<Products>();
        }

        [Key]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }

        public ICollection<Products> Products { get; set; }
    }
}
