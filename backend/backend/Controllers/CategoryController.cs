using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

using System.Text;
using Slugify;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Google.Authenticator;
using MODELS;
using Tool;
using BUS.Interface;
using Newtonsoft.Json;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private iCategoryBUS iCategoryBUS;
        private IConfiguration config;
        public CategoryController (iCategoryBUS k,IConfiguration _config)
        {
            config=_config;
            iCategoryBUS=k;
        }
        [Route("getAllCategory")]
        [HttpGet]
        public List<Category> getAll()
        {
            return iCategoryBUS.GetCategory();
          
        }
    }
}