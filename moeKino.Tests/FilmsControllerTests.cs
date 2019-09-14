using Microsoft.VisualStudio.TestTools.UnitTesting;
using moeKino.Controllers;
using System.Web.Mvc;

namespace moeKino.Tests {
    [TestClass]
    public class FilmsControllerTests {
        FilmsController controller;

        [TestInitialize]
        public void init() {
            controller = new FilmsController();
        }

        [TestMethod]
        public void filmsIndexTest() {
            //error za dbContext
            ViewResult result = controller.Index() as ViewResult;
            Assert.AreEqual("", result.ViewBag.Title);
        }

    }
}
