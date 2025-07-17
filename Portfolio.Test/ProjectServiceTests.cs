using FluentAssertions;
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
    public class ProjectServiceTests
    {
        private readonly Mock<IProjectRepository> _mockProjectRepository;
        private readonly ProjectService _projectService;

        public ProjectServiceTests()
        {
            _mockProjectRepository = new Mock<IProjectRepository>();
            _projectService = new ProjectService(_mockProjectRepository.Object);
        }



        [Fact]
        public async Task GetByIdAsync_WithExistingProject_ReturnsProject()
        {
            //Arrange
            var projectId = "abc123";
            var selectedProject = new Project { Id = projectId, Name = "Test project", Description = "Test description" };

            _mockProjectRepository.Setup(repo => repo.GetByIdAsync(projectId)).ReturnsAsync(selectedProject);
            
            //Act

            var result = await _projectService.GetByIdAsync(projectId);

            //Assert
            result.Success.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Errors.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task GetByIdAsync_WithNoneExistingProjectId_ReturnsError()
        {
            // Arrange
            var projectId = "abc123";

            _mockProjectRepository.Setup(r => r.GetByIdAsync(projectId)).ReturnsAsync((Project)null!);

            // Act
            var result = await _projectService.GetByIdAsync(projectId);

            // Assert
            result.Success.Should().BeFalse();
            result.Errors.Should().ContainSingle("No project found!");
            result.Value.Should().BeNull();
        }

        [Fact]
        public async Task GetByIdAsync_WithRepositoryThrowException_ReturnsError()
        {
            // Arrange
            var projectId = "error-case";

            _mockProjectRepository.Setup(r => r.GetByIdAsync(projectId)).ThrowsAsync(new Exception("DB failure"));

            // Act
            var result = await _projectService.GetByIdAsync(projectId);

            // Assert
            result.Success.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.Contains("DB failure"));
            result.Value.Should().BeNull();
        }

        [Fact]
        public async Task CreateProjectAsync_WithValidInput_ReturnsSuccessResult()
        {
            // Arrange
            var request = new ProjectCreateRequestModel
            {
                Name = "My Project",
                Description = "Some description",
                FrontendTechStack = ["Vue", "Tailwind"],
                BackendTechStack = ["ASP.NET Core", "MongoDB"],
                FrontendGitHubUrl = "FrontendUrl",
                BackendGitHubUrl = "BackendUrl",
                ImagesPath = ["img1.png", "img2.png"]
            };

            // Simuleer succesvolle insert in database
            _mockProjectRepository.Setup(r => r.AddAsync(It.IsAny<Project>())).ReturnsAsync(true);

            // Act
            var result = await _projectService.CreateProjectAsync(request);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
            result.Value.Should().NotBeNull();;
            result.Value.Should().BeEquivalentTo(request, options => options
                .ComparingByMembers<ProjectCreateRequestModel>());
        }

        [Fact]
        public async Task CreateProjectAsync_WhitRepositoryFalseResponse_ReturnsFailureResult()
        {
            // Arrange
            var request = new ProjectCreateRequestModel
            {
                Name = "My Project"
            };

            _mockProjectRepository.Setup(r => r.AddAsync(It.IsAny<Project>())).ReturnsAsync(false);

            // Act
            var result = await _projectService.CreateProjectAsync(request);

            // Assert
            result.Success.Should().BeFalse();
            result.Value.Should().BeNull();
            result.Errors.Should().Contain("Could not create new project");
        }

        [Fact]
        public async Task CreateProjectAsync_WhithExceptionThrown_ReturnsErrorResult()
        {
            // Arrange
            var request = new ProjectCreateRequestModel
            {
                Name = "My Project"
            };

            _mockProjectRepository.Setup(r => r.AddAsync(It.IsAny<Project>()))
                .ThrowsAsync(new Exception("DB failure"));

            // Act
            var result = await _projectService.CreateProjectAsync(request);

            // Assert
            result.Success.Should().BeFalse();
            result.Value.Should().BeNull();
            result.Errors.Should().Contain(e => e.Contains("DB failure"));
        }

        [Fact]
        public async Task DeleteProjectAsync_WhithValidProjectId_ReturnsSuccess()
        {
            // Arrange
            var projectId = "abc123";
            var existingProject = new Project { Id = projectId, Name = "Test Project" };

            _mockProjectRepository.Setup(r => r.GetByIdAsync(projectId)).ReturnsAsync(existingProject);
            _mockProjectRepository.Setup(r => r.DeleteAsync(existingProject)).Returns(Task.CompletedTask);

            // Act
            var result = await _projectService.DeleteProjectAsync(projectId);

            // Assert
            result.Success.Should().BeTrue();
            result.Value.Should().BeNull();
            result.Errors.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task DeleteProjectAsync_WhithInValidProjectId_ReturnsError()
        {
            // Arrange
            var projectId = "abc123";
            _mockProjectRepository.Setup(r => r.GetByIdAsync(projectId))
                          .ReturnsAsync((Project)null!);

            // Act
            var result = await _projectService.DeleteProjectAsync(projectId);

            // Assert
            result.Success.Should().BeFalse();
            result.Value.Should().BeNull();
            result.Errors.Should().Contain("Project does not exist!");
        }

        [Fact]
        public async Task DeleteProjectAsync_WhithExceptionThrown_ReturnsError()
        {
            // Arrange
            var projectId = "abc123";
            var project = new Project { Id = projectId };

            _mockProjectRepository.Setup(r => r.GetByIdAsync(projectId))
                                  .ReturnsAsync(project);

            _mockProjectRepository.Setup(r => r.DeleteAsync(project))
                                  .ThrowsAsync(new Exception("DB failure"));

            // Act
            var result = await _projectService.DeleteProjectAsync(projectId);

            // Assert
            result.Success.Should().BeFalse();
            result.Value.Should().BeNull();
            result.Errors.Should().Contain(e => e.Contains("DB failure"));
        }

        [Fact]
        public async Task GetAllAsync_WithExistingProjects_ReturnsProjects()
        {
            // Arrange
            var projects = new List<Project>
            {
                new() { Id = "1", Name = "Test Project 1" },
                new() { Id = "2", Name = "Test Project 2" }
            };

            _mockProjectRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(projects);

            // Act
            var result = await _projectService.GetAllAsync();

            // Assert
            result.Success.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(projects);
            result.Errors.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task GetAllAsync_WithNoneExistingProjects_ReturnsError()
        {
            // Arrange
            _mockProjectRepository.Setup(r => r.GetAllAsync()).ReturnsAsync([]);

            // Act
            var result = await _projectService.GetAllAsync();

            // Assert
            result.Success.Should().BeFalse();
            result.Value.Should().BeNull();
            result.Errors.Should().Contain("No projects found");
        }

        [Fact]
        public async Task GetAllAsync_WhithExceptionThrown_ReturnsError()
        {
            // Arrange
            _mockProjectRepository.Setup(r => r.GetAllAsync()).ThrowsAsync(new Exception("DB failure"));

            // Act
            var result = await _projectService.GetAllAsync();

            // Assert
            result.Success.Should().BeFalse();
            result.Value.Should().BeNull();
            result.Errors.Should().Contain(e => e.Contains("DB failure"));
        }
    }
}
