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
    public class PaperLogicTest
    {
        public Mock<IPaperDAL> IPaperDALMock;
        public PaperLogic paperLogic;
        public Paper testPaper;

        public Guid DeletedPaperGuid;

        public Paper UniquePaper;
        public Paper NotUniquePaper;

        public PaperLogicTest()
        {
            DeletedPaperGuid = Guid.NewGuid();

            IPaperDALMock = new Mock<IPaperDAL>();

            IPaperDALMock.Setup(b => b.AddPaper(It.IsAny<Paper>())).Returns(() => true);
            IPaperDALMock.Setup(b => b.AddPaper(It.Is<Paper>(x => x.Name == "NotUniquePaper"))).Throws(new InvalidOperationException());

            IPaperDALMock.Setup(b => b.DeletePaper(It.IsAny<Guid>())).Returns(() => true);
            IPaperDALMock.Setup(b => b.DeletePaper(It.Is<Guid>(x => x == DeletedPaperGuid))).Returns(() => false);

            paperLogic = new PaperLogic(IPaperDALMock.Object);
        }

        [TestInitialize]
        public void Initialize()
        {
            DateTime dateTime = new DateTime(2021, 3, 1);
            testPaper = new Paper("TestPaper", "Saratov", "PaperEnt", 2020, 3, "Paper", 1224, dateTime, "ISSN 1233-1213");
            UniquePaper = new Paper("TestPaper", "Saratov", "PaperEnt", 2021, 3, "Paper", 1224, dateTime, "ISSN 1233-1213");
            NotUniquePaper = new Paper("NotUniquePaper", "Saratov", "PaperEnt", 2021, 3, "Paper", 1224, dateTime, "ISSN 1233-1213");
        }

        #region AddPaper
        [TestMethod]
        public void AddPaperUniquePaperTrue()
        {
            Assert.AreEqual(true, paperLogic.AddPaper(UniquePaper).Count == 0);
        }

        [TestMethod]
        public void AddPaperNotUniquePaperThrowsInvalidOperationException()
        {
            Assert.ThrowsException<System.InvalidOperationException>(() => paperLogic.AddPaper(NotUniquePaper));
        }

        [TestMethod]
        public void AddPaperNameIncorrectTrue()
        {
            DateTime dateTime = new DateTime(2021, 3, 1);
            Paper paper = new Paper("", "Saratov", "PaperEnt", 2021, 3, "Paper", 1224, dateTime, "ISSN 1233-1213");
            List<DataValidationError> dataValidationExceptions = paperLogic.AddPaper(paper);
            Assert.AreEqual(true, dataValidationExceptions.Count == 1 &&
                dataValidationExceptions.Exists(x => x.ErrorValue == paper.Name)
                && dataValidationExceptions.Exists(x => x.Message == "Name validation exception"));
        }

        [TestMethod]
        public void AddPaperPlaceOfPublicationIncorrectTrue()
        {
            DateTime dateTime = new DateTime(2021, 3, 1);
            Paper paper = new Paper("Name", "USa", "PaperEnt", 2021, 3, "Paper", 1224, dateTime, "ISSN 1233-1213");
            List<DataValidationError> dataValidationExceptions = paperLogic.AddPaper(paper);
            Assert.AreEqual(true, dataValidationExceptions.Count == 1 &&
                dataValidationExceptions.Exists(x => x.ErrorValue == paper.PlaceOfPublication)
                && dataValidationExceptions.Exists(x => x.Message == "PlaceOfPublication validation exception"));
        }

        [TestMethod]
        public void AddPaperPublisherIncorrectTrue()
        {
            string publisher = "".PadLeft(301, 'a');
            DateTime dateTime = new DateTime(2021, 3, 1);
            Paper paper = new Paper("Name", "Saratov", publisher, 2021, 3, "Paper", 1224, dateTime, "ISSN 1233-1213");
            List<DataValidationError> dataValidationExceptions = paperLogic.AddPaper(paper);
            Assert.AreEqual(true, dataValidationExceptions.Count == 1 &&
                dataValidationExceptions.Exists(x => x.ErrorValue == paper.Publisher)
                && dataValidationExceptions.Exists(x => x.Message == "Publisher validation exception"));
        }

        [TestMethod]
        public void AddPaperYearOfPublishingIncorrectTrue()
        {
            DateTime dateTime = new DateTime(2021, 3, 1);
            Paper paper = new Paper("Name", "Saratov", "SarEnt", 2022, 3, "Paper", 1224, dateTime, "ISSN 1233-1213");
            List<DataValidationError> dataValidationExceptions = paperLogic.AddPaper(paper);
            Assert.AreEqual(true, dataValidationExceptions.Count == 2 &&
                dataValidationExceptions.Exists(x => x.ErrorValue == paper.YearOfPublishing.ToString())
                && dataValidationExceptions.Exists(x => x.Message == "YearOfPublishing validation exception"));
        }

        [TestMethod]
        public void AddPaperNoteIncorrectTrue()
        {
            string note = "".PadLeft(2001, 'a');
            DateTime dateTime = new DateTime(2021, 3, 1);
            Paper paper = new Paper("Name", "Saratov", "SarEnt", 2021, 3, note, 1224, dateTime, "ISSN 1233-1213");
            List<DataValidationError> dataValidationExceptions = paperLogic.AddPaper(paper);
            Assert.AreEqual(true, dataValidationExceptions.Count == 1 &&
                dataValidationExceptions.Exists(x => x.ErrorValue == paper.Note)
                && dataValidationExceptions.Exists(x => x.Message == "Note validation exception"));
        }

        [TestMethod]
        public void AddPaperNumberOfPagesIncorrectTrue()
        {
            DateTime dateTime = new DateTime(2021, 3, 1);
            Paper paper = new Paper("Name", "Saratov", "SarEnt", 2021, -3, "Paper", 1224, dateTime, "ISSN 1233-1213");
            List<DataValidationError> dataValidationExceptions = paperLogic.AddPaper(paper);
            Assert.AreEqual(true, dataValidationExceptions.Count == 1 &&
                dataValidationExceptions.Exists(x => x.ErrorValue == paper.NumberOfPages.ToString())
                && dataValidationExceptions.Exists(x => x.Message == "NumberOfPages validation exception"));
        }

        [TestMethod]
        public void AddPaperISSNIncorrectTrue()
        {
            DateTime dateTime = new DateTime(2021, 3, 1);
            Paper paper = new Paper("Name", "Saratov", "SarEnt", 2021, 3, "Paper", 1224, dateTime, "ISSN1233-1213");
            List<DataValidationError> dataValidationExceptions = paperLogic.AddPaper(paper);
            Assert.AreEqual(true, dataValidationExceptions.Count == 1 &&
                dataValidationExceptions.Exists(x => x.ErrorValue == paper.ISSN.ToString())
                && dataValidationExceptions.Exists(x => x.Message == "ISSN validation exception"));
        }

        [TestMethod]
        public void AddPaperNumberIncorrectTrue()
        {
            DateTime dateTime = new DateTime(2021, 3, 1);
            Paper paper = new Paper("Name", "Saratov", "SarEnt", 2021, 3, "Paper", -12, dateTime, "ISSN 1233-1213");
            List<DataValidationError> dataValidationExceptions = paperLogic.AddPaper(paper);
            Assert.AreEqual(true, dataValidationExceptions.Count == 1 &&
                dataValidationExceptions.Exists(x => x.ErrorValue == paper.Number.ToString())
                && dataValidationExceptions.Exists(x => x.Message == "Number validation exception"));
        }

        [TestMethod]
        public void AddPaperDateIncorrectTrue()
        {
            DateTime dateTime = new DateTime(2020, 3, 1);
            Paper paper = new Paper("Name", "Saratov", "SarEnt", 2021, 3, "Paper", 12, dateTime, "ISSN 1233-1213");
            List<DataValidationError> dataValidationExceptions = paperLogic.AddPaper(paper);
            Assert.AreEqual(true, dataValidationExceptions.Count == 1 &&
                dataValidationExceptions.Exists(x => x.ErrorValue == paper.Date.ToString())
                && dataValidationExceptions.Exists(x => x.Message == "Date validation exception"));
        }
        #endregion

        #region DeletePaper
        [TestMethod]
        public void DeletePaperTrue()
        {
            Assert.AreEqual(true, paperLogic.DeletePaper(testPaper.Id));
        }

        [TestMethod]
        public void DeletePaperFalse()
        {
            Assert.AreEqual(false, paperLogic.DeletePaper(DeletedPaperGuid));
        }
        #endregion

        [TestCleanup]
        public void Cleanup()
        {
            RAMMemory.Library.Clear();
        }
    }
}
