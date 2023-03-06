using System;
using System.Threading.Tasks;
using API.Controllers;
using API.Model;
using AutoMapper;
using DTO.ViewModel.Hotel;
using Logger.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Repository.Interfaces.Unit;
using Service.Interfaces;
using Service.Interfaces.Unit;
using Service.Models;

namespace API.Tests.Controllers
{
    [TestFixture]
    public class HotelControllerTests : ServiceResult
    {
        private Mock<IServiceUnit> _serviceMock;
        private HotelController _controller;
        // create mock objects for the dependencies
        private Mock<IRepositoryUnit> _repositoryMock;
        private Mock<IEventLogger> _loggerMock;
        private Mock<IMapper> _mapperMock;
        
        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IServiceUnit>();
            _controller = new HotelController(_serviceMock.Object);
            _repositoryMock = new Mock<IRepositoryUnit>();
            _loggerMock = new Mock<IEventLogger>();
            _mapperMock = new Mock<IMapper>();
        }

        [Test]
        public async Task GetHotelData_ValidInputs_ReturnsOk()
        {
            // Arrange
            HotelDataRequest request = new HotelDataRequest { HotelId = 7294, TargetDate = "2016-03-15T00:00:00.000+01:00" };
            var hotelId = 7294;
            var targetDate = DateTime.Parse(request.TargetDate);
            string message = "Data successfully get";
            var hotelService = new Service.Implementations.HotelService(_repositoryMock.Object, _loggerMock.Object, _mapperMock.Object);

            // Act
            var result = await hotelService.GetHotelData(hotelId, targetDate);

            // Assert
            Assert.AreEqual(message, result.Message);
            Assert.NotNull(result.Data);
        }

        [Test]
        public async Task GetHotelData_InValidtargetDateInput_ReturnsNoHotelRates()
        {
            // Arrange
            var hotelId = 7294;
            var targetDate = DateTime.Now;
            string message = "Data successfully get";
            var hotelService = new Service.Implementations.HotelService(_repositoryMock.Object, _loggerMock.Object, _mapperMock.Object);

            // Act
            var result = await hotelService.GetHotelData(hotelId, targetDate);

            // Assert
            Assert.AreEqual(message, result.Message);
            Assert.Zero(result.Data.hotelRates.Count);
        }

        [Test]
        public async Task GetHotelData_InValidInputs_ReturnsNothing()
        {
            // Arrange
            var hotelId = 1;
            var targetDate = DateTime.Now;
            string message = "Result not found";
            var hotelService = new Service.Implementations.HotelService(_repositoryMock.Object, _loggerMock.Object, _mapperMock.Object);

            // Act
            var result = await hotelService.GetHotelData(hotelId, targetDate);

            // Assert
            Assert.AreEqual(message, result.Message);
            Assert.IsNull(result.Data);
        }

        [Test]
        public async Task GetHotelData_ServiceThrowsException_ReturnsBadRequest()
        {
            // Arrange
            HotelDataRequest request = new HotelDataRequest { HotelId = 1, TargetDate = DateTime.Now.ToString() };
            var hotelId = 1;
            DateTime targetDate = DateTime.Now;
            var errorMessage = "Unhandled error";
            var expectedResult = new ServiceResult<Hotels> { Data = new Hotels() };
            //_serviceMock.Setup(x => x.Hotel.GetHotelData(hotelId, targetDate)).Throws(new Exception(errorMessage));
            
            // Act
            var result = await _controller.GetHotelData(request);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
            var serviceResult = (ServiceResult)((BadRequestObjectResult)result.Result).Value;
            Assert.IsNotNull(serviceResult);
            Assert.IsNotNull(serviceResult.Error);
            Assert.IsFalse(serviceResult.IsSuccess);
            Assert.AreEqual(errorMessage, serviceResult.Error.ErrorMessage);
        }

        [Test]
        public async Task GetHotelData_WithoutRequiredHotelId_ControllerReturnsBadRequestWithRequiredValue()
        {
            // Arrange
            HotelDataRequest request = new HotelDataRequest { HotelId = -1, TargetDate = DateTime.Now.ToString() };
            var hotelId = -1;
            var targetDate = DateTime.Now;
            var errorMessage = "HotelId required";
            _serviceMock.Setup(x => x.Hotel.GetHotelData(hotelId, targetDate)).Throws(new Exception(errorMessage));

            // Act
            var result = await _controller.GetHotelData(request);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
            var badRequestResult = (BadRequestObjectResult)result.Result;
            var serviceResult = (ServiceResult<object>)badRequestResult.Value;
            Assert.IsFalse(serviceResult.IsSuccess);
            Assert.AreEqual(errorMessage, serviceResult.Message);
        }

        [Test]
        public async Task GetHotelData_WithoutRequiredtargetDate_ControllerReturnsBadRequestWithRequiredValue()
        {
            // Arrange
            HotelDataRequest request = new HotelDataRequest { HotelId = 1, TargetDate = ""};
            var errorMessage = "arrivalDate required";

            // Act
            var result = await _controller.GetHotelData(request);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
            var badRequestResult = (BadRequestObjectResult)result.Result;
            var serviceResult = (ServiceResult<object>)badRequestResult.Value;
            Assert.IsFalse(serviceResult.IsSuccess);
            Assert.AreEqual(errorMessage, serviceResult.Message);
        }
    }
}
