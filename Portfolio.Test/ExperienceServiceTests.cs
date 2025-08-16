using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Portfolio.Core.Entities;
using Portfolio.Core.Interfaces.Repositories;
using Portfolio.Core.Services;
using Portfolio.Core.Services.Models;
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
        public async Task CreateExperienceAsync_WithValidInput_ReturnsSuccess()
        {
            // Arrange
            var request = new ExperienceCreateRequestModel
            {
                Position = "Software Engineer",
                Company = "Tech Corp",
                Location = "New York, NY",
                StartDate = new DateTime(2020, 1, 1),
                EndDate = new DateTime(2022, 12, 31),
                Information = "Worked on various projects"
            };

            _mockExperienceRepository.Setup(r => r.AddAsync(It.IsAny<Experience>())).ReturnsAsync(true);

            // Act
            var result = await _experienceService.CreateExperienceAsync(request);

            // Assert
            result.Success.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(request);
            result.Errors.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task CreateExperienceAsync_WithInvalidInput_ReturnsError()
        {
            // Arrange
            var request = new ExperienceCreateRequestModel
            {
                Position = "Software Engineer",
                Company = "Tech Corp",
                Location = "New York, NY",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
            };

            _mockExperienceRepository.Setup(r => r.AddAsync(It.IsAny<Experience>())).ReturnsAsync(false);

            // Act
            var result = await _experienceService.CreateExperienceAsync(request);
            
            // Assert
            result.Success.Should().BeFalse();
            result.Value.Should().BeNull();
            result.Errors.Should().Contain("Could not create new experience");
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
        public async Task DeleteExperienceAsync_WithInvalidId_ReturnsError()
        {
            // Arrange
            var experienceId = "abc123";
            _mockExperienceRepository.Setup(repo => repo.GetByIdAsync(experienceId)).ReturnsAsync((Experience)null!);
            
            // Act
            var result = await _experienceService.DeleteExperienceAsync(experienceId);
            
            // Assert
            result.Success.Should().BeFalse();
            result.Value.Should().BeNull();
            result.Errors.Should().Contain($"No experience found with id: {experienceId}");
        }

        [Fact]
        public async Task DeleteExperienceAsync_WithRepositoryThrowsException_ReturnsError()
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
            _mockExperienceRepository.Setup(repo => repo.DeleteAsync(experienceToDelete)).ThrowsAsync(new Exception("Database error"));
            
            // Act
            var result = await _experienceService.DeleteExperienceAsync(experienceId);
            
            // Assert
            result.Success.Should().BeFalse();
            result.Errors.Should().ContainSingle($"An error occurred while deleting experience with id: {experienceId}. Please try again or contact support");
            result.Value.Should().BeNull();
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
