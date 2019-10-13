using Introduction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Introduction.Services
{
    public interface ISupplierService
    {
        List<Suppliers> GetAllSuppliers();
    }
}
