using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDataManager.Library.Internal.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMDataManager.Library.DataAccess
{
    public class ProductData : IProductData
    {
        private readonly ISqlDataAccess _sql;

        public ProductData( ISqlDataAccess sql)
        {
            _sql = sql;
        }
        public List<ProductModel> GetProducts()
        {
            var output = _sql.LoadData<ProductModel, dynamic>("sp_Product_GetAll", new { }, "TRMData1");

            return output;
        }


        public ProductModel GetProductById(int productid)
        {
            var output = _sql.LoadData<ProductModel, dynamic>("sp_Product_GetById", new { productid }, "TRMData1").FirstOrDefault();

            return output;
        }
    }
}
