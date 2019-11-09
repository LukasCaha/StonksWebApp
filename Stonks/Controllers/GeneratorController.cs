using Microsoft.EntityFrameworkCore;
using Stonks.Models;
using Stonks.Plugins.Database;
using Stonks.Plugins.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stonks.Controllers
{
    public class GeneratorController
    {
        static List<StockValueInTime> LastIteration { get; set; }
        static int numberOfIteretions { get; set; }

        private readonly Database _context;

        public GeneratorController(Database context)
        {
            _context = context;
        }

        public void Clock()
        {
            //Generate(LastIteration);
        }

        void AddValueAndTimestampToDatabase()
        {
            /*Time portfolio = new Portfolio(user.id, Settings.startingCash);
            _context.Add(portfolio);
            await _context.SaveChangesAsync();*/
        }

        public void Generate(List<StockValueInTime> lastIteration)
        {
            LastIteration.Clear();
            foreach (StockValueInTime stock in lastIteration)
            {
                StockValueInTime iteratedStock = stock;

                for (int i = 0; i < numberOfIteretions; i++)
                {
                    iteratedStock = Generator.RandomlyModify(iteratedStock);
                }
               LastIteration.Add(iteratedStock);
            }
        }

        public async Task GetInitialData()
        {
            List<Stock> initialStocks = await _context.Stock.ToListAsync();
            LastIteration = new List<StockValueInTime>();
            foreach (var stock in initialStocks)
            {
                LastIteration.Add(new StockValueInTime { stockId = stock.id, value = stock.currentValue });
            }

        }
    }
}
