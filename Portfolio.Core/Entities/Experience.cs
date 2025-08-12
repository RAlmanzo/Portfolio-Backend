using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Entities
{
    public class Experience : BaseEntity
    {
        public required string Location { get; set; }
        public required string Company { get; set; }
        public required DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public required string Position { get; set; }
        public string? Information { get; set; }
    }
}
