using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace CoreAssignmentForRollBased.ViewModel
{
    public class ProfileViewModel
    {
        [Key]
        public int ProfileId { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        [DisplayName("Mobile Number")]
        public string MobileNumber { get; set; }
        [Required]
        [DisplayName("Address")]
        public string Address { get; set; }
        public string ApplicatioUserId { get; set; }
        [Required]
        [DisplayName("Profile Photo")]
        public IFormFile ProfilePhoto { get; set; }
    }
}
