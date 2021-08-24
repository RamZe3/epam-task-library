﻿using Epam.Library.BLL.DateCheck;
using Epam.Library.BLL.Interfaces;
using Epam.Library.DAL.Interfaces;
using Epam.Library.Entities;
using Epam.Library.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.BLL
{
    public class InformationResourceLogic : IInformationResourceLogic
    {
        private IInformationResourceDAL _informationResourceDAL;
        private IDataValidator _dataValidator;

        public InformationResourceLogic(IInformationResourceDAL informationResourceDAL)
        {
            _dataValidator = new DataValidator();
            _informationResourceDAL = informationResourceDAL;
        }

        public List<Book> FindBooksByAuthor(Author author)
        {
            return _informationResourceDAL.FindBooksByAuthor(author);
        }

        public List<InformationResource> FindPatentsAndBooksByAuthor(Author author)
        {
            return _informationResourceDAL.FindPatentsAndBooksByAuthor(author);
        }

        public List<Patent> FindPatentsByAuthor(Author author)
        {
            return _informationResourceDAL.FindPatentsByAuthor(author);
        }

        public InformationResource FindResourceByName(string name)
        {
            return _informationResourceDAL.FindResourceByName(name);
        }

        public List<InformationResource> GetLibrary()
        {
            return _informationResourceDAL.GetLibrary();
        }

        public List<InformationResource> GetSortedLibraryByYearOfPublishing(bool reverse)
        {
            return _informationResourceDAL.GetSortedLibraryByYearOfPublishing(reverse);
        }

        public List<InformationResource> GroupingResourceByYearOfPublication()
        {
            return _informationResourceDAL.GroupingResourceByYearOfPublication();
        }

        public List<Book> SmartBookSearchByPublisher(string str)
        {
            return _informationResourceDAL.SmartBookSearchByPublisher(str);
        }
    }
}
