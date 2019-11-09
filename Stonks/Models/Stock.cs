using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stonks.Models
{
    public class Stock
    {
        [Key]
        public int id { get; set; }
        public string company { get; set; }
        public string stockCode { get; set; }
        public string description { get; set; }
        public decimal currentValue { get; set; }
        public double GrowthTrend { get; set; }
        public List<StockValueInTime> history { get; set; }
    }

    public class StockValueInTime
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("Stock")]
        public int stockId { get; set; }
        public decimal value { get; set; }
        public int timestamp { get; set; }
    }

    class StockDependency
    {
        public int sourceID { get; set; }
        public int targetID { get; set; }
        public double multiplier { get; set; }



    }
}
