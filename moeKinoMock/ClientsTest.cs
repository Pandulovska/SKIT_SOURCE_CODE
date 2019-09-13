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
    public class ClientsTest
    {
        public Mock<ApplicationDbContext> mockContext;
        public Mock<DbSet<Client>> mockSetClients;
        public Mock<DbSet<Ticket>> mockSetTickets;

        public DbSet<Client> dbSetClients;
        public DbSet<Ticket> dbSetTickets;

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
            mockSetClients = new Mock<DbSet<Client>>();
            mockSetTickets = new Mock<DbSet<Ticket>>();

            //Define the mock Repository as ApplicationDbContext
            mockContext = new Mock<ApplicationDbContext>();

            dbSetClients = GetQueryableMockDbSet(
               new Client { ClientId = 120, Email = "user1@yahoo.com", movies = new List<Film>(), Name = "User1", Points = 10 },
               new Client { ClientId = 121, Email = "user2@yahoo.com", movies = new List<Film>(), Name = "User2", Points = 10 },
               new Client { ClientId = 122, Email = "user3@yahoo.com", movies = new List<Film>(), Name = "User3", Points = 20 },
               new Client { ClientId = 123, Email = "user4@yahoo.com", movies = new List<Film>(), Name = "User4", Points = 10 },
               new Client { ClientId = 124, Email = "user5@yahoo.com", movies = new List<Film>(), Name = "User5", Points = 20 }
              );

            dbSetTickets = GetQueryableMockDbSet(
                 new Ticket { ClientId = 120, Date = "05-09-2018", Id = 1, Movie = "Ime1", NumberTickets = 2, Time = "16:00" },
                 new Ticket { ClientId = 120, Date = "17-09-2018", Id = 3, Movie = "Ime3", NumberTickets = 1, Time = "20:00" },
                 new Ticket { ClientId = 122, Date = "06-09-2018", Id = 3, Movie = "Ime3", NumberTickets = 1, Time = "20:00" },
                 new Ticket { ClientId = 123, Date = "05-09-2018", Id = 2, Movie = "Ime2", NumberTickets = 5, Time = "18:00" },
                 new Ticket { ClientId = 123, Date = "07-09-2018", Id = 3, Movie = "Ime3", NumberTickets = 2, Time = "20:00" }
             );
        }

        //db.Clients
        [TestMethod]
        public void getClients()
        {
            mockContext.Setup(c => c.Clients).Returns(dbSetClients);
            var service = new ClientsController(mockContext.Object);

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

        // db.Clients.Add(client);
        [TestMethod]
        public void addClient()
        {
            Client newClient = new Client { ClientId = 125, Email = "user6@yahoo.com", movies = new List<Film>(), Name = "User6", Points = 0 };
            mockSetClients.Setup(m => m.Add(It.IsAny<Client>())).Returns(newClient);

            //Setting up the mockSet to mockContext
            mockContext.Setup(c => c.Clients).Returns(mockSetClients.Object);

            var service = new ClientsController(mockContext.Object);
            //Check the equality between the returned data and the expected data
            var client = service.addClient(newClient);
            Assert.AreEqual(client.Email, "user6@yahoo.com");
            Assert.AreEqual(client.Name, "User6");
            Assert.AreEqual(client.ClientId, 125);
        }

        [TestMethod]
        public void getTickets()
        {
            mockContext.Setup(c => c.Tickets).Returns(dbSetTickets);
            var service = new ClientsController(mockContext.Object);

            List<Ticket> allTickets = service.getAllTickets().ToList();
            Assert.AreEqual(allTickets[2].ClientId, 122);
            Assert.AreEqual(allTickets[3].Movie, "Ime2");
            Assert.AreEqual(allTickets[4].NumberTickets, 2);
            Assert.AreEqual(allTickets[0].Time, "16:00");
            Assert.AreEqual(allTickets[1].Id, 3);
        }

    }
}
