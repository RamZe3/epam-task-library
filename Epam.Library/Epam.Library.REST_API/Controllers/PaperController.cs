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
    public class PaperController : ApiController
    {
        public IHttpActionResult Get(Guid id)
        {
            InformationResource resource = DependenciesResolverConfig.DependenciesResolver.informationResourceLogic.GetLibrary().Find(x => x.Id == id);
            PaperIssueLogic paperIssueLogic = new PaperIssueLogic();

            if (resource != null && resource is Paper)
            {
                Paper paper = (Paper)resource;
                CatalogPaperModel model = new CatalogPaperModel()
                {
                    Paper = paper,
                    PaperIssues = paperIssueLogic.GetPaperIssues(paper.ISSN)
                };

                return Ok(model);
            }

            return NotFound();
        }

        [AuthorizeLogger(Roles = "admin, librarian")]
        [ActionLogger(description: "admin action")]
        public IHttpActionResult Create([FromUri] CreatePaperModel model)
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
        public IHttpActionResult Update([FromUri] UpdatePaperModel model)
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
            PaperIssueLogic paperIssueLogic = new PaperIssueLogic();
            Paper paper = (Paper)DependenciesResolverConfig.DependenciesResolver.informationResourceLogic.GetLibrary().Find(x => x.Id == id);

            List<PaperIssue> paperIssues = paperIssueLogic.GetPaperIssues(paper.ISSN);
            paperIssues.ForEach(x => DependenciesResolverConfig.DependenciesResolver.iRLogicWithRoles.UpdateResourceStatus(x.Id, "deleted"));

            DependenciesResolverConfig.DependenciesResolver.iRLogicWithRoles.UpdateResourceStatus(id, "deleted");
        }
    }
}
