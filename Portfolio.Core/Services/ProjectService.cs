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
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ResultModel<Project>> GetByIdAsync(string id)
        {
            try
            {
                var project = await _projectRepository.GetByIdAsync(id);

                if (project == null)
                {
                    return new ResultModel<Project>
                    {
                        Success = false,
                        Errors = ["No project found!"]
                    };
                }

                return new ResultModel<Project>
                {
                    Success = true,
                    Value = project
                };
            }
            catch (Exception ex)
            {
                return new ResultModel<Project>
                {
                    Success = false,
                    Errors = [$"An error occurred while retrieving project: {ex.Message}"]
                };
            }
        }

        public Task<ResultModel<Project>> CreateProjectAsync(ProjectCreateRequestModel ProjectCreateRequestModel)
        {
            throw new NotImplementedException();
        }

        public Task<ResultModel<Project>> DeleteProjectAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ResultModel<IEnumerable<Project>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResultModel<Project>> UpdateProjectAsync(ProjectUpdateRequestModel ProjectUpdateRequestModel)
        {
            throw new NotImplementedException();
        }
    }
}
