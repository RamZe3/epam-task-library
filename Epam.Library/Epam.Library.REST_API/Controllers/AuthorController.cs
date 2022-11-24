using Epam.Library.Entities;
using Epam.Library.REST_API.Models.Authors;
using Epam.Library.REST_API.Models.Logic.SearchFilters;
using EPAM.Library.MVCPL.Filters;
using EPAM.Library.REST_API;
using EPAM.Library.REST_API.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Epam.Library.REST_API.Controllers
{
    [ExceptionLogger]
    public class AuthorController : ApiController
    {
        public IEnumerable<Author> GetAuthors([FromUri]AuthorSearchFilter filter)
        {
            List<Author> authors = DependenciesResolverConfig.DependenciesResolver.authorSQLDAL.GetAuthors();
            authors = filter.Filter(authors);
            return authors;
        }

        [AuthorizeLogger(Roles = "admin, librarian")]
        [ActionLogger(description: "admin action")]
        public IHttpActionResult Create([FromUri] CreateAuthorModel model)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }

            Author author = AutoMapperConfig.Mapper.Map<Author>(model);

            DependenciesResolverConfig.DependenciesResolver.authorSQLDAL.AddAuthorWithoutTran(author);
            return Ok();
        }
    }
}
