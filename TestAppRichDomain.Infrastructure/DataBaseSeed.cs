using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestAppRichDomain.Core.Entries;

namespace TestAppRichDomain.Infrastructure
{
    public class DataBaseSeed
    {
        public static async Task SeedIdentityAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminUserName = "admin@candyshop.tk";
            string adminRole = "Admin";
            var result = await roleManager.CreateAsync(new IdentityRole(adminRole));
            var adminUser = new IdentityUser { UserName = adminUserName, Email = "admin@candyshop.tk" };
            var regResult = await userManager.CreateAsync(adminUser,  "123qwe123QWE@");
            adminUser = await userManager.FindByNameAsync(adminUserName);
            await userManager.AddToRoleAsync(adminUser, adminRole);
        }

        public static async Task SeedShopAsync(SiteContext db)
        {
            db.Database.EnsureCreated();
            if (!await db.Set<Item>().AnyAsync())
                db.Set<Item>().AddRange(GetItems());
            if (!await db.Set<Basket>().AnyAsync())
                db.Set<Basket>().AddRange(GetBaskets());
            if (!await db.Set<Order>().AnyAsync())
                db.Set<Order>().AddRange(GetOrders());
            await db.SaveChangesAsync();
        }

        private static IEnumerable<Item> GetItems()
        {
            return new List<Item>()
            {
                new Item("Assorted Chocolate Candy", "Venenatis tellus in metus vulputate eu scelerisque felis imperdiet proin. Quisque egestas diam in arcu cursus. Sed viverra tellus in hac. Quis commodo odio aenean sed adipiscing diam donec adipiscing.", "\\Images\\chocolateCandy2.jpg", 15.5M, true),
                new Item("Another Assorted Chocolate Candy", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Cursus risus at ultrices mi tempus imperdiet nulla malesuada pellentesque. Tortor posuere ac ut consequat. Sagittis nisl rhoncus mattis rhoncus urna neque viverra justo. Lacus sed turpis tincidunt id aliquet risus feugiat in. Viverra aliquet eget sit amet tellus cras adipiscing enim eu.", "\\Images\\chocolateCandy.jpg", 10M, true),
                new Item("Another Chocolate Candy", "Turpis egestas pretium aenean pharetra magna ac placerat vestibulum. Sed faucibus turpis in eu mi bibendum neque egestas. At in tellus integer feugiat scelerisque. Elementum integer enim neque volutpat ac tincidunt.", "\\Images\\chocolateCandy3.jpg", 11.5M, true),
                new Item("Assorted Fruit Candy", "Vitae congue eu consequat ac felis donec et. Praesent semper feugiat nibh sed pulvinar proin gravida hendrerit. Vel eros donec ac odio. A lacus vestibulum sed arcu non odio euismod lacinia at. Nisl suscipit adipiscing bibendum est ultricies integer. Nec tincidunt praesent semper feugiat nibh.", "\\Images\\fruitCandy.jpg", 5.5M, true),
                new Item("Fruit Candy", "Purus sit amet luctus venenatis lectus magna fringilla. Consectetur lorem donec massa sapien faucibus et molestie ac. Sagittis nisl rhoncus mattis rhoncus urna neque viverra.","\\Images\\fruitCandy2.jpg", 7.5M, true)
            };
        }

        private static IEnumerable<Order> GetOrders()
        {
            return new List<Order>()
            {
                new Order("username", "", "", new List<OrderItem>()
                {
                    new OrderItem(new OrderItemOrdered(1, "Title"), 10, 1)
                }),
                new Order("another", "", "", new List<OrderItem>()
                {
                    new OrderItem(new OrderItemOrdered(1, "Another title"), 5, 1)
                })
            };
        }

        private static IEnumerable<Basket> GetBaskets()
        {
            var basket1 = new Basket("username");
            basket1.AddItem(1, 10, 1);
            basket1.AddItem(2, 5, 1);
            return new List<Basket>() { basket1 };
        }
    }
}
