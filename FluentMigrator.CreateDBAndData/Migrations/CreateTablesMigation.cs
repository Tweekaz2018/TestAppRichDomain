using FluentMigrator.SqlServer;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentMigrator.CreateDBAndData.Migrations
{
    [Migration(1610594914)]
    public class CreateTablesMigation : Migration
    {
        public override void Down()
        {
            Execute.Sql("DROP DATABASE siteDB");
        }

        public override void Up()
        {
            Execute.Sql(@"
CREATE TABLE[AspNetRoles](
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT[PK_AspNetRoles] PRIMARY KEY([Id])
);
            CREATE TABLE[AspNetUsers](
            
                [Id] nvarchar(450) NOT NULL,
            
                [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT[PK_AspNetUsers] PRIMARY KEY([Id])
);

            

            CREATE TABLE[Baskets](
            
                [Id] int NOT NULL IDENTITY,
            
                [UserId] nvarchar(40) NOT NULL,
                CONSTRAINT[PK_Baskets] PRIMARY KEY([Id])
);

            

            CREATE TABLE[Items](
            
                [Id] int NOT NULL IDENTITY,
            
                [Title] nvarchar(50) NOT NULL,
            
                [Description] nvarchar(1000) NOT NULL,
            
                [Image] nvarchar(max) NOT NULL,
            
                [Price] decimal(18, 2) NOT NULL,
             
                 [IsAviable] bit NOT NULL,
    CONSTRAINT[PK_Items] PRIMARY KEY([Id])
);

            

            CREATE TABLE[Orders](
            
                [Id] int NOT NULL IDENTITY,
            
                [UserId] nvarchar(max) NULL,
    [OrderDate] datetime2 NOT NULL,
    [Address] nvarchar(max) NOT NULL,
    [Comment] nvarchar(max) NULL,
    CONSTRAINT[PK_Orders] PRIMARY KEY([Id])
);

            

            CREATE TABLE[AspNetRoleClaims](
            
                [Id] int NOT NULL IDENTITY,
            
                [RoleId] nvarchar(450) NOT NULL,
            
                [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT[PK_AspNetRoleClaims] PRIMARY KEY([Id]),
    CONSTRAINT[FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId]) REFERENCES[AspNetRoles]([Id]) ON DELETE CASCADE
);

            

            CREATE TABLE[AspNetUserClaims](
            
                [Id] int NOT NULL IDENTITY,
            
                [UserId] nvarchar(450) NOT NULL,
            
                [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT[PK_AspNetUserClaims] PRIMARY KEY([Id]),
    CONSTRAINT[FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId]) REFERENCES[AspNetUsers]([Id]) ON DELETE CASCADE
);

            

            CREATE TABLE[AspNetUserLogins](
            
                [LoginProvider] nvarchar(450) NOT NULL,
            
                [ProviderKey] nvarchar(450) NOT NULL,
            
                [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT[PK_AspNetUserLogins] PRIMARY KEY([LoginProvider], [ProviderKey]),
    CONSTRAINT[FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId]) REFERENCES[AspNetUsers]([Id]) ON DELETE CASCADE
);

            

            CREATE TABLE[AspNetUserRoles](
            
                [UserId] nvarchar(450) NOT NULL,
            
                [RoleId] nvarchar(450) NOT NULL,
                CONSTRAINT[PK_AspNetUserRoles] PRIMARY KEY([UserId], [RoleId]),
    CONSTRAINT[FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId]) REFERENCES[AspNetRoles]([Id]) ON DELETE CASCADE,
    CONSTRAINT[FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId]) REFERENCES[AspNetUsers]([Id]) ON DELETE CASCADE
);

            

            CREATE TABLE[AspNetUserTokens](
            
                [UserId] nvarchar(450) NOT NULL,
            
                [LoginProvider] nvarchar(450) NOT NULL,
            
                [Name] nvarchar(450) NOT NULL,
            
                [Value] nvarchar(max) NULL,
    CONSTRAINT[PK_AspNetUserTokens] PRIMARY KEY([UserId], [LoginProvider], [Name]),
    CONSTRAINT[FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId]) REFERENCES[AspNetUsers]([Id]) ON DELETE CASCADE
);

            

            CREATE TABLE[BasketItems](
            
                [Id] int NOT NULL IDENTITY,
            
                [Price] decimal(18, 2) NOT NULL,
             
                 [Quantity] int NOT NULL,
    [ItemId] int NOT NULL,
    [BasketId] int NOT NULL,
    CONSTRAINT[PK_BasketItems] PRIMARY KEY([Id]),
    CONSTRAINT[FK_BasketItems_Baskets_BasketId] FOREIGN KEY([BasketId]) REFERENCES[Baskets]([Id]) ON DELETE CASCADE
);

            

            CREATE TABLE[OrderItems](
            
                [Id] int NOT NULL IDENTITY,
            
                [Item_ItemId] int NULL,
            
                [Item_Title] nvarchar(50) NULL,
    [Price] decimal(18, 2) NOT NULL,
 
     [Quantity] int NOT NULL,
    [OrderId] int NULL,
    CONSTRAINT[PK_OrderItems] PRIMARY KEY([Id]),
    CONSTRAINT[FK_OrderItems_Orders_OrderId] FOREIGN KEY([OrderId]) REFERENCES[Orders]([Id]) ON DELETE NO ACTION
);

            

            CREATE INDEX[IX_AspNetRoleClaims_RoleId] ON[AspNetRoleClaims]([RoleId]);

            

            CREATE UNIQUE INDEX[RoleNameIndex] ON[AspNetRoles]([NormalizedName]) WHERE[NormalizedName] IS NOT NULL;

            

            CREATE INDEX[IX_AspNetUserClaims_UserId] ON[AspNetUserClaims]([UserId]);

            

            CREATE INDEX[IX_AspNetUserLogins_UserId] ON[AspNetUserLogins]([UserId]);

            

            CREATE INDEX[IX_AspNetUserRoles_RoleId] ON[AspNetUserRoles]([RoleId]);

            

            CREATE INDEX[EmailIndex] ON[AspNetUsers]([NormalizedEmail]);

            

            CREATE UNIQUE INDEX[UserNameIndex] ON[AspNetUsers]([NormalizedUserName]) WHERE[NormalizedUserName] IS NOT NULL;

            

            CREATE INDEX[IX_BasketItems_BasketId] ON[BasketItems]([BasketId]);

            

            CREATE INDEX[IX_OrderItems_OrderId] ON[OrderItems]([OrderId]);        
            ");

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
