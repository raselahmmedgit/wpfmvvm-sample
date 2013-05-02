using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RnD.WPFMVVMSample.Data;
using RnD.WPFMVVMSample.Domain;

namespace RnD.WPFMVVMSample.Service
{
    public class CategoryService : ICategoryService
    {
        #region Global Variable Declaration

        private readonly ICategoryRepository _iCategoryRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public CategoryService(ICategoryRepository iCategoryRepository, IUnitOfWork iUnitOfWork)
        {
            this._iCategoryRepository = iCategoryRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        //public IQueryable<Category> GetCategorys()
        //{
        //    var Categorys = _iCategoryRepository.GetAll();
        //    return Categorys.AsQueryable();
        //}

        public Category GetCategory(int id)
        {
            var Category = _iCategoryRepository.GetById(id);
            return Category;
        }

        public IQueryable<Category> GetAll()
        {
            var Categorys = _iCategoryRepository.GetAll();
            return Categorys.AsQueryable();
        }

        #endregion

        #region Create Method

        public void Create(Category Category)
        {
            _iCategoryRepository.Insert(Category);
            Save();
        }

        #endregion

        #region Update Method

        public void Update(Category Category)
        {
            _iCategoryRepository.Update(Category);
            Save();
        }

        #endregion

        #region Delete Method

        public void Delete(Category Category)
        {
            var deleteCategory = GetCategory(Category.CategoryId);
            _iCategoryRepository.Delete(deleteCategory);
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

    public interface ICategoryService : IGeneric<Category>
    {
        //IQueryable<Category> GetCategorys();
        Category GetCategory(int id);
    }

}
