using Microsoft.Extensions.Logging;
using Moq;
using Portfolio.Core.Interfaces.Repositories;
using Portfolio.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
