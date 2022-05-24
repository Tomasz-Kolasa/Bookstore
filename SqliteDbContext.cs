using Bookstore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bookstore
{
    class SqliteDbContext :DbContext
    {
        public DbSet<TransactionEntity> Transactions { get; set; }
        public DbSet<ShopProductEntity> ShopProducts { get; set; }
        public DbSet<BookEntity> Books { get; set; }
        public DbSet<MagazineEntity> Magazines { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=bookstore.db");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ShopProductEntity>(entity =>
            {
                entity.HasMany(x => x.Transactions).WithOne(x => x.ShopProduct).HasForeignKey(x => x.ShopProductId);
            });
        }
    }

    abstract class DbSeeder
    {
        public static void SeedShopProductsIfNeeded(SqliteDbContext database)
        {
            database.Database.Migrate();
            database.Database.EnsureCreated();

            if( database.ShopProducts.Any() == false)
            {
                SeedBooks(database);
                SeedMagazines(database);
                database.SaveChanges();
                Console.WriteLine("ShopProducts seeded.");
            }
        }

        private static void SeedBooks(SqliteDbContext database)
        {
            var books = new List<BookEntity>
            {
                new BookEntity
                {
                    ISBN = "12345678",
                    Title = "C# programming book",
                    Publisher = "Golden Books",
                    PublicationDate = new DateTime(2008, 6, 1),
                    Price = 49.99m,
                    Quantity = 20,
                    AuthorName = "John",
                    AuthorSurname = "Doe"
                },
                new BookEntity
                {
                    ISBN = "9876544",
                    Title = "What to eat",
                    Publisher = "Books Inc",
                    PublicationDate = new DateTime(2019, 4, 6),
                    Price = 30.00m,
                    Quantity = 30,
                    AuthorName = "Anna",
                    AuthorSurname = "Fluffy"
                }
            };

            database.ShopProducts.AddRange(books);
        }

        private static void SeedMagazines(SqliteDbContext database)
        {
            var magazines = new List<MagazineEntity>
            {
                new MagazineEntity
                {
                    ISBN = "3456669",
                    Title = "Bravo Girl",
                    Publisher = "Crazy Magazines",
                    PublicationDate = new DateTime(2004, 3, 2),
                    Price = 4.50m,
                    Quantity = 10,
                    IssueNumber = 23
                },
                new MagazineEntity
                {
                    ISBN = "3479658",
                    Title = "Time",
                    Publisher = "N.Y. Kings",
                    PublicationDate = new DateTime(2022, 1, 7),
                    Price = 12.99m,
                    Quantity = 100,
                    IssueNumber = 15
                }
            };

            database.ShopProducts.AddRange(magazines);
        }
    }
}
