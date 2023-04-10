using AutoMapper;
using HotelReservations.Controllers;
using HotelReservations.Data.Persistence;
using HotelReservations.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SignInResult = Microsoft.AspNetCore.Mvc.SignInResult;

namespace HotelReservations.Tests;

public class ManagerControllerTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Privacy_ReturnsPrivacyView()
    {
        // Arrange
        var mockContext = new Mock<HotelDbContext>();
        var mockMapper = new Mock<IMapper>();
        var controller = new HomeController(mockContext.Object, mockMapper.Object);

        // Act
        var result = controller.Privacy() as ViewResult;

        // Assert
        Assert.IsNotNull(result);
    }
}