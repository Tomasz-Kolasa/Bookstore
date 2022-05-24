using Bookstore.Sale;
using System;

namespace Bookstore
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                var bookstore = new Shop();
                bookstore.Start();
            }
            catch(Exception)
            {
                Console.WriteLine("Wystąpił problem z aplikacją.");
            }
        }
    }
}
