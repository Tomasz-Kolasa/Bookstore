using System;

namespace Bookstore.Products
{
    abstract class ShopProduct:IChargable
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public DateTime PublicationDate { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        abstract public void DisplayProductInfo();
        public decimal GetPrice()
        {
            return Price;
        }
    }
}
