using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAppRichDomain.Core.Entries;
using Xunit;

namespace TestApp.IntegrationTests.Repository_Tests
{
    public class Repository_Basket_Tests : RepoTestFixture<Basket>
    {
        [Fact]
        public async Task Add_Test()
        {
            var repository = GetRepository();
            var item = new Basket("username");

            await repository.AddAsync(item);

            var newItem = (await repository.ListAllAsync()).First();

            Assert.Equal(item, newItem);
            Assert.True(newItem?.Id > 0);
        }
        [Fact]
        public async Task Update_Test()
        {
            var repository = GetRepository();
            var item = new Basket("username");
            await repository.AddAsync(item);
            var newItem = (await repository.ListAllAsync()).First();
            newItem.AddItem(1, 10);

            await repository.UpdateAsync(newItem);
            newItem = (await repository.ListAllAsync()).First();

            Assert.Equal(item, newItem);
            Assert.Equal(1, newItem.BasketItems.Count);
        }
        [Fact]
        public async Task Delete_Test()
        {
            string name = "username";
            var repository = GetRepository();
            var item = new Basket(name);
            await repository.AddAsync(item);

            await repository.DeleteAsync(item);

            Assert.DoesNotContain(await repository.ListAllAsync(),
                i => i.UserId == name);
        }
    }
}
