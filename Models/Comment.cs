using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinSharkWebAPI.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public int? StockId { get; set; }
        public Stock? Stocks { get; set; }

    }
}