
using CoreAssignmentForRollBased.Data;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreAssignmentForRollBased.Models
{
    public class Profile
    {
        [Key]
        public int ProfileId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }    
        [Required]
        public string MobileNumber { get; set; }
        [Required]
        public string Address { get; set; }
        public string ProfilePhoto { get; set; }
        public string ApplicatioUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}
