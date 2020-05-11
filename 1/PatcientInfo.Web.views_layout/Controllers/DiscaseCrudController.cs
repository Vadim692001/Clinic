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
    public class DiscaseCrudController : Controller
    {
        private ICollection<Discase> objects;
        private IEnumerable<DicaseSelectionModel> selectionModelObjects { get; set; }
        private IEnumerable<DiscacaseEditingModel> editingModelObjects { get; set; }

        public ICollection<Discase> Objects
        {
            get { return objects; }
            set
            {
                objects = value;
                selectionModelObjects = objects
                    .Select(e => (DicaseSelectionModel)e).OrderBy(e => e.Nazva);
                editingModelObjects = objects
                    .Select(e => (DiscacaseEditingModel)e).OrderBy(e => e.Nazva);
            }
        }

        public IEnumerable<DicaseType> DicaseTypes { get; set; }


        public DiscaseCrudController()
        {
            Objects = StaticDataContext.Discases;
            DicaseTypes = StaticDataContext.DicaseTypes;
           
        }
        // GET: DiscaseCrud
        public ActionResult Index()
        {
            //return View();
            return View(selectionModelObjects);
        }

        public ViewResult Create()
        {
            ViewBag.DicaseTypeName = CreateDicaseTypesNameSelectList();
            return View(new DiscacaseEditingModel());
        }

        [HttpPost]
        public ActionResult Create(DiscacaseEditingModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.DicaseTypeName = CreateDicaseTypesNameSelectList();
                return View(model);
            }
            //Objects.Add((Discase)model);
            Objects.Add(CreateEntityObject(model));
         //   StaticDataContext.Save();
            TempData["message"] = string.Format(
                "Дані пацієнта \"{0}\" збережено", model.Nazva);
            return RedirectToAction("Index");
        }

        private Discase CreateEntityObject(DiscacaseEditingModel model)
        {
            Discase entityObject = new Discase();

            entityObject.Nazva = model.Nazva;

            entityObject.DicaseType = DicaseTypes.First(e => e.Nazva == model.DicaseTypeName);
            entityObject.Descripotion = model.Descripotion;
            entityObject.note = model.note;

            // entityObject.Date = model.Date;
            return entityObject;
        }



        [HttpGet]
        public ActionResult Edit(int id)
        {
            DiscacaseEditingModel model = editingModelObjects.First(e => e.Id == id);
            ViewBag.DicaseTypeName =
                CreateDicaseTypesNameSelectList(model.DicaseTypeName);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(DiscacaseEditingModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.DicaseTypeName =
                    CreateDicaseTypesNameSelectList(model.DicaseTypeName);
                return View(model);
            }
            var entityObject = Objects.First(e => e.Id == model.Id);
            UpdateEntityObject(entityObject, model);
            //StaticDataContext.Save();
            TempData["message"] = string.Format(
                "Зміни дані про  пацєнта \"{0}\" збережено", model.Nazva);
            return RedirectToAction("Index");
        }

        private void UpdateEntityObject(Discase entityObject,
                DiscacaseEditingModel model)
        {
            entityObject.Nazva = model.Nazva;

            entityObject.DicaseType = DicaseTypes.First(e => e.Nazva == model.DicaseTypeName);
            entityObject.Descripotion = model.Descripotion;
            entityObject.note = model.note;
        }

        public ActionResult Delete(int id)
        {
            var model = editingModelObjects.First(e => e.Id == id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Discase model)
        {
            Discase entityObject = Objects.First(e => e.Id == model.Id);
            Objects.Remove(entityObject);
           // StaticDataContext.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var model = editingModelObjects.First(e => e.Id == id);
            return View(model);
        }

        List<SelectListItem> CreateDicaseTypesNameSelectList(string selectedValue = "")
        {
            List<string> values = new List<string>();
            values.AddRange(DicaseTypes.Select(e => e.Nazva));
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (string e in values)
            {
                list.Add(new SelectListItem
                {
                    Text = e,
                    Value = e,
                    Selected = e == selectedValue
                });
            }
            return list;
        }

    }
}