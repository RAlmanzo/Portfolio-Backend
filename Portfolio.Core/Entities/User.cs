using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public string Nationality { get; set; }
        public string DriverLicense { get; set; }
        public string Residence { get; set; }
        public string GitHub {  get; set; }
        public string Linkedin { get; set; }
        public ICollection<string> Images { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<string> Languages { get; set; }
        public ICollection<Education> Educations { get; set; }
    }
}
