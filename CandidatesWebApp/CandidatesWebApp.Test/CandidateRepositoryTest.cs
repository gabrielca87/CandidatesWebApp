using CandidatesWebApp.Data;
using CandidatesWebApp.Models;
using CandidatesWebApp.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace CandidatesWebApp.Test
{
    [TestClass]
    public class CandidateRepositoryTest
    {
        private List<Candidate> _candidate = new List<Candidate>
        {
            new Candidate() { Id = 1, FirstName = "Gabriel", LastName = "Coca", EmailAddress = "gabrielcoca87@gmail.com", PhoneNumber = "111-111-1111", ResidentialZipCode = "12345" },
            new Candidate() { Id = 2, FirstName = "John", LastName = "Smith", EmailAddress = "john.smith@gmail.com", PhoneNumber = "222-222-2222", ResidentialZipCode = "2222" }
        };

        [TestMethod]
        public async Task GetAllAsync_ReturnsCandidates()
        {
            //Arrange
            var data = _candidate.AsQueryable();

            var mockSet = new Mock<DbSet<Candidate>>();
            mockSet.As<IDbAsyncEnumerable<Candidate>>().Setup(x => x.GetAsyncEnumerator()).Returns(new TestDbAsyncEnumerator<Candidate>(data.GetEnumerator()));
            mockSet.As<IQueryable<Candidate>>().Setup(m => m.Provider).Returns(new TestDbAsyncQueryProvider<Candidate>(data.Provider));
            mockSet.As<IQueryable<Candidate>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Candidate>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Candidate>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<CandidatesWebAppDbContext>();
            mockContext.Setup(s => s.Candidates).Returns(mockSet.Object);

            var repository = new CandidateRepository(mockContext.Object);

            //Act
            var result = await repository.GetAllAsync();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("Gabriel", result.FirstOrDefault().FirstName);
            Assert.AreEqual("Smith", result.LastOrDefault().LastName);

            mockSet.Verify();
        }

        [TestMethod]
        public async Task GetAsync_CandidateId_ReturnsCandidate()
        {
            //Arrange
            var data = _candidate.AsQueryable();
            int id = data.FirstOrDefault().Id.Value;

            var mockSet = new Mock<DbSet<Candidate>>();
            mockSet.As<IDbAsyncEnumerable<Candidate>>().Setup(x => x.GetAsyncEnumerator()).Returns(new TestDbAsyncEnumerator<Candidate>(data.GetEnumerator()));
            mockSet.As<IQueryable<Candidate>>().Setup(m => m.Provider).Returns(new TestDbAsyncQueryProvider<Candidate>(data.Provider));
            mockSet.As<IQueryable<Candidate>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Candidate>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Candidate>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<CandidatesWebAppDbContext>();
            mockContext.Setup(s => s.Candidates).Returns(mockSet.Object);

            var repository = new CandidateRepository(mockContext.Object);

            //Act
            var result = await repository.GetAsync(id);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Gabriel", result.FirstName);

            mockSet.Verify();
        }

        [TestMethod]
        public async Task FindAsync_SearchCriteriaValues_ReturnsCandidates()
        {
            //Arrange
            var data = _candidate.AsQueryable();
            string firstName = "Gabriel";
            string lastName = "Smith";

            var mockSet = new Mock<DbSet<Candidate>>();
            mockSet.As<IDbAsyncEnumerable<Candidate>>().Setup(x => x.GetAsyncEnumerator()).Returns(new TestDbAsyncEnumerator<Candidate>(data.GetEnumerator()));
            mockSet.As<IQueryable<Candidate>>().Setup(m => m.Provider).Returns(new TestDbAsyncQueryProvider<Candidate>(data.Provider));
            mockSet.As<IQueryable<Candidate>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Candidate>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Candidate>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<CandidatesWebAppDbContext>();
            mockContext.Setup(s => s.Candidates).Returns(mockSet.Object);

            var repository = new CandidateRepository(mockContext.Object);

            //Act
            var result = await repository.FindAsync(firstName, lastName, null, null, null);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("Gabriel", result.FirstOrDefault()?.FirstName);
            Assert.AreEqual("Smith", result.LastOrDefault()?.LastName);

            mockSet.Verify();
        }

        [TestMethod]
        public async Task ExistsAsync_ReturnsBoolean()
        {
            //Arrange
            var data = _candidate.AsQueryable();
            int candidate = _candidate.FirstOrDefault().Id.Value;

            var mockSet = new Mock<DbSet<Candidate>>();
            mockSet.As<IDbAsyncEnumerable<Candidate>>().Setup(x => x.GetAsyncEnumerator()).Returns(new TestDbAsyncEnumerator<Candidate>(data.GetEnumerator()));
            mockSet.As<IQueryable<Candidate>>().Setup(m => m.Provider).Returns(new TestDbAsyncQueryProvider<Candidate>(data.Provider));
            mockSet.As<IQueryable<Candidate>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Candidate>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Candidate>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<CandidatesWebAppDbContext>();
            mockContext.Setup(s => s.Candidates).Returns(mockSet.Object);

            var repository = new CandidateRepository(mockContext.Object);

            //Act
            var result = await repository.ExistsAsync(candidate);

            //Assert
            Assert.AreEqual(true, result);

            mockSet.Verify();
        }
    }
}
