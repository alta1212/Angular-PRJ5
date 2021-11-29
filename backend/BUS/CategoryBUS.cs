using BUS.Interface;
using MODELS;
using DAL.Interface;
using System.Collections.Generic;

namespace BUS
{
    public class CategoryBUS : BUS.Interface.iCategoryBUS
    {
        private iCategoryDAL categoryDAl;
        public CategoryBUS(iCategoryDAL a)
        {
            categoryDAl=a;
        }

        List<Category> iCategoryBUS.GetCategory()
        {
            return categoryDAl.GetCategory();
        }
    }
}