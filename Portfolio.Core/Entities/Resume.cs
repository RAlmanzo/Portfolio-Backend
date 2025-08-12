using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Entities
{
    public class Resume : BaseEntity
    {
        public required string UserId { get; set; }
        public ICollection<Skill> Skills { get; set; } = [];
        public ICollection<Education> Educations { get; set; } = [];
        public ICollection<Experience> Experiences { get; set; } = [];
    }
}
