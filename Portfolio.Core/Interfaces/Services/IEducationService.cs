using Portfolio.Core.Entities;
using Portfolio.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces.Services
{
    public interface IEducationService
    {
        Task<ResultModel<Education>> GetByIdAsync(string id);
        Task<ResultModel<IEnumerable<Education>>> GetAllAsync();
        Task<ResultModel<Education>> CreateEducationAsync(EducationCreateRequestModel EducationCreateRequestModel);
        Task<ResultModel<Education>> UpdateEducationAsync(EducationtUpdateRequestModel EducationUpdateRequestModel);
        Task<ResultModel<Education>> DeleteEducationAsync(string id);
    }
}
