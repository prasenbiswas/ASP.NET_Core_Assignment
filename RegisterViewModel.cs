using System.ComponentModel.DataAnnotations;

namespace CoreAssignmentForRollBased.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password), ErrorMessage = " Password and confirmation password did not match")]
        public string ConfirmPassword { get; set; }
    }
}
