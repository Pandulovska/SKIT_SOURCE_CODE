using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using moeKino.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;

namespace moeKino.Controllers
{
    [Authorize]
    public class ClientsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Clients
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return View(db.Clients.ToList());
            }
            else
                return RedirectToAction("Index", "Films");
        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterViewModel client)
        {
            if (ModelState.IsValid)
            {             
                    var user = new ApplicationUser { UserName = client.Email, Email = client.Email };
                    var result = await UserManager.CreateAsync(user, client.Password);

                    if (result.Succeeded)
                    {
                        var newClient = new Client();
                        newClient.Name = client.Email;
                        newClient.Email = client.Email;
                        db.Clients.Add(newClient);
                        db.SaveChanges();

                        try
                        {
                            var User = UserManager.FindByEmail(client.Email);
                            UserManager.AddToRole(User.Id, "User");
                        }
                        catch (Exception ex)
                        {
                            return HttpNotFound();
                        }
                        return RedirectToAction("Index");
                    }
                return HttpNotFound();                
               
            }

            return HttpNotFound();
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientId,Name,Email")] Client client)
        {
            if (ModelState.IsValid)
            {
                var foundUser = UserManager.FindByEmail(client.Name);
                foundUser.Email = client.Email;
                foundUser.UserName = client.Email;
                UserManager.Update(foundUser);

                client.Name = client.Email;
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
     
                return RedirectToAction("Index");
            }
            return View(client);
        }
               
        public ActionResult Tickets()
        {
            foreach ( var client in db.Clients) {
                if (client.Name == User.Identity.Name) {
                    ViewBag.ClientId = client.ClientId;
                    break;
                }
            } 
           
            return View(db.Tickets.ToList());

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
}
