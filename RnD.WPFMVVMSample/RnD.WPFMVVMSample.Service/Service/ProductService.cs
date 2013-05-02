using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RnD.WPFMVVMSample.Data;
using RnD.WPFMVVMSample.Domain;

namespace RnD.WPFMVVMSample.Service
{
    public class ProductService : IProductService
    {
        #region Global Variable Declaration

        private readonly IProductRepository _iProductRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public ProductService(IProductRepository iProductRepository, IUnitOfWork iUnitOfWork)
        {
            this._iProductRepository = iProductRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        //public IQueryable<Product> GetProducts()
        //{
        //    var Products = _iProductRepository.GetAll();
        //    return Products.AsQueryable();
        //}

        public Product GetProduct(int id)
        {
            var Product = _iProductRepository.GetById(id);
            return Product;
        }

        public IQueryable<Product> GetAll()
        {
            var Products = _iProductRepository.GetAll();
            return Products.AsQueryable();
        }

        #endregion

        #region Create Method

        public void Create(Product Product)
        {
            _iProductRepository.Insert(Product);
            Save();
        }

        #endregion

        #region Update Method

        public void Update(Product Product)
        {
            _iProductRepository.Update(Product);
            Save();
        }

        #endregion

        #region Delete Method

        public void Delete(Product Product)
        {
            var deleteProduct = GetProduct(Product.ProductId);
            _iProductRepository.Delete(deleteProduct);
            Save();
        }

        #endregion

        #region Save By Commit

        public void Save()
        {
            _iUnitOfWork.Commit();
        }

        #endregion
    }

    public interface IProductService : IGeneric<Product>
    {
        //IQueryable<Product> GetProducts();
        Product GetProduct(int id);
    }

}
