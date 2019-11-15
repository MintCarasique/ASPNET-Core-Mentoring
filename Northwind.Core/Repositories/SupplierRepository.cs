using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Northwind.Core.Models;

namespace Northwind.Core.Repositories
{
    public class SupplierRepository : IRepository<Supplier>
    {
        private readonly NorthwindContext _dbContext;

        private readonly ILogger<SupplierRepository> _logger;

        public SupplierRepository(NorthwindContext context, ILogger<SupplierRepository> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        public List<Supplier> GetAll()
        {
            _logger.LogInformation("Getting all suppliers from database");
            return _dbContext.Suppliers.ToList();
        }

        public Supplier Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Supplier updatingObject)
        {
            throw new NotImplementedException();
        }

        public void Create(Supplier newObject)
        {
            throw new NotImplementedException();
        }
    }
}
