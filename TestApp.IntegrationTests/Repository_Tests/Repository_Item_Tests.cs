using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestAppRichDomain.Core.Entries;
using Xunit;

namespace TestApp.IntegrationTests.Repository_Tests
{
    public class Repository_Item_Tests : RepoTestFixture<Item>
    {

        [Fact]
        public async Task Add_Get_Items_Test()
        {
            var repository = GetRepository();
            await repository.AddAsync(new Item("qwe,", "descr", "qwe", 10, true));

            var items = await repository.ListAllAsync();

            Assert.NotNull(items);
        }
    }
}
