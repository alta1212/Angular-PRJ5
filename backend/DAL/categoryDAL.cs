using System;
using MODELS;
using DAL.Helper;
using System.Collections.Generic;
using DAL.Interface;
using System.Linq;

namespace DAL
{
    public class categoryDAL : DAL.Interface.iCategoryDAL
    {
        IDatabaseHelper databaseHelper;
        public categoryDAL(IDatabaseHelper k)
        {
             databaseHelper=k;
        }
        public List<Category> GetCategory()
        {
           string msgError = "";
            
            try 
            {
                 var dt = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "getCategory");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<Category>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}