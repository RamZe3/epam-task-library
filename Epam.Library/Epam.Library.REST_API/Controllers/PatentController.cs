using Epam.Library.Entities;
using Epam.Library.REST_API.Models.Logic;
using Epam.Library.REST_API.Models.Patents;
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
    public class PatentController : ApiController
    {
        public IHttpActionResult Get(Guid id)
        {
            InformationResource resource = DependenciesResolverConfig.DependenciesResolver.informationResourceLogic.GetLibrary().Find(x => x.Id == id);

            if (resource != null && resource is Patent)
            {
                return Ok(resource);
            }

            return NotFound();
        }

        [AuthorizeLogger(Roles = "admin")]
        [ActionLogger(description: "admin action")]
        public IHttpActionResult Create([FromUri] CreatePatentModel model)
        {
            AuthorsModel authorsModel = new AuthorsModel();
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }

            Patent patent = AutoMapperConfig.Mapper.Map<Patent>(model);

            model.AuthorsId.ForEach(x => patent.Inventors.Add(authorsModel.GetAuthorById(new Guid(x))));

            DependenciesResolverConfig.DependenciesResolver.patentsLogicWithRoles.AddPatent(patent);
            return Ok();
        }

        [AuthorizeLogger(Roles = "admin")]
        [ActionLogger(description: "admin action")]
        public IHttpActionResult Update([FromUri] UpdatePatentModel model)
        {
            AuthorsModel authorsModel = new AuthorsModel();
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }

            Patent patent = AutoMapperConfig.Mapper.Map<Patent>(model);

            model.AuthorsId.ForEach(x => patent.Inventors.Add(authorsModel.GetAuthorById(new Guid(x))));

            DependenciesResolverConfig.DependenciesResolver.patentsLogicWithRoles.UpdatePatent(patent);
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
