using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppRichDomain.UI.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public IEnumerable<OrderItemModel> OrderItems { get; set; }
    }
}
