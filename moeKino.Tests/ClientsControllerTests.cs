using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using moeKino.Controllers;
using moeKino.Models;

namespace moeKino.Tests
{
    [TestClass]
    public class ClientsControllerTests
    {
        ClientsController controller;

        [TestInitialize]
        public void init()
        {
            controller = new ClientsController();
        }

        [Priority(1)]
        [TestMethod]
        public void clientsDetailsTest()
        {
            ViewResult result = controller.Details(18) as ViewResult;

            Client client = (Client)result.Model;
            Assert.AreEqual("admin@yahoo.com", client.Email);
            Assert.AreEqual("admin@yahoo.com", client.Name);
            Assert.AreEqual(0, client.Points);

            //Testing the View returned by a Controller
            Assert.AreEqual("Details", result.ViewName);
        }

        [Priority(2)]
        [TestMethod]
        public void clientsDetailsTestInvalidId1()
        {
            var result = controller.Details(-1) as HttpNotFoundResult;
            Assert.AreEqual(404, result.StatusCode);
        }

        [Priority(3)]
        [TestMethod]
        public void clientsDetailsTestInvalidId2()
        {
            var nullResult = controller.Details(null) as HttpStatusCodeResult;
            Assert.AreEqual(400, nullResult.StatusCode);
        }

        [Priority(4)]
        [TestMethod]
        public void clientsCreateTest()
        {
            ViewResult result = controller.Create() as ViewResult;
            Assert.AreEqual("Create", result.ViewName);
        }

        [Priority(5)]
        [TestMethod]
        public void clientsEditTest()
        {
            ViewResult result = controller.Edit(18) as ViewResult;
            Client client = (Client)result.Model;
            Assert.AreEqual("admin@yahoo.com", client.Name);
            Assert.AreEqual("admin@yahoo.com", client.Email);
            Assert.AreEqual(0, client.Points);

            //Testing the View returned by a Controller
            Assert.AreEqual("Edit", result.ViewName);
        }

        [Priority(6)]
        [TestMethod]
        public void clientsEditTestInvalidId()
        {
            var result = controller.Edit(-1) as HttpNotFoundResult;
            Assert.AreEqual(404, result.StatusCode);
        }
    }
}
