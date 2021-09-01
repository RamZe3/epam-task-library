using Epam.Library.Entities;
using Epam.Library.Entities.Interfaces;
using Epam.Library.RAMMemoryDAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Library.UnitTests.RAMMemoryDALTests
{
    [TestClass]
    public class RAMMemoryTests
    {
        public RAMMemory rAMMemory;

        public List<Author> authors;
        public Book bookWithAuthorIvanIvanov;
        public Patent patentWithAuthorIvanIvanov;
        public Book bookWithNameClub;
        public Patent patentWithNameClub;
        public Book bookWithPublisherSarBook;
        public Book bookWithPublisherSarEnt;
        public Book bookWithPublisherBookSar;

        public RAMMemoryTests()
        {
            rAMMemory = new RAMMemory();
        }

        [TestInitialize]
        public void Initialize()
        {
            DateTime dateTime1 = new DateTime(2009, 3, 1, 7, 0, 0);
            DateTime dateTime2 = new DateTime(2010, 3, 1, 7, 0, 0);

            authors = new List<Author>() { new Author("Ivan", "Ivanov"), new Author("Artem", "Petrov") };

            bookWithAuthorIvanIvanov = new Book("TestBook1", authors, "Saratov", "BookSar", 2000, 12, "note", "ISBN 8-12-12-1");
            patentWithAuthorIvanIvanov = new Patent("Phone", authors, "Russia", 132, dateTime1, dateTime2, 12, "");

            bookWithNameClub = new Book("Club", authors, "Saratov", "BookSar", 2000, 12, "note", "ISBN 8-12-12-1");
            patentWithNameClub = new Patent("Club", authors, "Russia", 132, dateTime1, dateTime2, 12, "");

            bookWithPublisherSarBook = new Book("TestBook2", authors, "Saratov", "SarBook", 2000, 12, "note", "ISBN 8-12-12-1");
            bookWithPublisherSarEnt = new Book("TestBook3", authors, "Saratov", "SarEnt", 2000, 12, "note", "ISBN 8-12-12-1");
            bookWithPublisherBookSar = new Book("TestBook4", authors, "Saratov", "BookSar", 2000, 12, "note", "ISBN 8-12-12-1");

            RAMMemory.Library.Add(bookWithAuthorIvanIvanov);
            RAMMemory.Library.Add(patentWithAuthorIvanIvanov);
            RAMMemory.Library.Add(bookWithNameClub);
            RAMMemory.Library.Add(patentWithNameClub);
            RAMMemory.Library.Add(bookWithPublisherSarBook);
            RAMMemory.Library.Add(bookWithPublisherSarEnt);
            RAMMemory.Library.Add(bookWithPublisherBookSar);
        }

        #region FindBooksByAuthor
        [TestMethod]
        public void FindBooksByAuthorСoincidentallyTrue()
        {
            Author author = new Author("Ivan", "Ivanov");
            List<Book> books = rAMMemory.FindBooksByAuthor(author);

            Assert.AreEqual(true, books.Contains(bookWithAuthorIvanIvanov));
        }

        [TestMethod]
        public void FindBooksByAuthorNoCoincidenceFalse()
        {
            Author author = new Author("Ramil", "Gukov");
            List<Book> books = rAMMemory.FindBooksByAuthor(author);

            Assert.AreEqual(true, books.Count == 0);
        }
        #endregion

        #region FindPatentsByAuthor
        [TestMethod]
        public void FindPatentsByAuthorСoincidentallyTrue()
        {
            Author author = new Author("Ivan", "Ivanov");
            List<Patent> patents = rAMMemory.FindPatentsByAuthor(author);

            Assert.AreEqual(true, patents.Contains(patentWithAuthorIvanIvanov));
        }

        [TestMethod]
        public void FindPatentsByAuthorNoCoincidenceTrue()
        {
            Author author = new Author("Ramil", "Gukov");
            List<Patent> patents = rAMMemory.FindPatentsByAuthor(author);

            Assert.AreEqual(true, patents.Count == 0);
        }
        #endregion

        #region FindPatentsAndBooksByAuthor
        [TestMethod]
        public void FindPatentsAndBooksByAuthorСoincidentallyTrue()
        {
            Author author = new Author("Ivan", "Ivanov");
            List<InformationResource> patentsAndBooks = rAMMemory.FindPatentsAndBooksByAuthor(author);

            Assert.AreEqual(true, patentsAndBooks.Contains(bookWithAuthorIvanIvanov) && patentsAndBooks.Contains(patentWithAuthorIvanIvanov));
        }

        [TestMethod]
        public void FindPatentsAndBooksByAuthorNoCoincidenceFalse()
        {
            Author author = new Author("Ramil", "Gukov");
            List<InformationResource> patentsAndBooks = rAMMemory.FindPatentsAndBooksByAuthor(author);

            Assert.AreEqual(true, patentsAndBooks.Count == 0);
        }
        #endregion

        #region FindResourcesByName
        [TestMethod]
        public void FindResourcesByNameСoincidentallyTrue()
        {
            List<InformationResource> informationResources = rAMMemory.FindResourcesByName("Club");

            Assert.AreEqual(true, informationResources.Contains(bookWithNameClub) && informationResources.Contains(patentWithNameClub));
        }

        [TestMethod]
        public void FindResourcesByNameNoCoincidenceTrue()
        {
            List<InformationResource> informationResources = rAMMemory.FindResourcesByName("No Club");

            Assert.AreEqual(true, informationResources.Count == 0);
        }
        #endregion

        #region GetLibrary
        [TestMethod]
        public void GetLibraryTrue()
        {

            Assert.AreEqual(true, rAMMemory.GetLibrary() == RAMMemory.Library);
        }
        #endregion

        #region SmartBookSearchByPublisher
        [TestMethod]
        public void SmartBookSearchByPublisherTwoOccurrencesTrue()
        {
            Dictionary<string ,List<Book>> books = rAMMemory.SmartBookSearchByPublisher("Sar");

            List<Book> booksWithPublisherSarBook = new List<Book>();
            List<Book> booksWithPublisherSarEnt = new List<Book>();

            books.TryGetValue("SarBook", out booksWithPublisherSarBook);
            books.TryGetValue("SarEnt", out booksWithPublisherSarEnt);

            Assert.AreEqual(2, books.Count);
            Assert.AreEqual(true, books.ContainsKey("SarBook"));
            Assert.AreEqual(true, books.ContainsKey("SarEnt"));
            Assert.AreEqual(true, booksWithPublisherSarBook.Contains(bookWithPublisherSarBook));
            Assert.AreEqual(true, booksWithPublisherSarEnt.Contains(bookWithPublisherSarEnt));
        }

        [TestMethod]
        public void SmartBookSearchByPublisherOneOccurrencesTrue()
        {
            Dictionary<string, List<Book>> books = rAMMemory.SmartBookSearchByPublisher("Book");
            List<Book> booksWithPublisherBookSar = new List<Book>();
            books.TryGetValue("BookSar", out booksWithPublisherBookSar);

            Assert.AreEqual(1, books.Count);
            Assert.AreEqual(true, books.ContainsKey("BookSar"));
            Assert.AreEqual(true, booksWithPublisherBookSar.Contains(bookWithPublisherBookSar));
        }
        #endregion

        #region GetSortedLibraryByYearOfPublishing
        [TestMethod]
        public void GetSortedLibraryByYearOfPublishingTrue()
        {
            List<InformationResource> informationResources = rAMMemory.GetSortedLibraryByYearOfPublishing(false);
            IEnumerable<IHaveYearOfPublishing> iHaveYearOfPublishingresources = informationResources.OfType<IHaveYearOfPublishing>();

            IHaveYearOfPublishing lastItem = null;
            bool normalOrder = true;
            foreach (var item in iHaveYearOfPublishingresources)
            {
                if (lastItem == null)
                {
                    lastItem = item;
                }
                else
                {
                    if (!(lastItem.GetYearOfPublishing() <= item.GetYearOfPublishing()))
                    {
                        normalOrder = false;
                    }
                }
            }

            Assert.AreEqual(true, normalOrder);
        }

        [TestMethod]
        public void GetSortedLibraryByYearOfPublishingWithReverseTrue()
        {
            List<InformationResource> informationResources = rAMMemory.GetSortedLibraryByYearOfPublishing(true);
            IEnumerable<IHaveYearOfPublishing> iHaveYearOfPublishingresources = informationResources.OfType<IHaveYearOfPublishing>();

            IHaveYearOfPublishing lastItem = null;
            bool normalOrder = true;
            foreach (var item in iHaveYearOfPublishingresources)
            {
                if (lastItem == null)
                {
                    lastItem = item;
                }
                else
                {
                    if (!(lastItem.GetYearOfPublishing() >= item.GetYearOfPublishing()))
                    {
                        normalOrder = false;
                    }
                }
            }

            Assert.AreEqual(true, normalOrder);
        }
        #endregion

        #region GroupingResourceByYearOfPublication
        public void GroupingResourceByYearOfPublicationTrue()
        {
            Dictionary<int, List<InformationResource>> GroupingInformationResource = rAMMemory.GroupingResourceByYearOfPublication();
            List<InformationResource> resYearOfPublication2000 = new List<InformationResource>();
            List<InformationResource> resYearOfPublication2009 = new List<InformationResource>();
            GroupingInformationResource.TryGetValue(2000, out resYearOfPublication2000);
            GroupingInformationResource.TryGetValue(2009, out resYearOfPublication2009);

            Assert.AreEqual(2, GroupingInformationResource.Count);
            Assert.AreEqual(true, GroupingInformationResource.ContainsKey(2000));
            Assert.AreEqual(true, GroupingInformationResource.ContainsKey(2009));
            Assert.AreEqual(true, resYearOfPublication2000.Contains(bookWithPublisherSarBook));
            Assert.AreEqual(true, resYearOfPublication2009.Contains(patentWithNameClub));
        }
        #endregion

        [TestCleanup]
        public void Cleanup()
        {
            RAMMemory.Library.Clear();
        }
    }
}
