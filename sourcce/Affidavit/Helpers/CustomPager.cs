using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Affidavit.Helpers
{
    public static class CustomPager
    {
        public static MvcHtmlString NSPagination(this HtmlHelper helper, INSPageSettings settings)
        {
            var totalPages = (int)Math.Ceiling((decimal)settings.dataItems / (decimal)settings.size);
            var currentPage = settings.page;
            var startPage = settings.page - 5;
            var endPage = currentPage + 4;

            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }

            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }


            TagBuilder navTag = new TagBuilder("nav");
            navTag.Attributes.Add("aria-label", "Page navigation");
            navTag.AddCssClass("NS-paginator");
            TagBuilder ulTag = new TagBuilder("ul");
            ulTag.AddCssClass("pagination");

            TagBuilder prevTag = new TagBuilder("li");
            TagBuilder prevaTag = new TagBuilder("a");
            TagBuilder prevSpanTag = new TagBuilder("span");

            if (settings.page == 1)
            {
                prevTag.AddCssClass("disabled");
            }
            else
            {
                if (currentPage != 1)
                {
                    prevTag.Attributes.Add("data-page", (currentPage - 1).ToString());
                }

            }


            prevaTag.Attributes.Add("href", "#");
            prevaTag.Attributes.Add("aria-label", "Previous");
            prevSpanTag.Attributes.Add("aria-hidden", "true");
            prevSpanTag.InnerHtml += "&laquo;";
            prevaTag.InnerHtml += prevSpanTag;
            prevTag.InnerHtml += prevaTag;

            ulTag.InnerHtml += prevTag;

            for (int i = startPage; i <= endPage; i++)
            {
                TagBuilder liTag = new TagBuilder("li");
                if (i == settings.page)
                {
                    liTag.AddCssClass("active");
                }

                TagBuilder aTag = new TagBuilder("a");
                aTag.Attributes.Add("href", "#");
                aTag.SetInnerText((i).ToString());
                liTag.Attributes.Add("data-page", (i).ToString());
                liTag.InnerHtml += aTag;

                ulTag.InnerHtml += liTag;
            }




            TagBuilder nextTag = new TagBuilder("li");
            TagBuilder nextaTag = new TagBuilder("a");
            TagBuilder nextSpanTag = new TagBuilder("span");

            if (settings.page == endPage || endPage == 0)
            {
                nextTag.AddCssClass("disabled");
            }
            else
            {
                if (currentPage != endPage)
                {
                    nextTag.Attributes.Add("data-page", (currentPage + 1).ToString());
                }

            }

            nextaTag.Attributes.Add("href", "#");
            nextaTag.Attributes.Add("aria-label", "Next");
            nextSpanTag.Attributes.Add("aria-hidden", "true");
            nextSpanTag.InnerHtml += "&raquo;";
            nextaTag.InnerHtml += nextSpanTag;
            nextTag.InnerHtml += nextaTag;

            ulTag.InnerHtml += nextTag;

            navTag.InnerHtml += ulTag;

            return MvcHtmlString.Create(navTag.ToString(TagRenderMode.Normal));
        }
    }

    public interface INSPagination : IEnumerable
    {
    }

    public class NSPagination : INSPagination
    {

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public NSPagination(IEnumerable sdf)
        {

        }
    }


    public interface INSPageSettings
    {
        /// <summary>
        /// Total Number of items for paginate
        /// </summary>
        int dataItems { get; set; }

        /// <summary>
        /// Current Page
        /// </summary>
        int page { get; set; }

        /// <summary>
        /// Number of items per page
        /// </summary>
        int size { get; set; }

    }

    public class NSPageSettings : INSPageSettings
    {
        /// <summary>
        /// Total Number of items for paginate
        /// </summary>
        public int dataItems { get; set; }

        /// <summary>
        /// Current Page
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// Number of items per page
        /// </summary>
        public int size { get; set; }
    }
}