using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using moeKino.Controllers;
using moeKino.Models;

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
            var result = controller.Login("/Films/Index") as ViewResult;
            Assert.AreEqual("/Films/Index", result.ViewBag.ReturnUrl);
        }

        //
        //GET Test 2 - Login with null parameter //should fail!!
        [TestMethod]
        public void loginGetTestNullParam() {
            var result = controller.Login(null) as ViewResult;
            Assert.IsNull(result.ViewBag.ReturnUrl);
        }
        /*
        [TestMethod]
        public async Task loginPostTest()
        {
            LoginViewModel model = new LoginViewModel();
            model.Email = "admin@yahoo.com";
            model.Password = "Admin1*";
            model.RememberMe = false;

            var actionResult = await controller.Login(model, "/");
            
            var result = actionResult as ViewResult;
            Assert.IsNotNull(result);

            //Assert.AreEqual("/Films/Index", result.ViewBag.ReturnUrl);
        }
        */
        //GET Test - Register
        [TestMethod]
        public void registerGetTest() {
            var result = controller.Register() as ViewResult;
            Assert.AreEqual("Register",result.ViewName);
        }
    }
}
