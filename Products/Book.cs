using System;

namespace Bookstore.Products
{
    class Book:ShopProduct
    {
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        override public void DisplayProductInfo()
        {
            Console.WriteLine($"Książka:\nId: {Id}\nISBN: {ISBN}\nTytuł: {Title}\nWydawca: {Publisher}\n"+
                $"Data wydania: {PublicationDate.ToShortDateString()}\nCena: {Price}\nIlość: {Quantity}"+
                $"\nAutor: {AuthorName} {AuthorSurname}");
        }
    }
}
