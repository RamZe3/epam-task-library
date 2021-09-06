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
    public class PaperLogicTest
    {
        public IPaperLogic paperLogic;
        public IPaperDAL paperDAL;

        public Paper testPaper;

        public Paper UniquePaper;
        public Paper NotUniquePaper;

        public PaperLogicTest()
        {
            DependenciesResolver dependenciesResolver = new DependenciesResolver();
            paperDAL = dependenciesResolver.paperDAL;
            paperLogic = dependenciesResolver.paperLogic;
        }

        [TestInitialize]
        public void Initialize()
        {
            DateTime dateTime = new DateTime(2021, 3, 1);
            testPaper = new Paper("TestPaper", "Saratov", "PaperEnt", 2021, 3, "Paper", 1224, dateTime, "ISSN 1233-1214");
            UniquePaper = new Paper("TestPaper", "Saratov", "PaperEnt", 2021, 3, "Paper", 1224, dateTime, "ISSN 1233-1216");
            NotUniquePaper = new Paper("NotUniquePaper", "Saratov", "PaperEnt", 2021, 3, "Paper", 1224, dateTime, "");

            paperLogic.AddPaper(NotUniquePaper);
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
        #endregion

        #region DeletePaper
        [TestMethod]
        public void DeletePaperTrue()
        {
            paperLogic.AddPaper(testPaper);
            Assert.AreEqual(true, paperLogic.DeletePaper(testPaper.Id));
        }

        [TestMethod]
        public void DeletePaperFalse()
        {
            Assert.AreEqual(false, paperLogic.DeletePaper(testPaper.Id));
        }
        #endregion

        [TestCleanup]
        public void Cleanup()
        {
            RAMMemory.Library.Clear();
        }
    }
}
