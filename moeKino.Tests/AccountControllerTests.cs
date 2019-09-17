using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using moeKino.Controllers;

namespace moeKino.Tests {
    [TestClass]
    public class AccountControllerTests {
        AccountController controller;

        [TestInitialize]
        public void init() {
            controller = new AccountController();
        }
        
        //
        //GET Test - Login
        [TestMethod]
        public void loginGetTest() {
            var result = controller.Login("test") as ViewResult;
            Assert.AreEqual("test", result.ViewBag.ReturnUrl);
        }

        //
        //GET Test 2 - Login with null parameter
        [TestMethod]
        public void loginGetTestNullParam() {
            var result = controller.Login(null) as ViewResult;
            Assert.IsNull(result.ViewBag.ReturnUrl);
        }

        //GET Test - Register
        [TestMethod]
        public void registerGetTest() {
            var result = controller.Register() as ViewResult;
            Assert.AreEqual("Register",result.ViewName);
        }
    }
}
