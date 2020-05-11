using PatcientInfo.Data;
using PatcientInfo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatcientInfo.Web.Controllers
{
    public class DiscaseTypeController : Controller
    {
        public IEnumerable<DicaseType> Objects { get; set; }

        public DiscaseTypeController()
        {
            Objects = StaticDataContext.DicaseTypes;
        }

        // GET: Countries
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult ObjectsInfoDiscaseType()
        {
            return View(Objects);
        }

        public PartialViewResult _DescriptiveInfo(int id)
        {
            var obj = Objects.First(e => e.Id == id);
            string[] model = null;
            if (!string.IsNullOrWhiteSpace(obj.Descripotion))
            {
                string s = "Опис\n" + obj.Descripotion;
                model = s.Split(new[] { '\n' },
                    StringSplitOptions.RemoveEmptyEntries);
            }
            return PartialView(model);
        }
    }
}