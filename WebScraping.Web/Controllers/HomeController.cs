using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebScraping.Api;
using WebScraping.Web.Models;

namespace WebScraping.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            List<NewsItem> newsItems = LakewoodScoopApi.ScrapeLakewoodScoop();
            return View(newsItems);
        }
    }
}
