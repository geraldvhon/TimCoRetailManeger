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
    public class SaleData : ISaleData
    {
        private readonly IProductData _productData;
        private readonly ISqlDataAccess _sql;

        public SaleData(IProductData productData, ISqlDataAccess sql)
        {
            _productData = productData;
            _sql = sql;
        }

        public void SaveSale(SaleModel saleInfo, string casherId)
        {
            // TODO - Make this Solid/Dry/Better
            // Stary filling in the models we will save to the database
            List<SaleDetailDBModel> details = new List<SaleDetailDBModel>();

            var taxrate = ConfigHelper.GetTaxRate() / 100;

            foreach (var item in saleInfo.SaleDetails)
            {
                // Fillin the available information
                var detail = new SaleDetailDBModel
                {
                    ProductId = item.ProductID,
                    Quantity = item.Quantity
                };

                var productInfo = _productData.GetProductById(detail.ProductId);

                if (productInfo == null)
                {
                    throw new Exception($"The product id of { item.ProductID } cound not be found in the database.");
                }

                detail.PurchasePrice = (productInfo.RetailPrice * detail.Quantity);

                if (productInfo.IsTaxable)
                {
                    detail.Tax = (detail.PurchasePrice * taxrate);
                }
                details.Add(detail);
            }

            // Create the Sale Model
            SaleDbModel sale = new SaleDbModel
            {
                SubTotal = details.Sum(x => x.PurchasePrice),
                Tax = details.Sum(x => x.Tax),
                CasherID = casherId
            };

            sale.Total = sale.SubTotal + sale.Tax;

            // Save the Sale Model


            try
            {
                _sql.StartTransaction("TRMData1");
                _sql.SaveDataInTransaction<SaleDbModel>("sp_Sale_Insert", sale);

                // Get the Id from the Sale Model
                sale.Id = _sql.LoadDataInTransaction<int, dynamic>("sp_Sale_Lookup", new { sale.CasherID, sale.SaleDate }).FirstOrDefault();

                // Finish filling in the Sale Detail models
                foreach (var item in details)
                {
                    item.SaleId = sale.Id;

                    // Save the Sale Detail Models
                    _sql.SaveDataInTransaction("sp_SaleDetail_Insert", item);
                }

                _sql.CommitTransaction();

            }
            catch
            {
                _sql.RollbackTransaction();
                throw;
            }

        }

        public List<SaleReportModel> GetSaleReport()
        {

            var output = _sql.LoadData<SaleReportModel, dynamic>("spSale_Salereport", new { }, "TRMData1");

            return output;
        }




    }
}
