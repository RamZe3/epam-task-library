using Epam.Library.Entities;
using Epam.Library.REST_API.Models.Logic.PaperIssues;
using Epam.Library.REST_API.Models.PaperIssues;
using Epam.Library.REST_API.Models.Papers;
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
    public class PaperIssueController : ApiController
    {
        public IHttpActionResult Get(Guid id)
        {
            InformationResource resource = DependenciesResolverConfig.DependenciesResolver.informationResourceLogic.GetLibrary().Find(x => x.Id == id);
            PaperIssueLogic paperIssueLogic = new PaperIssueLogic();

            if (resource != null && resource is Paper)
            {
                Paper paper = (Paper)resource;
                PaperIssue paperIssue = paperIssueLogic.AdaptPaperToPaperIsuue(paper);
                CatalogPaperIssueModel model = new CatalogPaperIssueModel()
                {
                    Paper = paper,
                    PaperIssue = paperIssue
                };

                return Ok(model);
            }

            return NotFound();
        }

        [AuthorizeLogger(Roles = "admin")]
        [ActionLogger(description: "admin action")]
        public IHttpActionResult Create([FromUri] CreatePaperIssueModel model)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }

            Paper paper = AutoMapperConfig.Mapper.Map<Paper>(model);

            DependenciesResolverConfig.DependenciesResolver.papersLogicWithRoles.AddPaper(paper);
            return Ok();
        }

        [AuthorizeLogger(Roles = "admin")]
        [ActionLogger(description: "admin action")]
        public IHttpActionResult Update([FromUri] UpdatePaperIssueModel model)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }

            Paper paper = AutoMapperConfig.Mapper.Map<Paper>(model);

            DependenciesResolverConfig.DependenciesResolver.papersLogicWithRoles.UpdatePaper(paper);
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
