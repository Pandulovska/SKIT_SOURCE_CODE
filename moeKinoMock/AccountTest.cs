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
    public class AccountTest
    {
        public Mock<ApplicationDbContext> mockContext;
        public Mock<DbSet<Client>> mockSetClients;
        public DbSet<Client> dbSetClients;

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

            //Define the mock Repository as ApplicationDbContext
            mockContext = new Mock<ApplicationDbContext>();

            dbSetClients = GetQueryableMockDbSet(
               new Client { ClientId = 120, Email = "user1@yahoo.com", movies = new List<Film>(), Name = "User1", Points = 10 },
               new Client { ClientId = 121, Email = "user2@yahoo.com", movies = new List<Film>(), Name = "User2", Points = 10 },
               new Client { ClientId = 122, Email = "user3@yahoo.com", movies = new List<Film>(), Name = "User3", Points = 20 },
               new Client { ClientId = 123, Email = "user4@yahoo.com", movies = new List<Film>(), Name = "User4", Points = 10 },
               new Client { ClientId = 124, Email = "user5@yahoo.com", movies = new List<Film>(), Name = "User5", Points = 20 }
              );
        }

        // db.Clients.Add(client);
        [TestMethod]
        public void addClient()
        {
            Client newClient = new Client { ClientId = 125, Email = "user6@yahoo.com", movies = new List<Film>(), Name = "User6", Points = 0 };
            mockSetClients.Setup(m => m.Add(It.IsAny<Client>())).Returns(newClient);

            //Setting up the mockSet to mockContext
            mockContext.Setup(c => c.Clients).Returns(mockSetClients.Object);

            var service = new AccountController(mockContext.Object);
            //Check the equality between the returned data and the expected data
            var client = service.addClient(newClient);
            Assert.AreEqual(client.Email, "user6@yahoo.com");
            Assert.AreEqual(client.Name, "User6");
            Assert.AreEqual(client.ClientId, 125);
        }
    }
}
