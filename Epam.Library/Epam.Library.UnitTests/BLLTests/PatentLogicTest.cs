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
    public class PatentLogicTest
    {
        public Mock<IPatentDAL> iPatentDALMock;
        public PatentLogic patentLogic;
        public Patent testPatent;

        public Guid DeletedPaperGuid;

        List<Author> authors;

        public Patent UniquePatent;
        public Patent NotUniquePatent;
        public PatentLogicTest()
        {
            DeletedPaperGuid = Guid.NewGuid();

            iPatentDALMock = new Mock<IPatentDAL>();

            iPatentDALMock.Setup(b => b.AddPatent(It.IsAny<Patent>())).Returns(() => true);
            iPatentDALMock.Setup(b => b.AddPatent(It.Is<Patent>(x => x.Name == "NotUniquePatent"))).Throws(new InvalidOperationException());

            iPatentDALMock.Setup(b => b.DeletePatent(It.IsAny<Guid>())).Returns(() => true);
            iPatentDALMock.Setup(b => b.DeletePatent(It.Is<Guid>(x => x == DeletedPaperGuid))).Returns(() => false);

            patentLogic = new PatentLogic(iPatentDALMock.Object);
        }

        [TestInitialize]
        public void Initialize()
        {
            DateTime dateTime1 = new DateTime(2009, 3, 1, 7, 0, 0);
            DateTime dateTime2 = new DateTime(2010, 3, 1, 7, 0, 0);
            authors = new List<Author>() { new Author("Ivan", "Ivanov"), new Author("Artem", "Petrov") };
            testPatent = new Patent("testPatent", authors, "Russia", 132, dateTime1, dateTime2, 12, "221");

            UniquePatent = new Patent("UniquePatent", authors, "Russia", 132, dateTime1, dateTime2, 12, "221");
            NotUniquePatent = new Patent("NotUniquePatent", authors, "Russia", 132, dateTime1, dateTime2, 12, "221");
        }

        #region AddPatent
        [TestMethod]
        public void AddPatentNotUniquePatentThrowsInvalidOperationException()
        {
            Assert.ThrowsException<System.InvalidOperationException>(() => patentLogic.AddPatent(NotUniquePatent));
        }

        [TestMethod]
        public void AddPatentUniquePatentTrue()
        {
            Assert.AreEqual(true, patentLogic.AddPatent(UniquePatent).Count == 0);
        }


        [TestMethod]
        public void AddPatentNameIncorrectTrue()
        {
            DateTime dateTime1 = new DateTime(2009, 3, 1, 7, 0, 0);
            DateTime dateTime2 = new DateTime(2010, 3, 1, 7, 0, 0);
            Patent patent = new Patent("", authors, "Russia", 132, dateTime1, dateTime2, 12, "221");
            List<DataValidationError> dataValidationExceptions = patentLogic.AddPatent(patent);
            Assert.AreEqual(true, dataValidationExceptions.Count == 1 &&
                dataValidationExceptions.Exists(x => x.ErrorValue == patent.Name)
                && dataValidationExceptions.Exists(x => x.Message == "Name validation exception"));
        }

        
        [TestMethod]
        public void AddPatentIsAuthorsIncorrectTrue()
        {
            authors.Clear();
            DateTime dateTime1 = new DateTime(2009, 3, 1, 7, 0, 0);
            DateTime dateTime2 = new DateTime(2010, 3, 1, 7, 0, 0);
            Patent patent = new Patent("Name", authors, "Russia", 132, dateTime1, dateTime2, 12, "221");
            List<DataValidationError> dataValidationExceptions = patentLogic.AddPatent(patent);
            Assert.AreEqual(true, dataValidationExceptions.Count == 1
                && dataValidationExceptions.Exists(x => x.Message == "Inventors validation exception"));
        }

        [TestMethod]
        public void AddPatentCountryIncorrectTrue()
        {
            DateTime dateTime1 = new DateTime(2009, 3, 1, 7, 0, 0);
            DateTime dateTime2 = new DateTime(2010, 3, 1, 7, 0, 0);
            Patent patent = new Patent("Name", authors, "USa", 132, dateTime1, dateTime2, 12, "221");
            List<DataValidationError> dataValidationExceptions = patentLogic.AddPatent(patent);
            Assert.AreEqual(true, dataValidationExceptions.Count == 1 &&
                dataValidationExceptions.Exists(x => x.ErrorValue == patent.Country)
                && dataValidationExceptions.Exists(x => x.Message == "Country validation exception"));
        }

        [TestMethod]
        public void AddPatentRegistrationNumberIncorrectTrue()
        {
            DateTime dateTime1 = new DateTime(2009, 3, 1, 7, 0, 0);
            DateTime dateTime2 = new DateTime(2010, 3, 1, 7, 0, 0);
            Patent patent = new Patent("Name", authors, "Russia", -132, dateTime1, dateTime2, 12, "221");
            List<DataValidationError> dataValidationExceptions = patentLogic.AddPatent(patent);
            Assert.AreEqual(true, dataValidationExceptions.Count == 1 &&
                dataValidationExceptions.Exists(x => x.ErrorValue == patent.RegistrationNumber.ToString())
                && dataValidationExceptions.Exists(x => x.Message == "RegistrationNumber validation exception"));
        }

        [TestMethod]
        public void AddPatentNoteIncorrectTrue()
        {
            string note = "".PadLeft(2001, 'a');
            DateTime dateTime1 = new DateTime(2009, 3, 1, 7, 0, 0);
            DateTime dateTime2 = new DateTime(2010, 3, 1, 7, 0, 0);
            Patent patent = new Patent("Name", authors, "Russia", 132, dateTime1, dateTime2, -12, note);
            List<DataValidationError> dataValidationExceptions = patentLogic.AddPatent(patent);
            Assert.AreEqual(true, dataValidationExceptions.Count == 2 &&
                dataValidationExceptions.Exists(x => x.ErrorValue == patent.Note)
                && dataValidationExceptions.Exists(x => x.Message == "Note validation exception"));
        }

        [TestMethod]
        public void AddPatentNumberOfPagesIncorrectTrue()
        {
            DateTime dateTime1 = new DateTime(2009, 3, 1, 7, 0, 0);
            DateTime dateTime2 = new DateTime(2010, 3, 1, 7, 0, 0);
            Patent patent = new Patent("Name", authors, "Russia", 132, dateTime1, dateTime2, -12, "221");
            List<DataValidationError> dataValidationExceptions = patentLogic.AddPatent(patent);
            Assert.AreEqual(true, dataValidationExceptions.Count == 1 &&
                dataValidationExceptions.Exists(x => x.ErrorValue == patent.NumberOfPages.ToString())
                && dataValidationExceptions.Exists(x => x.Message == "NumberOfPages validation exception"));
        }

        [TestMethod]
        public void AddPatentDateOfApplicationIncorrectTrue()
        {
            DateTime dateTime1 = new DateTime(2112, 3, 1, 7, 0, 0);
            DateTime dateTime2 = new DateTime(2010, 3, 1, 7, 0, 0);
            Patent patent = new Patent("Name", authors, "Russia", 132, dateTime1, dateTime2, 12, "221");
            List<DataValidationError> dataValidationExceptions = patentLogic.AddPatent(patent);
            Assert.AreEqual(true, dataValidationExceptions.Count == 2 &&
                dataValidationExceptions.Exists(x => x.ErrorValue == patent.DateOfApplication.ToString())
                && dataValidationExceptions.Exists(x => x.Message == "DateOfApplication validation exception"));
        }

        [TestMethod]
        public void AddPatentDateOfPublicationIncorrectTrue()
        {
            DateTime dateTime1 = new DateTime(2009, 3, 1, 7, 0, 0);
            DateTime dateTime2 = new DateTime(2008, 3, 1, 7, 0, 0);
            Patent patent = new Patent("Name", authors, "Russia", 132, dateTime1, dateTime2, -12, "221");
            List<DataValidationError> dataValidationExceptions = patentLogic.AddPatent(patent);
            Assert.AreEqual(true, dataValidationExceptions.Count == 2 &&
                dataValidationExceptions.Exists(x => x.ErrorValue == patent.DateOfPublication.ToString())
                && dataValidationExceptions.Exists(x => x.Message == "DateOfPublication validation exception"));
        }
        #endregion

        #region DeletePatent
        [TestMethod]
        public void DeletePatentTrue()
        {
            Assert.AreEqual(true, patentLogic.DeletePatent(testPatent.Id));
        }

        [TestMethod]
        public void DeletePatentFalse()
        {
            Assert.AreEqual(false, patentLogic.DeletePatent(DeletedPaperGuid));
        }
        #endregion

        [TestCleanup]
        public void Cleanup()
        {
            RAMMemory.Library.Clear();
        }
    }
}
