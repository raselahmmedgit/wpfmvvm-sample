using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RnD.WPFMVVMSample.Domain;

namespace RnD.WPFMVVMSample.Data
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IDatabaseFactory iDatabaseFactory)
            : base(iDatabaseFactory)
        {
        }

    }

    public interface IProductRepository : IRepositoryBase<Product>
    {

    }
}
