using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stonks.Models
{
    abstract public class Person
    {
        [Key]
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }

    public class Portfolio
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("User")]
        public int userId { get; set; }
        public List<Share> listOfShares { get; set; }
        public List<Transaction> transactions { get; set; }
        public decimal cash { get; set; }

        public Portfolio(int userId, decimal cash)
        {
            this.userId = userId;
            this.cash = cash;
        }
    }

    public class Transaction
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("Portfolio")]
        public int portfolioId { get; set; }
        public decimal value { get; set; }
        public bool verified { get; set; }
        public decimal cash { get; set; }
        public decimal assets { get; set; }
    }

    public class Share
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("Portfolio")]
        public int portfolioId { get; set; }
        [ForeignKey("Stock")]
        public int stockId { get; set; }
        public int amount { get; set; }
        public decimal purchaseValue { get; set; }
        public decimal currentValue { get; set; }

    }
}
