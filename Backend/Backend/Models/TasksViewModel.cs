using System.Collections.Generic;

namespace Backend.Models
{
    public class TasksViewModel
    {
        public List<TaskList> Tasks { get; set; }
        public AddTaskViewModel AddTaskViewModel { get; set; }
    }
}
