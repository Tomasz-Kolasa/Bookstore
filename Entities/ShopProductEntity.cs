using System;
using System.Collections.Generic;

namespace Bookstore.Entities
{
    public abstract class ShopProductEntity
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public DateTime PublicationDate { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public List<TransactionEntity> Transactions { get; set; }
    }
}
