using Portfolio.Core.Entities;
using Portfolio.Core.Services.Models;

namespace Portfolio.Core.Interfaces.Services
{
    public interface IProjectService
    {
        Task<ResultModel<Project>> GetByIdAsync(string id);
        Task<ResultModel<IEnumerable<Project>>> GetAllAsync();
        Task<ResultModel<Project>> CreateProjectAsync(ProjectCreateRequestModel ProjectCreateRequestModel);
        Task<ResultModel<Project>> UpdateProjectAsync(ProjectUpdateRequestModel ProjectUpdateRequestModel);
        Task<ResultModel<Project>> DeleteProjectAsync(string id);
    }
}
