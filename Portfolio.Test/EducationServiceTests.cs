using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Portfolio.Core.Entities;
using Portfolio.Core.Interfaces.Repositories;
using Portfolio.Core.Interfaces.Services;
using Portfolio.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Portfolio.Tests
{
    public class EducationServiceTests
    {
        private readonly Mock<IEducationRepository> _mockEducationRepository;
        private readonly Mock<ILogger<EducationService>> _mockLogger;
        private readonly EducationService _educationService;

        public EducationServiceTests()
        {
            _mockEducationRepository = new Mock<IEducationRepository>();
            _mockLogger = new Mock<ILogger<EducationService>>();
            _educationService = new EducationService(_mockEducationRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetByIdAsync_WithExistingEducation_ReturnsEducation()
        {
            //Arrange
            var educationId = "abc123";
            var selectedEducation = new Education { Id = educationId, Degree = "test data", School = "test school" };

            _mockEducationRepository.Setup(repo => repo.GetByIdAsync(educationId)).ReturnsAsync(selectedEducation);

            //Act

            var result = await _educationService.GetByIdAsync(educationId);

            //Assert
            result.Success.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Errors.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task GetByIdAsync_WithNoneExistingEducationId_ReturnsError()
        {
            // Arrange
            var educationId = "abc123";

            _mockEducationRepository.Setup(r => r.GetByIdAsync(educationId)).ReturnsAsync((Education)null!);

            // Act
            var result = await _educationService.GetByIdAsync(educationId);

            // Assert
            result.Success.Should().BeFalse();
            result.Errors.Should().ContainSingle($"No education found with id: {educationId}");
            result.Value.Should().BeNull();
        }

        [Fact]
        public async Task GetByIdAsync_WithRepositoryThrowException_ReturnsError()
        {
            // Arrange
            var educationId = "abc123";

            _mockEducationRepository.Setup(r => r.GetByIdAsync(educationId)).ThrowsAsync(new Exception("DB failure"));

            // Act
            var result = await _educationService.GetByIdAsync(educationId);

            // Assert
            result.Success.Should().BeFalse();
            result.Errors.Should().ContainSingle().Which.Should().Be($"An error occured while retrieving education with id: {educationId}. Please try again or contact support");
            result.Value.Should().BeNull();
        }

        [Fact]
        public async Task DeleteEducationAsync_WhithValidId_ReturnsSuccess()
        {
            // Arrange
            var educationId = "abc123";
            var existingEducation = new Education { Id = educationId, Degree = "Test degree" };

            _mockEducationRepository.Setup(r => r.GetByIdAsync(educationId)).ReturnsAsync(existingEducation);
            _mockEducationRepository.Setup(r => r.DeleteAsync(existingEducation)).Returns(Task.CompletedTask);

            // Act
            var result = await _educationService.DeleteEducationAsync(educationId);

            // Assert
            result.Success.Should().BeTrue();
            result.Value.Should().BeNull();
            result.Errors.Should().BeNullOrEmpty();
        }
    }
}
