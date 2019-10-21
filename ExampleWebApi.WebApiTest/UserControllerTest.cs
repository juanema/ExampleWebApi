using ExampleWebApi.Controllers;
using ExampleWebApi.Domain.Models;
using ExampleWebApi.Domain.Models.Validations;
using ExampleWebApi.Domain.Services;
using Microsoft.Extensions.Localization;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ExampleWebApi.WebApiTest
{
    public class UserControllerTest
    {

        readonly UserController _controller;
        readonly IUserService _service;

        public UserControllerTest()
        {
            var mock = new Mock<IStringLocalizer<UserController>>();
            _service = new UserServiceFake();
            _controller = new UserController(_service, mock.Object);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.Get();

            // Assert
            Assert.IsType<List<User>>(okResult.Result);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _controller.Get();

            // Assert
            var items = Assert.IsType<List<User>>(okResult.Result);
            Assert.Equal(3, items.Count);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOneItem()
        {
            // Act
            var okResult = _controller.Get(1);

            // Assert
            var user = okResult.Result;
            Assert.IsType<User>(user);
            Assert.Equal(1, user.Id);
        }

        [Fact]
        public void Get_WhenCalled_NotFoundItem()
        {
            // Assert

            var okResult = _controller.Get(88);

            var status = okResult.Status;

            Assert.Equal(TaskStatus.Faulted, status);

        }

        [Fact]
        public void Delete_WhenCalled_DeleteTheItem()
        {
            // Act
            var okResult = _controller.Delete(3);
            var response = okResult.Result;

            // Assert
            Assert.True(response.Success);


            var nextCall = _controller.Get(3);
            var status = nextCall.Status;

            Assert.Equal(TaskStatus.Faulted, status);
        }

        [Fact]
        public void Delete_WhenCalled_NotFoundItem()
        {
            // Act
            var okResult = _controller.Delete(88);
            var response = okResult.Result;

            // Assert
            Assert.False(response.Success);

        }

        [Fact]
        public void Post_WhenCalled_Create()
        {
            // Act
            var okResult = _controller.Post(4, new Domain.Models.Validations.SaveUserValidation() { Name = "Larry the Lobster", BirthDate = new DateTime(1983, 2, 5) });
            var response = okResult.Result;

            // Assert
            Assert.True(response.Success);

            var nextCall = _controller.Get(4);
            User user = nextCall.Result;

            Assert.Equal(4, user.Id);

        }

        [Fact]
        public void Post_WhenCalled_Change()
        {
            // Act
            var okResult = _controller.Post(2, new Domain.Models.Validations.SaveUserValidation() { Name = "Larry the Lobster", BirthDate = new DateTime(1983, 2, 5) });
            var response = okResult.Result;

            // Assert
            Assert.True(response.Success);

            var nextCall = _controller.Get(2);
            User user = nextCall.Result;

            Assert.Equal("Larry the Lobster", user.Name);
            Assert.Equal(new DateTime(1983, 2, 5), user.Birthdate);

        }

        [Fact]
        public void Put_WhenCalled_Doesnt_Create()
        {
            // Act
            var okResult = _controller.Put(4, new Domain.Models.Validations.SaveUserValidation() { Name = "Larry the Lobster", BirthDate = new DateTime(1983, 2, 5) });
            var response = okResult.Result;

            // Assert
            Assert.False(response.Success);

        }

        [Fact]
        public void Put_WhenCalled_Change()
        {
            // Act
            var okResult = _controller.Put(2, new Domain.Models.Validations.SaveUserValidation() { Name = "Larry the Lobster", BirthDate = new DateTime(1983, 2, 5) });
            var response = okResult.Result;

            // Assert
            Assert.True(response.Success);

            var nextCall = _controller.Get(2);
            User user = nextCall.Result;

            Assert.Equal("Larry the Lobster", user.Name);
            Assert.Equal(new DateTime(1983, 2, 5), user.Birthdate);

        }

        [Fact]
        public void SaveUserValidation_Test()
        {
            SaveUserValidation validation = new SaveUserValidation() { Name = "Larry the Lobster", BirthDate = new DateTime(1983, 2, 5) };
            // 
            Assert.False(ValidateModel(validation).Any());
            //
            validation = new SaveUserValidation() { Name = null, BirthDate = new DateTime(1983, 2, 5) };
            // 
            Assert.True(ValidateModel(validation).Any());
            //
            validation = new SaveUserValidation() { Name = "Larry the Lobster", BirthDate = null };
            // 
            Assert.True(ValidateModel(validation).Any());



        }

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

    }
}
