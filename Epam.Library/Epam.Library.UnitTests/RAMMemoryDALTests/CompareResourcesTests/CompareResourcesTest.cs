using Epam.Library.Entities;
using Epam.Library.RAMMemoryDAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Epam.Library.UnitTests.RAMMemoryDALTests.CompareResourcesTests
{
    [TestClass]
    public class CompareResourcesTest
    {
        public ComparisonerResources comparisonerResources;

        public CompareResourcesTest()
        {
            this.comparisonerResources = new ComparisonerResources();
        }

        #region CompareAuthors
        [TestMethod]
        public void CompareAuthorsNotInOrderTrue()
        {
            List<Author> authors1 = new List<Author>() { new Author("Ivan", "Ivanov"), new Author("Artem", "Petrov") };
            List<Author> authors2 = new List<Author>() { new Author("Artem", "Petrov"), new Author("Ivan", "Ivanov") };
            Assert.AreEqual(true, comparisonerResources.CompareAuthors(authors1, authors2));
        }

        [TestMethod]
        public void CompareAuthorsInOrderTrue()
        {
            List<Author> authors1 = new List<Author>() { new Author("Artem", "Petrov"), new Author("Ivan", "Ivanov") };
            List<Author> authors2 = new List<Author>() { new Author("Artem", "Petrov"), new Author("Ivan", "Ivanov") };
            Assert.AreEqual(true, comparisonerResources.CompareAuthors(authors1, authors2));
        }

        [TestMethod]
        public void CompareAuthorsVariousFalse()
        {
            List<Author> authors1 = new List<Author>() { new Author("Artem", "Petrov"), new Author("Ivan", "Ivanov") };
            List<Author> authors2 = new List<Author>() { new Author("Artem", "Petrov"), new Author("Sasha", "Naumov") };
            Assert.AreEqual(false, comparisonerResources.CompareAuthors(authors1, authors2));
        }

        [TestMethod]
        public void CompareAuthorsExtraAuthorFalse()
        {
            List<Author> authors1 = new List<Author>() { new Author("Artem", "Petrov"),
                new Author("Ivan", "Ivanov"),
                new Author("Sasha", "Naumov")};
            List<Author> authors2 = new List<Author>() { new Author("Artem", "Petrov"), new Author("Sasha", "Naumov") };
            Assert.AreEqual(false, comparisonerResources.CompareAuthors(authors1, authors2));
        }
        #endregion

        #region CompareBooks
        [TestMethod]
        public void CompareBooksSameTrue()
        {
            List<Author> authors = new List<Author>() { new Author("Ivan", "Ivanov"), new Author("Artem", "Petrov") };

            Book book1 = new Book("Whole", authors, "Saratov", "BookSar", 2000, 12, "note", "ISBN 7-12-12-0");
            Book book2 = new Book("Whole", authors, "Saratov", "BookSar", 2000, 12, "note", "ISBN 7-12-12-0");
            Assert.AreEqual(true, comparisonerResources.CompareBooks(book1, book2));
        }

        [TestMethod]
        public void CompareBooksVariousISBNFalse()
        {
            List<Author> authors = new List<Author>() { new Author("Ivan", "Ivanov"), new Author("Artem", "Petrov") };

            Book book1 = new Book("Whole", authors, "Saratov", "BookSar", 2000, 12, "note", "ISBN 8-12-12-0");
            Book book2 = new Book("Whole", authors, "Saratov", "BookSar", 2000, 12, "note", "ISBN 7-12-12-0");
            Assert.AreEqual(false, comparisonerResources.CompareBooks(book1, book2));
        }

        [TestMethod]
        public void CompareBooksEmptyISBNVariousNameAuthorsAndPublisherFalse()
        {
            List<Author> authors1 = new List<Author>() { new Author("Ivan", "Ivanov"), new Author("Artem", "Petrov") };
            List<Author> authors2 = new List<Author>() { new Author("Artem", "Petrov"), new Author("Ivan", "Ivanov") };

            Book book1 = new Book("Club", authors1, "Saratov", "SarBook", 2000, 12, "note", "");
            Book book2 = new Book("Whole", authors2, "Saratov", "BookSar", 2000, 12, "note", "");
            Assert.AreEqual(false, comparisonerResources.CompareBooks(book1, book2));
        }

        [TestMethod]
        public void CompareBooksEmptyISBNSameNameAuthorsAndPublisherTrue()
        {
            List<Author> authors = new List<Author>() { new Author("Ivan", "Ivanov"), new Author("Artem", "Petrov") };

            Book book1 = new Book("Whole", authors, "Saratov", "BookSar", 2000, 12, "note", "");
            Book book2 = new Book("Whole", authors, "Saratov", "BookSar", 2000, 12, "note", "");
            Assert.AreEqual(true, comparisonerResources.CompareBooks(book1, book2));
        }
        #endregion

        #region ComparePapers
        public void ComparePapersSameTrue()
        {
            DateTime dateTime = new DateTime(2008, 3, 1, 7, 0, 0);
            Paper paper1 = new Paper("Azbuka", "Saratov", "PaperEnt", 2021, 3, "Paper", 1223, dateTime, "ISSN 1233-1213");
            Paper paper2 = new Paper("Azbuka", "Saratov", "PaperEnt", 2021, 3, "Paper", 1223, dateTime, "ISSN 1233-1213");
            Assert.AreEqual(true, comparisonerResources.ComparePaper(paper1, paper2));
        }

        [TestMethod]
        public void ComparePapersVariousISSNFalse()
        {
            DateTime dateTime = new DateTime(2008, 3, 1, 7, 0, 0);
            Paper paper1 = new Paper("Azbuka", "Saratov", "PaperEnt", 2021, 3, "Paper", 1223, dateTime, "ISSN 1233-1213");
            Paper paper2 = new Paper("Azbuka", "Saratov", "PaperEnt", 2021, 3, "Paper", 1223, dateTime, "ISSN 1233-1213");
            Assert.AreEqual(false, comparisonerResources.ComparePaper(paper1, paper2));
        }

        [TestMethod]
        public void ComparePapersEmptyISSNVariousNameDateAndPublisherFalse()
        {
            DateTime dateTime = new DateTime(2008, 3, 1, 7, 0, 0);
            Paper paper1 = new Paper("Azbuka", "Saratov", "PaperEnt", 2021, 3, "Paper", 1223, dateTime, "");
            Paper paper2 = new Paper("Club", "Saratov", "PaperEntSaratov", 2021, 3, "Paper", 1223, DateTime.Now, "");
            Assert.AreEqual(false, comparisonerResources.ComparePaper(paper1, paper2));
        }

        [TestMethod]
        public void ComparePapersEmptyISSNSameNameDateAndPublisherTrue()
        {
            DateTime dateTime = new DateTime(2008, 3, 1, 7, 0, 0);
            Paper paper1 = new Paper("Azbuka", "Saratov", "PaperEnt", 2021, 3, "Paper", 1223, dateTime, "");
            Paper paper2 = new Paper("Azbuka", "Saratov", "PaperEnt", 2021, 3, "Paper", 1223, dateTime, "");
            Assert.AreEqual(true, comparisonerResources.ComparePaper(paper1, paper2));
        }
        #endregion

        #region ComparePatent
        [TestMethod]
        public void ComparePatentsSameTrue()
        {
            DateTime dateTime1 = new DateTime(2007, 3, 1, 7, 0, 0);
            DateTime dateTime2 = new DateTime(2008, 3, 1, 7, 0, 0);
            List<Author> authors = new List<Author>() { new Author("Ivan", "Ivanov"), new Author("Artem", "Petrov") };
            Patent patent1 = new Patent("Phone", authors, "Russia", 132, dateTime1, dateTime2, 12, "");
            Patent patent2 = new Patent("Phone", authors, "Russia", 132, dateTime1, dateTime2, 12, "");
            Assert.AreEqual(true, comparisonerResources.ComparePatent(patent1, patent2));
        }

        [TestMethod]
        public void ComparePatentsVariousRegistrationNumberAndCountryFalse()
        {
            DateTime dateTime = new DateTime(2008, 3, 1, 7, 0, 0);
            Paper paper1 = new Paper("Azbuka", "Russia", "PaperEnt", 2021, 3, "Paper", 12345, dateTime, "ISSN 1233-1213");
            Paper paper2 = new Paper("Azbuka", "USA", "PaperEnt", 2021, 3, "Paper", 123, dateTime, "ISSN 1233-1213");
            Assert.AreEqual(false, comparisonerResources.ComparePaper(paper1, paper2));
        }

        [TestMethod]
        public void ComparePatentsVariousRegistrationNumberSameCountryFalse()
        {
            DateTime dateTime = new DateTime(2008, 3, 1, 7, 0, 0);
            Paper paper1 = new Paper("Azbuka", "USA", "PaperEnt", 2021, 3, "Paper", 12234, dateTime, "");
            Paper paper2 = new Paper("Club", "USA", "PaperEntSaratov", 2021, 3, "Paper", 1223, DateTime.Now, "");
            Assert.AreEqual(false, comparisonerResources.ComparePaper(paper1, paper2));
        }

        [TestMethod]
        public void ComparePatentsSameRegistrationNumberVariousCountryFalse()
        {
            DateTime dateTime = new DateTime(2008, 3, 1, 7, 0, 0);
            Paper paper1 = new Paper("Azbuka", "Russia", "PaperEnt", 2021, 3, "Paper", 1223, dateTime, "");
            Paper paper2 = new Paper("Club", "USA", "PaperEntSaratov", 2021, 3, "Paper", 1223, DateTime.Now, "");
            Assert.AreEqual(false, comparisonerResources.ComparePaper(paper1, paper2));
        }
        #endregion
    }
}
