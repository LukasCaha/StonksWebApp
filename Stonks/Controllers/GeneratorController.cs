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
    }
}
