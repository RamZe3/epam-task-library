using Epam.Library.BLL.DateCheck;
using Epam.Library.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Epam.Library.UnitTests.BLLTests.DateCheckTests.CheckersTests
{
    [TestClass]
    public class PatentDateCheckerTest
    {
        PatentDateChecker patentDateChecker;

        public PatentDateCheckerTest()
        {
            patentDateChecker = new PatentDateChecker();
        }

        #region IsInventorsCorrect
        [TestMethod]
        public void IsInventorsCorrectAllAuthorsCorrectTrue()
        {
            Author author1 = new Author("Ivan", "Ivanov");
            Author author2 = new Author("Ramil", "Gukov");
            List<Author> authors = new List<Author>() { author1, author2 };

            Assert.AreEqual(true, patentDateChecker.IsInventorsCorrect(authors));
        }

        [TestMethod]
        public void IsInventorsCorrectOneAuthorInCorrectFalse()
        {
            Author author1 = new Author("Ivan", "Ivanov");
            Author inCorrectauthor2 = new Author("IncorrectName", "Gukov");
            List<Author> authors = new List<Author>() { author1, inCorrectauthor2 };

            Assert.AreEqual(false, patentDateChecker.IsInventorsCorrect(authors));
        }

        [TestMethod]
        public void IsInventorsCorrectAuthorsInCorrectFalse()
        {
            Author inCorrectauthor1 = new Author("IncorrectName", "Ivanov");
            Author inCorrectauthor2 = new Author("IncorrectName", "Gukov");
            List<Author> authors = new List<Author>() { inCorrectauthor1, inCorrectauthor2 };

            Assert.AreEqual(false, patentDateChecker.IsInventorsCorrect(authors));
        }
        #endregion

        #region IsRegistrationNumberCorrect
        [TestMethod]
        public void IsRegistrationNumberCorrectTrue()
        {
            Assert.AreEqual(true, patentDateChecker.IsRegistrationNumberCorrect(12));
        }

        [TestMethod]
        public void IsRegistrationNumberCorrectNegativeNumberFalse()
        {
            Assert.AreEqual(false, patentDateChecker.IsRegistrationNumberCorrect(-12));
        }

        [TestMethod]
        public void IsRegistrationNumberCorrectNumberMoreMaxSizeFalse()
        {
           // Assert.AreEqual(false, patentDateChecker.IsRegistrationNumberCorrect(999999999));
        }
        #endregion

        #region IsCountryCorrect
        [TestMethod]
        public void IsCountryCorrectTrue()
        {
            Assert.AreEqual(true, patentDateChecker.IsCountryCorrect("Russia"));
        }

        [TestMethod]
        public void IsCountryCorrectLongCountryFalse()
        {
            string country = "".PadLeft(201, 'a');
            Assert.AreEqual(false, patentDateChecker.IsCountryCorrect(country));
        }
        #endregion

        #region IsDateOfApplicationCorrect
        [TestMethod]
        public void IsDateOfApplicationCorrectTrue()
        {
            DateTime dateTime = new DateTime(2002, 9, 8);
            Assert.AreEqual(true, patentDateChecker.IsDateOfApplicationCorrect(dateTime));
        }

        [TestMethod]
        public void IsDateOfApplicationCorrectLessMinYearOfApplicationFalse()
        {
            DateTime dateTime = new DateTime(1473, 9, 8);
            Assert.AreEqual(false, patentDateChecker.IsDateOfApplicationCorrect(dateTime));
        }
        [TestMethod]
        public void IsDateOfApplicationCorrectMoreMaxYearOfPublishingFalse()
        {
            DateTime dateTime = new DateTime(DateTime.Now.Year+1, 9, 8);
            Assert.AreEqual(false, patentDateChecker.IsDateOfApplicationCorrect(dateTime));
        }

        #endregion

        #region IsDateOfPublicationCorrect
        [TestMethod]
        public void IsDateOfPublicationCorrectTrue()
        {
            DateTime dateOfApplication = new DateTime(2001, 9, 8);
            DateTime dateOfPublication = new DateTime(2002, 9, 8);
            Assert.AreEqual(true, patentDateChecker.IsDateOfPublicationCorrect(dateOfApplication, dateOfPublication));
        }

        [TestMethod]
        public void IsDateOfPublicationCorrectLessMinYearOfApplicationFalse()
        {
            DateTime dateOfApplication = new DateTime(2002, 9, 8);
            DateTime dateOfPublication = new DateTime(1472, 9, 8);
            Assert.AreEqual(false, patentDateChecker.IsDateOfPublicationCorrect(dateOfApplication, dateOfPublication));
        }
        [TestMethod]
        public void IsDateOfPublicationCorrectMoreMaxYearOfApplicationFalse()
        {
            DateTime dateOfApplication = new DateTime(2001, 9, 8);
            DateTime dateOfPublication = new DateTime(DateTime.Now.Year + 1, 9, 8);
            Assert.AreEqual(false, patentDateChecker.IsDateOfPublicationCorrect(dateOfApplication, dateOfPublication));
        }

        [TestMethod]
        public void IsDateOfPublicationCorrectLessDateOfApplicationFalse()
        {
            DateTime dateOfApplication = new DateTime(2002, 9, 8);
            DateTime dateOfPublication  = new DateTime(2001, 9, 8);
            Assert.AreEqual(false, patentDateChecker.IsDateOfPublicationCorrect(dateOfApplication, dateOfPublication));
        }
        #endregion
    }
}
