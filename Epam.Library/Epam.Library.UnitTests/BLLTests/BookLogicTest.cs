using Epam.Library.BLL;
using Epam.Library.DAL.Interfaces;
using Epam.Library.Entities;
using Epam.Library.Entities.Exceptions;
using Epam.Library.RAMMemoryDAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace Epam.Library.UnitTests.BLLTests
{
    [TestClass]
    public class BookLogicTest
    {
        public Mock<IBookDAL> IBookDALMock;

        public BookLogic bookLogic;

        public Book testBook;

        public Book UniqueBook;
        public Book NotUniqueBook;
        public List<Author> authors;

        public Guid DeletedBookGuid;



        public BookLogicTest()
        {
            DeletedBookGuid = Guid.NewGuid();

            IBookDALMock = new Mock<IBookDAL>();

            IBookDALMock.Setup(b => b.AddBook(It.IsAny<Book>())).Returns(() => true);
            IBookDALMock.Setup(b => b.AddBook(It.Is<Book>(x => x.Name == "NotUniqueBook"))).Throws(new InvalidOperationException());

            IBookDALMock.Setup(b => b.DeleteBook(It.IsAny<Guid>())).Returns(() => true);
            IBookDALMock.Setup(b => b.DeleteBook(It.Is<Guid>(x => x == DeletedBookGuid))).Returns(() => false);

            bookLogic = new BookLogic(IBookDALMock.Object);
        }

        [TestInitialize]
        public void Initialize()
        {
            authors = new List<Author>() { new Author("Ivan", "Ivanov"), new Author("Artem", "Petrov") };

            UniqueBook = new Book("TestBook", authors, "Saratov", "BookSar", 2000, 12, "note", "ISBN 7-12-12-0");
            NotUniqueBook = new Book("NotUniqueBook", authors, "Saratov", "BookSar", 2000, 12, "note", "ISBN 7-12-12-0");

            testBook = new Book("TestBook", authors, "Saratov", "BookSar", 2000, 12, "note", "ISBN 7-12-12-0");
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

        [TestMethod]
        public void AddBookIsAuthorsCorrectIncorrectTrue()
        {
            authors.Clear();
            Book book = new Book("Name", authors, "Saratov", "BookSar", 2000, 12, "note", "ISBN 7-12-12-0");
            List<DataValidationError> dataValidationExceptions = bookLogic.AddBook(book);
            Assert.AreEqual(true, dataValidationExceptions.Count == 1 &&
                dataValidationExceptions.Exists(x => x.ErrorValue == "")
                && dataValidationExceptions.Exists(x => x.Message == "Authors validation exception"));
        }

        [TestMethod]
        public void AddBookPlaceOfPublicationIncorrectTrue()
        {
            Book book = new Book("Name", authors, "USa", "BookSar", 2000, 12, "note", "ISBN 7-12-12-0");
            List<DataValidationError> dataValidationExceptions = bookLogic.AddBook(book);
            Assert.AreEqual(true, dataValidationExceptions.Count == 1 &&
                dataValidationExceptions.Exists(x => x.ErrorValue == book.PlaceOfPublication)
                && dataValidationExceptions.Exists(x => x.Message == "PlaceOfPublication validation exception"));
        }

        [TestMethod]
        public void AddBookPublisherIncorrectTrue()
        {
            string publisher = "".PadLeft(301, 'a');
            Book book = new Book("Name", authors, "Saratov", publisher, 2000, 12, "note", "ISBN 7-12-12-0");
            List<DataValidationError> dataValidationExceptions = bookLogic.AddBook(book);
            Assert.AreEqual(true, dataValidationExceptions.Count == 1 &&
                dataValidationExceptions.Exists(x => x.ErrorValue == book.Publisher)
                && dataValidationExceptions.Exists(x => x.Message == "Publisher validation exception"));
        }

        [TestMethod]
        public void AddBookYearOfPublishingIncorrectTrue()
        {
            Book book = new Book("Name", authors, "Saratov", "BookSar", 2022, 12, "note", "ISBN 7-12-12-0");
            List<DataValidationError> dataValidationExceptions = bookLogic.AddBook(book);
            Assert.AreEqual(true, dataValidationExceptions.Count == 1 &&
                dataValidationExceptions.Exists(x => x.ErrorValue == book.YearOfPublishing.ToString())
                && dataValidationExceptions.Exists(x => x.Message == "YearOfPublishing validation exception"));
        }

        [TestMethod]
        public void AddBookNoteIncorrectTrue()
        {
            string note = "".PadLeft(2001, 'a');
            Book book = new Book("Name", authors, "Saratov", "BookSar", 2000, 12, note, "ISBN 7-12-12-0");
            List<DataValidationError> dataValidationExceptions = bookLogic.AddBook(book);
            Assert.AreEqual(true, dataValidationExceptions.Count == 1 &&
                dataValidationExceptions.Exists(x => x.ErrorValue == book.Note)
                && dataValidationExceptions.Exists(x => x.Message == "Note validation exception"));
        }

        [TestMethod]
        public void AddBookNumberOfPagesIncorrectTrue()
        {
            Book book = new Book("Name", authors, "Saratov", "BookSar", 2000, -12, "note", "ISBN 7-12-12-0");
            List<DataValidationError> dataValidationExceptions = bookLogic.AddBook(book);
            Assert.AreEqual(true, dataValidationExceptions.Count == 1 &&
                dataValidationExceptions.Exists(x => x.ErrorValue == book.NumberOfPages.ToString())
                && dataValidationExceptions.Exists(x => x.Message == "NumberOfPages validation exception"));
        }

        [TestMethod]
        public void AddBookISBNIncorrectTrue()
        {
            Book book = new Book("Name", authors, "Saratov", "BookSar", 2000, 12, "note", "ISN 7-12-12-0");
            List<DataValidationError> dataValidationExceptions = bookLogic.AddBook(book);
            Assert.AreEqual(true, dataValidationExceptions.Count == 1 &&
                dataValidationExceptions.Exists(x => x.ErrorValue == book.ISBN.ToString())
                && dataValidationExceptions.Exists(x => x.Message == "ISBN validation exception"));
        }
        #endregion

        #region DeleteBook
        [TestMethod]
        public void DeleteBookTrue()
        {
            Assert.AreEqual(true, bookLogic.DeleteBook(testBook.Id));
        }

        [TestMethod]
        public void DeleteBookFalse()
        {
            Assert.AreEqual(false, bookLogic.DeleteBook(DeletedBookGuid));
        }
        #endregion

        [TestCleanup]
        public void Cleanup()
        {
            RAMMemory.Library.Clear();
        }
    }
}
