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
        public string TechStack { get; set; }
        public string GitHub { get; set; }
        public ICollection<string> Images { get; set; }
    }
}
