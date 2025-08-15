using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Portfolio.Core.Entities;
using Portfolio.Core.Interfaces.Repositories;
using Portfolio.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Portfolio.Tests
{
    public class ExperienceServiceTests
    {
        private readonly Mock<IExperienceRepository> _mockExperienceRepository;
        private readonly Mock<ILogger<ExperienceService>> _mockLogger;
        private readonly ExperienceService _experienceService;
        public ExperienceServiceTests()
        {
            _mockExperienceRepository = new Mock<IExperienceRepository>();
            _mockLogger = new Mock<ILogger<ExperienceService>>();
            _experienceService = new ExperienceService(_mockExperienceRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetByIdAsync_WithExistingExperience_ReturnsExperience()
        {
            // Arrange
            var experienceId = "abc123";
            var selectedExperience = new Experience
            {
                Id = experienceId,
                Position = "Software Engineer",
                Company = "Tech Corp",
                Location = "New York, NY",
                StartDate = new DateTime(2020, 1, 1),
            };

            _mockExperienceRepository.Setup(repo => repo.GetByIdAsync(experienceId)).ReturnsAsync(selectedExperience);

            // Act
            var result = await _experienceService.GetByIdAsync(experienceId);

            // Assert
            result.Success.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Errors.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task GetByIdAsync_WithNonExistingExperienceId_ReturnsError()
        {
            // Arrange
            var experienceId = "abc123";

            _mockExperienceRepository.Setup(r => r.GetByIdAsync(experienceId)).ReturnsAsync((Experience)null!);

            // Act
            var result = await _experienceService.GetByIdAsync(experienceId);

            // Assert
            result.Success.Should().BeFalse();
            result.Value.Should().BeNull();
            result.Errors.Should().ContainSingle($"No experience found with id: {experienceId}");
        }

        [Fact]
        public async Task GetByIdAsync_WithRepositoryThrowsException_ReturnsError()
        {
            // Arrange
            var experienceId = "abc123";
            _mockExperienceRepository.Setup(r => r.GetByIdAsync(experienceId)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _experienceService.GetByIdAsync(experienceId);

            // Assert
            result.Success.Should().BeFalse();
            result.Errors.Should().ContainSingle($"An error occurred while retrieving experience with id: {experienceId}. Please try again or contact support");
            result.Value.Should().BeNull();
        }

        [Fact]
        public async Task DeleteExperienceAsync_WithValidId_ReturnsSuccess()
        {
            // Arrange
            var experienceId = "abc123";
            var experienceToDelete = new Experience 
            { 
                Id = experienceId, 
                Position = "Developer", 
                Company = "Company A", 
                Location = "Location A", 
                StartDate = DateTime.Now 
            };

            _mockExperienceRepository.Setup(repo => repo.GetByIdAsync(experienceId)).ReturnsAsync(experienceToDelete);
            _mockExperienceRepository.Setup(repo => repo.DeleteAsync(experienceToDelete)).Returns(Task.CompletedTask);
            
            // Act
            var result = await _experienceService.DeleteExperienceAsync(experienceId);
            
            // Assert
            result.Success.Should().BeTrue();
            result.Value.Should().BeNull();
            result.Errors.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task GetallAsync_WithExistingExperiences_ReturnsExperiences()
        {
            // Arrange
            var experiences = new List<Experience>
            {
                new() { Id = "1", Position = "Developer", Company = "Company A", Location = "Location A", StartDate = DateTime.Now },
                new() { Id = "2", Position = "Tester", Company = "Company B", Location = "Location B", StartDate = DateTime.Now }
            };

            _mockExperienceRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(experiences);

            // Act
            var result = await _experienceService.GetAllAsync();

            // Assert
            result.Success.Should().BeTrue();
            result.Value.Should().NotBeNullOrEmpty();
            result.Value.Should().BeEquivalentTo(experiences);
            result.Errors.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task GetAllAsync_WithNoneExistingExperiences_ReturnsError()
        {
            // Arrange
            _mockExperienceRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync([]);

            // Act
            var result = await _experienceService.GetAllAsync();

            // Assert
            result.Success.Should().BeFalse();
            result.Value.Should().BeNull();
            result.Errors.Should().ContainSingle("No experiences found.");
        }

        [Fact]
        public async Task GetAllAsync_WithRepositoryThrowsException_ReturnsError()
        {
            // Arrange
            _mockExperienceRepository.Setup(repo => repo.GetAllAsync()).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _experienceService.GetAllAsync();

            // Assert
            result.Success.Should().BeFalse();
            result.Errors.Should().ContainSingle("An error occurred while retrieving experiences. Please try again or contact support");
            result.Value.Should().BeNull();
        }
    }
}
