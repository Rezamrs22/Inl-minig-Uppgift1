using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeService;
using System;

namespace UserRegistrationTests
{
    [TestClass]
    public class UserRegistrationServiceTests
    {
        private UserRegistrationService _service;

        [TestInitialize]
        public void Setup()
        {
            _service = new UserRegistrationService();
        }

        [TestMethod]
        public void RegisterUser_ShouldSucceed_WhenDataIsValid()
        {
            // Arrange
            var user = new User { Username = "RezaSafdari", Email = "reza@reza.se", Password = "Asdf123!" };

            // Act
            var result = _service.Register(user);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("RezaSafdari", result.Username);
            Assert.AreEqual("Registration successful", result.Message);
        }

        [TestMethod]
        public void RegisterUser_ShouldFail_WhenUsernameIsTooShort()
        {
            var user = new User { Username = "Ali", Email = "reza@reza.se", Password = "Asdf123!" };
            var result = _service.Register(user);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Username must be between 5 and 20 alphanumeric characters", result.Message);
        }

        [TestMethod]
        public void RegisterUser_ShouldFail_WhenPasswordIsWeak()
        {
            var user = new User { Username = "RezaSafdari", Email = "reza@reza.se", Password = "password" };
            var result = _service.Register(user);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Password must be at least 8 characters and include a special character", result.Message);
        }

        [TestMethod]
        public void RegisterUser_ShouldFail_WhenEmailIsInvalid()
        {
            var user = new User { Username = "AlexPersson", Email = "alexalex.com", Password = "Asdf123!" };
            var result = _service.Register(user);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Invalid email format", result.Message);
        }

        [TestMethod]
        public void RegisterUser_ShouldFail_WhenEmailDoesNotEndWithSe()
        {
            var user = new User { Username = "AlexPersson", Email = "alex@alex.com", Password = "Asdf123!" };
            var result = _service.Register(user);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Email must end with .se", result.Message);
        }

        [TestMethod]
        public void RegisterUser_ShouldSucceed_WhenEmailEndsWithSe()
        {
            var user = new User { Username = "AlexPersson", Email = "alex@alex.se", Password = "Asdf123!" };
            var result = _service.Register(user);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Registration successful", result.Message);
        }

        [TestMethod]
        public void RegisterUser_ShouldFail_WhenUsernameIsNotUnique()
        {
            var user1 = new User { Username = "RezaSafdari", Email = "reza@reza.se", Password = "Asdf123!" };
            var user2 = new User { Username = "RezaSafdari", Email = "reza@reza.se", Password = "Asdf123!" };

            _service.Register(user1);
            var result = _service.Register(user2);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Username already taken", result.Message);
        }
    }
}
