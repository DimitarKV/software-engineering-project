using HotelReservations.Data.Entities;

namespace HotelReservations.Tests;

public class UserEntityTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void FullNameTest_WithCorrectInformationExpected()
    {
        // Arrange
        User user = new User
        {
            FirstName = "Todor",
            LastName = "Todorov"
        };

        string expectedFullName = "Todor Todorov";

        // Act
        string actualFullName = user.FullName;

        // Assert
        Assert.AreEqual(expectedFullName, actualFullName);
    }
    
    [Test]
    public void FullNameTest_WithWrongInformationExpected()
    {
        // Arrange
        User user = new User
        {
            FirstName = "Todor",
            LastName = "Todorov"
        };

        string expectedFullName = "Todorov Todor";

        // Act
        string actualFullName = user.FullName;

        // Assert
        Assert.AreNotEqual(expectedFullName, actualFullName);
    }
}