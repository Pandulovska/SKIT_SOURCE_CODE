using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using moeKino.Controllers;

namespace moeKino.Tests
{
    [TestClass]
    public class ManageControllerTests
    {
        ManageController controller;

        [TestInitialize]
        public void init()
        {
            controller = new ManageController();
        }

        [TestMethod]
        public void indexTest()
        {
            ViewResult result = controller.ChangePassword() as ViewResult;
            Assert.AreEqual("ChangePassword", result.ViewName);
        }

    }
}
