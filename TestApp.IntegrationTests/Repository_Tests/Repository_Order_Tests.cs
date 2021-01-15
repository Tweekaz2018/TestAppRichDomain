using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestAppRichDomain.Core.Entries;
using TestAppRichDomain.Core.Specification;
using Xunit;

namespace TestApp.IntegrationTests
{
    public class Repository_Order_Tests : RepoTestFixture<Order>
    {
        [Fact]
        public async Task Get_Order_By_Id_Test()
        {
            var repository = GetRepository();
            var order = new Order("username", "", "", new List<OrderItem>());
            await repository.AddAsync(order);

            var orderDB = await repository.GetByIdAsync(order.Id);

            Assert.Equal(order, orderDB);
        }

        [Fact]
        public async Task Get_Order_First_Or_Default_Test()
        {
            string username = "username";
            var repository = GetRepository();
            var order = new Order(username, "", "", new List<OrderItem>());
            var order1 = new Order("qwe", "", "", new List<OrderItem>());
            await repository.AddAsync(order);
            await repository.AddAsync(order1);

            var spec = new CustomerOrdersWithItemsSpecification(username);
            var orderDB = await repository.FirstOrDefaultAsync(spec);

            Assert.Equal(order, orderDB);
        }
    }
}
