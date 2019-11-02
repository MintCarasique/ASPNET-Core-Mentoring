using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Northwind.Models;
using Northwind.Repositories;

namespace Northwind.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public void CreateCategory(Category category)
        {
            _categoryRepository.Create(category);
        }

        public byte[] GetCategoryImage(int id)
        {
            var picture = _categoryRepository.Get(id).Picture;
            if (picture != null)
            {
                using (var ms = new MemoryStream())
                {
                    const int offset = 78;
                    ms.Write(picture, offset, picture.Length - offset);
                    var bmp = new Bitmap(ms);
                    using (var bmpms = new MemoryStream())
                    {
                        bmp.Save(bmpms, System.Drawing.Imaging.ImageFormat.Bmp);
                        return bmpms.ToArray();
                    }
                }
            }
            return new byte[0];
        }

        public List<Category> GetAllCategories()
        {
            return _categoryRepository.GetAll();
        }

        public Category GetCategory(int id)
        {
            return _categoryRepository.Get(id);
        }

        public void UpdateCategory(Category category)
        {
            _categoryRepository.Update(category);
        }
    }
}
