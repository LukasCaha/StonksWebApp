using Microsoft.AspNetCore.Mvc;
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
    public class GeneratorController : Controller
    {
        private readonly Database _context;

        public GeneratorController(Database context)
        {
            _context = context;

            Generate(LastIteration);
        }

        static List<Stock> LastIteration { get; set; }
        static int numberOfIteretions { get; set; }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task AddValueAndTimestampToDatabase(List<StockValueInTime> lastIteration)
        {
            foreach(StockValueInTime changedStock in lastIteration)
            {
                changedStock.timestamp = 1; //TODO pocitadlo tahu
                _context.Add(changedStock);
                await _context.SaveChangesAsync();
            }
        }



        public List<Stock> Generate(List<Stock> lastIteration)
        {
            var buffer = new List<Stock>();
            foreach (var stock in lastIteration)
            {
                var tmp = stock;
                for (int i = 0; i < numberOfIteretions; i++)
                {
                    tmp = Generator.RandomlyModify(tmp);
                }
               buffer.Add(tmp);
            }
            return buffer;
        }

        public async Task GetInitialData()
        {
            LastIteration = await _context.Stock.ToListAsync();
        }

        public StockValueInTime StockToTimeStock(Stock item)
        {
            return new StockValueInTime()
            {
                stockId = item.id,
                timestamp = 10,//TODO
                value = item.currentValue
            };
        }

        // both 'last' and 'dependencies' are expected to be (ascending)ordered by id 
        static Dictionary<int,double> PropagateDependencies(List<Stock> last, List<StockDependency> dependencies)
        {
            int i = 0;
            var curr = last[i];
            var dict = new Dictionary<int, double>();
            foreach (var dependency in dependencies)
            {
                while (curr.id != dependency.sourceID)
                {
                    if (i != last.Count - 1)
                    {
                        var tmp = curr;
                        curr = last[++i];
                        if (tmp.id > curr.id)
                            throw new Exception("stock-list is not ordered!");
                    }
                    else break;
                }
                if (dict.TryGetValue(dependency.targetID, out var value))
                {
                    dict[dependency.targetID] = value + dependency.multiplier * 1.0;//curr.GrowthRate;
                }
                else         //TODO
                {
                    dict[dependency.targetID] = dependency.multiplier * 1.0;//curr.GrowthRate;
                }
            }
            return dict;
        }

    }
}
