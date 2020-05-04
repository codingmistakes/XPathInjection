using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using XPathInjection.Models;
using XPathInjection.Utility;

namespace XPathInjection.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View(new List<Offer>());
        }

        public IActionResult Search(string title)
        {
            if (String.IsNullOrEmpty(title))
            {
                return View("Index", new List<Offer>());
            }

            ViewBag.Search = title.Trim();

            string xmlPath = _webHostEnvironment.ContentRootPath + "\\offers.xml";

            return View("Index", OfferParser.QueryOffer(title.Trim(), xmlPath));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
