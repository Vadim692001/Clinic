using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatcientInfo.Entities;
using PatcientInfo.Web.Controllers;
using PatcientInfo.Web.Models;

namespace ModylTect4_5
{
    [TestClass]
    public class UnitTest2
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
        public void PRACTPatcientByRegionsInfo_ViewBagAreEqualParam_True()
        {
            var controller = new PatcientController()
            {
                Objects = objects
            };
            string categoryName = null;

            var result = controller
                .List() as ViewResult;
            var selectedCategoryName = result.ViewBag.SelectedCategoryName as string;

            Assert.AreEqual(categoryName, selectedCategoryName);
        }

        [TestMethod]
        public void PRACTPatcientByRegionsInfo_ModelIsEnumerableOfPatcient()
        {
            var controller = new PatcientController()
            {
                Objects = objects
            };

            var result = controller.BrowseByLetters() as ViewResult;
            var model = result.Model;

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

      

        [TestMethod]
        public void PRACTPatcientByRegionsInfo_ParamALL_CATEGORIES_ModelContainsAllObjects()
        {
            var controller = new PatcientController()
            {
                Objects = objects
            };

            var result = controller.Browse() as ViewResult;
            var model = result.Model as IEnumerable<Patcient>;

            Assert.IsNotInstanceOfType(model, typeof(IEnumerable<PatcientEditingModel>));
        }

       
    }
}
