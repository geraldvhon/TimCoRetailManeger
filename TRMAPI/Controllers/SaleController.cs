using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TRMDataManager.Library.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SaleController : ControllerBase
    {
        
        private readonly ISaleData _saleData;

        public SaleController( ISaleData saleData)
        {           
            _saleData = saleData;
        }


        [Authorize(Roles = "Cashier")]
        [HttpPost]
        public void Post(SaleModel sale)
        {
            //Console.WriteLine(sale);
            
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // old-way RequestContext.Principal.Identity.GetUserId();

            _saleData.SaveSale(sale, userId);
        }

        [Authorize(Roles = "Admin, Manager")]
        [Route("GetSalesReport")]
        [HttpGet]
        public List<SaleReportModel> GetSalesReport()
        {
            //if (RequestContext.Principal.IsInRole("Admin"))
            //{
            //    //Do Admin Stuff
            //}
            //else if (RequestContext.Principal.IsInRole("Manager"))
            //{
            //    //Do Manager Stuff
            //}

            
            return _saleData.GetSaleReport();
        }
    }
}
