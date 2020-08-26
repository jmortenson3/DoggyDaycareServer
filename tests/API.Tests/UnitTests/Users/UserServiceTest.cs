using API.Exceptions;
using API.Users;
using Common.Users;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace API.Tests.UnitTests.Users
{
    public class UserServiceTest
    {
        private readonly UserService _sut;
        private readonly Mock<RoleManager<IdentityRole>> _mockRoleManager;
        private readonly Mock<SignInManager<ApplicationUser>> _mockSignInManager;
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;

        public UserServiceTest()
        {
            _mockRoleManager = new Mock<RoleManager<IdentityRole>>(
                new Mock<IRoleStore<IdentityRole>>().Object,
                new IRoleValidator<IdentityRole>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<ILogger<RoleManager<IdentityRole>>>().Object);
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(
                new Mock<IUserStore<ApplicationUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<ApplicationUser>>().Object,
                new IUserValidator<ApplicationUser>[0],
                new IPasswordValidator<ApplicationUser>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<ApplicationUser>>>().Object);
            _mockSignInManager = new Mock<SignInManager<ApplicationUser>>(
                _mockUserManager.Object,
                new Mock<IHttpContextAccessor>().Object,
                new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<ApplicationUser>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object,
                new Mock<IUserConfirmation<ApplicationUser>>().Object);

            _sut = new UserService(_mockUserManager.Object, _mockSignInManager.Object);
        }

        [Fact]
        public async Task Register_ShouldReturnUser()
        {
            // Arrange
            var model = new UserRegisterModel{ Email = "stevie@test.com", Password = "test123", RememberMe = true };
            _mockUserManager
                .Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), model.Password))
                .ReturnsAsync(new MockIdentityResult(Succeeded: true));

            // Act
            var user = await _sut.Register(model);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(model.Email, user.Email);
        }

        [Fact]
        public async Task Register_ShouldCallCreateAsyncOnce()
        {
            // Arrange
            var applicationUser = new ApplicationUser { Email = "stevie@test.com" };
            var model = new UserRegisterModel { Email = "stevie@test.com", Password = "test123", RememberMe = true };
            _mockUserManager
                .Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), model.Password))
                .ReturnsAsync(new MockIdentityResult(Succeeded: true));

            // Act
            var user = await _sut.Register(model);

            // Assert
            _mockUserManager.Verify(x => x.CreateAsync(It.IsAny<ApplicationUser>(), model.Password), Times.Once);
            Assert.Equal(applicationUser.Email, user.Email);
        }

        [Fact]
        public async Task Register_ShouldThrowAppException_WhenNullEmail()
        {
            // Arrange
            var model = new UserRegisterModel { Email = null, Password = "test123", RememberMe = true };
            // Act
            var ex = await Assert.ThrowsAsync<AppException>(async () => await _sut.Register(model));

            // Assert
            Assert.Equal("Email cannot be empty or whitespace.", ex.Message);
        }

        [Fact]
        public async Task Register_ShouldThrowAppException_WhenEmptyEmail()
        {
            // Arrange
            var model = new UserRegisterModel { Email = "", Password = "test123", RememberMe = true };

            // Act
            var ex = await Assert.ThrowsAsync<AppException>(async () => await _sut.Register(model));

            // Assert
            Assert.Equal("Email cannot be empty or whitespace.", ex.Message);
        }

        [Fact]
        public async Task Register_ShouldThrowAppException_WhenNullPassword()
        {
            // Arrange
            var model = new UserRegisterModel { Email = "stevie@test.com", Password = null, RememberMe = true };

            // Act
            var ex = await Assert.ThrowsAsync<AppException>(async () => await _sut.Register(model));

            // Assert
            Assert.Equal("Password cannot be empty or whitespace.", ex.Message);

        }

        [Fact]
        public async Task Register_ShouldThrowAppException_WhenEmptyPassword()
        {
            // Arrange
            var applicationUser = new ApplicationUser { Email = "stevie@test.com" };
            var model = new UserRegisterModel { Email = "stevie@test.com", Password = "", RememberMe = true };

            // Act
            var ex = await Assert.ThrowsAsync<AppException>(async () => await _sut.Register(model));

            // Assert
            Assert.Equal("Password cannot be empty or whitespace.", ex.Message);

        }

        [Fact]
        public async Task Login_ShouldReturnUser()
        {
            // Arrange
            var applicationUser = new ApplicationUser { Email = "stevie@test.com" };
            var model = new UserLoginModel { Email = "stevie@test.com", Password = "test123", RememberMe = true };

            _mockUserManager.Setup(x => x.FindByEmailAsync(model.Email))
                .ReturnsAsync(applicationUser);

            _mockSignInManager
                .Setup(x => x.PasswordSignInAsync(applicationUser, model.Password, model.RememberMe, false))
                .ReturnsAsync(SignInResult.Success);

            // Act
            var user = await _sut.Login(model);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(model.Email, user.Email);
        }

        [Fact]
        public async Task Authenticate_ShouldCallPasswordSignInAsyncOnce()
        {
            // Arrange
            var applicationUser = new ApplicationUser { Email = "stevie@test.com" };
            var model = new UserLoginModel { Email = "stevie@test.com", Password = "test123", RememberMe = true };


            _mockUserManager.Setup(x => x.FindByEmailAsync(model.Email))
                .ReturnsAsync(applicationUser);
            _mockSignInManager.Setup(x => x.PasswordSignInAsync(It.IsAny<ApplicationUser>(),
                model.Password, model.RememberMe, false)).ReturnsAsync(SignInResult.Success);

            // Act
            var user = await _sut.Login(model);

            // Assert
            _mockSignInManager.Verify(x => x.PasswordSignInAsync(It.IsAny<ApplicationUser>(), model.Password, model.RememberMe, false), Times.Once);
        }

        [Fact]
        public async Task Authenticate_ShouldThrowException_WhenUserDoesNotExist()
        {
            // Arrange
            var applicationUser = new ApplicationUser { Email = "stevie@test.com" };
            var model = new UserLoginModel { Email = "stevie@test.com", Password = "test123", RememberMe = true };

            _mockUserManager.Setup(x => x.FindByEmailAsync(model.Email))
                .ReturnsAsync(() => null);

            // Act
            var ex = await Assert.ThrowsAsync<AppException>(async () => await _sut.Login(model));

            // Assert
            Assert.Equal($"Cannot find user with email {model.Email}", ex.Message);
        }

        [Fact]
        public async Task Authenticate_ShouldThrowAppException_WhenNullEmail()
        {
            // Arrange
            var model = new UserLoginModel { Email = null, Password = "test123", RememberMe = true };

            // Act
            var ex = await Assert.ThrowsAsync<AppException>(async () => await _sut.Login(model));

            // Assert
            Assert.Equal("Email cannot be empty or whitespace.", ex.Message);
        }

        [Fact]
        public async Task Authenticate_ShouldThrowAppException_WhenEmptyEmail()
        {
            // Arrange
            var model = new UserLoginModel { Email = "", Password = "test123", RememberMe = true };

            // Act
            var ex = await Assert.ThrowsAsync<AppException>(async () => await _sut.Login(model));

            // Assert
            Assert.Equal("Email cannot be empty or whitespace.", ex.Message);
        }

        [Fact]
        public async Task Authenticate_ShouldThrowAppException_WhenNullPassword()
        {
            // Arrange
            var model = new UserLoginModel { Email = "stevie@test.com", Password = null, RememberMe = true };

            // Act
            var ex = await Assert.ThrowsAsync<AppException>(async () =>
                    await _sut.Login(model));

            // Assert
            Assert.Equal("Password cannot be empty or whitespace.", ex.Message);

        }

        [Fact]
        public async Task Authenticate_ShouldThrowAppException_WhenEmptyPassword()
        {
            // Arrange
            var model = new UserLoginModel { Email = "stevie@test.com", Password = "", RememberMe = true };

            // Act
            var ex = await Assert.ThrowsAsync<AppException>(async () =>
                    await _sut.Login(model));

            // Assert
            Assert.Equal("Password cannot be empty or whitespace.", ex.Message);

        }

        [Fact]
        public async Task GetCurrentUser_ShouldCallGetUserOnce()
        {
            // Arrange
            ClaimsPrincipal claim = new ClaimsPrincipal();

            // Act
            var user = await _sut.GetCurrentUser(claim);

            // Assert
            _mockUserManager.Verify(x => x.GetUserAsync(claim), Times.Once);
        }

        [Fact]
        public async Task SignOut_ShouldCallSignOutAsyncOnce()
        {
            // Arrange
            ClaimsPrincipal claim = new ClaimsPrincipal();

            // Act
            await _sut.SignOut();

            // Assert
            _mockSignInManager.Verify(x => x.SignOutAsync(), Times.Once);
        }


        private class MockIdentityResult : IdentityResult
        {
            public MockIdentityResult(bool Succeeded)
            {
                this.Succeeded = Succeeded;
            }
        }

        private class MockSignInResult : SignInResult
        {
            public MockSignInResult()
            {

            }
        }
    }

}
