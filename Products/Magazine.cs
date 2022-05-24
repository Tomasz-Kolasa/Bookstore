using System;

namespace Bookstore.Products
{
    class Magazine:ShopProduct
    {
        public int IssueNumber { get; set; }
        override public void DisplayProductInfo()
        {
            Console.WriteLine($"Magazyn:\nId: {Id}\nISBN: {ISBN}\nTytuł: {Title}\nWydawca: {Publisher}\n" +
                $"Data wydania: {PublicationDate.ToShortDateString()}\nCena: {Price}\nIlość: {Quantity}"+
                $"\nNumer wydania: {IssueNumber}");
        }
    }
}
