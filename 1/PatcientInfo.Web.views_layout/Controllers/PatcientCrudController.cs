using PatcientInfo.Data;
using PatcientInfo.Entities;
using PatcientInfo.Web.Models;
using PatcientInfo.Web.Models.Dicase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PatcientInfo.Web.Controllers
{
    public class PatcientCrudController : Controller
    {
        private ICollection<Patcient> objects;
        private IEnumerable<PatcientSelectionModel> selectionModelObjects { get; set; }
        private IEnumerable<PatcientEditingModel> editingModelObjects { get; set; }

        public ICollection<Patcient> Objects
        {
            get { return objects; }
            set
            {
                objects = value;
                selectionModelObjects = objects
                    .Select(e => (PatcientSelectionModel)e).OrderBy(e => e.Sorname);
                editingModelObjects = objects
                    .Select(e => (PatcientEditingModel)e).OrderBy(e => e.Sorname);
            }
        }
      


        public IEnumerable<DicaseType> DicaseTypes { get; set; }
        public IEnumerable<Discase> Discases { get; set; }

        public PatcientCrudController()
        {
            Objects = StaticDataContext.Patcients;
            DicaseTypes = StaticDataContext.DicaseTypes;
            Discases = StaticDataContext.Discases;
        }   
        // GET: PatcientCrud
        public ActionResult Index()
        {
            //return View();
            return View(selectionModelObjects);
        }

        public ViewResult Create()
        {
            ViewBag.DicaseName = CreateDicaseNameSelectList();
            return View(new PatcientEditingModel());
        }

        [HttpPost]
        public ActionResult Create(PatcientEditingModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.DicaseName = CreateDicaseNameSelectList();
                return View(model);
            }
            //Objects.Add((Patcient)model);
            Objects.Add(CreateEntityObject(model));
            //StaticDataContext.Save();
            TempData["message"] = string.Format(
                "Дані пацієнта \"{0}\" збережено", model.Sorname);
            return RedirectToAction("Index");
        }

        private Patcient CreateEntityObject(PatcientEditingModel model)
        {
            Patcient entityObject = new Patcient();

            entityObject.Sorname = model.Sorname;
            
            //entityObject.Dicase =  Discases.First(e => e.Nazva == model.DicaseName);
            //entityObject.Doctor = model.Doctor;
            //entityObject.Medical_card = model.Medical_card;

            // entityObject.Date = model.Date;
            return entityObject;
        }



        [HttpGet]
        public ActionResult Edit(int id)
        {
            PatcientEditingModel model = editingModelObjects.First(e => e.Id == id);
            ViewBag.DicaseName =
                CreateDicaseNameSelectList(model.DicaseName);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(PatcientEditingModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.DicaseName =
                    CreateDicaseNameSelectList(model.DicaseName);
                return View(model);
            }
            //var entityObject = Objects.First(e => e.Id == model.Id);
            //UpdateEntityObject(entityObject, model);
          //  StaticDataContext.Save();
            TempData["message"] = string.Format(
                "Зміни дані про  пацєнта \"{0}\" збережено", model.Sorname);
            return RedirectToAction("Index");
        }

        private void UpdateEntityObject(Patcient entityObject,
                PatcientEditingModel model)
        {
            entityObject.Sorname = model.Sorname;
          
            entityObject.Dicase = Discases.First(e => e.Nazva == model.DicaseName);
            entityObject.Doctor = model.Doctor;
            entityObject.Medical_card = model.Medical_card;
            //entityObject.Date = model.Date;
        }

        public ActionResult Delete(int id)
        {
            var model = editingModelObjects.First(e => e.Id == id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Patcient model)
        {
            Patcient entityObject = Objects.First(e => e.Id == model.Id);
            Objects.Remove(entityObject);
            //StaticDataContext.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var model = editingModelObjects.First(e => e.Id == id);
            return View(model);
        }

        List<SelectListItem> CreateDicaseNameSelectList(string selectedValue = "")
        {
            List<string> values = new List<string>();
           // values.AddRange(Discases.Select(e => e.Nazva));
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