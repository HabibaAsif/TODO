using Backend.Areas.Identity.Data;

namespace Backend.Models
{
    public class TaskList
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public bool Status { get; set; }
        public string UserId { get; set; }
        public BackendUser User { get; set; }
    }
}
