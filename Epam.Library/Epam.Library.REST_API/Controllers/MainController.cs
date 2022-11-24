using Epam.Library.Entities;
using Epam.Library.REST_API.Models.Logic.Pages;
using Epam.Library.REST_API.Models.Logic.SearchFilters;
using Epam.Library.REST_API.Models.Resources;
using EPAM.Library;
using EPAM.Library.MVCPL.Filters;
using EPAM.Library.REST_API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Epam.Library.REST_API.Controllers
{
    [ExceptionLogger]
    public class MainController : ApiController
    {
        //[AuthorizeLogger]
        public IHttpActionResult GetLibraryWithPages([FromUri] ResourceSearchFilter filter, [FromUri]PageInfo pageInfo)
        {
            List<InformationResource> resources = DependenciesResolverConfig.DependenciesResolver.informationResourceLogic.GetLibrary();
            resources = filter.Filter(resources);
            resources = pageInfo.GetItems<InformationResource>(resources);

            return Ok(new {resources = AutoMapperConfig.Mapper.Map<IEnumerable<ResourceCatalogModel>>(resources),
                TotalPages = pageInfo.TotalPages, PageNumber = pageInfo.PageNumber});
        }

        public IHttpActionResult GetLibrary([FromUri] ResourceSearchFilter filter)
        {
            List<InformationResource> resources = DependenciesResolverConfig.DependenciesResolver.informationResourceLogic.GetLibrary();
            resources = filter.Filter(resources);

            return Ok(AutoMapperConfig.Mapper.Map<IEnumerable<ResourceCatalogModel>>(resources));
        }
    }
}
