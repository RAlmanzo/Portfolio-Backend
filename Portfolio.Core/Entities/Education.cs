using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Entities
{
    public class Education: BaseEntity
    {
        public string School {  get; set; }
        public string Training { get; set; }
        public string Degree { get; set; }
    }
}
