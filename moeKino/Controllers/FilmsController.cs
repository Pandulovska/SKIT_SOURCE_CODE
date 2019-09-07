using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using moeKino.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Routing;
using System.Collections.Generic;

namespace moeKino.Controllers
{
    public class FilmsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Films
        public ActionResult Index()
        {
            
            foreach (var film in db.Films.ToList())
            {
                var ratings = db.MovieRatings.Where(d => d.movieId.Equals(film.Id)).ToList();
                if (ratings.Count() > 0)
                {
                    var ratingSum = ratings.Sum(d => d.rating);
                    var ratingCount = ratings.Count();
                    film.Rating = Convert.ToDouble(ratingSum) / ratingCount;
                }
                else
                {
                    film.Rating = 0;
                }

                
            }
            var points = 0;
            foreach (var item in db.Clients)
            {

                if (item.Email == User.Identity.GetUserName())
                {
                    points = item.Points;
                    break;
                }
            }
            ViewBag.Points = points;
            return View(db.Films.ToList());
        }
        public ActionResult Soon() {

            return View();
        }
        public ActionResult ArchivedMovies()
        {
            return View(db.ArchivedFilms.ToList());
        }
        public ActionResult BestMovies()
        {

            foreach (var film in db.Films.ToList())
            {
                var ratings = db.MovieRatings.Where(d => d.movieId.Equals(film.Id)).ToList();
                if (ratings.Count() > 0)
                {
                    var ratingSum = ratings.Sum(d => d.rating);
                    var ratingCount = ratings.Count();
                    film.Rating = Convert.ToDouble(ratingSum) / ratingCount;
                }
                else
                {
                    film.Rating = 0;
                }
            }
                return View(db.Films.ToList());
        }
        public ActionResult AcceptGift(int p)
        {
            int id = 0;
            foreach (var item in db.Clients)
            {

                if (item.Email == User.Identity.GetUserName())
                {
                    id = item.ClientId;
                    break;
                }
            }
            var najdiKlient = db.Clients.Find(id);
            najdiKlient.Points = najdiKlient.Points-p;
            db.SaveChanges();

            return RedirectToAction("Index", "Films");
        }
        [NoDirectAccess]
        public ActionResult Gift1() {
            return View();
        }
        [NoDirectAccess]
        public ActionResult Gift2()
        {
            return View();
        }
        [NoDirectAccess]
        public ActionResult Gift3()
        {
            return View();
        }

        //2 akcii za dodavanje klient na odreden film
        [HttpGet]
         public ActionResult AddClientToMovie(int id) {
            var model = new RatingClient();

            ViewBag.Client = User.Identity.Name;
            model.FilmId = id;
             model.Clients = db.Clients.ToList();
             var film = db.Films.Find(id);
             ViewBag.Name = film.Name;
             ViewBag.Time = film.Time;
             return View(model);
         }
         [HttpPost]
         public ActionResult AddClientToMovie(RatingClient model)
         {
             var film = db.Films.Find(model.FilmId);
             var client = db.Clients.Find(model.ClientId);
             film.clients.Add(client);
             client.Points += (10*model.NumberTickets);
             film.Audience += model.NumberTickets;

            Ticket ticket = new Ticket(model.ClientId,model.Date,model.Time,model.NumberTickets,film.Name);
            db.Tickets.Add(ticket);
            db.SaveChanges();
            if (client.Points >= 50 && client.Points < 100)
            {
                return RedirectToAction("Gift1", "Films");
            }
            else if (client.Points == 100)
            {
                return RedirectToAction("Gift2", "Films");

            }
            else if (client.Points > 100)
            {
                int id = 0;
                foreach (var item in db.Clients)
                {

                    if (item.Email == User.Identity.GetUserName())
                    {
                        id = item.ClientId;
                        break;
                    }
                }
                var najdiKlient = db.Clients.Find(id);
                najdiKlient.Points =najdiKlient.Points-101;
                db.SaveChanges();
                return RedirectToAction("Gift3", "Films");

            }
            return RedirectToAction("Index", "Films"); 
            //return View("Index", db.Films.ToList());
         }

        // GET: Films/Details/5
        public ActionResult Details(int? id)
        {
             if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.Films.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            if (User.IsInRole("User")) {
                Client client=db.Clients.Where(d => d.Name == User.Identity.Name).First();
                ViewBag.Id = client.ClientId;
            }
            foreach (var movie in db.Films.ToList())
            {
                var ratings = db.MovieRatings.Where(d => d.movieId.Equals(movie.Id)).ToList();
                if (ratings.Count() > 0)
                {
                    var ratingSum = ratings.Sum(d => d.rating);
                    var ratingCount = ratings.Count();
                    movie.Rating = Convert.ToDouble(ratingSum) / ratingCount;
                }
                else
                {
                    movie.Rating = 0;
                }
                            
            }
            return View(film);
        }

        [HttpPost]
        public ActionResult Details(string userRate,string id, string clientId)
        {
            int userId = Convert.ToInt32(clientId);
            int movieId = Convert.ToInt32(id);
            Film film =db.Films.Find(movieId);
           
              List<MovieRatings> ratings = db.MovieRatings.Where(d => d.movieId.Equals(film.Id)).ToList();
              int num=ratings.FindAll(r => r.clientId == userId).Count();
                if (num == 0)
                {
                    MovieRatings rating = new MovieRatings(movieId, Convert.ToInt32(userRate), userId);
                    db.MovieRatings.Add(rating);
                    db.SaveChanges();

                var rejting = 0.0;
                   
                        var ratingSum = ratings.Sum(d => d.rating);
                        ratingSum += Convert.ToInt32(userRate);
                        var ratingCount = ratings.Count();
                        ratingCount++;
                        rejting = Convert.ToDouble(ratingSum) / ratingCount;      

                
                return Json(new { success = true, responseText = "Thank you for rating this movie!", rate= rejting.ToString("#.##")}, JsonRequestBehavior.AllowGet);

                }
                else {
                    return Json(new { success = false, responseText = "You have already rated this movie!" }, JsonRequestBehavior.AllowGet);
                }
        
        }

        // GET: Films/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Films/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Url,Genre,Director,ReleaseDate,ShortDescription,Stars")] Film film)
        {
            if (ModelState.IsValid)
            {
                db.Films.Add(film);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(film);
        }

        // GET: Films/Edit/5
        [Authorize(Roles="Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.Films.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        // POST: Films/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Url,Genre,Director,ReleaseDate,ShortDescription,Stars")] Film film)
        {
            if (ModelState.IsValid)
            {
                db.Entry(film).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(film);
        }
               

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class NoDirectAccessAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.UrlReferrer == null ||
     filterContext.HttpContext.Request.Url.Host != filterContext.HttpContext.Request.UrlReferrer.Host)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Films", action = "Index" }));
            }
        }
    }
}
