using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.InMemory;
using System;
using TestAppRichDomain.Infrastructure;
using Xunit;
using TestAppRichDomain.Shared;

namespace TestApp.IntegrationTests
{
    public abstract class RepoTestFixture<T> where T : BaseEntity
    {
        protected SiteContext _dbContext;

        protected static DbContextOptions<SiteContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<SiteContext>();
            builder.UseInMemoryDatabase("testDB")
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        protected Repository<T> GetRepository()
        {
            var options = CreateNewContextOptions();

            _dbContext = new SiteContext(options);
            return new Repository<T>(_dbContext);
        }
    }
}
