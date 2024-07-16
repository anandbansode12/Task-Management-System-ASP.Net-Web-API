using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS_Project.Models
{
    public class Task1
    {
        [Key]
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AssignedTo { get; set; }
        [ForeignKey("AssignedUserUserId")]
        public User AssignedUser { get; set; }
        public int CreatedBy { get; set; }
        public User Creator { get; set; }
        public string Status { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedDate { get; set; }

        // Foreign key properties
        public int AssignedUserUserId { get; set; }
        public int CreatorUserId { get; set; }

        // Navigation properties

        [ForeignKey("CreatorUserId")]
        public User CreatorUser { get; set; }
    }
}
