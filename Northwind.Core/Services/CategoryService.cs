using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Northwind.Core.Models;
using Northwind.Core.Repositories;

namespace Northwind.Core.Services
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
                return GetImageBytesFromOleField(picture);
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

        private byte[] GetImageBytesFromOleField(byte[] oleFieldBytes)

        {
            const string bitmapIdBlock = "BM";
            const string jpgIdBlock = "\u00FF\u00D8\u00FF";
            const string pngIdBlock = "\u0089PNG\r\n\u001a\n";
            const string gifIdBlock = "GIF8";
            const string tiffIdBlock = "II*\u0000";

            byte[] imageBytes;

            // Get a UTF7 Encoded string version
            Encoding u8 = Encoding.UTF7;
            string strTemp = u8.GetString(oleFieldBytes);

            // Get the first 300 characters from the string
            string strVTemp = strTemp.Substring(0, 300);



            // Search for the block
            int iPos = -1;
            if (strVTemp.IndexOf(bitmapIdBlock, StringComparison.Ordinal) != -1)
                iPos = strVTemp.IndexOf(bitmapIdBlock, StringComparison.Ordinal);
            else if (strVTemp.IndexOf(jpgIdBlock, StringComparison.Ordinal) != -1)
                iPos = strVTemp.IndexOf(jpgIdBlock, StringComparison.Ordinal);
            else if (strVTemp.IndexOf(pngIdBlock, StringComparison.Ordinal) != -1)
                iPos = strVTemp.IndexOf(pngIdBlock, StringComparison.Ordinal);
            else if (strVTemp.IndexOf(gifIdBlock, StringComparison.Ordinal) != -1)
                iPos = strVTemp.IndexOf(gifIdBlock, StringComparison.Ordinal);
            else if (strVTemp.IndexOf(tiffIdBlock, StringComparison.Ordinal) != -1)
                iPos = strVTemp.IndexOf(tiffIdBlock, StringComparison.Ordinal);

            // From the position above get the new image
            if (iPos == -1)
                return oleFieldBytes;

            //Array.Copy(
            MemoryStream ms = new MemoryStream();
            ms.Write(oleFieldBytes, iPos, oleFieldBytes.Length - iPos);
            imageBytes = ms.ToArray();
            ms.Close();
            ms.Dispose();

            return imageBytes;

        }
    }
}
