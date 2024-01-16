using Microsoft.AspNetCore.Mvc;
using Moq;
using TpControlWork.Controllers;
using TpControlWork.Domain.Enums;
using TpControlWork.Domain.Models;
using TpControlWork.Enums;
using TpControlWork.Services.Interfaces;
using TpControlWork.ViewModels;

namespace TpControlWork.Tests.Controllers
{
    [TestFixture]
    public class EmployeeControllerTests
    {
        private Mock<IEmployeeService> _employeeServiceMock;
        private EmployeeController _employeeController;

        [SetUp]
        public void Setup()
        {
            _employeeServiceMock = new Mock<IEmployeeService>();
            _employeeController = new EmployeeController(_employeeServiceMock.Object);
        }

        [Test]
        public async Task CreateEmployee_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var employeeRequest = new EmployeeRequestModel
            {
                Name = "John Doe",
                EmployeeType = EEmployeeType.FullTime,
                PaymentType = new PaymentTypeRequestModel { PaymentType = EPaymentType.Hourly, HourlyRate = 20, HoursWorked = 40 },
                Earnings = new List<EarningRequestModel>
                {
                    new EarningRequestModel { Type = EEarningType.Overtime, OvertimeHours = 5, OvertimeRate = 30 }
                }
            };

            // Act
            var result = await _employeeController.CreateEmployee(employeeRequest) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Value, Is.EqualTo("Employee created successfully"));
        }

        [Test]
        public async Task DeleteEmployee_ValidEmployeeId_ReturnsNoContent()
        {
            // Arrange
            var employeeId = 1;

            // Act
            var result = await _employeeController.DeleteEmployee(employeeId) as NoContentResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(204));
        }
    }
}
