using CoreAssignmentForRollBased.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CoreAssignmentForRollBased.Data
{
    public class ApplicationUser : IdentityUser
    {
        public Profile Profile { get; set; }
    }
}
