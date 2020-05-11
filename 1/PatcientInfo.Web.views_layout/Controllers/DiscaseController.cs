using PatcientInfo.Data;
using PatcientInfo.Entities;
using PatcientInfo.Web.Models.Dicase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatcientInfo.Web.Controllers
{
    public class DiscaseController : Controller
    {
        private IEnumerable<Discase> objects;
        private IEnumerable<DiscaseTableModel> tableModelObjects;
        private IEnumerable<DicaseInfoModel> infoModelObjects;
        private IEnumerable<DiscaseViewModel> viewModelObjects;
        public IEnumerable<Discase> Objects
        {
            get { return objects; }
            set
            {
                objects = value;
                tableModelObjects = objects.Select(e => (DiscaseTableModel)e)
    .OrderBy(e => e.Nazva);
                infoModelObjects = objects.Select(e => (DicaseInfoModel)e)
                   .OrderBy(e => e.Nazva);
                viewModelObjects = objects.Select(e => (DiscaseViewModel)e)
                    .OrderBy(e => e.Nazva);
            }
        }



        public DiscaseController()
        {
            Objects = StaticDataContext.Discases;

        }// GET: Index
        public ActionResult Index()
        {
            return View();
        }
        #region Робота з даними

        public ViewResult ObjectsInfoDiscase()
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
        public ViewResult DiscaseByRegionsInfo(
            string categoryName = NavigationController.ALL_CATEGORIES)
        {
            IEnumerable<Discase> models = Objects.OrderBy(e => e.Nazva);
            if (!string.IsNullOrEmpty(categoryName) &&
                categoryName != NavigationController.ALL_CATEGORIES)
            {
                models = models
                    .Where(e => e.DicaseType.Nazva == categoryName);
            }
            ViewBag.SelectedCategoryName = categoryName;
            return View(models);
        }


        #endregion // Робота з даними

        #region Реалізація асинхронної форми

        public ActionResult Selection()
        {
            ViewBag.selDicaseTypeName = CreateDicaseNameSelectList();
            return View(tableModelObjects);
        }

        const string ALL_VALUES = "...";

        List<SelectListItem> CreateDicaseNameSelectList()
        {
            List<string> values = new List<string>();
            values.Add(ALL_VALUES);
            values.AddRange(Objects
                .Select(e => e.DicaseType.Nazva).Distinct());
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (string e in values)
            {
                list.Add(new SelectListItem
                {
                    Text = e,
                    Value = e
                });
            }
            return list;
        }

        public PartialViewResult _SelectData(string selNazva, string selDicaseTypeName
                )
        {
            var model = tableModelObjects;
            if (!string.IsNullOrWhiteSpace(selNazva))
                model = model.Where(e => e.Nazva.ToLower()
                    .StartsWith(selNazva.ToLower()));
            if (selDicaseTypeName != null && selDicaseTypeName != ALL_VALUES)
                model = model.Where(e => e.DicaseTypeName == selDicaseTypeName);
          

            System.Threading.Thread.Sleep(2000);
            return PartialView("_TableBody", model);
        }
        #endregion
        public const string ALL_PAGE_LINK_NAME = "..";

        public ActionResult BrowseByLetters()
        {
            ViewBag.Letters = new[] { ALL_PAGE_LINK_NAME }
                .Concat(Objects
                    .Select(e => e.Nazva[0].ToString())
                    .Distinct().OrderBy(e => e));
            return View(infoModelObjects);
        }

        public PartialViewResult _GetDataByLetter(string selLetter)
        {
            var model = infoModelObjects;
            if (selLetter != null && selLetter != ALL_PAGE_LINK_NAME)
                model = model.Where(e => e.Nazva[0] == selLetter[0]);
            System.Threading.Thread.Sleep(2000);
            return PartialView("_BrowseData", model);
        }



        #region Використання формату JSON

        public ViewResult List()
        {
            return View(viewModelObjects);
        }

        public ViewResult _Details(int id)
        {
            var obj = viewModelObjects.First(e => e.Id == id);
            return View(obj);
        }

        public ViewResult Browse()
        {
            return View(viewModelObjects);
        }

        [HttpPost]
        public JsonResult JsonInfo(int id)
        {
            var info = GetInfo(id);
            System.Threading.Thread.Sleep(2000);
            return Json(info);
        }

        string[] GetInfo(int id)
        {
            var obj = Objects.First(e => e.Id == id);
            string s = null;
            if (!string.IsNullOrWhiteSpace(obj.note))
            {
                s += "Примітка: " + obj.note + "\n";
            }
            if (!string.IsNullOrWhiteSpace(obj.Descripotion))
            {
                s += "Опис\n" + obj.Descripotion;
            }
            string[] info = null;
            if (s != null)
            {
                info = s.Split(new[] { '\n' },
                    StringSplitOptions.RemoveEmptyEntries);
            }
            return info;
        }

        [HttpPost]
        public JsonResult JsonIdInfo(int id)
        {
            var info = GetInfo(id);
            return Json(new { Id = id, Info = info }); //, JsonRequestBehavior.AllowGet
        }

        #endregion // Використання формату JSON


    }
}
