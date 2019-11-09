using Stonks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Stonks.Controllers
{
    public class GeneratorController
    {
        private readonly Database _context;

        public GeneratorController(Database context)
        {
            _context = context;
        }

        public static void StartClock()
        {
            /*for (int i = 0; i < length; i++)
            {
                for (int i = 0; i < length; i++)
                {
                    AddValueAndTimestampToDatabase();
                }
            }*/
        }

        void AddValueAndTimestampToDatabase()
        {
            /*Time portfolio = new Portfolio(user.id, Settings.startingCash);
            _context.Add(portfolio);
            await _context.SaveChangesAsync();*/
        }



        // both 'last' and 'dependencies' are expected to be (ascending) ordered by id 
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
                else
                {
                    dict[dependency.targetID] = dependency.multiplier * 1.0;//curr.GrowthRate;
                }
            }
            return dict;
        }

    }
}
