using Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Areas.Identity.Data
{
    public class BackendUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<TaskList> TaskLists { get; set; }
    }
}
