using GoldSilver.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace GoldSilver.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(
                    this HtmlHelper html,
                    PagingInfo pagingInfo,
                    Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            
            TagBuilder ul = new TagBuilder("ul");
            ul.AddCssClass("pagination");


            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder li = new TagBuilder("li");

                // Construct an <a> tag
                TagBuilder tag = new TagBuilder("a"); 
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();

                if (i == pagingInfo.page)
                    tag.AddCssClass("active");

                li.InnerHtml += tag;
                ul.InnerHtml += li;
            }

            return MvcHtmlString.Create(ul.ToString());
        }
    }
}