using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Domain;
using TodoList.Domain.Entities;
using TodoList.Models;
using TodoList.Models.Home;
using TodoList.Mvc.Controllers;

namespace TodoList.Controllers
{
    [Authorize]
    public class HomeController : TodoBaseController
    {
        private readonly TodoListContext _context;

        public HomeController(TodoListContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            return View(new HomeViewModel
            {
                Tasks = await GetTasksCurrentUserAsync()
            });
        }

        public async Task<IActionResult> CreateAsync(HomeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Tasks = await GetTasksCurrentUserAsync();
                return View("Index", model);
            }

            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Name.ToLower() == model.TaskName.ToLower()
            && t.UserId == CurrentUserId
            && t.ExpiredDate == model.DateTime);

            if (task != null) {
                model.Tasks = await GetTasksCurrentUserAsync();
                ViewBag.Error = "Taкая задача уже существует!";

                return View("Index", model);
            }

            await _context.Tasks.AddAsync(new TaskApp
            {
                Name = model.TaskName,
                ExpiredDate = model.DateTime.Value,
                UserId = CurrentUserId
            });

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateCompletedAsync(int id, bool isCompleted)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            if (task != null)
            {
                task.IsCompleted = isCompleted;
                _context.Tasks.Update(task);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateNameAsync(int id, string name)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            if (task != null)
            {
                if (task.Name != name)
                {
                    task.Name = name;
                    _context.Tasks.Update(task);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        private async Task<IEnumerable<TaskApp>> GetTasksCurrentUserAsync()
        {
            return await _context.Tasks
                .Where(t => t.UserId == CurrentUserId)
                .ToListAsync();
        }

        [AllowAnonymous]
        [Route("/NotFound")]
        public IActionResult PageNotFound()
        {
            return View();
        }

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        //public IActionResult Index()
        //{
        //    return View();
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
