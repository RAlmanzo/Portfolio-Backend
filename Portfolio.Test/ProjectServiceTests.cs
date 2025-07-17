using FluentAssertions;
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
            result.Errors.Should().BeNull();
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
    }
}
