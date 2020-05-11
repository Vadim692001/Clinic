using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using PatcientInfo.Data;
using PatcientInfo.Entities;
using PatcientInfo.Web.Models;

namespace PatcientInfo.Web.Controllers
{
    public class PatcientController : Controller
    {
        private IEnumerable<Patcient> objects;
        private IEnumerable<PatcientTableModel> tableModelObjects;
        private IEnumerable<PatcientInfoModel> infoModelObjects;
        private IEnumerable<PatcientViewModel> viewModelObjects;

        public IEnumerable<Patcient> Objects
        {
            get { return objects; }
            set
            {
                objects = value;
                tableModelObjects = objects.Select(e => (PatcientTableModel)e)
                    .OrderBy(e => e.Sorname);
                infoModelObjects = objects.Select(e => (PatcientInfoModel)e)
                    .OrderBy(e => e.Sorname);
                viewModelObjects = objects.Select(e => (PatcientViewModel)e)
                    .OrderBy(e => e.Sorname);
            }
        }

 

        public PatcientController()
        {
            Objects = StaticDataContext.Patcients;
           
        }

        // GET: Patcient
        public ActionResult Index()
        {
            return View();
        }

        #region Робота з даними

        public ViewResult ObjectsInfo()
        {
            return View(Objects);
        }

        public PartialViewResult _DescriptiveInfo(int id)
        {
            var obj = Objects.First(e => e.Id == id);
            string[] model = null;
            if (!string.IsNullOrWhiteSpace(obj.Description))
            {
                string s = "Опис\n" + obj.Description;
                model = s.Split(new[] { '\n' },
                    StringSplitOptions.RemoveEmptyEntries);
            }
            return PartialView(model);
        }

        #endregion // Робота з даними



        public ViewResult PatcientByRegionsInfo(
                string categoryName = NavigationController.ALL_CATEGORIES)
        {
            IEnumerable<Patcient> models = Objects.OrderBy(e => e.Sorname);
            if (!string.IsNullOrEmpty(categoryName) &&
                categoryName != NavigationController.ALL_CATEGORIES)
            {
                models = models
                    .Where(e => e.Dicase.Nazva == categoryName);
            }
            ViewBag.SelectedCategoryName = categoryName;
            return View(models);
        }




        #region Реалізація асинхронної форми

        public ActionResult Selection()
        {
            ViewBag.selDicaseName = CreateDicaseNameSelectList();
            return View(tableModelObjects);
        }

        const string ALL_VALUES = "...";

        List<SelectListItem> CreateDicaseNameSelectList()
        {
            List<string> values = new List<string>();
            values.Add(ALL_VALUES);
            values.AddRange(Objects
                .Select(e => e.Dicase.Nazva).Distinct());
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

        public PartialViewResult _SelectData(string selSorname, string selDicaseName,
                string selDoctor)
        {
            var model = tableModelObjects;
            if (!string.IsNullOrWhiteSpace(selSorname))
                model = model.Where(e => e.Sorname.ToLower()
                    .StartsWith(selSorname.ToLower()));
            if (selDicaseName != null && selDicaseName != ALL_VALUES)
                model = model.Where(e => e.DicaseName == selDicaseName);
            if (!string.IsNullOrWhiteSpace(selDoctor))
                model = model.Where(e => e.Doctor.ToLower()
                    .StartsWith(selDoctor.ToLower())); 
           

            System.Threading.Thread.Sleep(2000);
            return PartialView("_TableBody", model);
        }

        #endregion // Реалізація асинхронної форми



        #region Використання посилань AJAX

        public const string ALL_PAGE_LINK_NAME = "..";

        public ActionResult BrowseByLetters()
        {
            ViewBag.Letters = new[] { ALL_PAGE_LINK_NAME }
                .Concat(Objects
                    .Select(e => e.Sorname[0].ToString())
                    .Distinct().OrderBy(e => e));
            return View(infoModelObjects);
        }

        public PartialViewResult _GetDataByLetter(string selLetter)
        {
            var model = infoModelObjects;
            if (selLetter != null && selLetter != ALL_PAGE_LINK_NAME)
                model = model.Where(e => e.Sorname[0] == selLetter[0]);
            System.Threading.Thread.Sleep(2000);
            return PartialView("_BrowseData", model);
        }

        #endregion // Використання посилань AJAX



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
            if (!string.IsNullOrWhiteSpace(obj.Note))
            {
                s += "Примітка: " + obj.Note + "\n";
            }
            if (!string.IsNullOrWhiteSpace(obj.Description))
            {
                s += "Опис\n" + obj.Description;
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
