using Epam.Library.Entities;
using Epam.Library.RAMMemoryDAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Epam.Library.UnitTests.RAMMemoryDALTests
{
    [TestClass]
    public class PaperRAMDALTests
    {

        public PaperRAMDAL paperRAMDAL;
        public Paper testPaper;

        public PaperRAMDALTests()
        {
            paperRAMDAL = new PaperRAMDAL();
        }

        [TestInitialize]
        public void Initialize()
        {
            DateTime dateTime = new DateTime(2008, 3, 1, 7, 0, 0);

            testPaper = new Paper("testPaper", "Saratov", "PaperEnt", 2021, 3, "Paper", 1223, dateTime, "ISSN 1233-1213");
            Paper testPaperWithoutISSN = new Paper("testPaperWithoutISSN", "Saratov", "PaperEnt", 2021, 3, "Paper", 1223, dateTime, "");

            paperRAMDAL.AddPaper(testPaper);
            paperRAMDAL.AddPaper(testPaperWithoutISSN);
        }

        #region AddPaper
        [TestMethod]
        public void AddPaperUniquePaperTrue()
        {
            DateTime dateTime = new DateTime(2009, 3, 1, 7, 0, 0);

            Paper paper = new Paper("Club Paper", "Saratov", "PaperEntSar", 2021, 3, "Paper", 1223, dateTime, "ISSN 1243-1213");
            Assert.AreEqual(true, paperRAMDAL.AddPaper(paper));
        }

        [TestMethod]
        public void AddPaperSameISSNVariousNameThrowInvalidOperationException()
        {
            DateTime dateTime = new DateTime(2009, 3, 1, 7, 0, 0);

            Paper paper = new Paper("Club Paper", "Saratov", "PaperEntSar", 2021, 3, "Paper", 1223, dateTime, "ISSN 1233-1213");
            Assert.ThrowsException<System.InvalidOperationException>(() => paperRAMDAL.AddPaper(paper));
        }

        [TestMethod]
        public void AddPaperSameISSNAndNameTrue()
        {
            DateTime dateTime = new DateTime(2009, 3, 1, 7, 0, 0);

            Paper paper = new Paper("testPaper", "Saratov", "PaperEnt", 2021, 3, "Paper note", 12234, dateTime, "ISSN 1233-1213");
            Assert.AreEqual(true, paperRAMDAL.AddPaper(paper));
        }

        [TestMethod]
        public void AddPaperEmptyISSNSameNamePublisherAndDateThrowInvalidOperationException()
        {
            DateTime dateTime = new DateTime(2008, 3, 1, 7, 0, 0);

            Paper paper = new Paper("testPaperWithoutISSN", "Saratov", "PaperEnt", 2021, 3, "Paper", 1223, dateTime, "");
            Assert.ThrowsException<System.InvalidOperationException>(() => paperRAMDAL.AddPaper(paper));
        }
        #endregion

        #region DeletePaper
        [TestMethod]
        public void DeletePaperTrue()
        {
            Assert.AreEqual(true, paperRAMDAL.DeletePaper(testPaper.Id));
        }

        [TestMethod]
        public void DeletePaperFalse()
        {
            paperRAMDAL.DeletePaper(testPaper.Id);
            Assert.AreEqual(false, paperRAMDAL.DeletePaper(testPaper.Id));
        }
        #endregion

        [TestCleanup]
        public void Cleanup()
        {
            RAMMemory.Library.Clear();
        }
    }
}
