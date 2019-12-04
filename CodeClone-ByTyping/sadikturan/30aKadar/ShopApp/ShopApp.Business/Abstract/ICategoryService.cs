using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.Business.Abstract
{
   public interface ICategoryService
    {
        List<Category> GetAll();
        void Create(Category entity);
        void Update(Category entity);
        void Delete(Category entity);
        Category GetById(int id);//k25
        Category GetByIdWithProducts(int id);//k26 
        void DeleteFromCategory(int categoryId, int productId);
    }
}
