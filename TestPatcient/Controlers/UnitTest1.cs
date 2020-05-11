  using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatcientInfo.Entities;
using PatcientInfo.Web.Controllers;

namespace TestPatcient.Controlers
{
    [TestClass]
    public class UnitTest1
    {


        List<Patcient> objects = new List<Patcient> {
            new Patcient() {
                Sorname = "Петренко",
                Doctor = "Лобанов",
                Dicase=new Discase(){Nazva = "aaa"}
            },
             new Patcient() {
                Sorname = "Коваленко",
                Doctor = "Лобанов",
                Dicase=new Discase(){Nazva = "aaa"}
            }, new Patcient() {
                Sorname = "Петлюра",
                Doctor = "Ромоновський",
                 Dicase=new Discase(){Nazva = "bbb"}
           },
        };
          
            [TestMethod]
        //public void TestMethod1() {
        public void GenreByRegionsInfo_ResultIsViewResult()
        {
            var controller = new PatcientController()
            {
                Objects = objects
            };

            var result = controller.PatcientByRegionsInfo();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }


        [TestMethod]
        public void GenreByRegionsInfo_ViewBagAreEqualParam_True()
        {
            var controller = new PatcientController()
            {
                Objects = objects
            };
            string categoryName = "aaa";

            var result = controller
                .PatcientByRegionsInfo(categoryName) as ViewResult;
            var selectedCategoryName = result.ViewBag.SelectedCategoryName as string;

            Assert.AreEqual(categoryName, selectedCategoryName);
        }

        [TestMethod]
        public void GenreByRegionsInfo_ModelIsEnumerableOfPatcient()
        {
            var controller = new PatcientController()
            {
                Objects = objects
            };

            var result = controller.PatcientByRegionsInfo() as ViewResult;
            var model = result.Model;

            Assert.IsInstanceOfType(model, typeof(IEnumerable<Patcient>));
        }

        [TestMethod]
        public void GenreByRegionsInfo_NoParam_ModelContainsAllObjects()
        {
            var controller = new PatcientController()
            {
                Objects = objects
            };

            var result = controller.PatcientByRegionsInfo() as ViewResult;
            var model = result.Model as IEnumerable<Patcient>;

            Assert.AreEqual(objects.Count, model.Count());
        }

        [TestMethod]
        public void GenreByRegionsInfo_ParamALL_CATEGORIES_ModelContainsAllObjects()
        {
            var controller = new PatcientController()
            {
                Objects = objects
            };

            var result = controller.PatcientByRegionsInfo(
                NavigationController.ALL_CATEGORIES) as ViewResult;
            var model = result.Model as IEnumerable<Patcient>;

            Assert.AreEqual(objects.Count, model.Count());
        }

        [TestMethod]
        public void GenreByRegionsInfo_ParamEasternEurope_ModelContains2Object()
        {
            var controller = new PatcientController()
            {
                Objects = objects
            };
            string categoryName = "aaa";

            var result = controller
                .PatcientByRegionsInfo(categoryName) as ViewResult;
            var model = result.Model as IEnumerable<Patcient>;

            Assert.AreEqual(2, model.Count());
        }

    }
}

