using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public string DriverLicense { get; set; } = string.Empty;
        public string Residence { get; set; } = string.Empty;
        public string GitHub { get; set; } = string.Empty;
        public string Linkedin { get; set; } = string.Empty;
        public string ResumeId { get; set; } = string.Empty;
        public ICollection<string> Images { get; set; } = [];
        public ICollection<Project> Projects { get; set; } = [];
        public ICollection<string> Languages { get; set; } = [];
    }
}
