using System.ComponentModel.DataAnnotations;

namespace CoreAssignmentForRollBased.ViewModel
{
    public class UserViewModel
    {
        [Key]
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}
