using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    public class TasksController : Controller
    {
        private readonly BackendContext _context;
        private readonly UserManager<BackendUser> _userManager;

        public TasksController(BackendContext context, UserManager<BackendUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Display the list of tasks
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var tasks = await _context.UserTasks
                .Where(t => t.UserId == user.Id)
                .ToListAsync();

            var viewModel = new TasksViewModel
            {
                Tasks = tasks,
                AddTaskViewModel = new AddTaskViewModel() // Initialize empty AddTaskViewModel
            };

            return View(viewModel);
        }

        // GET: Display the page for adding a new task
        [HttpGet]
        public IActionResult Add()
        {
            var viewModel = new AddTaskViewModel();
            return View(viewModel);
        }

        // POST: Handle adding a new task
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddTaskViewModel viewModel)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                var task = new TaskList
                {
                    Title = viewModel.Title,
                    Status = false, // Assuming new tasks are not completed by default
                    UserId = user.Id,
                    ID = Guid.NewGuid()
                };

                _context.UserTasks.Add(task);
                await _context.SaveChangesAsync();

                return RedirectToAction("List");
            }

            return View(viewModel); // Return to the Add view if the model state is invalid
        }

        // POST: Delete a task
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var task = await _context.UserTasks
                .FirstOrDefaultAsync(t => t.ID == id && t.UserId == user.Id);

            if (task == null)
            {
                return NotFound();
            }

            _context.UserTasks.Remove(task);
            await _context.SaveChangesAsync();

            return RedirectToAction("List");
        }

        // GET: Edit a task
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var task = await _context.UserTasks
                .FirstOrDefaultAsync(t => t.ID == id && t.UserId == user.Id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Update a task
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TaskList viewModel)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var task = await _context.UserTasks
                .FirstOrDefaultAsync(t => t.ID == viewModel.ID && t.UserId == user.Id);

            if (task == null)
            {
                return NotFound();
            }

            task.Title = viewModel.Title;
            task.Status = viewModel.Status;

            _context.Update(task);
            await _context.SaveChangesAsync();

            return RedirectToAction("List");
        }
    }
}
