using Microsoft.VisualStudio.TestTools.UnitTesting;
using moeKino.Models;
using moeKino.Controllers;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;


namespace moeKinoMock
{
    [TestClass]
    public class FilmsTest
    {
       public Mock<DbSet<Film>> mockSet;
       public Mock<DbSet<MovieRatings>> mockSetRatings;
       public Mock<ApplicationDbContext> mockContext;
       public Mock<DbSet<Client>> mockSetClients;
       public Mock<DbSet<Ticket>> mockSetTickets;

        public DbSet<Film> dbSetFilms;
       public DbSet<MovieRatings> dbSetRatings;
       public DbSet<Client> dbSetClients;
       public DbSet<ArchivedFilm> dbSetArchivedFilms;

        public static DbSet<T> GetQueryableMockDbSet<T>(params T[] sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

            return dbSet.Object;
        }

        [TestInitialize]
        public void createDummyData()
        {
            //Define the mock type as DbSet<Film>
            mockSet = new Mock<DbSet<Film>>();
            mockSetRatings = new Mock<DbSet<MovieRatings>>();
            mockSetClients = new Mock<DbSet<Client>>();
            mockSetTickets = new Mock<DbSet<Ticket>>();

            //Define the mock Repository as ApplicationDbContext
            mockContext = new Mock<ApplicationDbContext>();

            dbSetFilms = GetQueryableMockDbSet(
                 new Film { Id = 1, Name = "Ime1", Url = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png", Genre = "Mystery/Drama", Director = "Director 1", ReleaseDate = "16 February 2018 (USA)", ShortDescription = "Short description", Stars = "Star 1, Star 2", Rating = 8.5, Audience = 1005, Time = "16:00" },
                 new Film { Id = 2, Name = "Ime2", Url = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png", Genre = "Comedy", Director = "Director 3", ReleaseDate = "19 October 2018 (USA)", ShortDescription = "Short description", Stars = "Star 1, Star 2", Rating = 6, Audience = 701, Time = "20:00" },
                 new Film { Id = 3, Name = "Ime3", Url = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png", Genre = "Musical", Director = "Director 2", ReleaseDate = "6 May 2018 (USA)", ShortDescription = "Short description", Stars = "Star 1, Star 2", Rating = 9.25, Audience = 3050, Time = "22:00" }
             );

            dbSetRatings = GetQueryableMockDbSet(
                new MovieRatings { id = 1, movieId= 1, rating = 8, clientId = 122 },
                new MovieRatings { id = 2, movieId = 1, rating = 9, clientId = 124 },
                new MovieRatings { id = 3, movieId = 2, rating = 6, clientId = 124 },
                new MovieRatings { id = 4, movieId = 3, rating = 10, clientId = 122 },
                new MovieRatings { id = 5, movieId = 3, rating = 9, clientId = 120 },
                new MovieRatings { id = 6, movieId = 3, rating = 10, clientId = 121 },
                new MovieRatings { id = 7, movieId = 3, rating = 8, clientId = 123 }
               );

            dbSetClients = GetQueryableMockDbSet(
               new Client { ClientId = 120, Email="user1@yahoo.com", movies = new List<Film>(), Name="User1", Points=10 },
               new Client { ClientId = 121, Email = "user2@yahoo.com", movies = new List<Film>(), Name = "User2", Points = 10 },
               new Client { ClientId = 122, Email = "user3@yahoo.com", movies = new List<Film>(), Name = "User3", Points = 20 },
               new Client { ClientId = 123, Email = "user4@yahoo.com", movies = new List<Film>(), Name = "User4", Points = 10 },
               new Client { ClientId = 124, Email = "user5@yahoo.com", movies = new List<Film>(), Name = "User5", Points = 20 }
              );

            dbSetArchivedFilms = GetQueryableMockDbSet(
               new ArchivedFilm { Id = 100, Audience = 12004, Director = "Director 1", Genre = "Drama", LastScreening = "04.09.2018", Name ="Adrift", ReleaseDate = "February 24, 2017 (United States)", Url = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png"},
               new ArchivedFilm { Id = 102, Audience = 10214, Director = "Director 4", Genre = "Horror", LastScreening = "14.09.2018", Name = "Horror Movie", ReleaseDate = "March 02, 2017 (United States)", Url = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png"}
               );
        }

        [TestMethod]
        public void getFilm()
        {
            mockSet.Setup(m => m.Find(It.IsAny<int>())).Returns(dbSetFilms.First());

            //Setting up the mockSet to mockContext
            mockContext.Setup(c => c.Films).Returns(mockSet.Object);

            var service = new FilmsController(mockContext.Object);
            //Check the equality between the returned data and the expected data
            var foundFilm = service.getFilm(2);
            Assert.AreEqual(foundFilm.Name, "Ime1");
            Assert.AreEqual(foundFilm.Audience, 1005);
            Assert.AreEqual(foundFilm.Rating, 8.5);
        }


        [TestMethod]
        public void addFilm()
        {
            Film newFilm = new Film(4, "Ime4", "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png", "Musical", "Director 2", "6 May 2018 (USA)", "Short description", "Star 1, Star 2", 0.0, 0, "22:00");
            mockSet.Setup(m => m.Add(It.IsAny<Film>())).Returns(newFilm);

            //Setting up the mockSet to mockContext
            mockContext.Setup(c => c.Films).Returns(mockSet.Object);

            var service = new FilmsController(mockContext.Object);
            //Check the equality between the returned data and the expected data
            var foundFilm = service.addFilm(newFilm);
            Assert.AreEqual(foundFilm.Name, "Ime4");
            Assert.AreEqual(foundFilm.Audience, 0);
            Assert.AreEqual(foundFilm.Rating, 0.0);
        }

        [TestMethod]
        public void getFilms()
        {            
            mockContext.Setup(c => c.Films).Returns(dbSetFilms);
            var service = new FilmsController(mockContext.Object);

            List<Film> allFilms = service.getAllFilms().ToList();
            Assert.AreEqual(allFilms[0].Name, "Ime1");
            Assert.AreEqual(allFilms[1].Name, "Ime2");
            Assert.AreEqual(allFilms[2].Name, "Ime3");
        }

        //db.Clients
        [TestMethod]
        public void getClients()
        {
            mockContext.Setup(c => c.Clients).Returns(dbSetClients);
            var service = new FilmsController(mockContext.Object);

            List<Client> allClients = service.getAllClients().ToList();
            Assert.AreEqual(allClients[0].Name, "User1");
            Assert.AreEqual(allClients[1].Name, "User2");
            Assert.AreEqual(allClients[2].Email, "user3@yahoo.com");
            Assert.AreEqual(allClients[3].ClientId, 123);
            Assert.AreEqual(allClients[4].Name, "User5");
        }

        //db.Clients.Find(id);
        [TestMethod]
        public void getClient()
        {
            mockSetClients.Setup(m => m.Find(It.IsAny<int>())).Returns(dbSetClients.First());

            //Setting up the mockSet to mockContext
            mockContext.Setup(c => c.Clients).Returns(mockSetClients.Object);

            var service = new FilmsController(mockContext.Object);
            //Check the equality between the returned data and the expected data
            var foundClient = service.getClient(120);
            Assert.AreEqual(foundClient.Name, "User1");
            Assert.AreEqual(foundClient.Email, "user1@yahoo.com");
            Assert.AreEqual(foundClient.Points, 10);
        }

        //db.ArchivedFilms.ToList()
        [TestMethod]
        public void getArchivedFilms()
        {
            mockContext.Setup(c => c.ArchivedFilms).Returns(dbSetArchivedFilms);
            var service = new FilmsController(mockContext.Object);

            List<ArchivedFilm> allArchivedFilms = service.getAllArchivedFilms().ToList();
            Assert.AreEqual(allArchivedFilms[0].Genre, "Drama");
            Assert.AreEqual(allArchivedFilms[1].LastScreening, "14.09.2018");
            Assert.AreEqual(allArchivedFilms[0].Name, "Adrift");
            Assert.AreEqual(allArchivedFilms[1].Id, 102);
            Assert.AreEqual(allArchivedFilms[1].ReleaseDate, "March 02, 2017 (United States)");
        }

        // db.Tickets.Add(ticket);
        [TestMethod]
        public void addTicket()
        {
            Ticket newTicket = new Ticket {ClientId=120, Date= "05-09-2018", Id=1, Movie="Ime1",NumberTickets=2,Time="16:00" };
            mockSetTickets.Setup(m => m.Add(It.IsAny<Ticket>())).Returns(newTicket);

            //Setting up the mockSet to mockContext
            mockContext.Setup(c => c.Tickets).Returns(mockSetTickets.Object);

            var service = new FilmsController(mockContext.Object);
            //Check the equality between the returned data and the expected data
            var ticket = service.addTicket(newTicket);
            Assert.AreEqual(ticket.Movie, "Ime1");
            Assert.AreEqual(ticket.Date, "05-09-2018");
            Assert.AreEqual(ticket.NumberTickets, 2);
        }

        //db.MovieRatings.Add(rating);
        [TestMethod]
        public void addRating()
        {
            MovieRatings newRating = new MovieRatings {id=8, clientId=121, movieId=2, rating=10};
            mockSetRatings.Setup(m => m.Add(It.IsAny<MovieRatings>())).Returns(newRating);

            //Setting up the mockSet to mockContext
            mockContext.Setup(c => c.MovieRatings).Returns(mockSetRatings.Object);

            var service = new FilmsController(mockContext.Object);
            //Check the equality between the returned data and the expected data
            var rating = service.addRating(newRating);
            Assert.AreEqual(rating.rating, 10);
            Assert.AreEqual(rating.clientId, 121);
            Assert.AreEqual(rating.movieId, 2);
        }

        //Can't mock function where()...........
        // db.Clients.Where(d => d.Name == User.Identity.Name).First();
        //db.MovieRatings.Where(d => d.movieId.Equals(film.Id));
        /*[TestMethod]
        public void getRatings()
        {
            IQueryable<MovieRatings> queryableRatings = dbSetRatings.AsQueryable().Where(m=>m.movieId==3);
           mockSetRatings.Setup(m => m.Where(It.IsAny<Expression<System.Func<MovieRatings,bool>>>())).Returns(queryableRatings);
            mockContext.Setup(c => c.MovieRatings).Returns(mockSetRatings.Object);


            var service = new FilmsController(mockContext.Object);
            var rating = service.getRatingsForMovieId(3).Select(m=>m.rating).Average();
            Assert.AreEqual(rating, dbSetFilms.ToList()[2].Rating);
        }*/
    }
}
