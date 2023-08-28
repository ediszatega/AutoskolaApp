using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Core.Models
{
    public enum Role
    {
        Admin,
        Customer,
        Employee,
        Lecturer,
        Instructor
    }
    public class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public bool EmailVerified { get; set; } = false;
        public string? PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Role Role { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Token { get; set; }

        [ForeignKey(nameof(City))]
        public int CityId { get; set; }
        public virtual City? City { get; set; }
        public string? ProfileImage { get; set; }
        public bool IsActive { get; set; }

        public User()
        {
            IsActive = true;
        }
    }
}
