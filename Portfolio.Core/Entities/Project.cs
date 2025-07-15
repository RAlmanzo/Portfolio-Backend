using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Entities
{
    public class Project: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<string> FrontendTechStack { get; set; }
        public ICollection<string> BackendTechStack { get; set; }
        public string FrontendGitHubUrl { get; set; }
        public string BackendGitHubUrl { get; set; }
        public ICollection<string> ImagesPath { get; set; }
    }
}
