using Castle.Core.Logging;
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
    }
}
