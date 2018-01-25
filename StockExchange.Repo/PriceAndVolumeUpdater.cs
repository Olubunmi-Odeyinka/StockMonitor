using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using StockExchange.Data;
using StockExchange.Domain;

namespace StockExchange.Repo
{
  public class PriceAndVolumeUpdater
  {

        private int  GeneratePrice()
        {
            Random rnd = new Random();
            return rnd.Next(700, 1000);
        }
        private int GenerateVolume()
        {
            Random rnd = new Random();
            return rnd.Next(10000, 1000000);
        }

        public void UpdatePrices()
        {
            GenericRepository<ActiveStock> genericRepository = new GenericRepository<ActiveStock>(new StockContext());
            var activeStock  = genericRepository.All();
            foreach (var activeStockItem in activeStock)
            {
                activeStockItem.PriceTraded = GeneratePrice();
                activeStockItem.Volume = GenerateVolume();

                genericRepository.Update(activeStockItem);
            }
        }
    }
}