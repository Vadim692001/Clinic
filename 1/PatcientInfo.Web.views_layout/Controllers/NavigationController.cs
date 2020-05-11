using PatcientInfo.Data;
using PatcientInfo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatcientInfo.Web.Controllers
{
    public class NavigationController : Controller
    {
        public IEnumerable<Patcient> Patcient { get; set; }
        public IEnumerable<Discase> Discases { get; set; }

        public NavigationController()
        {
            Patcient = StaticDataContext.Patcients;
            Discases = StaticDataContext.Discases;
        }

        //// GET: Category
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public const string ALL_CATEGORIES = "...";

        [ChildActionOnly]
        public PartialViewResult PatcientByRegionsMenu(
                string categoryName = ALL_CATEGORIES)
        {
            ViewBag.SelectedCategoryName = categoryName;
            List<string> categoryNames = new List<string>();
            categoryNames.Add(ALL_CATEGORIES);
            categoryNames.AddRange(Patcient
                    .Select(e => e.Dicase.Nazva)
                    .Distinct().OrderBy(e => e));
            return PartialView(categoryNames);
        }

      

        [ChildActionOnly]
        public PartialViewResult DiscaseByRegionsMenu(
                string categoryName = ALL_CATEGORIES)
        {
            ViewBag.SelectedCategoryName = categoryName;
            List<string> categoryNames = new List<string>();
            categoryNames.Add(ALL_CATEGORIES);
            categoryNames.AddRange(Discases
                    .Select(e => e.DicaseType.Nazva)
                    .Distinct().OrderBy(e => e));
            return PartialView(categoryNames);
        }
    }
}