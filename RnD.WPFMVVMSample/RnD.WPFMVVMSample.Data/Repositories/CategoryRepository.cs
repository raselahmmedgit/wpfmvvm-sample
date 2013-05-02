using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RnD.WPFMVVMSample.Domain;

namespace RnD.WPFMVVMSample.Data
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(IDatabaseFactory iDatabaseFactory)
            : base(iDatabaseFactory)
        {
        }

    }

    public interface ICategoryRepository : IRepositoryBase<Category>
    {

    }
}
