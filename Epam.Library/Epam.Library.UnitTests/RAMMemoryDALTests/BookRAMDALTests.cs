using Epam.Library.Entities;
using Epam.Library.RAMMemoryDAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Epam.Library.UnitTests.RAMMemoryDALTests
{
    [TestClass]
    public class CompareResourcesTest
    {
        public BookRAMDAL bookRAMDAL;
        public Book testBook;

        public CompareResourcesTest()
        {
            bookRAMDAL = new BookRAMDAL();
        }

        [TestInitialize]
        public void Initialize()
        {
            List<Author> authors = new List<Author>() { new Author("Ivan", "Ivanov"), new Author("Artem", "Petrov") };

            testBook = new Book("TestBook", authors, "Saratov", "BookSar", 2000, 12, "note", "ISBN 8-12-12-0");
            Book testBookWithoutISBN = new Book("testBookWithoutISBN", authors, "Saratov", "BookSar", 2000, 12, "note", "");

            bookRAMDAL.AddBook(testBook);
            bookRAMDAL.AddBook(testBookWithoutISBN);
        }

        #region AddBook
        [TestMethod]
        public void AddBookUniqueBookTrue()
        {
            List<Author> authors = new List<Author>() { new Author("Ivan", "Ivanov"), new Author("Artem", "Petrov") };

            Book book1 = new Book("Whole", authors, "Saratov", "Book", 2000, 12, "note", "ISBN 8-13-12-0");
            Assert.AreEqual(true, bookRAMDAL.AddBook(book1));
        }

        [TestMethod]
        public void AddBookSameISBNThrowInvalidOperationException()
        {
            List<Author> authors = new List<Author>() { new Author("Ivan", "Ivanov"), new Author("Artem", "Petrov") };

            Book book1 = new Book("Club", authors, "Samara", "SarBook", 2000, 12, "note", "ISBN 8-12-12-0");
            Assert.ThrowsException<System.InvalidOperationException>(() => bookRAMDAL.AddBook(book1));
        }

        [TestMethod]
        public void AddBookSameNamePublisherAndAuthorsThrowInvalidOperationException()
        {
            List<Author> authors = new List<Author>() { new Author("Ivan", "Ivanov"), new Author("Artem", "Petrov") };

            Book book1 = new Book("testBookWithoutISBN", authors, "Saratov", "BookSar", 2000, 12, "note", "");
            Assert.ThrowsException<System.InvalidOperationException>(() => bookRAMDAL.AddBook(book1));
        }
        #endregion

        #region DeleteBook
        [TestMethod]
        public void DeleteBookTrue()
        {
            Assert.AreEqual(true, bookRAMDAL.DeleteBook(testBook.Id));
        }

        [TestMethod]
        public void DeleteBookFalse()
        {
            bookRAMDAL.DeleteBook(testBook.Id);
            Assert.AreEqual(false, bookRAMDAL.DeleteBook(testBook.Id));
        }
        #endregion

        [TestCleanup]
        public void Cleanup()
        {
            RAMMemory.Library.Clear();
        }
    }
}