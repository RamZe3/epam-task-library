using Epam.Library.Entities;
using Epam.Library.Entities.Exceptions;
using Epam.Library.REST_API.Models.Books;
using Epam.Library.REST_API.Models.Logic;
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
    public class BookController : ApiController
    {
        public IHttpActionResult Get(Guid id)
        {
            InformationResource resource = DependenciesResolverConfig.DependenciesResolver.informationResourceLogic.GetLibrary().Find(x => x.Id == id);

            if (resource != null && resource is Book)
            {
                return Ok(resource);
            }

            return NotFound();
        }

        [AuthorizeLogger(Roles = "admin")]
        [ActionLogger(description: "admin action")]
        public IHttpActionResult Create([FromUri]CreateBookModel model)
        {
            AuthorsModel authorsModel = new AuthorsModel();
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }
            
            Book book = AutoMapperConfig.Mapper.Map<Book>(model);

            model.AuthorsId.ForEach(x => book.Authors.Add(authorsModel.GetAuthorById(new Guid(x))));

            DependenciesResolverConfig.DependenciesResolver.booksLogicWithRoles.AddBook(book);
            return Ok();
        }

        [AuthorizeLogger(Roles = "admin")]
        [ActionLogger(description: "admin action")]
        public IHttpActionResult Update([FromUri]UpdateBookModel model)
        {
            AuthorsModel authorsModel = new AuthorsModel();
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }

            Book book = AutoMapperConfig.Mapper.Map<Book>(model);

            model.AuthorsId.ForEach(x => book.Authors.Add(authorsModel.GetAuthorById(new Guid(x))));

            DependenciesResolverConfig.DependenciesResolver.booksLogicWithRoles.UpdateBook(book);
            return Ok();
        }

        [AuthorizeLogger(Roles = "admin, librarian")]
        [ActionLogger(description: "admin action")]
        public void Delete(Guid id)
        {
            DependenciesResolverConfig.DependenciesResolver.iRLogicWithRoles.UpdateResourceStatus(id, "deleted");
        }
    }
}
