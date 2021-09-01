using Epam.Library.BLL.DateCheck;
using Epam.Library.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Epam.Library.UnitTests.BLLTests.DateCheckTests.CheckersTests
{
    [TestClass]
    public class BookDateCheckerTest
    {
        BookDateChecker bookDateChecker;

        public BookDateCheckerTest()
        {
            bookDateChecker = new BookDateChecker();
        }

        #region IsAuthorsCorrect
        [TestMethod]
        public void IsAuthorsCorrectAllAuthorsCorrectTrue()
        {
            Author author1 = new Author("Ivan", "Ivanov");
            Author author2 = new Author("Ramil", "Gukov");
            List<Author> authors = new List<Author>() { author1, author2 };

            Assert.AreEqual(true, bookDateChecker.IsAuthorsCorrect(authors));
        }

        [TestMethod]
        public void IsAuthorsCorrectOneAuthorInCorrectFalse()
        {
            Author author1 = new Author("Ivan", "Ivanov");
            Author inCorrectauthor2 = new Author("IncorrectName", "Gukov");
            List<Author> authors = new List<Author>() { author1, inCorrectauthor2 };

            Assert.AreEqual(false, bookDateChecker.IsAuthorsCorrect(authors));
        }

        [TestMethod]
        public void IsAuthorsCorrectAuthorsInCorrectFalse()
        {
            Author inCorrectauthor1 = new Author("IncorrectName", "Ivanov");
            Author inCorrectauthor2 = new Author("IncorrectName", "Gukov");
            List<Author> authors = new List<Author>() { inCorrectauthor1, inCorrectauthor2 };

            Assert.AreEqual(false, bookDateChecker.IsAuthorsCorrect(authors));
        }
        #endregion

        #region IsISBNCorrect
        [TestMethod]
        public void IsISBNCorrectTrue()
        {
            Assert.AreEqual(true, bookDateChecker.IsISBNCorrect("ISBN 7-12-12-0"));
        }

        public void IsISBNCorrectInCorrectISBNFalse()
        {
            Assert.AreEqual(false, bookDateChecker.IsISBNCorrect("ISBN7-12-12-0"));
        }
        #endregion

        #region IsYearOfPublishingCorrect
        [TestMethod]
        public void IsYearOfPublishingCorrectTrue()
        {
            Assert.AreEqual(true, bookDateChecker.IsYearOfPublishingCorrect(1988));
        }

        [TestMethod]
        public void IsYearOfPublishingCorrectYearLessMinYearOfPublishingFalse()
        {
            Assert.AreEqual(false, bookDateChecker.IsYearOfPublishingCorrect(1399));
        }

        public void IsYearOfPublishingCorrectYearMoreMaxYearOfPublishingFalse()
        {
            Assert.AreEqual(false, bookDateChecker.IsYearOfPublishingCorrect(DateTime.Now.Year + 1));
        }

        #endregion
    }
}
