using Bookstore.Products;
using System;

namespace Bookstore.Sale
{
    class Transaction
    {
        public Transaction(TransactionTypes transactionType, IChargable product, decimal price)
        {
            Type = transactionType;
            ShopProduct = product;
            Date = DateTime.Now;
            Price = price;
        }
        public int Id { get; set; }
        public TransactionTypes Type { get; set; }
        public IChargable ShopProduct { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
    }
}
