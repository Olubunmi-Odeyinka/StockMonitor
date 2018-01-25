using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using StockExchange.Auth.Helpers;
using StockExchange.Auth.Helpers.Model;
using StockExchange.Repo;

namespace StockExchange.Service
{
    public class PriceUpdateInitiator 
    {
      public void DecideAndUpdatePrice()
        {
            //Decide to use this instead of windows service
            //If random value is 3 OR 2 then update the price.
            Random rnd = new Random();
            var value = rnd.Next(1, 3);
            if (value == 3 || value == 2)
            {
                UpdatePrice();
            }
        }

        public void UpdatePrice()
        {
           var priceUpdator = new PriceAndVolumeUpdater();
            priceUpdator.UpdatePrices();
        }
    }
}