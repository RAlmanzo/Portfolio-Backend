using Portfolio.Core.Entities;
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

        public Task<ResultModel<Project>> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ResultModel<Project>> UpdateProjectAsync(ProjectUpdateRequestModel ProjectUpdateRequestModel)
        {
            throw new NotImplementedException();
        }
    }
}
