using System.Collections.Generic;
using MODELS;
namespace DAL.Interface
{
    public partial interface iCategoryDAL
    {
        List<Category>  GetCategory();
    }
}