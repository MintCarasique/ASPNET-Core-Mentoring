using System.Collections.Generic;
using System.Linq;
using Introduction.Models;

namespace Introduction.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly NorthwindContext _dbContext;

        public SupplierService(NorthwindContext context)
        {
            _dbContext = context;
        }

        public List<Supplier> GetAllSuppliers()
        {
            return _dbContext.Suppliers.ToList();
        }
    }
}
