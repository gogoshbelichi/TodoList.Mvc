using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TodoList.Mvc.Controllers
{
    public abstract class TodoBaseController : Controller
    {
        protected int CurrentUserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
