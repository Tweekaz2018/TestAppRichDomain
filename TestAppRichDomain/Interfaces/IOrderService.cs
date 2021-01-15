using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestAppRichDomain.Core.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrderAsync(int basketId, string comment, string address);
    }
}
