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
    public class EducationService : IEducationService
    {
        public Task<ResultModel<Education>> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ResultModel<Education>> CreateEducationAsync(EducationCreateRequestModel EducationCreateRequestModel)
        {
            throw new NotImplementedException();
        }

        public Task<ResultModel<Education>> DeleteEducationAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ResultModel<IEnumerable<Education>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResultModel<Education>> UpdateEducationAsync(EducationtUpdateRequestModel EducationUpdateRequestModel)
        {
            throw new NotImplementedException();
        }
    }
}
