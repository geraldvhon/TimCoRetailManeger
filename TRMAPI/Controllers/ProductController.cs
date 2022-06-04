using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRMDataManager.Library.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Cashier, ADMIN")]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IProductData _productData;

        public ProductController(IConfiguration config, IProductData productData)
        {
            _config = config;
            _productData = productData;
        }

        [HttpGet]
        public IEnumerable<ProductModel> Get()
        {
            return _productData.GetProducts();
        }
    }
}
