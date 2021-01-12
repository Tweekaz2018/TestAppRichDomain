using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestAppRichDomain.Core.Entries
{
    public class Order : BaseEntity
    {
        public string UserId { get; private set; }
        public OrderDevileryType OrderDevileryType { get; private set; }
        public DateTime OrderDate { get; private set; } = DateTime.Now;

        public string Address { get; private set; }
        public string Comment { get; private set; }

        private readonly List<OrderItem> _orderItems = new List<OrderItem>();
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        private Order()
        {
            //Ef
        }

        public Order(string userId, OrderDevileryType orderDevileryType, string address, string comment, List<OrderItem> orderItems)
        {
            UserId = userId;
            Comment = comment;
            OrderDevileryType = orderDevileryType;
            Address = address;
            _orderItems = orderItems;
        }

        public decimal OrderTotal => _orderItems.Sum(x => x.Price * x.Quantity);

    }
}
