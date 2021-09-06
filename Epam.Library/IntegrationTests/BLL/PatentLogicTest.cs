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
    public class PatentLogicTest
    {
        public IPatentLogic patentLogic;
        public IPatentDAL patentDAL;

        public Patent testPatent;
        List<Author> authors;

        public Patent UniquePatent;
        public Patent NotUniquePatent;

        public PatentLogicTest()
        {
            DependenciesResolver dependenciesResolver = new DependenciesResolver();
            patentDAL = dependenciesResolver.patentDAL;
            patentLogic = dependenciesResolver.patentLogic;
        }

        [TestInitialize]
        public void Initialize()
        {
            DateTime dateTime1 = new DateTime(2009, 3, 1, 7, 0, 0);
            DateTime dateTime2 = new DateTime(2010, 3, 1, 7, 0, 0);
            authors = new List<Author>() { new Author("Ivan", "Ivanov"), new Author("Artem", "Petrov") };
            testPatent = new Patent("testPatent", authors, "USA", 132, dateTime1, dateTime2, 12, "221");

            UniquePatent = new Patent("UniquePatent", authors, "USA", 132, dateTime1, dateTime2, 12, "221");
            NotUniquePatent = new Patent("NotUniquePatent", authors, "Russia", 132, dateTime1, dateTime2, 12, "221");

            patentLogic.AddPatent(NotUniquePatent);
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

        #endregion

        #region DeletePatent
        [TestMethod]
        public void DeletePatentTrue()
        {
            patentLogic.AddPatent(testPatent);
            Assert.AreEqual(true, patentLogic.DeletePatent(testPatent.Id));
        }

        [TestMethod]
        public void DeletePatentFalse()
        {
            Assert.AreEqual(false, patentLogic.DeletePatent(testPatent.Id));
        }
        #endregion

        [TestCleanup]
        public void Cleanup()
        {
            RAMMemory.Library.Clear();
        }
    }
}
