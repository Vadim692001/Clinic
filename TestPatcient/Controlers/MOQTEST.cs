using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PatcientInfo.Entities;
using PatcientInfo.Web.Controllers;
using System.Web.Mvc;
using PatcientInfo.Web.Models;

namespace TestPatcient.Controlers
{
 
    [TestClass]
    public class MOQTEST
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
        //public void TestMethod1() {
        public void Index_Model_IsSelectionModelCollection_moq()
        {
            var mock = new Mock<IList<Patcient>>();
            var controller = new PatcientCrudController()
            {
                Objects = mock.Object
            };

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsInstanceOfType(result.Model,
                typeof(IEnumerable<PatcientSelectionModel>));
        }

        [TestMethod]
        public void CreateGet_Model_IsEditingModelObject_moq()
        {
            var mock = new Mock<IList<Patcient>>();
            var controller = new PatcientCrudController()
            {
                Objects = mock.Object
            };

            ViewResult result = controller.Create() as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(PatcientEditingModel));
        }

        [TestMethod]
        ////
        public void CreatePost_Result_RedirectToActionIndex_moq()
        {
            var mock = new Mock<IList<Patcient>>();
            var controller = new PatcientCrudController()
            {
                Objects = mock.Object

            };
            var model = new PatcientEditingModel();


            ActionResult result = controller.Create(model);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var redirectResult = result as RedirectToRouteResult;
            Assert.AreEqual(redirectResult.RouteValues["action"], "Index");
        }

        [TestMethod]
        public void CreatePost_TempData_KeysContains_message_moq()
        {
            var mock = new Mock<IList<Patcient>>();
            var controller = new PatcientCrudController()
            {
                Objects = mock.Object
            };
            var model = new PatcientEditingModel();


            ActionResult result = controller.Create(model);

            Assert.IsFalse(controller.TempData.Keys.Contains("massage"));
        }

        [TestMethod]
        public void CreatePost_ModelStateIsNotValid_ReturnedViewResult_moq()
        {
            var mock = new Mock<IList<Patcient>>();
            var controller = new PatcientCrudController()
            {
                Objects = mock.Object
            };
            var model = new PatcientEditingModel();
            controller.ModelState.AddModelError("", "error message");

            ActionResult result = controller.Create(model);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual(viewResult.Model, model);
        }


    }
}
