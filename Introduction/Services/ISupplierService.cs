using System.Collections.Generic;
using Northwind.Models;

namespace Northwind.Services
{
    public interface ISupplierService
    {
        /// <summary>
        /// Get list with all suppliers
        /// </summary>
        /// <returns>List of suppliers</returns>
        List<Supplier> GetAllSuppliers();
    }
}
