using Bookstore.Entities;
using Bookstore.Products;
using ConsoleWriterLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bookstore.Sale
{
    class TransactiosManager
    {
        private SqliteDbContext database;
        private EntitiesMapper mapper;
        private ShopProduct ShopProductBeingSold;
        public TransactiosManager(SqliteDbContext db, EntitiesMapper mapper)
        {
            database = db;
            this.mapper = mapper;
        }

        public void SaleProduct()
        {
            ConsoleWriter.Separator();
            Console.WriteLine("Podaj Id produktu który ma zostać sprzedany: ");
            Console.WriteLine("Enter aby anulować\n");
            string productId = Console.ReadLine();

            if (productId == "") return;

            bool isInputNumber = int.TryParse(productId, out int id);

            if (isInputNumber && IsProductInDatabaseAndRemoveFromStockWentOk(id))
            {
                TransactionEntity transactionEntity = new TransactionEntity
                {
                    Type = TransactionTypes.Sale,
                    ShopProductId = id,
                    Date = DateTime.Now,
                    Price = ShopProductBeingSold.GetPrice()
                };

                database.Transactions.Add(transactionEntity);
                database.SaveChanges();

                ConsoleWriter.Info($"Sprzedano produkt Id: {ShopProductBeingSold.Id}\nNr sprzedaży: {transactionEntity.Id} ");

                ShopProductBeingSold = null;
            }
            else
            {
                ConsoleWriter.Info("Wybrany produkt jest niedostępny.");
            }
        }

        public void ReturnProduct()
        {
            ConsoleWriter.DoubleSeparator();
            ConsoleWriter.Info("Lista sprzedanych produktów:");
            DisplayTransactions(DisplayTransactionTypes.Sale);
            Console.WriteLine("\nPodaj Id transakcji z której produkt chcesz zwrócić: ");
            Console.WriteLine("Enter aby anulować\n");
            string productId = Console.ReadLine();

            if (productId == "") return;

            bool isInputNumber = Int32.TryParse(productId, out int id);

            if (isInputNumber)
            {
                var saleTransaction = database.Transactions.FirstOrDefault(x => x.Id == id);

                if(null != saleTransaction)
                {
                    ShopProductEntity shopProductEntity = database.ShopProducts.FirstOrDefault(x => x.Id == saleTransaction.ShopProductId);
                    shopProductEntity.Quantity++;

                    TransactionEntity transactionEntity = new TransactionEntity
                    {
                        Type = TransactionTypes.Return,
                        ShopProductId = saleTransaction.ShopProductId,
                        Date = DateTime.Now,
                        Price = saleTransaction.Price
                    };

                    database.Transactions.Add(transactionEntity);
                    database.SaveChanges();
                    ConsoleWriter.Info("Produkt zwrócony.");
                }
                else
                {
                    ConsoleWriter.Info("Nie ma takiej transakcji.");
                }
            }
            else
            {
                ConsoleWriter.Info("Nieprawidłowy wybór.");
            }
        }

        private bool IsProductInDatabaseAndRemoveFromStockWentOk(int productId)
        {
            var product = database.ShopProducts.FirstOrDefault(x=> x.Id==productId);

            if ((product != null) && (product.Quantity > 0))
            {
                ShopProductBeingSold = mapper.GetProductMappedFromEntity(product);
                product.Quantity--;
                database.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DisplayTransactions(DisplayTransactionTypes transactionType)
        {
            List<TransactionEntity> transactionEntities;

            if (transactionType==DisplayTransactionTypes.All)
            {
                transactionEntities = database.Transactions.ToList();
            }
            else if(transactionType==DisplayTransactionTypes.Sale)
            {
                transactionEntities = database.Transactions.Where(x => x.Type == TransactionTypes.Sale).ToList();
            }
            else
            {
                transactionEntities = database.Transactions.Where(x => x.Type == TransactionTypes.Return).ToList();
            }

            ShopProductEntity shopProductEntity;
            ShopProduct shopProduct;

            ConsoleWriter.Info($"Transakcje typu - {transactionType}:");

            int transactionsQuantity = transactionEntities.Count();

            if (transactionsQuantity == 0)
            {
                Console.WriteLine("Nie znaleziono żadnych transakcji.");
                ConsoleWriter.Separator();
            }
            else
            {
                foreach (TransactionEntity transaction in transactionEntities)
                {
                    ConsoleWriter.Separator();
                    Console.WriteLine($"Id transakcji: {transaction.Id}");
                    Console.WriteLine($"Typ transakcji: {transaction.Type}");
                    Console.WriteLine($"Data: {transaction.Date}");
                    Console.WriteLine($"Cena: {transaction.Price}");
                    Console.WriteLine("==Szczegóły produktu:==============");
                    Console.WriteLine($"ShopProductId: {transaction.ShopProductId}");
                    shopProductEntity = database.ShopProducts.FirstOrDefault(x => x.Id == transaction.ShopProductId);
                    shopProduct = mapper.GetProductMappedFromEntity(shopProductEntity);
                    shopProduct.DisplayProductInfo();
                }
            }
        }
    }
}
