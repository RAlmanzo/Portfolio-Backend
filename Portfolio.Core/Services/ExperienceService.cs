using Microsoft.Extensions.Logging;
using Portfolio.Core.Entities;
using Portfolio.Core.Interfaces.Repositories;
using Portfolio.Core.Interfaces.Services;
using Portfolio.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Services
{
    public class ExperienceService : IExperienceService
    {
        private readonly IExperienceRepository _experienceRepository;
        private readonly ILogger<ExperienceService> _logger;

        public ExperienceService(IExperienceRepository experienceRepository, ILogger<ExperienceService> logger)
        {
            _experienceRepository = experienceRepository;
            _logger = logger;
        }

        public Task<ResultModel<Experience>> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ResultModel<Experience>> CreateExperienceAsync(ExperienceCreateRequestModel ExperienceCreateRequestModel)
        {
            throw new NotImplementedException();
        }

        public Task<ResultModel<Experience>> DeleteExperienceAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ResultModel<IEnumerable<Experience>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResultModel<Experience>> UpdateExperienceAsync(ExperienceUpdateRequestModel ExperienceUpdateRequestModel)
        {
            throw new NotImplementedException();
        }
    }
}
