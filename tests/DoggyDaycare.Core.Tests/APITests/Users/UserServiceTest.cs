using DoggyDaycare.API.Exceptions;
using DoggyDaycare.API.Users;
using DoggyDaycare.Infrastructure.Identity;
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

namespace DoggyDaycare.Core.Tests.APITests.Users
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
            var applicationUser = new ApplicationUser { Email = "stevie@test.com" };
            var password = "test123";
            _mockUserManager
                .Setup(x => x.CreateAsync(applicationUser, password))
                .ReturnsAsync(new MockIdentityResult(Succeeded: true));

            // Act
            var user = await _sut.Register(applicationUser, password);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(applicationUser.Email, user.Email);
        }

        [Fact]
        public async Task Register_ShouldCallCreateAsyncOnce()
        {
            // Arrange
            var applicationUser = new ApplicationUser { Email = "stevie@test.com" };
            var password = "test123";
            _mockUserManager
                .Setup(x => x.CreateAsync(applicationUser, password))
                .ReturnsAsync(new MockIdentityResult(Succeeded: true));

            // Act
            var user = await _sut.Register(applicationUser, password);

            // Assert
            _mockUserManager.Verify(x => x.CreateAsync(applicationUser, password), Times.Once);
            Assert.Equal(applicationUser.Email, user.Email);
        }

        [Fact]
        public async Task Register_ShouldThrowAppException_WhenNullEmail()
        {
            // Arrange
            var applicationUser = new ApplicationUser { Email = null };
            var password = "test123";

            // Act
            var ex = await Assert.ThrowsAsync<AppException>(async () => await _sut.Register(applicationUser, password));

            // Assert
            Assert.Equal("Email cannot be empty or whitespace.", ex.Message);
        }

        [Fact]
        public async Task Register_ShouldThrowAppException_WhenEmptyEmail()
        {
            // Arrange
            var applicationUser = new ApplicationUser { Email = "" };
            var password = "test123";

            // Act
            var ex = await Assert.ThrowsAsync<AppException>(async () => await _sut.Register(applicationUser, password));

            // Assert
            Assert.Equal("Email cannot be empty or whitespace.", ex.Message);
        }

        [Fact]
        public async Task Register_ShouldThrowAppException_WhenNullPassword()
        {
            // Arrange
            var applicationUser = new ApplicationUser { Email = "stevie@test.com" };
            string password = null;

            // Act
            var ex = await Assert.ThrowsAsync<AppException>(async () => await _sut.Register(applicationUser, password));

            // Assert
            Assert.Equal("Password cannot be empty or whitespace.", ex.Message);

        }

        [Fact]
        public async Task Register_ShouldThrowAppException_WhenEmptyPassword()
        {
            // Arrange
            var applicationUser = new ApplicationUser { Email = "stevie@test.com" };
            var password = "";

            // Act
            var ex = await Assert.ThrowsAsync<AppException>(async () => await _sut.Register(applicationUser, password));

            // Assert
            Assert.Equal("Password cannot be empty or whitespace.", ex.Message);

        }

        [Fact]
        public async Task Authenticate_ShouldReturnUser()
        {
            // Arrange
            var email = "stevie@test.com";
            var password = "test123";
            var rememberMe = false;

            _mockUserManager.Setup(x => x.FindByEmailAsync(email))
                .ReturnsAsync(new ApplicationUser { Email = email });

            _mockSignInManager
                .Setup(x => x.PasswordSignInAsync(new ApplicationUser { Email = email }, password, rememberMe, false))
                .ReturnsAsync(new MockSignInResult());

            // Act
            var user = await _sut.Authenticate(email, password, rememberMe);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(email, user.Email);
        }

        [Fact]
        public async Task Authenticate_ShouldCallPasswordSignInAsyncOnce()
        {
            // Arrange
            var email = "stevie@test.com";
            var password = "test123";
            var rememberMe = false;

            var applicationUser = new ApplicationUser { Email = email };

            _mockUserManager.Setup(x => x.FindByEmailAsync(email))
                .ReturnsAsync(applicationUser);

            // Act
            var user = await _sut.Authenticate(email, password, rememberMe);

            // Assert
            _mockSignInManager.Verify(x => x.PasswordSignInAsync(applicationUser, password, rememberMe, false), Times.Once);
        }

        [Fact]
        public async Task Authenticate_ShouldThrowException_WhenUserDoesNotExist()
        {
            // Arrange
            var email = "stevie_doesnt_exist@test.com";
            var password = "test123";
            var rememberMe = false;

            _mockUserManager.Setup(x => x.FindByEmailAsync(email))
                .ReturnsAsync(() => null);

            // Act
            var ex = await Assert.ThrowsAsync<AppException>(async () => await _sut.Authenticate(email, password, rememberMe));

            // Assert
            Assert.Equal($"Cannot find user with email {email}", ex.Message);
        }

        [Fact]
        public async Task Authenticate_ShouldThrowAppException_WhenNullEmail()
        {
            // Arrange
            string email = null;
            var password = "test123";
            var rememberMe = false;

            // Act
            var ex = await Assert.ThrowsAsync<AppException>(async () => await _sut.Authenticate(email, password, rememberMe));

            // Assert
            Assert.Equal("Email cannot be empty or whitespace.", ex.Message);
        }

        [Fact]
        public async Task Authenticate_ShouldThrowAppException_WhenEmptyEmail()
        {
            // Arrange
            var email = "";
            var password = "test123";
            var rememberMe = false;

            // Act
            var ex = await Assert.ThrowsAsync<AppException>(async () => await _sut.Authenticate(email, password, rememberMe));

            // Assert
            Assert.Equal("Email cannot be empty or whitespace.", ex.Message);
        }

        [Fact]
        public async Task Authenticate_ShouldThrowAppException_WhenNullPassword()
        {
            // Arrange
            var email = "stevie@test.com";
            string password = null;
            var rememberMe = false;

            // Act
            var ex = await Assert.ThrowsAsync<AppException>(async () => 
                    await _sut.Authenticate(email, password, rememberMe));

            // Assert
            Assert.Equal("Password cannot be empty or whitespace.", ex.Message);

        }

        [Fact]
        public async Task Authenticate_ShouldThrowAppException_WhenEmptyPassword()
        {
            // Arrange
            var email = "stevie@test.com";
            var password = "";
            var rememberMe = false;

            // Act
            var ex = await Assert.ThrowsAsync<AppException>(async () => 
                    await _sut.Authenticate(email, password, rememberMe));

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
