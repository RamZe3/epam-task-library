using Epam.Library.Entities;
using Epam.Library.RAMMemoryDAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Epam.Library.UnitTests.RAMMemoryDALTests
{
    [TestClass]
    public class PatentRAMDALTests
    {

        public PatentRAMDAL patentRAMDAL;
        public Patent testPatent;

        public PatentRAMDALTests()
        {
            patentRAMDAL = new PatentRAMDAL();
        }

        [TestInitialize]
        public void Initialize()
        {
            DateTime dateTime1 = new DateTime(2008, 3, 1, 7, 0, 0);
            DateTime dateTime2 = new DateTime(2009, 3, 1, 7, 0, 0);
            List<Author> authors = new List<Author>() { new Author("Ivan", "Ivanov"), new Author("Artem", "Petrov") };

            testPatent = new Patent("testPatent", authors, "Russia", 132, dateTime1, dateTime2, 12, "221");

            patentRAMDAL.AddPatent(testPatent);
        }

        #region AddPatent
        [TestMethod]
        public void AddPatentUniquePatentTrue()
        {
            DateTime dateTime1 = new DateTime(2008, 3, 1, 7, 0, 0);
            DateTime dateTime2 = new DateTime(2009, 3, 1, 7, 0, 0);
            List<Author> authors = new List<Author>() { new Author("Ivan", "Ivanov"), new Author("Artem", "Petrov") };
            Patent patent = new Patent("Phone", authors, "Russia", 1324, dateTime1, dateTime2, 12, "221");
            Assert.AreEqual(true, patentRAMDAL.AddPatent(patent));
        }

        [TestMethod]
        public void AddPatentSameRegistrationNumberAndCountryThrowInvalidOperationException()
        {
            DateTime dateTime1 = new DateTime(2008, 3, 1, 7, 0, 0);
            DateTime dateTime2 = new DateTime(2009, 3, 1, 7, 0, 0);
            List<Author> authors = new List<Author>() { new Author("Ivan", "Ivanov"), new Author("Artem", "Petrov") };
            Patent patent = new Patent("Phone", authors, "Russia", 132, dateTime1, dateTime2, 12, "221");
            Assert.ThrowsException<System.InvalidOperationException>(() => patentRAMDAL.AddPatent(patent));
        }

        [TestMethod]
        public void AddPatentSameRegistrationNumberVariousCountryTrue()
        {
            DateTime dateTime1 = new DateTime(2008, 3, 1, 7, 0, 0);
            DateTime dateTime2 = new DateTime(2009, 3, 1, 7, 0, 0);
            List<Author> authors = new List<Author>() { new Author("Ivan", "Ivanov"), new Author("Artem", "Petrov") };
            Patent patent = new Patent("Phone", authors, "USA", 132, dateTime1, dateTime2, 12, "221");
            Assert.AreEqual(true, patentRAMDAL.AddPatent(patent));
        }

        [TestMethod]
        public void AddPatentSameCountryVariousRegistrationNumberTrue()
        {
            DateTime dateTime1 = new DateTime(2008, 3, 1, 7, 0, 0);
            DateTime dateTime2 = new DateTime(2009, 3, 1, 7, 0, 0);
            List<Author> authors = new List<Author>() { new Author("Ivan", "Ivanov"), new Author("Artem", "Petrov") };
            Patent patent = new Patent("Phone", authors, "Russia", 1324, dateTime1, dateTime2, 12, "221");
            Assert.AreEqual(true, patentRAMDAL.AddPatent(patent));
        }
        #endregion

        #region DeletePatent
        [TestMethod]
        public void DeletePatentTrue()
        {
            Assert.AreEqual(true, patentRAMDAL.DeletePatent(testPatent.Id));
        }

        [TestMethod]
        public void DeletePatentFalse()
        {
            patentRAMDAL.DeletePatent(testPatent.Id);
            Assert.AreEqual(false, patentRAMDAL.DeletePatent(testPatent.Id));
        }
        #endregion

        [TestCleanup]
        public void Cleanup()
        {
            RAMMemory.Library.Clear();
        }
    }
}