using Portfolio.Core.Entities;
using Portfolio.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<ResultModel<User>> GetByIdAsync(string id);
        Task<ResultModel<IEnumerable<User>>> GetAllAsync();
        Task<ResultModel<User>> CreateUserAsync(UserCreateRequestModel UserCreateRequestModel);
        Task<ResultModel<User>> UpdateUserAsync(UserUpdateRequestModel UserUpdateRequestModel);
        Task<ResultModel<User>> DeleteUserAsync(string id);
    }
}
