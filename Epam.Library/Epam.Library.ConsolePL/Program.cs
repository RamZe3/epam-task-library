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
            IBookLogic bookLogic = dependenciesResolver.bookLogic;
            IPaperLogic paperLogic = dependenciesResolver.paperLogic;
            IPatentLogic patentLogic = dependenciesResolver.patentLogic;

            Book book1 = new Book("Whole", authors, "Saratov", "BookSar", 2000, 12, "", "ISBN 7-12-12-0");
            bookLogic.AddBook(book1);
            Book book2 = new Book("Club", authors, "Saratov", "BookSar", 2000, 12, "", "ISBN 7-12-12-8");
            bookLogic.AddBook(book2);

            List<Author> authors1 = new List<Author>();
            authors1.Add(new Author("Artem", "Urlov"));
            authors1.Add(new Author("Ramil", "Fitkulin"));

            try
            {
                Book book3 = new Book("Club", authors1, "Saratov", "BookSar", 2000, 12, "", "ISBN 7-12-12-8");
                bookLogic.AddBook(book3);
            }
            catch (InvalidOperationException ex)
            {

                Console.WriteLine(ex.Message);
            }

            //ISSN 1233-1213
            DateTime dateTime1 = DateTime.Now;
            Paper paper = new Paper("Azbuka",  "Saratov", "PaperEnt", 2021, 3, "Paper", 1223, dateTime1, "ISSN 1233-1213");
            paperLogic.AddPaper(paper);
            try
            {
                Paper paper1 = new Paper("Not Azbuka",  "Saratov", "PaperEnt", 2021, 3, "Paper", 1223, dateTime1, "ISSN 1233-1213");
                paperLogic.AddPaper(paper1);
            }
            catch (InvalidOperationException ex)
            {

                Console.WriteLine(ex.Message);
            }

            Patent patent1 = new Patent("Phone",  authors, "Russia", 132, dateTime, dateTime1, 12, "");
            patentLogic.AddPatent(patent1);
            try
            {
                Patent patent2 = new Patent("Iphone",   authors, "Russia", 132, dateTime, dateTime1, 12, "221");
                patentLogic.AddPatent(patent2);
            }
            catch (InvalidOperationException ex)
            {

                Console.WriteLine(ex.Message);
            }

            List<Book> books = Logic.FindBooksByAuthor(new Author("Ramil", "Fitkulin"));
            foreach (var book in books)
            {
                Console.WriteLine(book.Id + " " + book.Name + " " + book.ISBN);
            }
            Console.WriteLine();

            List<Patent> patents = Logic.FindPatentsByAuthor(new Author("Ramil", "Fitkulin"));
            foreach (var patent in patents)
            {
                Console.WriteLine("Patents");
                Console.WriteLine(patent.Name + " " + patent.Country);
            }
            Console.WriteLine();

            List<InformationResource> informationResources = Logic.FindPatentsAndBooksByAuthor(new Author("Ramil", "Fitkulin"));
            foreach (var resource in informationResources)
            {
                Console.WriteLine("Patents and Books");
                Console.WriteLine(resource.Name);
            }
            Console.WriteLine();

            foreach (var item in Logic.GetSortedLibraryByYearOfPublishing(false))
            {
                var resource = (IHaveYearOfPublishing)item;
                Console.WriteLine(item.Name + " " + resource.GetYearOfPublishing());
            }
            Console.WriteLine();

            foreach (var book in Logic.SmartBookSearchByPublisher("Sar"))
            {
                Console.WriteLine(book.Name + " " + book.Publisher);
            }
            Console.WriteLine();

            foreach (var item in Logic.GroupingResourceByYearOfPublication())
            {
                var resource = item;
                Console.WriteLine(item.Name);
            }
            Console.WriteLine();
            Console.WriteLine(Logic.FindResourceByName("Whole").Name);
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
