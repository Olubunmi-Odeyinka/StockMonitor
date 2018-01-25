using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using StockExchange.Auth.Helpers;
using StockExchange.Auth.Interfaces;
using StockExchange.Domain;
using StockExchange.Repo;

namespace StockExchange.Web.Controllers
{
    [Authorize]
    public class ActiveStocksController : Controller
    {
        private IConfigurationReader _configurationReader;
        public ActiveStocksController(IConfigurationReader configurationReader)
        {
            _configurationReader = configurationReader;
        }

        public ActionResult Live()
        {
            var userName = ClaimsPrincipal.Current.Identity.Name;
            var tokenGen = new TokenManager(_configurationReader);
            var cipherName = StringCipher.Encrypt(userName, AuthConstants.Password);
            var newToken = tokenGen.GenerateNewTokenWithExpiryData(cipherName);
            return View("Live", model:newToken);
        }

      
    }
}
