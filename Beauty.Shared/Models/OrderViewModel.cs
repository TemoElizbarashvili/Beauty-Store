using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beauty.Shared.Models
{
    public class OrderViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
        public string Status { get; set; }
    }
}
