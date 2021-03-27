using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testApplication.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public string Publisher { get; set; }
        public bool Sample { get; set; }
    }
}
