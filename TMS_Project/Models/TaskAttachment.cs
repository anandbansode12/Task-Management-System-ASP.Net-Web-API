using System.ComponentModel.DataAnnotations;

namespace TMS_Project.Models
{
    public class TaskAttachment
    {
        [Key]
        public int AttachmentId { get; set; }
        public int TaskId { get; set; }
        public Task1 Task1 { get; set; }
        public string FilePath { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
