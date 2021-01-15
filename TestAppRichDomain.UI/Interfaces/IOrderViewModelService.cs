using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppRichDomain.UI.Models;

namespace TestAppRichDomain.UI.Interfaces
{
    public interface IOrderViewModelService
    {
        Task<List<OrderModel>> GetOrders(string username);
    }
}
