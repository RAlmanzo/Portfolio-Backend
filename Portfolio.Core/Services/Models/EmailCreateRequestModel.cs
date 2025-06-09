using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Services.Models
{
    public class EmailCreateRequestModel
    {
        public required string From { get; set; }
        public required string Subject { get; set; }
        public required string Message { get; set; }
    }
}
