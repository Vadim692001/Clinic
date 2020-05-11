using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatcientInfo.Entities;
using PatcientInfo.Web.Controllers;
using Moq;
using System.Web.Mvc;
using PatcientInfo.Web.Models;

namespace ModylTect4_5
{
    [TestClass]
    public class UnitTest1
    {
        List<Patcient> objects = new List<Patcient> {
        new Patcient()
        {
                Sorname = "Петренко",
                Doctor = "Лобанов",
                Dicase=new Discase(){Nazva = "aaa"}
            },
             new Patcient()
             {
                Sorname = "Коваленко",
                Doctor = "Лобанов",
                Dicase=new Discase(){Nazva = "aaa"}
            }, new Patcient()
            {
                Sorname = "Петлюра",
                Doctor = "Ромоновський",
                 Dicase=new Discase(){Nazva = "bbb"}
            }
};

        [TestMethod]
        public void EditGet_Model_IsEditingModelObject_moq()
        {
            var mock = new Mock<IList<Patcient>>();
            var controller = new PatcientCrudController()
            {
                Objects = mock.Object
            };
            var model = new PatcientEditingModel();
            ViewResult result = controller.Edit(model) as ViewResult;

            //Assert.IsInstanceOfType(result.Model, typeof(PatcientEditingModel));
        }

        [TestMethod]
        ////
        public void EditPost_Result_RedirectToActionIndex_moq()
        {
            var mock = new Mock<IList<Patcient>>();
            var controller = new PatcientCrudController()
            {
                Objects = mock.Object

            };
            var model = new PatcientEditingModel();


            ActionResult result = controller.Edit(model);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var redirectResult = result as RedirectToRouteResult;
            Assert.AreEqual(redirectResult.RouteValues["action"], "Index");
        }

        [TestMethod]
        public void EditPost_TempData_KeysContains_message_moq()
        {
            var mock = new Mock<IList<Patcient>>();
            var controller = new PatcientCrudController()
            {
                Objects = mock.Object
            };
            var model = new PatcientEditingModel();


            ActionResult result = controller.Edit(model);

            Assert.IsFalse(controller.TempData.Keys.Contains("massage"));
        }

        [TestMethod]
        public void EditPost_ModelStateIsNotValid_ReturnedViewResult_moq()
        {
            var mock = new Mock<IList<Patcient>>();
            var controller = new PatcientCrudController()
            {
                Objects = mock.Object
            };
            var model = new PatcientEditingModel();
            controller.ModelState.AddModelError("", "error message");

            ActionResult result = controller.Edit(model);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual(viewResult.Model, model);
        }

    }
}
