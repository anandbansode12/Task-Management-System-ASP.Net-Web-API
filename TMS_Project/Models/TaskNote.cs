using System.ComponentModel.DataAnnotations;

namespace TMS_Project.Models
{
    public class TaskNote
    {
        [Key]
        public int NoteId { get; set; }
        public int TaskId { get; set; }
        public Task1 Task1 { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Note { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
