using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using moeKino.Controllers;

namespace moeKino.Tests {
    [TestClass]
    public class HomeControllerTests {
        HomeController controller;

        [TestInitialize]
        public void init() {
            controller = new HomeController();
        }

        [TestMethod]
        public void indexTest() {
            ViewResult result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void aboutTest() {
            ViewResult result = controller.About() as ViewResult;
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void contactTest() {
            ViewResult result = controller.Contact() as ViewResult;
            Assert.AreEqual("Your contact page.", result.ViewBag.Message);
        }
    }
}
