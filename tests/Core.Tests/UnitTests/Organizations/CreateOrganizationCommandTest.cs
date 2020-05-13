﻿using Core.Common;
using Core.Organizations;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace Core.Tests.UnitTests.Organizations
{
    public class CreateOrganizationCommandTest
    {
        private readonly Mock<IAsyncRepository<Organization>> _repository;

        public CreateOrganizationCommandTest()
        {
            _repository = new Mock<IAsyncRepository<Organization>>();
            _repository.Setup(x => x.AddAsync(It.IsAny<Organization>()))
                .ReturnsAsync(new Organization { Id = 2, Name = "St. Larry's" });
        }

        [Fact]
        public async void ShouldReturnOrganization()
        {
            // Arrange
            var command = new CreateOrganizationCommand(null, name: "St. Larry's", null);

            // Act
            var handler = new CreateOrganizationCommandHandler(_repository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void ShouldCallAddAsyncOnce()
        {
            // Arrange
            var command = new CreateOrganizationCommand(null, name: "St. Larry's", null);

            // Act
            var handler = new CreateOrganizationCommandHandler(_repository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            _repository.Verify(x => x.AddAsync(It.IsAny<Organization>()), Times.Once);
        }
    }
}
