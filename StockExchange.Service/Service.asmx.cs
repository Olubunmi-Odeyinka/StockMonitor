using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Accessibility;
using Newtonsoft.Json;
using StockExchange.Auth.Helpers;
using StockExchange.Data;
using StockExchange.Domain;
using StockExchange.Domain.ViewModels;
using StockExchange.Repo;

namespace StockExchange.Service
{
    /// <summary>
    /// Summary description for Service
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Service : System.Web.Services.WebService
    {
       

        [WebMethod]
        public string LiveStock(string token)
        { 
            
            var authHeader = new AuthHelper();

            if (token == null)
                return "You are not authorized to call this Service";

            string userName;
            if (!authHeader.ValidateToken(token, out userName))
                return "Your token is no longer valid reload your page ";

            var freshToken = authHeader.GenerateReFreshToken(token);

            var userStockRepo = new GenericRepository<UserStock>(new StockContext());
            var userStocks = userStockRepo.FindByInclude(c => c.UserName == userName).ToList();
            var userStockCompanyIds = userStocks.Select(c => c.CompanyId);
            var activeStockRepo = new GenericRepository<ActiveStock>(new StockContext());
            var userActiveStock = activeStockRepo.FindByInclude(c => userStockCompanyIds.Contains(c.CompanyId), t => t.Company).ToList();


            var data = userActiveStock.Select(c => new ActiveStockViewModel
                {
                   TicketSymbol = c.Company.TicketSymbol,
                   PriceTraded = c.PriceTraded,
                   Id = c.Id,
                   PreviousDayPriceTraded = c.PreviousDayPriceTraded,
                   RateChange = c.PriceTraded-c.PreviousDayPriceTraded,
                   Volume = c.Volume
                   
                }).ToList();
            
            var dataString =  JsonConvert.SerializeObject(new { AuthToken = freshToken, Data = data});

            PriceUpdateInitiator instance = new PriceUpdateInitiator();
            instance.DecideAndUpdatePrice();

            return dataString;
        }
    }
}
