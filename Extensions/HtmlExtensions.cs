﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Maitonn.Core;


public static class HtmlExtensions
{
    public static MvcHtmlString MaitonnPage(this HtmlHelper html, PagingInfo pagingInfo, string cssClass, string routeName = null)
    {
        if (pagingInfo.TotalPages <= 0)
        {
            return MvcHtmlString.Empty;
        }
        var container = new TagBuilder("ul");
        container.AddCssClass("pagination");
        #region prev page
        TagBuilder prepage = new TagBuilder("li");

        if (pagingInfo.CurrentPage == 1)
        {
            prepage.AddCssClass("disabled");
        }
        prepage.AddCssClass("prev");
        var preLink = new TagBuilder("a");
        preLink.AddCssClass("page-link");
        preLink.MergeAttribute("data-cssclass", cssClass);
        preLink.MergeAttribute("href", GenerateUrl(html, (pagingInfo.CurrentPage - 1) > 0 ? (pagingInfo.CurrentPage - 1) : 1, routeName));
        preLink.InnerHtml = "<i></i>上一页";
        prepage.InnerHtml = preLink.ToString();
        container.InnerHtml += prepage.ToString();
        #endregion


        #region inner page
        bool leftqujian = false;
        bool rightqujian = false;
        for (int i = 1; i <= pagingInfo.TotalPages; i++)
        {
            var link = new TagBuilder("a");
            link.AddCssClass("page-link");
            link.MergeAttribute("data-cssclass", cssClass);
            link.MergeAttribute("href", GenerateUrl(html, i, routeName));
            link.InnerHtml = i.ToString();
            if (pagingInfo.CurrentPage - i >= 2 && i > 1)
            {
                var litarget = new TagBuilder("li");
                if (!leftqujian)
                {
                    TagBuilder dotted = new TagBuilder("span");
                    dotted.InnerHtml = "...";
                    dotted.AddCssClass("dotted");
                    litarget.InnerHtml = dotted.ToString();
                    container.InnerHtml += litarget.ToString();
                    leftqujian = true;

                }
            }
            else if (i - pagingInfo.CurrentPage >= 2 && i < pagingInfo.TotalPages)
            {
                var litarget = new TagBuilder("li");
                if (!rightqujian)
                {
                    TagBuilder dotted = new TagBuilder("span");
                    dotted.InnerHtml = "...";
                    dotted.AddCssClass("dotted");
                    litarget.InnerHtml = dotted.ToString();
                    container.InnerHtml += litarget.ToString();
                    rightqujian = true;
                }
            }
            else
            {
                TagBuilder currentPagebtn = new TagBuilder("li");
                if (i == pagingInfo.CurrentPage)
                {
                    currentPagebtn.AddCssClass("active");
                }
                currentPagebtn.InnerHtml = link.ToString();
                container.InnerHtml += currentPagebtn.ToString();
            }
        }
        #endregion


        #region next page
        TagBuilder nextpage = new TagBuilder("li");

        if (pagingInfo.CurrentPage == pagingInfo.TotalPages)
        {
            nextpage.AddCssClass("disabled");
        }
        nextpage.AddCssClass("next");
        var nextLink = new TagBuilder("a");
        nextLink.AddCssClass("page-link");
        nextLink.MergeAttribute("data-cssclass", cssClass);
        nextLink.MergeAttribute("href", GenerateUrl(html, pagingInfo.CurrentPage + 1, routeName));
        nextLink.InnerHtml = "<i></i>下一页";
        nextpage.InnerHtml = nextLink.ToString();
        container.InnerHtml += nextpage.ToString();
        #endregion


     

        #region page box
        pagingInfo.ShowPageBox = true;
        if (pagingInfo.ShowPageBox)
        {

            TagBuilder listPageJump = new TagBuilder("input");
            listPageJump.AddCssClass("list-page-jump");
            listPageJump.MergeAttribute("data-url", GenerateUrl(html, 1, routeName));
            listPageJump.MergeAttribute("data-maxpage", pagingInfo.TotalPages.ToString());

            container.InnerHtml += listPageJump.ToString();

            TagBuilder listPageLink = new TagBuilder("a");
            listPageLink.AddCssClass("list-page-link");
            listPageLink.AddCssClass("page-link");
            listPageLink.MergeAttribute("data-cssclass", cssClass);
            listPageLink.MergeAttribute("href", "#");
            container.InnerHtml += listPageLink.ToString();

            TagBuilder jumpPage = new TagBuilder("a");
            jumpPage.AddCssClass("list-page-btn");
            jumpPage.SetInnerText("确定");
            jumpPage.MergeAttribute("href", "javascript:void(0);");

            container.InnerHtml += jumpPage.ToString();

            TagBuilder pageScripts = new TagBuilder("script");

            StringBuilder scriptBuilder = new StringBuilder();

            scriptBuilder.Append(@"
                $(function(){
                   var currentPage="+pagingInfo.CurrentPage+";"+ @"
                   $('.list-page-btn').click(function(){
                       var $pageBox=$(this).siblings('.list-page-jump');
                       var page=$pageBox.val();
                       if(page===''){
                          $pageBox.focus();
                       }else{
                           var link=$(this).siblings('.list-page-link');
                           var maxpage=$pageBox.data('maxpage');
                           var url=$pageBox.data('url');
                           page=page>maxpage?maxpage:page;
                           if(page!=currentPage){
                              gopage(page,url,link);
                           }
                       }
                    });
                    $('.list-page-jump').keyup(function(){
                        var value=$(this).val();
                        if(isNaN(value)||value<=0){
                            $(this).val('');    
                        }
                    });
                    $('.list-page-jump').keydown(function(e){
                        if (e.keyCode === 13) {
                             $('.list-page-btn').click();
                        }
                    });
                    function gopage(page,url,link){
                        if(url.indexOf('page='>-1)){
                            url=url.replace(/page=(\d+)/,'page='+page);
                        }else{
                            if(url.indexOf('?')>-1){
                               url=url+'?page='+page; 
                            }else{
                               url=url+'&page='+page;
                            }
                        }
                        link.attr('href',url);
                        link.click();
                    }
                })");
            pageScripts.InnerHtml = scriptBuilder.ToString();

            container.InnerHtml += pageScripts.ToString();
        }
        #endregion
        return MvcHtmlString.Create(container.ToString());
    }

    /// <summary>
    /// generate paging url
    /// </summary>
    /// <param name="pageIndex">page index to generate navigate url</param>
    /// <returns>navigated url for pager item</returns>
    private static string GenerateUrl(HtmlHelper html, int page, string routeName)
    {
        //return null if  page index larger than total page count or page index is current page index

        var routeValues = new RouteValueDictionary();

        routeValues["page"] = page;

        var rq = html.ViewContext.HttpContext.Request.QueryString;

        foreach (string key in rq.Keys)
        {
            if (key != "page")
                routeValues[key] = rq[key];
        }

        // Add action
        routeValues["action"] = (string)html.ViewContext.RouteData.Values["action"];
        // Add controller
        routeValues["controller"] = (string)html.ViewContext.RouteData.Values["controller"];
        // Return link
        var urlHelper = new UrlHelper(html.ViewContext.RequestContext);
        if (!String.IsNullOrEmpty(routeName))
            return urlHelper.RouteUrl(routeName, routeValues);

        return urlHelper.RouteUrl(routeValues);

    }

}


public class PagingInfo
{
    public int TotalItems { get; set; }
    public int ItemsPerPage { get; set; }
    public int CurrentPage { get; set; }
    public bool ShowPageBox { get; set; }
    public int TotalPages
    {
        get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
    }
}
