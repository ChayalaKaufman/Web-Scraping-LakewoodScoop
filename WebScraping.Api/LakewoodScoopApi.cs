using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace WebScraping.Api
{
    public static class LakewoodScoopApi
    {
        public static List<NewsItem> ScrapeLakewoodScoop()
        {
            var html = GetLakewoodScoopHtml();
            return GetNewsItems(html);
        }

        private static string GetLakewoodScoopHtml()
        {
            var handler = new HttpClientHandler();
            handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Add("user-agent", "user agent");
                var url = $"https://www.thelakewoodscoop.com";
                var html = client.GetStringAsync(url).Result;
                return html;
            }
        }

        private static List<NewsItem> GetNewsItems(string html)
        {
            var parser = new HtmlParser();
            IHtmlDocument document = parser.ParseDocument(html);
            var itemDivs = document.QuerySelectorAll("#content .post");
            List<NewsItem> newsItems = new List<NewsItem>();
            foreach (var div in itemDivs)
            {
                NewsItem news = new NewsItem();
                var a = div.QuerySelectorAll("h2 a").First();
                news.Title = a.TextContent.Trim();
                news.Url = a.Attributes["href"].Value;

                var image = div.QuerySelector("img");
                news.ImageUrl = image.Attributes["src"].Value;

                var date = div.QuerySelector(".postmetadata-top");
                if (!String.IsNullOrEmpty(date.TextContent))
                {
                    news.Date = DateTime.Parse(date.TextContent.Trim());
                }

                newsItems.Add(news);
            }

            return newsItems;
        }
    }

}
