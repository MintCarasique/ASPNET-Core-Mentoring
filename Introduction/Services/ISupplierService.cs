using Introduction.Models;
using System.Collections.Generic;

namespace Introduction.Services
{
    public interface ISupplierService
    {
        List<Supplier> GetAllSuppliers();
    }
}
