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
            //get the project
            var project = await _projectRepository.GetByIdAsync(id);

            //create new resultmodel
            var projectResultModel = new ResultModel<Project>();

            //check if exists
            if (project == null)
            {
                projectResultModel.Success = false;
                projectResultModel.Errors = ["No project found!"];
                return projectResultModel;
            }

            //if exists
            projectResultModel.Success = true;
            projectResultModel.Value = project;
            return projectResultModel;
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
