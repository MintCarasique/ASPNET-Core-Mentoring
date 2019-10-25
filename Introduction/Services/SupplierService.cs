using System.Collections.Generic;
using System.Linq;
using Northwind.Models;
using Northwind.Repositories;

namespace Northwind.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IRepository<Supplier> _supplierRepository;

        public SupplierService(IRepository<Supplier> supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public List<Supplier> GetAllSuppliers()
        {
            return _supplierRepository.GetAll();
        }
    }
}
