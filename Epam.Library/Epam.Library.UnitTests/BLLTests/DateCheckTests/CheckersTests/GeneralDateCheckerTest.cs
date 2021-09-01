using Epam.Library.BLL.DateCheck;
using Epam.Library.Entities;
using Epam.Library.RAMMemoryDAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Epam.Library.UnitTests.BLLTests.DateCheckTests.CheckersTests
{
    [TestClass]
    public class GeneralDateCheckerTest
    {
        GeneralDateChecker generalDateChecker;

        public GeneralDateCheckerTest()
        {
            generalDateChecker = new GeneralDateChecker();
        }
         

        #region IsNameCorrect
        [TestMethod]
        public void IsNameCorrectTrue()
        {
            Assert.AreEqual(true, generalDateChecker.IsNameCorrect("Name"));
        }

        [TestMethod]
        public void IsNameCorrectLongNameFalse()
        {
            string Longname = "".PadLeft(301, 'a');
            Assert.AreEqual(false, generalDateChecker.IsNameCorrect(Longname));
        }

        #endregion

        #region IsNoteCorrect

        [TestMethod]
        public void IsNoteCorrectTrue()
        {
            Assert.AreEqual(true, generalDateChecker.IsNoteCorrect("Name"));
        }

        [TestMethod]
        public void IsNoteCorrectLongNoteFalse()
        {
            string longNote = "".PadLeft(2001, 'a');
            Assert.AreEqual(false, generalDateChecker.IsNoteCorrect(longNote));
        }
        #endregion

        #region IsAuthorCorrect
        [TestMethod]
        public void IsAuthorCorrectTrue()
        {
            Author author = new Author("Name", "Surname");
            Assert.AreEqual(true, generalDateChecker.IsAuthorCorrect(author));
        }

        [TestMethod]
        public void IsAuthorCorrectTrueLongNameFalse()
        {
            string longname = "A".PadRight(51, 'a');
            Author author = new Author(longname, "Surname");
            Assert.AreEqual(false, generalDateChecker.IsAuthorCorrect(author));
        }

        [TestMethod]
        public void IsAuthorCorrectTrueLongSurnameFalse()
        {
            string longSurname = "A".PadRight(201, 'a');
            Author author = new Author("Name", longSurname);
            Assert.AreEqual(false, generalDateChecker.IsAuthorCorrect(author));
        }

        [TestMethod]
        public void IsAuthorCorrectDifferentLanguagesNameFalse()
        {
            Author author = new Author("Nameимя", "Surname");
            Assert.AreEqual(false, generalDateChecker.IsAuthorCorrect(author));
        }

        [TestMethod]
        public void IsAuthorCorrectDifferentLanguagesSurNameFalse()
        {
            Author author = new Author("Name", "Surnameфамилия");
            Assert.AreEqual(false, generalDateChecker.IsAuthorCorrect(author));
        }

        [TestMethod]
        public void IsAuthorCorrectNameWithHyphenTrue()
        {
            Author author = new Author("Name-Name", "Surname");
            Assert.AreEqual(true, generalDateChecker.IsAuthorCorrect(author));
        }

        [TestMethod]
        public void IsAuthorCorrectNameWithHyphenFalse()
        {
            Author author = new Author("Name-name", "Surname");
            Assert.AreEqual(false, generalDateChecker.IsAuthorCorrect(author));
        }

        [TestMethod]
        public void IsAuthorCorrectNameHyphenFirstFalse()
        {
            Author author = new Author("-Name", "Surname");
            Assert.AreEqual(false, generalDateChecker.IsAuthorCorrect(author));
        }

        [TestMethod]
        public void IsAuthorCorrectNameHyphenLastFalse()
        {
            Author author = new Author("Name-", "Surname");
            Assert.AreEqual(false, generalDateChecker.IsAuthorCorrect(author));
        }

        [TestMethod]
        public void IsAuthorCorrectEmptyNameFalse()
        {
            Author author = new Author("", "Surname");
            Assert.AreEqual(false, generalDateChecker.IsAuthorCorrect(author));
        }

        [TestMethod]
        public void IsAuthorCorrectEmptySurNameFalse()
        {
            Author author = new Author("Name", "");
            Assert.AreEqual(false, generalDateChecker.IsAuthorCorrect(author));
        }

        [TestMethod]
        public void IsAuthorCorrectFamilyPrefixesSmallLetterSurNameFalse()
        {
            Author author = new Author("Name", "de surname");
            Assert.AreEqual(false, generalDateChecker.IsAuthorCorrect(author));
        }

        [TestMethod]
        public void IsAuthorCorrectFamilyPrefixesSurNameTrue()
        {
            Author author = new Author("Name", "de Surname");
            Assert.AreEqual(false, generalDateChecker.IsAuthorCorrect(author));
        }

        [TestMethod]
        public void IsAuthorCorrectSurNameWithHyphenFalse()
        {
            Author author = new Author("Name", "Surname-surname");
            Assert.AreEqual(false, generalDateChecker.IsAuthorCorrect(author));
        }

        [TestMethod]
        public void IsAuthorCorrectSurNameHyphenFirstFalse()
        {
            Author author = new Author("Name", "-Surname");
            Assert.AreEqual(false, generalDateChecker.IsAuthorCorrect(author));
        }

        public void IsAuthorCorrectSurNameHyphenLastFalse()
        {
            Author author = new Author("Name", "Surname-");
            Assert.AreEqual(false, generalDateChecker.IsAuthorCorrect(author));
        }
        #endregion

        #region IsPublisherCorrect
        [TestMethod]
        public void IsPublisherCorrectTrue()
        {
            Assert.AreEqual(true, generalDateChecker.IsPublisherCorrect("publisher"));
        }

        [TestMethod]
        public void IsPublisherCorrectLongPublisherFalse()
        {
            string publisher = "".PadLeft(301, 'a');
            Assert.AreEqual(false, generalDateChecker.IsPublisherCorrect(publisher));
        }

        [TestMethod]
        public void IsPublisherEmptyPublisherFalse()
        {
            string publisher = "";
            Assert.AreEqual(false, generalDateChecker.IsPublisherCorrect(publisher));
        }
        #endregion

        #region IsNumberOfPagesCorrect
        [TestMethod]
        public void IsNumberOfPagesCorrectTrue()
        {
            Assert.AreEqual(true, generalDateChecker.IsNumberOfPagesCorrect(12));
        }

        [TestMethod]
        public void IsNumberOfPagesCorrectNegativeNumberFalse()
        {
            Assert.AreEqual(false, generalDateChecker.IsNumberOfPagesCorrect(-1));
        }
        #endregion

        #region IsPlaceOfPublicationCorrect
        [TestMethod]
        public void IsPlaceOfPublicationTrue()
        {
            Assert.AreEqual(true, generalDateChecker.IsPlaceOfPublicationCorrect("Saratov"));
        }

        [TestMethod]
        public void IsPlaceOfPublicationLongNameFalse()
        {
            string Longname = "".PadLeft(201, 'a');
            Assert.AreEqual(false, generalDateChecker.IsPlaceOfPublicationCorrect(Longname));
        }

        [TestMethod]
        public void IsPlaceOfPublicationHyphenFirstFalse()
        {
            Assert.AreEqual(false, generalDateChecker.IsPlaceOfPublicationCorrect("-Saratov"));
        }

        [TestMethod]
        public void IsPlaceOfPublicationHyphenLastFalse()
        {
            Assert.AreEqual(false, generalDateChecker.IsPlaceOfPublicationCorrect("Saratov-"));
        }

        [TestMethod]
        public void IsPlaceOfPublicationEmptyNameFalse()
        {
            Assert.AreEqual(false, generalDateChecker.IsPlaceOfPublicationCorrect(""));
        }

        [TestMethod]
        public void IsPlaceOfPublicationWithSpaceSmallLetterTrue()
        {
            Assert.AreEqual(true, generalDateChecker.IsPlaceOfPublicationCorrect("Saratov saratov"));
        }

        [TestMethod]
        public void IsPlaceOfPublicationWithSpaceTrue()
        {
            Assert.AreEqual(true, generalDateChecker.IsPlaceOfPublicationCorrect("Saratov Saratov"));
        }
        #endregion


        [TestCleanup]
        public void Cleanup()
        {
            RAMMemory.Library.Clear();
        }
    }
}
