using Bookist.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Microsoft.AspNetCore.Mvc;

[HtmlTargetElement("div", Attributes = "page-model")]
public class PagerTagHelper : TagHelper
{
    private readonly IUrlHelperFactory _urlHelperFactory;

    public PagerTagHelper(IUrlHelperFactory urlHelperFactory)
    {
        _urlHelperFactory = urlHelperFactory;
    }

    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; }

    public string PageAction { get; set; }
    public string PageController { get; set; }

    public PagerVM PageModel { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if(PageModel.TotalItems == 0)
        {
            return;
        }

        var routeValues = new RouteValueDictionary();
        foreach (var qs in ViewContext.HttpContext.Request.Query)
        {
            routeValues.Add(qs.Key, qs.Value);
        }

        var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);

        TagBuilder CreatePageButton(int page, string text)
        {
            routeValues["p"] = page;

            TagBuilder tag = new("a");
            tag.AddCssClass("btn btn-outline-primary mx-1");
            tag.Attributes["href"] = urlHelper.Action(PageAction, PageController, routeValues);
            tag.InnerHtml.Append(text);
            return tag;
        }

        //TagBuilder textTag = new("div");
        //textTag.AddCssClass("my-3");
        //textTag.InnerHtml.Append($"第 {PageModel.PageNo} 页，共 {PageModel.TotalPages} 页");
        //output.Content.AppendHtml(textTag);

        TagBuilder btnsTag = new("div");
        btnsTag.AddCssClass("my-3");

        var firstBtn = CreatePageButton(1, "最新");
        var prevBtn = CreatePageButton(PageModel.PageNo - 1, "上一页");
        if (PageModel.PageNo == 1)
        {
            firstBtn.AddCssClass("disabled");
            prevBtn.AddCssClass("disabled");
        }
        btnsTag.InnerHtml.AppendHtml(firstBtn);
        btnsTag.InnerHtml.AppendHtml(prevBtn);

        var nextBtn = CreatePageButton(PageModel.PageNo + 1, "下一页");
        var lastBtn = CreatePageButton(PageModel.TotalPages, "最旧");
        if (PageModel.PageNo == PageModel.TotalPages)
        {
            nextBtn.AddCssClass("disabled");
            lastBtn.AddCssClass("disabled");
        }
        btnsTag.InnerHtml.AppendHtml(nextBtn);
        btnsTag.InnerHtml.AppendHtml(lastBtn);

        output.Content.AppendHtml(btnsTag);
    }
}
