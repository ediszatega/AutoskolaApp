using Autoskola.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Core.ViewModels
{
    public class CustomerGetVM
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public bool EmailVerified { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Role { get; set; } = Models.Role.Customer.ToString();
        public string? Username { get; set; }
        public virtual City? City { get; set; }
        public string? ProfileImage { get; set; }
        public bool IsActive { get; set; }
    }
}
