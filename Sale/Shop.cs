using Bookstore.Entities;
using Bookstore.Products;
using ConsoleWriterLibrary;
using System;
using System.Linq;

namespace Bookstore.Sale
{
    class Shop
    {
        private SqliteDbContext database;
        private TransactiosManager transactionsManager;
        private EntitiesMapper mapper;
        public Shop()
        {
            database = new SqliteDbContext();
            mapper = new EntitiesMapper();
            transactionsManager = new TransactiosManager(database, mapper);

            DbSeeder.SeedShopProductsIfNeeded(database);
        }
        public void Start()
        {
            bool isChoiceRequired = true;
            string action;

            Console.WriteLine("\n\nWitamy w księgarni.");


            while (isChoiceRequired)
            {
                ConsoleWriter.DoubleSeparator();
                action = PickAction();

                switch (action)
                {
                    case "1":
                        Console.Clear();
                        DisplayShopProducts();
                        break;
                    case "2":
                        Console.Clear();
                        DisplayShopProducts();
                        transactionsManager.SaleProduct();
                        break;
                    case "3":
                        Console.Clear();
                        transactionsManager.DisplayTransactions(DisplayTransactionTypes.All);
                        break;
                    case "4":
                        Console.Clear();
                        transactionsManager.ReturnProduct();
                        break;
                    case "5":
                        Console.Clear();
                        Console.WriteLine("Koniec pracy, do zobaczenia.");
                        isChoiceRequired = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Nieprawidłowy wybór");
                        break;
                }
            }
        }

        private string PickAction()
        {
            Console.WriteLine("1) Wyświetl stany produktów");
            Console.WriteLine("2) Sprzedaż produktu");
            Console.WriteLine("3) Lista transakcji");
            Console.WriteLine("4) Zwrot produktu");
            Console.WriteLine("5) Wyjście z programu");
            Console.WriteLine("Wybierz akcję: ");
            return Console.ReadLine();
        }

        private void DisplayShopProducts()
        {
            ShopProduct product;
            var shopProductsEntities = database.ShopProducts.ToList();

            int productsQuantity = shopProductsEntities.Count();

            if(productsQuantity == 0)
            {
                Console.WriteLine("Nie znaleziono żadnych produktów.");
            }
            else
            {
                foreach (ShopProductEntity entityProduct in shopProductsEntities)
                {
                    product = mapper.GetProductMappedFromEntity(entityProduct);
                    ConsoleWriter.Separator();
                    product.DisplayProductInfo();
                }
            }
        }
    }
}
