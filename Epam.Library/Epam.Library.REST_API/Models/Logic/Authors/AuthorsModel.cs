using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epam.Library.REST_API.Models.Logic
{
    public class AuthorsModel
    {
        public Author GetAuthorById(Guid id)
        {
            Author author = DependenciesResolverConfig.DependenciesResolver.authorSQLDAL.GetAuthors().Find( x => x.Id == id);

            return author;
        }

    }
}