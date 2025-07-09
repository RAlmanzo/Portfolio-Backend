using Portfolio.Core.Entities;
using Portfolio.Core.Interfaces.Services;
using Portfolio.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Infrastructure.Services
{
    public class UserService : IUserService
    {
        public Task<ResultModel<User>> CreateUserAsync(UserCreateRequestModel UserCreateRequestModel)
        {
            throw new NotImplementedException();
        }

        public Task<ResultModel<User>> DeleteUserAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ResultModel<IEnumerable<User>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResultModel<User>> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ResultModel<User>> UpdateUserAsync(UserUpdateRequestModel UserUpdateRequestModel)
        {
            throw new NotImplementedException();
        }
    }
}
