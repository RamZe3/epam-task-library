using Epam.Library.BLL;
using Epam.Library.BLL.Interfaces;
using Epam.Library.DAL.Interfaces;
using Epam.Library.Dependencies;
using Epam.Library.Entities;
using Epam.Library.Entities.Exceptions;
using Epam.Library.RAMMemoryDAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace IntegrationTests
{
    [TestClass]
    public class BookLogicTest
    {
        public IBookLogic bookLogic;
        public IBookDAL bookDAL;

        public Book testBook;

        public Book UniqueBook;
        public Book NotUniqueBook;
        public List<Author> authors;

        public BookLogicTest()
        {
            DependenciesResolver dependenciesResolver = new DependenciesResolver();
            bookDAL = dependenciesResolver.bookDAL;
            bookLogic = dependenciesResolver.bookLogic;
        }

        [TestInitialize]
        public void Initialize()
        {
            authors = new List<Author>() { new Author("Ivan", "Ivanov"), new Author("Artem", "Petrov") };

            UniqueBook = new Book("TestBook", authors, "Saratov", "BookSar", 2000, 12, "note", "ISBN 7-12-12-0");
            NotUniqueBook = new Book("NotUniqueBook", authors, "Saratov", "BookSar", 2000, 12, "note", "ISBN 7-12-12-1");

            testBook = new Book("TestBook", authors, "Saratov", "BookSar", 2000, 12, "note", "ISBN 7-12-12-0");

            bookLogic.AddBook(NotUniqueBook);
        }

        #region AddBook
        [TestMethod]
        public void AddBookNotUniqueBookThrowsInvalidOperationException()
        {
            Assert.ThrowsException<System.InvalidOperationException>(() => bookLogic.AddBook(NotUniqueBook));
        }

        [TestMethod]
        public void AddBookUniqueBookTrue()
        {
            Assert.AreEqual(true, bookLogic.AddBook(UniqueBook).Count == 0);
        }


        [TestMethod]
        public void AddBookNameIncorrectTrue()
        {
            Book book = new Book("", authors, "Saratov", "BookSar", 2000, 12, "note", "ISBN 7-12-12-0");
            List<DataValidationError> dataValidationExceptions = bookLogic.AddBook(book);
            Assert.AreEqual(true, dataValidationExceptions.Count == 1 &&
                dataValidationExceptions.Exists(x => x.ErrorValue == book.Name)
                && dataValidationExceptions.Exists(x => x.Message == "Name validation exception"));
        }
        #endregion

        #region DeleteBook
        [TestMethod]
        public void DeleteBookTrue()
        {
            bookLogic.AddBook(testBook);
            Assert.AreEqual(true, bookLogic.DeleteBook(testBook.Id));
        }

        [TestMethod]
        public void DeleteBookFalse()
        {
            Assert.AreEqual(false, bookLogic.DeleteBook(testBook.Id));
        }
        #endregion

        [TestCleanup]
        public void Cleanup()
        {
            RAMMemory.Library.Clear();
        }
    }
}
