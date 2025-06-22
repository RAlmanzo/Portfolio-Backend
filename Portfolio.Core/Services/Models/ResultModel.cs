using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Services.Models
{
    public class ResultModel<T>
    {
        public bool Success { get; set; }
        public IEnumerable<string>? Errors { get; set; }
        public T? Value { get; set; }
    }
}
