using Microsoft.VisualStudio.TestTools.UnitTesting;
using moeKino.Controllers;
using moeKino.Models;
using System.Collections.Generic;
using System.Net;
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

            Film film = (Film) result.Model;
            Assert.AreEqual("Black Panther", film.Name);
            Assert.AreEqual(8, film.Rating);
            Assert.AreEqual(15, film.Audience);

            //Testing the View returned by a Controller
            Assert.AreEqual("Details",result.ViewName);
        }

        //
        //GET Test - Films/Details/{id} - invalid ID
        [TestMethod]
        public void filmsDetailsTestInvalidId() {
            var result = controller.Details(1) as HttpNotFoundResult;
            Assert.AreEqual(404, result.StatusCode);

            var nullResult = controller.Details(null) as HttpStatusCodeResult;
            Assert.AreEqual(400, nullResult.StatusCode);
        }

        //
        //POST Test - Films/Details/{id}
        [TestMethod]
        public void filmsDetailsPostTest() {
            //TODO
            throw new System.NotImplementedException();
        }

        //
        //GET Test - Films/Soon
        [TestMethod]
        public void filmsSoonTest() {
            var result = controller.Soon() as ViewResult;
            Assert.AreEqual("Soon", result.ViewName);
        }

        //
        //GET Test - Films/ArchivedMovies
        [TestMethod]
        public void filmsArchivedTest() {
            var result = controller.ArchivedMovies() as ViewResult;
            List<ArchivedFilm> films = (List<ArchivedFilm>)result.Model;
            ArchivedFilm firstFilm = films[0];
            Assert.AreEqual("Adrift", firstFilm.Name);
        }

        //
        //GET Test - Films/BestMovies
        [TestMethod]
        public void filmsBestMoviesTest() {
            var result = controller.BestMovies() as ViewResult;
            List<Film> films = (List<Film>)result.Model;
            Film bestFilm = films[0];
            Assert.AreEqual("Avengers: Infinity War", bestFilm.Name);
        }

        //
        //REDIRECT Test - AcceptGift/{p}
        [TestMethod]
        public void acceptGiftRedirectTest() {
            var result = controller.AcceptGift(1) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        //
        //GET Test - Films/AddClientToMovie/{id}
        [TestMethod]
        public void addClientToMovieGetTest() {
            //TODO
            throw new System.NotImplementedException();
        }
    }
}
