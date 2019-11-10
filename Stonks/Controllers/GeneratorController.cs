using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stonks.Models;
using Stonks.Models.Persons;
using Stonks.Plugins.Database;
using Stonks.Plugins.Generator;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace Stonks.Controllers
{
    [Route("[controller]/[action]")]
    public class GeneratorController : Controller
    {
        private readonly Database _context;
        private readonly object _lock = new object();

        public class Request
        {

        } 



        private long Ticks { get; set; } = 0;

        List<Stock> LastIteration { get; set; }
        List<StockDependency> Dependencies { get; set; }
        int IterationsPerTicks { get; set; }
        //List<Stock>  { get; set; }

        Queue<Request> pendingRequests = new Queue<Request>();


        public GeneratorController(Database context)
        {
            _context = context;

            LastIteration = context.Stock.ToList();
            Dependencies = new List<StockDependency>();
            timer.Elapsed +=  async (a,e) => await OneTick();

        }

        Timer timer { get; } = new Timer(1000){Enabled  = false };

        async Task OneTick()
        {
            var next = Generate(LastIteration);
            await _context.AddRangeAsync(next);
            await _context.SaveChangesAsync();
            LastIteration = next;

        }

        // POST: Generator/BuyShares
        [HttpPost("{userID}/{stockID}/{amount}/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BuyShares(int userID, int stockID, int amount)
        {
            // TODO: delay this operation

            //pendingRequests.Enqueue()
            var user = _context.Users.Find(userID);
            var stock = _context.Stock.Find(stockID);
            
            if(amount > 0 && user.userPortfolio.cash >= stock.currentValue * amount)
            {
                var portfolio = user.userPortfolio;
                user.userPortfolio.cash -= stock.currentValue * amount;

                if (user.userPortfolio.listOfShares.Any(x => x.id == stockID))
                    user.userPortfolio.listOfShares.Find(x => x.id == stockID).amount += amount;
                else
                    user.userPortfolio.listOfShares.Add(new Share()
                    {
                        stockId = stockID,
                        amount = amount,
                        portfolioId = user.userPortfolio.id
                    });

                await _context.SaveChangesAsync();

                return Ok();
            }
            return BadRequest();

        }
        // POST: Generator/BuyShares
        [HttpPost("{userID}/{stockID}/{amount}/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SellShares(int userID, int stockID, int amount)
        {
            // TODO: delay this operation

            //pendingRequests.Enqueue()
            var user = _context.Users.Find(userID);
            var stock = _context.Stock.Find(stockID);

            if (amount > 0 && user.userPortfolio.listOfShares.Find(x => x.id == stockID).amount >= amount)
            {
                var portfolio = user.userPortfolio;
                user.userPortfolio.cash += stock.currentValue * amount;

                if (user.userPortfolio.listOfShares.Any(x => x.id == stockID))
                    user.userPortfolio.listOfShares.Find(x => x.id == stockID).amount += amount;
                else
                    user.userPortfolio.listOfShares.Add(new Share()
                    {
                        stockId = stockID,
                        amount = amount,
                        portfolioId = user.userPortfolio.id
                    });

                await _context.SaveChangesAsync();

                return Ok();
            }
            return BadRequest();

        }


        List<Stock> Generate(List<Stock> lastIteration)
        {
            var deps = PropagateDependencies(lastIteration, Dependencies);
            var buffer = new List<Stock>();
            foreach (var stock in lastIteration)
            {
                var tmp = stock;
                for (int i = 0; i < IterationsPerTicks; i++)
                {
                    tmp = Generator.RandomlyModify(tmp, deps[tmp.id]);
                }
               buffer.Add(tmp);
            }
            return buffer;
        }


        StockValueInTime StockToTimeStock(Stock item)
        {
            return new StockValueInTime()
            {
                stockId = item.id,
                timestamp = Ticks,
                value = item.currentValue
            };
        }

        // both 'last' and 'dependencies' are expected to be (ascending)ordered by id 
        Dictionary<int,double> PropagateDependencies(List<Stock> last, List<StockDependency> dependencies)
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
                    dict[dependency.targetID] = value + dependency.multiplier * curr.GrowthTrend;
                }
                else         
                {
                    dict[dependency.targetID] = dependency.multiplier * curr.GrowthTrend;
                }
            }
            return dict;
        }

    }
}
