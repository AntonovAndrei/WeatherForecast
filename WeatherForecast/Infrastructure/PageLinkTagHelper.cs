using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using WeatherForecast.ViewModels;

namespace WeatherForecast.Infrastructure {

    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper {
        private IUrlHelperFactory urlHelperFactory;

        public PageLinkTagHelper(IUrlHelperFactory helperFactory) {
            urlHelperFactory = helperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public PagingInfo PageModel { get; set; }

        public string PageAction { get; set; }

        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }

        public override void Process(TagHelperContext context,
                TagHelperOutput output) {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder result = new TagBuilder("div");
            if(PageModel.TotalPages > 20)
            {
                List<int> pages = new List<int>()
                {
                    1, PageModel.CurrentPage - 2, PageModel.CurrentPage - 1, PageModel.CurrentPage, 
                    PageModel.CurrentPage + 1, PageModel.CurrentPage + 2, PageModel.TotalPages
                };

                foreach(int i in pages)
                {
                    if(i > 0)
                    {
                        TagBuilder tag = new TagBuilder("a");
                        tag.Attributes["href"] = urlHelper.Action(PageAction,
                           new { startDate = PageModel.SearchDate, pageNumber = i, search = PageModel.PageSearch });
                        if (PageClassesEnabled)
                        {
                            tag.AddCssClass(PageClass);
                            tag.AddCssClass(i == PageModel.CurrentPage
                                ? PageClassSelected : PageClassNormal);
                        }
                        tag.InnerHtml.Append(i.ToString());
                        result.InnerHtml.AppendHtml(tag);
                    }
                }
            }
            else 
            {
                for (int i = 1; i <= PageModel.TotalPages; i++)
                {
                    TagBuilder tag = new TagBuilder("a");
                    tag.Attributes["href"] = urlHelper.Action(PageAction,
                       new { startDate = PageModel.SearchDate, pageNumber = i, search = PageModel.PageSearch });
                    if (PageClassesEnabled)
                    {
                        tag.AddCssClass(PageClass);
                        tag.AddCssClass(i == PageModel.CurrentPage
                            ? PageClassSelected : PageClassNormal);
                    }
                    tag.InnerHtml.Append(i.ToString());
                    result.InnerHtml.AppendHtml(tag);
                }
            }
            
            output.Content.AppendHtml(result.InnerHtml);
        }
    }
}
