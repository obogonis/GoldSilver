﻿using GoldSilver.WebUI.Controllers;
using GoldSilver.WebUI.Infrastructure.Abstract;
using GoldSilver.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GoldSilver.Tests
{
    [TestClass]
    public class AdminSecurityTests
    {
        [TestMethod]
        public void Can_Login_With_Valid_Credentials()
        {
            // Arrange - create a mock authentication provider
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("admin", "secret")).Returns(true);

            // Arrange - create the view model
            LoginViewModel model = new LoginViewModel
            {
                UserName = "admin",
                Password = "p987654321"
            };

            // Arrange - create the controller
            AccountController target = new AccountController(mock.Object);

            // Act - authenticate using valid credentials
            ActionResult result = target.Login(model, "/MyURL");

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual("/MyURL", ((RedirectResult)result).Url);
        }

        [TestMethod]
        public void Cannot_Login_With_Invalid_Credentials()
        {
            // Arrange - create a mock authentication provider
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("badUser", "badPass")).Returns(false);

            // Arrange - create the view model
            LoginViewModel model = new LoginViewModel
            {
                UserName = "badUser",
                Password = "badPass"
            };

            // Arrange - create the controller
            AccountController target = new AccountController(mock.Object);

            // Act - authenticate using valid credentials
            ActionResult result = target.Login(model, "/MyURL");

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
        }
    }
}
