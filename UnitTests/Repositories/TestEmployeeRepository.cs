using Moq;
using Microsoft.EntityFrameworkCore;
using TpControlWork.DataAccess.Entities;
using TpControlWork.DataAccess.Interfaces;
using TpControlWork.DataAccess.Implementations;
using TpControlWork.DataAccess;

namespace TpControlWork.UnitTests.Repositories
{
    [TestFixture]
    public class EmployeeRepositoryTests
    {
        private Mock<ApplicationDbContext> _dbContextMock;
        private IEmployeeRepository _employeeRepository;

        [SetUp]
        public void SetUp()
        {
            _dbContextMock = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            _employeeRepository = new EmployeeRepository(_dbContextMock.Object);
        }

        [Test]
        public async Task CreateAsync_ShouldAddEntityToDbContext()
        {
            // Arrange
            var newEmployee = new Employee();

            // Act
            await _employeeRepository.CreateAsync(newEmployee);

            // Assert
            _dbContextMock.Verify(db => db.AddAsync(newEmployee, default), Times.Once);
            _dbContextMock.Verify(db => db.SaveChangesAsync(default), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_ShouldRemoveEntityFromDbContext()
        {
            // Arrange
            var deletingEmployee = new Employee();

            // Act
            await _employeeRepository.DeleteAsync(deletingEmployee);

            // Assert
            _dbContextMock.Verify(db => db.Remove(deletingEmployee), Times.Once);
            _dbContextMock.Verify(db => db.SaveChangesAsync(default), Times.Once);
        }
    }
}
