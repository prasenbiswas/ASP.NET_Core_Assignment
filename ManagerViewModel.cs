using System.ComponentModel.DataAnnotations;

namespace CoreAssignmentForRollBased.ViewModel
{
    public class ManagerViewModel
    {
        [Key]
        public string ManagerId { get; set; }
        public string ManagerName { get; set; }
        public string ManagerEmail { get; set; }
        public string ManagerRole { get; set; }
    }
}
