using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TpControlWork.Domain.Enums;
using TpControlWork.Domain.Models;
using TpControlWork.Services.Interfaces;
using TpControlWork.Services.Implementations.CalculateStrategies;
using TpControlWork.Domain.Models.Earnings;
using TpControlWork.Domain.Models.PaymentTypes;
using TpControlWork.Services.Implementations;

namespace TpControlWork.UnitTests.Services
{
    [TestFixture]
    public class StatisticsCalculatorServiceTests
    {
        private List<Employee> GetSampleEmployees()
        {
            return new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Name = "John Doe",
                    EmployeeType = EEmployeeType.FullTime,
                    PaymentType = new SalaryPayment { MonthlySalary = 5000 },
                    Earnings = new List<Earning> { new BonusEarnings { BonusAmount = 1000 } }
                },
                new Employee
                {
                    Id = 2,
                    Name = "Jane Smith",
                    EmployeeType = EEmployeeType.PartTime,
                    PaymentType = new HourlyPayment { HourlyRate = 20, HoursWorked = 25 },
                    Earnings = new List<Earning> { new OvertimeEarnings { OvertimeRate = 30, OvertimeHours = 5 } }
                },
            };
        }

        [Test]
        public async Task CalculateByStrategyAsync_AverageStrategy_ReturnsCorrectResult()
        {
            // Arrange
            var employeeServiceMock = new Mock<IEmployeeService>();
            var employees = GetSampleEmployees();
            employeeServiceMock.Setup(service => service.GetAllAsync()).ReturnsAsync(employees);

            var calculatorService = new StatisticsCalculatorService(employeeServiceMock.Object);
            calculatorService.Strategy = new CalculateAverageStrategy();

            // Act
            var result = await calculatorService.CalculateByStrategyAsync(null);

            // Assert
            Assert.That(result, Is.EqualTo(employees.Average(e => e.Salary)));
        }

        [Test]
        public void CalculateByStrategy_AverageStrategy_ReturnsCorrectResult()
        {
            // Arrange
            var employees = GetSampleEmployees();
            var calculatorService = new StatisticsCalculatorService(Mock.Of<IEmployeeService>());
            calculatorService.Strategy = new CalculateAverageStrategy();

            // Act
            var result = calculatorService.CalculateByStrategy(employees);

            // Assert
            Assert.That(result, Is.EqualTo(employees.Average(e => e.Salary)));
        }

        [Test]
        public async Task CalculateByStrategyAsync_MedianStrategy_ReturnsCorrectResult()
        {
            // Arrange
            var employeeServiceMock = new Mock<IEmployeeService>();
            var employees = GetSampleEmployees();
            employeeServiceMock.Setup(service => service.GetAllAsync()).ReturnsAsync(employees);

            var calculatorService = new StatisticsCalculatorService(employeeServiceMock.Object);
            calculatorService.Strategy = new CalculateMedianStrategy();

            // Act
            var result = await calculatorService.CalculateByStrategyAsync(null);

            // Assert
            Assert.That(result, Is.EqualTo(employees.Select(e => e.Salary).Average()));
        }

        [Test]
        public void CalculateByStrategy_MedianStrategy_ReturnsCorrectResult()
        {
            // Arrange
            var employees = GetSampleEmployees();
            var calculatorService = new StatisticsCalculatorService(Mock.Of<IEmployeeService>());
            calculatorService.Strategy = new CalculateMedianStrategy();

            // Act
            var result = calculatorService.CalculateByStrategy(employees);

            // Assert
            Assert.That(result, Is.EqualTo(employees.Select(e => e.Salary).Average()));
        }

        [Test]
        public void CalculateByStrategy_MinStrategy_ReturnsCorrectResult()
        {
            // Arrange
            var employees = GetSampleEmployees();
            var calculatorService = new StatisticsCalculatorService(Mock.Of<IEmployeeService>());
            calculatorService.Strategy = new CalculateMinStrategy();

            // Act
            var result = calculatorService.CalculateByStrategy(employees);

            // Assert
            Assert.That(result, Is.EqualTo(employees.Min(e => e.Salary)));
        }

        [Test]
        public void CalculateByStrategy_SumStrategy_ReturnsCorrectResult()
        {
            // Arrange
            var employees = GetSampleEmployees();
            var calculatorService = new StatisticsCalculatorService(Mock.Of<IEmployeeService>());
            calculatorService.Strategy = new CalculateSumStrategy();

            // Act
            var result = calculatorService.CalculateByStrategy(employees);

            // Assert
            Assert.That(result, Is.EqualTo(employees.Sum(e => e.Salary)));
        }

        [Test]
        public void CalculateByStrategy_UnsetStrategy_ThrowsInvalidOperationException()
        {
            // Arrange
            var employees = GetSampleEmployees();
            var calculatorService = new StatisticsCalculatorService(Mock.Of<IEmployeeService>());

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => calculatorService.CalculateByStrategy(employees));
        }

        [Test]
        public async Task CalculateByStrategyAsync_UnsetStrategy_ThrowsInvalidOperationException()
        {
            // Arrange
            var employeeServiceMock = new Mock<IEmployeeService>();
            var employees = GetSampleEmployees();
            employeeServiceMock.Setup(service => service.GetAllAsync()).ReturnsAsync(employees);

            var calculatorService = new StatisticsCalculatorService(employeeServiceMock.Object);

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () => await calculatorService.CalculateByStrategyAsync(null));
        }
    }
}
