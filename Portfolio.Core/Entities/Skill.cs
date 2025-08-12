using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Entities
{
    public class Skill
    {
        public required string Name { get; set; }
        public required string Category { get; set; }
        public int Proficiency { get; set; }
    }
}
