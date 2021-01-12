using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestAppRichDomain.Services.Interfaces
{
    public interface IBasketService
    {
        Task AddItemToBasket(string login, int itemId, decimal price, int quantity = 1);
        Task IncreaseItemInBasket(string login, int itemId, decimal price);
        Task ClearBasket(string login);
    }
}
