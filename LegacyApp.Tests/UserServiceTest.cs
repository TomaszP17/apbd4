using System;
using JetBrains.Annotations;
using Xunit;

namespace LegacyApp.Tests;

[TestSubject(typeof(UserService))]
public class UserServiceTest
{

    [Fact]
    public void AddUser_Should_Return_False_When_FirstName_Is_Missing()
    {
        // Arrange - Przygotowuejmy zaleznosci do sprawdzenia
        var userService = new UserService();
        // Act - Odpowiada za uruchamienie testowanie funkcyjonalnosci
        var addResult = userService.AddUser("", "Doe", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 1);
        // Assert - Sprawdzenie wyniku
        Assert.False(addResult);
    }
    [Fact]
    public void AddUser_Should_Return_False_When_LastName_Is_Missing()
    {
        // Arrange - Przygotowuejmy zaleznosci do sprawdzenia
        var userService = new UserService();
        // Act - Odpowiada za uruchamienie testowanie funkcyjonalnosci
        var addResult = userService.AddUser("John", "", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 1);
        // Assert - Sprawdzenie wyniku
        Assert.False(addResult);
    }
    [Fact]
    public void AddUser_Should_Return_False_When_Email_Is_Incorrect()
    {
        // Arrange - Przygotowuejmy zaleznosci do sprawdzenia
        var userService = new UserService();
        // Act - Odpowiada za uruchamienie testowanie funkcyjonalnosci
        var addResult = userService.AddUser("John", "Doe", "johndoegmailcom", DateTime.Parse("1982-03-21"), 1);
        // Assert - Sprawdzenie wyniku
        Assert.False(addResult);
    }
    [Fact]
    public void AddUser_Should_Throw_ArgumentException_When_User_Doesnt_Exist()
    {
        // Arrange - Przygotowuejmy zaleznosci do sprawdzenia
        var userService = new UserService();
        // Act - Odpowiada za uruchamienie testowanie funkcyjonalnosci
        // Assert - Sprawdzenie wyniku
        Assert.Throws<ArgumentException>(() =>
        {
            var addResult = userService.AddUser("John", "Doe", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 10);
        });
    }

    [Fact]
    public void AddUser_Should_Returns_False_When_User_Is_Under_21()
    {
        // Arrange - Przygotowuejmy zaleznosci do sprawdzenia
        var userService = new UserService();
        // Act - Odpowiada za uruchamienie testowanie funkcyjonalnosci
        var addResult = userService.AddUser("John", "Doe", "johndoegmailcom", DateTime.Parse("2021-03-21"), 1);
        // Assert - Sprawdzenie wyniku
        Assert.False(addResult);
    }
    [Fact]
    public void AddUser_Should_Returns_True_When_Client_Is_Very_Important()
    {
        // Arrange - Przygotowuejmy zaleznosci do sprawdzenia
        var userService = new UserService();
        // Act - Odpowiada za uruchamienie testowanie funkcyjonalnosci
        var addResult = userService.AddUser("Marcin", "Malewski", "malewski@gmail.pl", DateTime.Parse("1982-03-21"), 2);
        // Assert - Sprawdzenie wyniku
        Assert.True(addResult);
    }
    [Fact]
    public void AddUser_Should_Returns_True_When_Client_Is_Important()
    {
        // Arrange - Przygotowuejmy zaleznosci do sprawdzenia
        var userService = new UserService();
        // Act - Odpowiada za uruchamienie testowanie funkcyjonalnosci
        var addResult = userService.AddUser("Marcin", "Malewski", "malewski@gmail.pl", DateTime.Parse("1982-03-21"), 3);
        // Assert - Sprawdzenie wyniku
        Assert.True(addResult);
    }
    [Fact]
    public void AddUser_Should_Returns_False_When_Client_Has_Too_Lower_Credit_Limit()
    {
        // Arrange - Przygotowuejmy zaleznosci do sprawdzenia
        var userService = new UserService();
        // Act - Odpowiada za uruchamienie testowanie funkcyjonalnosci
        var addResult = userService.AddUser("Marcin", "Kowalski", "kowalski@wp.pl", DateTime.Parse("1982-03-21"), 1);
        // Assert - Sprawdzenie wyniku
        Assert.False(addResult);
    }
    
}