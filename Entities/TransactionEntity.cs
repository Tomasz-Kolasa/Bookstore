using Bookstore.Sale;
using System;

namespace Bookstore.Entities
{
    public class TransactionEntity
    {
        public int Id { get; set; }
        public TransactionTypes Type { get; set; }
        public int ShopProductId { get; set; }
        public ShopProductEntity ShopProduct { get; set; }

        public DateTime Date { get; set; }
        public decimal Price { get; set; }
    }
}
