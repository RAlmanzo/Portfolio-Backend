using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Services.Models
{
    public class ProjectCreateRequestModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> FrontendTechStack { get; set; }
        public IEnumerable<string> BackendTechStack { get; set; }
        public string FrontendGitHubUrl { get; set; }
        public string BackendGitHubUrl { get; set; }
        public IEnumerable<string> ImagesPath { get; set; }

    }
}
