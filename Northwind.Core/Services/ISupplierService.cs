using System.Collections.Generic;
using Northwind.Core.Models;

namespace Northwind.Core.Services
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
