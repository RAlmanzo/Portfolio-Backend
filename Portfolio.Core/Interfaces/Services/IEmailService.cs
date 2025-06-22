using Portfolio.Core.Entities;
using Portfolio.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces.Services
{
    public interface IEmailService
    {
        Task<ResultModel<string>> SendEmailAsync(EmailCreateRequestModel emailCreateRequestModel);
    }
}
