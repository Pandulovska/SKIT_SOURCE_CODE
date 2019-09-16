using Microsoft.VisualStudio.TestTools.UnitTesting;
using moeKino.Controllers;
using moeKino.Models;
using System.Collections.Generic;
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
            ViewResult result = controller.Index() as ViewResult;
            List<Film> films = (List<Film>)result.Model;

            //Testing the View Data returned by a Controller
            Assert.AreEqual(7, films[3].Rating);

            Assert.AreEqual("A Quiet Place", films[6].Name);
            Assert.AreEqual(0, films[6].Rating);
            Assert.AreEqual(3, films[6].Audience);

            Assert.AreEqual("Isle of Dogs", films[9].Name);
            Assert.AreEqual(0, films[9].Rating);
            Assert.AreEqual(1, films[9].Audience);
        }

        [TestMethod]
        public void filmsDetailsTest()
        {
            ViewResult result = controller.Details(280) as ViewResult;           

            Film film =(Film) result.Model;
            Assert.AreEqual("Black Panther", film.Name);
            Assert.AreEqual(8, film.Rating);
            Assert.AreEqual(15, film.Audience);

            //Testing the View returned by a Controller
            Assert.AreEqual("Details",result.ViewName);
        }

    }
}
