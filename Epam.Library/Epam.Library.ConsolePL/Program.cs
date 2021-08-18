using Epam.Library.Entities;
using Epam.Library.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.Library.Dependencies;
using Epam.Library.BLL.Interfaces;
using Epam.Library.Entities.Interfaces;

namespace Epam.Library.ConsolePL
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Author> authors = new List<Author>();
            authors.Add(new Author("Ramil", "Fitkulin"));
            authors.Add(new Author("Artem", "Urlov"));
            DateTime dateTime = new DateTime(2008, 3, 1, 7, 0, 0);

            DependenciesResolver dependenciesResolver = new DependenciesResolver();
            IInformationResourceLogic Logic = dependenciesResolver.InformationResourceLogic;
            Logic.AddBook("Whole", authors, "Saratov", "BookSar", 2000, 12, "", "ISBN 7-12-12-0");
            Logic.AddBook("Club", authors, "Saratov", "BookSar", 2000, 12, "", "ISBN 7-12-12-8");

            List<Author> authors1 = new List<Author>();
            authors1.Add(new Author("Artem", "Urlov"));
            authors1.Add(new Author("Ramil", "Fitkulin"));

            try
            {
                Logic.AddBook("Club", authors1, "Saratov", "BookSar", 2000, 12, "", "ISBN 7-12-12-8");
            }
            catch (InvalidOperationException ex)
            {

                Console.WriteLine(ex.Message);
            }

            //ISSN 1233-1213
            DateTime dateTime1 = DateTime.Now;
            Logic.AddPaper("Azbuka", "Saratov", "PaperEnt", 2021, 3, "Paper", 1223, dateTime1, "ISSN 1233-1213");
            try
            {
                Logic.AddPaper("Not Azbuka", "Saratov", "PaperEnt", 2021, 3, "Paper", 1223, dateTime1, "ISSN 1233-1213");
            }
            catch (InvalidOperationException ex)
            {

                Console.WriteLine(ex.Message);
            }

            Logic.AddPatent("Phone", authors, "Russia", 132, dateTime, dateTime1, 12, "");
            try
            {
                Logic.AddPatent("Iphone", authors, "Russia", 132, dateTime, dateTime1, 12, "221");
            }
            catch (InvalidOperationException ex)
            {

                Console.WriteLine(ex.Message);
            }

            List<Book> books = Logic.FindBooksByAuthor(new Author("Ramil", "Fitkulin"));
            foreach (var book in books)
            {
                Console.WriteLine(book.id + " " + book.name + " " + book.ISBN);
            }
            Console.WriteLine();

            List<Patent> patents = Logic.FindPatentsByAuthor(new Author("Ramil", "Fitkulin"));
            foreach (var patent in patents)
            {
                Console.WriteLine(patent.name + " " + patent.country);
            }
            Console.WriteLine();

            List<InformationResource> informationResources = Logic.FindPatentsAndBooksByAuthor(new Author("Ramil", "Fitkulin"));
            foreach (var resource in informationResources)
            {
                Console.WriteLine(resource.name);
            }
            Console.WriteLine();

            foreach (var item in Logic.GetSortedLibraryByYearOfPublishing(false))
            {
                var resource = (IHaveYearOfPublishing)item;
                Console.WriteLine(item.name + " " + resource.GetYearOfPublishing());
            }
            Console.WriteLine();

            foreach (var book in Logic.SmartBookSearchByPublisher("Sar"))
            {
                Console.WriteLine(book.name + " " + book.publisher);
            }
            Console.WriteLine();

            foreach (var item in Logic.GroupingResourceByYearOfPublication())
            {
                var resource = item;
                Console.WriteLine(item.name);
            }
            Console.WriteLine();
            Console.WriteLine(Logic.FindResourceByName("Whole").name);
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
