using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPAM.Library.MVCPL.Helpers
{
    using EPAM.Library.MVCPL.Models;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    //.............................
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
            PageInfo pageInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder tag;

            int startpage = pageInfo.PageNumber - 3 > 0 ? pageInfo.PageNumber -3 : 1;
            int endpage = pageInfo.PageNumber + 3 < pageInfo.TotalPages ? pageInfo.PageNumber + 3 : pageInfo.TotalPages;



            if (startpage != 1 && startpage != pageInfo.PageNumber)
            {
                tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(1));
                tag.InnerHtml = "1";
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }

            if (startpage > 2 && startpage != pageInfo.PageNumber)
            {
                tag = new TagBuilder("a");
                tag.InnerHtml = "...";
                // если текущая страница, то выделяем ее,
                // например, добавляя класс

                tag.AddCssClass("selected");
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }

            for (int i = startpage; i <= endpage; i++)
            {
                if (i == pageInfo.PageNumber)
                {
                    tag = new TagBuilder("a");
                    tag.InnerHtml = i.ToString();
                    // если текущая страница, то выделяем ее,
                    // например, добавляя класс

                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                    tag.AddCssClass("btn btn-default");
                }
                else
                {
                    tag = new TagBuilder("a");
                    tag.MergeAttribute("href", pageUrl(i));
                    tag.InnerHtml = i.ToString();
                    tag.AddCssClass("btn btn-default");
                }
                result.Append(tag.ToString());
            }


            if (endpage < pageInfo.TotalPages - 1 && endpage != pageInfo.TotalPages)
            {
                tag = new TagBuilder("a");
                tag.InnerHtml = "...";
                // если текущая страница, то выделяем ее,
                // например, добавляя класс

                tag.AddCssClass("selected");
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }

            if (endpage != pageInfo.TotalPages && endpage != pageInfo.PageNumber)
            {
                tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(pageInfo.TotalPages));
                tag.InnerHtml = pageInfo.TotalPages.ToString();
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }

            

            return MvcHtmlString.Create(result.ToString());
        }
    }
}