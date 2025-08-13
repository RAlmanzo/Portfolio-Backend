using Portfolio.Core.Entities;
using Portfolio.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces.Services
{
    public interface IExperienceService
    {
        Task<ResultModel<Experience>> GetByIdAsync(string id);
        Task<ResultModel<IEnumerable<Experience>>> GetAllAsync();
        Task<ResultModel<Experience>> CreateExperienceAsync(ExperienceCreateRequestModel ExperienceCreateRequestModel);
        Task<ResultModel<Experience>> UpdateExperienceAsync(ExperienceUpdateRequestModel ExperienceUpdateRequestModel);
        Task<ResultModel<Experience>> DeleteExperienceAsync(string id);
    }
}
