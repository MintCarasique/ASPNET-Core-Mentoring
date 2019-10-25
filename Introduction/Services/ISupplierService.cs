using System.Collections.Generic;
using Northwind.Models;

namespace Northwind.Services
{
    public interface ISupplierService
    {
        List<Supplier> GetAllSuppliers();
    }
}
