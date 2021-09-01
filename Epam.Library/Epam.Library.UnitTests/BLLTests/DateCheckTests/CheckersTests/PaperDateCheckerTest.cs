using Epam.Library.BLL.DateCheck;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Epam.Library.UnitTests.BLLTests.DateCheckTests.CheckersTests
{
    [TestClass]
    public class PaperDateCheckerTest
    {
        PaperDateChecker paperDateChecker;

        public PaperDateCheckerTest()
        {
            paperDateChecker = new PaperDateChecker();
        }

        #region IsYearOfPublishingCorrect
        [TestMethod]
        public void IsYearOfPublishingCorrectTrue()
        {
            Assert.AreEqual(true, paperDateChecker.IsYearOfPublishingCorrect(1988));
        }

        [TestMethod]
        public void IsYearOfPublishingCorrectYearLessMinYearOfPublishingFalse()
        {
            Assert.AreEqual(false, paperDateChecker.IsYearOfPublishingCorrect(1399));
        }

        public void IsYearOfPublishingCorrectYearMoreMaxYearOfPublishingFalse()
        {
            Assert.AreEqual(false, paperDateChecker.IsYearOfPublishingCorrect(DateTime.Now.Year + 1));
        }

        #endregion

        #region IsNumberCorrect
        [TestMethod]
        public void IsNumberCorrectPositiveTrue()
        {
            Assert.AreEqual(true, paperDateChecker.IsNumberCorrect(1));
        }

        [TestMethod]
        public void IsNumberCorrectNegativeFalse()
        {
            Assert.AreEqual(false, paperDateChecker.IsNumberCorrect(-1));
        }
        #endregion

        #region IsDateCorrect
        [TestMethod]
        public void IsDateCorrectSameYearTrue()
        {
            DateTime dateTime = new DateTime(2002, 7, 8);
            int year = 2002;
            Assert.AreEqual(true, paperDateChecker.IsDateCorrect(year, dateTime));
        }

        [TestMethod]
        public void IsDateCorrectDifferentYearFalse()
        {
            DateTime dateTime = new DateTime(2002, 7, 8);
            int year = 2003;
            Assert.AreEqual(false, paperDateChecker.IsDateCorrect(year, dateTime));
        }
        #endregion

        #region IsISSNCorrect
        [TestMethod]
        public void IsISSNCorrectTrue()
        {
            Assert.AreEqual(true, paperDateChecker.IsISSNCorrect("ISSN 1233-1213"));
        }

        public void IsISSNCorrectInCorrectISSNFalse()
        {
            Assert.AreEqual(false, paperDateChecker.IsISSNCorrect("ISSN1233-1213"));
        }
        #endregion
    }
}
