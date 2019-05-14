using System;
using System.Collections.Generic;
using System.Text;

namespace WebScraping.Api
{
    public class NewsItem
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public DateTime Date { get; set; }
    }
}
