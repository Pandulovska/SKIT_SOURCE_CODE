using Microsoft.VisualStudio.TestTools.UnitTesting;
using moeKino.Controllers;
using moeKino.Models;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace moeKino.Tests {
    [TestClass]
    public class FilmsControllerTests {
        FilmsController controller;

        [TestInitialize]
        public void init() {
            controller = new FilmsController();
        }

        [Priority(1)]
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

        [Priority(2)]
        [TestMethod]
        public void filmsDetailsTest()
        {
            ViewResult result = controller.Details(280) as ViewResult;           

            Film film = (Film) result.Model;
            Assert.AreEqual("Black Panther", film.Name);
            Assert.AreEqual(8, film.Rating);
            Assert.AreEqual(15, film.Audience);

            //Testing the View returned by a Controller
            Assert.AreEqual("Details",result.ViewName);
        }

        //
        //GET Test - Films/Details/{id} - invalid ID
        [Priority(3)]
        [TestMethod]
        public void filmsDetailsTestInvalidId1() {
            var result = controller.Details(1) as HttpNotFoundResult;
            Assert.AreEqual(404, result.StatusCode);
        }

        [Priority(4)]
        [TestMethod]
        public void filmsDetailsTestInvalidId2()
        {
            var nullResult = controller.Details(null) as HttpStatusCodeResult;
            Assert.AreEqual(400, nullResult.StatusCode);
        }

        //
        //POST Test - Films/Details/
        [Priority(5)]
        [TestMethod]
        public void filmsDetailsPostTest()
        {
            JsonResult result = controller.Details("8", "279", "87") as JsonResult;
            var responseText = result.Data.GetType().GetProperty("responseText").GetValue(result.Data);
            Assert.AreEqual(responseText, "You have already rated this movie!");
        }


        //
        //GET Test - Films/Soon
        [Priority(6)]
        [TestMethod]
        public void filmsSoonTest() {
            var result = controller.Soon() as ViewResult;
            Assert.AreEqual("Soon", result.ViewName);
        }

        //
        //GET Test - Films/ArchivedMovies
        [Priority(7)]
        [TestMethod]
        public void filmsArchivedTest() {
            var result = controller.ArchivedMovies() as ViewResult;
            List<ArchivedFilm> films = (List<ArchivedFilm>)result.Model;
            ArchivedFilm firstFilm = films[0];
            Assert.AreEqual("Adrift", firstFilm.Name);
        }

        //
        //GET Test - Films/BestMovies
        [Priority(8)]
        [TestMethod]
        public void filmsBestMoviesTest() {
            var result = controller.BestMovies() as ViewResult;
            List<Film> films = (List<Film>)result.Model;
            Film bestFilm = films[0];
            Assert.AreEqual("Avengers: Infinity War", bestFilm.Name);
        }

        //GET Test - Films/Create
        [Priority(9)]
        [TestMethod]
        public void filmsCreateTest()
        {
            ViewResult result = controller.Create() as ViewResult;
            Assert.AreEqual("Create", result.ViewName);
        }


        //POST Test - Films/Create
        [Priority(10)]
        [TestMethod]
        public void filmsCreatePostTest()
        {
            Film newFilm = new Film(4, "Ime4", "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png", "Musical", "Director 2", "6 May 2018 (USA)", "Short description", "Star 1, Star 2", 0.0, 0, "22:00");
            System.Web.Mvc.RedirectToRouteResult result = controller.Create(newFilm) as System.Web.Mvc.RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        //DELETE Test - Films1/Delete{id}
        [Priority(11)]
        [TestMethod]
        public void filmsDeleteTest()
        {
            int id = controller.getFilmIdByTitle("Ime4");
            Films1Controller films1Controller1 = new Films1Controller();
            IHttpActionResult actionResult = films1Controller1.DeleteFilm(id);
            var contentResult = actionResult as OkNegotiatedContentResult<Film>;
            Assert.IsNotNull(contentResult);
        }

        //GET Test - Films/Edit/{id}
        [Priority(12)]
        [TestMethod]
        public void filmsEditTest()
        {
            ViewResult result = controller.Edit(280) as ViewResult;
            Film film = (Film)result.Model;
            Assert.AreEqual("Black Panther", film.Name);
            Assert.AreEqual("16:00", film.Time);
            Assert.AreEqual(15, film.Audience);

            //Testing the View returned by a Controller
            Assert.AreEqual("Edit", result.ViewName);
        }

        //
        //GET Test - Films/Edit/{id} - invalid ID
        [Priority(13)]
        [TestMethod]
        public void filmsEditTestInvalidId1()
        {
            var result = controller.Details(1) as HttpNotFoundResult;
            Assert.AreEqual(404, result.StatusCode);

        }

        [Priority(14)]
        [TestMethod]
        public void filmsEditTestInvalidId2()
        {
            var nullResult = controller.Details(null) as HttpStatusCodeResult;
            Assert.AreEqual(400, nullResult.StatusCode);
        }

        //POST Test - Films/Edit/{id}
        [Priority(15)]
        [TestMethod]
        public void filmsEditPostTest()
        {
            Film film = controller.getFilm(280);
            film.Name = "Black Panther 2";
            System.Web.Mvc.RedirectToRouteResult result = controller.Edit(film) as System.Web.Mvc.RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(280, controller.getFilmIdByTitle("Black Panther 2"));
        }

        //POST Test - Films/Edit/{id}
        [Priority(16)]
        [TestMethod]
        public void filmsEditPostTest2()
        {
            Film film = controller.getFilm(280);
            film.Name = "Black Panther";
            System.Web.Mvc.RedirectToRouteResult result = controller.Edit(film) as System.Web.Mvc.RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(280, controller.getFilmIdByTitle("Black Panther"));
        }
    }
}
