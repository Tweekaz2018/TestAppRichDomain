using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestAppRichDomain.Core.Interfaces
{
    public interface IBasketService
    {
        Task AddItem(string userId, int itemId, int quantity = 1);
    }
}
