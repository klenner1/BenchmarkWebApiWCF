using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class ServiceController : Controller
    {
        public async ValueTask<IActionResult> Index()
        {
            return await Task.FromResult<IActionResult>(Ok());
        }

    }
}
