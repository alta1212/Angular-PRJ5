using System.Reflection;
using System;
using System.Collections.Generic;
using System.Text;
using MODELS;
namespace BUS.Interface
{
     public partial interface iCategoryBUS
    {   
        List<Category> GetCategory();
    }
}