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
using Epam.Library.Entities.Exceptions;
using Epam.Library.SQLDAL;

namespace Epam.Library.ConsolePL
{
    class Program
    {
        static void Main(string[] args)
        {

            /*List<Author> authors = new List<Author>();
            authors.Add(new Author("Ramil", "Fitkulin"));
            authors.Add(new Author("Artem", "Urlov"));
            DateTime dateTime = new DateTime(2008, 3, 1, 7, 0, 0);

            DependenciesResolver dependenciesResolver = new DependenciesResolver();
            IInformationResourceLogic Logic = dependenciesResolver.InformationResourceLogic;
            IBookLogic bookLogic = dependenciesResolver.bookLogic;
            IPaperLogic paperLogic = dependenciesResolver.paperLogic;
            IPatentLogic patentLogic = dependenciesResolver.patentLogic;
            Book bookWithAuthorIvanIvanov = new Book("TestBook", authors, "Saratov", "BookSar", 2000, 12, "note", "ISBN 7-12-12-1");
            bookLogic.AddBook(bookWithAuthorIvanIvanov);
            Book book1 = new Book("Whole", authors, "Saratov", "BookSar", 2000, 12, "", "ISBN 7-12-12-0");
            bookLogic.AddBook(book1);
            Book book2 = new Book("Cluba", authors, "Saratov", "BookSar", 2000, 12, "", "ISBN 7-12-12-8");
            bookLogic.AddBook(book2);
            Book book35 = new Book("Claub", authors, "Saratov", "BookSar123", 1998, 12, "", "ISBN 7-11-12-8");
            bookLogic.AddBook(book35);

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
                Paper paper1 = new Paper("Not Azbuka",  "Saratov", "BookAndPaperEnt", 2021, 3, "Paper", 1223, dateTime1, "ISSN 1233-1213");
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

            Console.WriteLine("Find Books");
            List<Book> books = Logic.FindBooksByAuthor(new Author("Ramil", "Fitkulin"));
            foreach (var book in books)
            {
                Console.WriteLine(books.Contains(book));
                Console.WriteLine(book.Id + " " + book.Name + " " + book.ISBN);
            }
            Console.WriteLine("End Find Books");
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

            Console.WriteLine("Search");
            foreach (var item in Logic.SmartBookSearchByPublisher("Book"))
            {
                Console.WriteLine("Publisher= " + item.Key);
                foreach (var book in item.Value)
                {
                    Console.WriteLine(book.Name);
                }
            }
            Console.WriteLine();

            
            Console.WriteLine("GroupBy");
            foreach (var item in Logic.GroupingResourceByYearOfPublication())
            {
                Console.WriteLine("Year= " +  item.Key);
                foreach (var res in item.Value)
                {
                    //IHaveYearOfPublication rea = (IHaveYearOfPublication)res;
                    Console.WriteLine(res.Name);
                }
            }
            Console.WriteLine();
            Console.WriteLine("SmartSearch");
            //Console.WriteLine(Logic.FindResourceByName("Whole"));
            Console.WriteLine();

            Author author = new Author("Ramil", "Fitkulin");
            Console.WriteLine(Logic.FindBooksByAuthor(author).Contains(bookWithAuthorIvanIvanov));
            Console.WriteLine();
            Console.WriteLine("Exept");
            DateTime dateTime11 = new DateTime(2009, 3, 1, 7, 0, 0);
            DateTime dateTime2 = new DateTime(2010, 3, 1, 7, 0, 0);
            Patent patent123 = new Patent("testPatent", authors, "CanadaA", 132, dateTime11, dateTime2, 12, "221");
            List<DataValidationError> dataValidationExceptions = patentLogic.AddPatent(patent123);
            foreach (var item in dataValidationExceptions)
            {
                Console.WriteLine(item.Message + " - " + item.ErrorValue);
            }
            Console.ReadKey();*/



            List<Author> authors = new List<Author>();
            authors.Add(new Author("Artem", "Ivanob"));
            authors.Add(new Author("Ram", "Fit"));
            Book book1 = new Book("Booksq", authors, "Saratov", "BookSar", 2000, 57, "", "ISBN 7-12-13-1");
            BookSQLDAL bookSQLDAL = new BookSQLDAL();
            bookSQLDAL.AddBook(book1);
            //bookSQLDAL.DeleteBook(new Guid("c6e04a9d-4a3f-44d9-83fa-8050dec08e89"));


            DateTime dateTime1 = new DateTime(1900, 2, 1);
            Paper paper = new Paper("Azbuka", "Saratov", "PaperEnt", 2021, 1223, "Paper", 12223, dateTime1, "ISSN 1233-1213");
            PaperSQLDAL paperSQLDAL = new PaperSQLDAL();
            paperSQLDAL.AddPaper(paper);

            Patent patent2 = new Patent("Iphone", authors, "Russia", 1342, DateTime.Now, dateTime1, 124, "221");
            PatentSQLDAL patentSQLDAL = new PatentSQLDAL();
            patentSQLDAL.AddPatent(patent2);

            //foreach (var item in patent2.Inventors)
            //{
            //    Console.WriteLine(item.Id);
            //}
            //Console.ReadLine();

            Guid guid = new Guid("8d13a908-2327-430d-a982-1a4de732a627");
            Console.WriteLine(guid);

            //book1.Id = guid;
            //bookSQLDAL.UpdateBook(book1);

            IFSQLDAL iFSQLDAL = new IFSQLDAL();
            foreach (var item in iFSQLDAL.GetSortedLibraryByYearOfPublishing(true))
            {
                Console.WriteLine(item.Name);
            }

            Console.WriteLine("Все");

            Console.ReadLine();
        }
    }
}
