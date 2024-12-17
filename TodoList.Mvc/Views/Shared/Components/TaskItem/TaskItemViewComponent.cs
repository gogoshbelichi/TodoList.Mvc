using Microsoft.AspNetCore.Mvc;
using TodoList.Domain.Entities;

namespace TodoList.Mvc.Views.Shared.Components.TaskItem
{
    public class TaskItemViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(TaskApp task)
        {
            return View(task);
        }
    }
}
