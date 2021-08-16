using Epam.Library.Entities;
using Epam.Library.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.Library.Dependencies;
using Epam.Library.BLL.Interfaces;

namespace Epam.Library.ConsolePL
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Author> authors = new List<Author>();
            authors.Add(new Author("Asd", "Qwe"));
            authors.Add(new Author("Zxc", "Fgh"));
            DependenciesResolver dependenciesResolver = new DependenciesResolver();
            IInformationResourceLogic Logic = dependenciesResolver.InformationResourceLogic;
            Logic.AddBook("ASD", authors, "Qwe", "Qwe", 2000, 12, "a", "ISBN 7-12-12-0");
            //Logic.AddBook("ASD", authors, "qwe", "zxc", 1, 12, "a", "az");
            foreach (var item in Logic.GetLibrary())
            {
                Console.WriteLine(item.name);
            }

            List<Book> books = Logic.FindBooksByAuthor(new Author("Asd", "Qwe"));
            foreach (var book in books)
            {
                Console.WriteLine(book.name);
            }

            Console.WriteLine(Logic.FindResourceByName("ASD").name);
            Console.ReadKey();
        }
    }
}
