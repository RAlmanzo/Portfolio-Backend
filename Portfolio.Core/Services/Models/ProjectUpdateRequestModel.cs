using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Services.Models
{
    public class ProjectUpdateRequestModel : ProjectCreateRequestModel
    {
        public string Id { get; set; }
    }
}
