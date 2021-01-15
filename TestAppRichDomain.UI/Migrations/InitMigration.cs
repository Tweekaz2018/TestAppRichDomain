using FluentMigrator;
using FluentMigrator.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppRichDomain.UI.Migrations
{
    [Migration(1610594911)]
    public class Migration_1610594911 : Migration
    {
        public override void Down()
        {
            Delete.Table("Items");
            Delete.Table("Baskets");
            Delete.Table("BasketItems");
            Delete.Table("Orders");
            Delete.Table("OrderItems");
            Delete.Table("Items");
        }

        public override void Up()
        {


            /*
            Create.Table("Items")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().Identity(1, 1)
                .WithColumn("Title").AsString()
                .WithColumn("Description").AsFixedLengthString(1000)
                .WithColumn("Image").AsString()
                .WithColumn("Price").AsDecimal()
                .WithColumn("IsAviable").AsBoolean();
            Create.Table("Baskets")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().Identity(1,1)
                .WithColumn("userId").AsString().NotNullable();
            Create.Table("BasketItems")
                .WithColumn("Price").AsDecimal().NotNullable()
                .WithColumn("Quantity").AsInt32()
                .WithColumn("ItemId").AsInt32()
                .WithColumn("BasketId").AsInt32()
                   .ForeignKey("FK_BasketItemToBasket", "Baskets", "Id");
            Create.Table("Orders")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity(1, 1)
                .WithColumn("UserId").AsString()
                .WithColumn("OrderData").AsDateTime().WithDefaultValue(DateTime.Now)
                .WithColumn("Address").AsString()
                .WithColumn("Comment").AsString();
            Create.Table("OrderItems")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity(1, 1)
                .WithColumn("Price").AsDecimal()
                .WithColumn("Quantity").AsInt32()
                .ForeignKey("FR_OrderItemsToOrder", "Orders", "Id");
            Create.Table("OrderItemOrdereds")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity(1, 1)
                .WithColumn("Title").AsString()
                .WithColumn("ItemId").AsInt32().ForeignKey("OrderItemOrderedToOrderItem", "OrderItems", "Id");
            
            Create.Table("AspNetRoles")
                .WithColumn("Id").AsString().PrimaryKey().NotNullable()
                .WithColumn("Name").AsString()
                .WithColumn("NormalizedName").AsString()
                .WithColumn("ConcurrencyStamp").AsString();
            Create.Table("AspNetUsers")
                .WithColumn("Id").AsString().PrimaryKey()
                .WithColumn("UserName").AsString()
                .WithColumn("NormalizedUserName").AsString()
                .WithColumn("Email").AsString()
                .WithColumn("NormalizedEmail").AsString()
                .WithColumn("EmailConfirmed").AsBoolean()
                .WithColumn("PasswordHash").AsString()
                .WithColumn("SecurityStamp").AsString()
                .WithColumn("ConcurrencyStamp").AsString()
                .WithColumn("PhoneNumber").AsString()
                .WithColumn("PhoneNumberConfirmed").AsBoolean()
                .WithColumn("TwoFactorEnabled").AsBoolean()
                .WithColumn("LockoutEnd").AsDateTimeOffset().Nullable()
                .WithColumn("LockoutEnabled").AsBoolean()
                .WithColumn("AccessFailedCount").AsInt32();
            Create.Table("AspNetRoleClaims")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity(1, 1)
                .WithColumn("RoleId").AsString().NotNullable().ForeignKey("FK_ClaimToRole", "AspNetRoles", "Id")
                .WithColumn("ClaimType").AsString().Nullable()
                .WithColumn("ClaimValue").AsString().Nullable();
            Create.Table("AspNetUserClaims")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity(1, 1)
                .WithColumn("UserId").AsString().NotNullable().ForeignKey("FK_ClaimToUser", "AspNetUsers", "Id")
                .WithColumn("ClaimType").AsString().Nullable()
                .WithColumn("ClaimValue").AsString().Nullable();
            Create.Table("AspNetUserLogins")
                .WithColumn("LoginProvider").AsString().NotNullable()
                .WithColumn("ProviderKey").AsString().NotNullable()
                .WithColumn("ProviderDisplayName").AsString().Nullable()
                .WithColumn("UserId").AsString().NotNullable().ForeignKey("FK_LoginsToUsers", "AspNetUsers", "Id");
            Create.PrimaryKey("PK_AspNetUserLogins").OnTable("AspNetUserLogins")
                .Columns("LoginProvider", "ProviderKey");
            Create.Table("AspNetUserRoles")
                .WithColumn("UserId").AsString().NotNullable().ForeignKey("FK_UserRolesToRoleId", "AspNetRoles", "Id")
                .WithColumn("RoleId").AsString().NotNullable().ForeignKey("FR_UserRolesToUserId", "AspNetUsers", "Id");
            Create.PrimaryKey("PR_AspNetUserRoles").OnTable("AspNetUserRoles").Columns("UserId", "RoleId");
            Create.Table("AspNetUserTokens")
                .WithColumn("UserId").AsString().NotNullable().ForeignKey("FK_AspNetUserTokensToUserId", "AspNetUsers", "Id")
                .WithColumn("LoginProvider").AsString().NotNullable()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Name").AsString().Nullable();
            Create.PrimaryKey("PK_AspNetUserTokens").OnTable("AspNetUserTokens")
                .Columns("UserId", "LoginProvider", "Name");
            Create.Index("IX_AspNetRoleClaims_RoleId")
                .OnTable("AspNetRoleClaims")
                .OnColumn("RoleId");
            Create.Index("RoleNameIndex")
                .OnTable("AspNetRoles")
                .OnColumn("NormalizedName")
                .Unique();
            //Filter
            Create.Index("IX_AspNetUserClaims_UserId")
                .OnTable("AspNetUserClaims")
                .OnColumn("UserId")
                .Unique();
            Create.Index("IX_AspNetUserLogins_UserId")
                .OnTable("AspNetUserLogins")
                .OnColumn("UserId");
            Create.Index("IX_AspNetUserRoles_RoleId")
                .OnTable("AspNetUserRoles")
                .OnColumn("RoleId");
            Create.Index("EmailIndex")
                .OnTable("AspNetUsers")
                .OnColumn("NormalizedEmail");
            Create.Index("UserNameIndex")
                .OnTable("AspNetUsers")
                .OnColumn("NormalizedUserName")
                .Unique();
            Create.Index("IX_BasketItems_BasketId")
               .OnTable("BasketItems")
               .OnColumn("BasketId")
               .Unique();
            Create.Index("IX_OrderItems_OrderId")
                .OnTable("OrderItems")
                .OnColumn("OrderId");

            */
            Insert.IntoTable("Items").Row(new
            {
                Title = "Assorted Chocolate Candy",
                Description = "Venenatis tellus in metus vulputate eu scelerisque felis imperdiet proin. Quisque egestas diam in arcu cursus. Sed viverra tellus in hac. Quis commodo odio aenean sed adipiscing diam donec adipiscing.",
                Price = 14.5M,
                IsAviable = true,
                Image = "\\Images\\chocolateCandy2.jpg"
            });
            Insert.IntoTable("Items").Row(new
            {
                Title = "Another Assorted Chocolate Candy",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Cursus risus at ultrices mi tempus imperdiet nulla malesuada pellentesque. Tortor posuere ac ut consequat. Sagittis nisl rhoncus mattis rhoncus urna neque viverra justo. Lacus sed turpis tincidunt id aliquet risus feugiat in. Viverra aliquet eget sit amet tellus cras adipiscing enim eu.",
                Price = 10M,
                IsAviable = true,
                Image = "\\Images\\chocolateCandy.jpg"
            });
            Insert.IntoTable("Items").Row(new
            {
                Title = "Another Chocolate Candy",
                Description = "Turpis egestas pretium aenean pharetra magna ac placerat vestibulum. Sed faucibus turpis in eu mi bibendum neque egestas. At in tellus integer feugiat scelerisque. Elementum integer enim neque volutpat ac tincidunt.",
                Price = 11.5M,
                IsAviable = true,
                Image = "\\Images\\chocolateCandy3.jpg"
            });
            Insert.IntoTable("Items").Row(new
            {
                Title = "Assorted Fruit Candy",
                Description = "Vitae congue eu consequat ac felis donec et. Praesent semper feugiat nibh sed pulvinar proin gravida hendrerit. Vel eros donec ac odio. A lacus vestibulum sed arcu non odio euismod lacinia at. Nisl suscipit adipiscing bibendum est ultricies integer. Nec tincidunt praesent semper feugiat nibh.",
                Price = 7.5M,
                IsAviable = true,
                Image = "\\Images\\fruitCandy.jpg"
            });
            Insert.IntoTable("Items").Row(new
            {
                Title = "Fruit Candy",
                Description = "Purus sit amet luctus venenatis lectus magna fringilla. Consectetur lorem donec massa sapien faucibus et molestie ac. Sagittis nisl rhoncus mattis rhoncus urna neque viverra.",
                Price = 13M,
                IsAviable = true,
                Image = "\\Images\\fruitCandy2.jpg"
            });

        }
    }
}
