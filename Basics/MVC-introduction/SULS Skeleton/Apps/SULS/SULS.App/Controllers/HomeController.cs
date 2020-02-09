using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Mapping;
using SIS.MvcFramework.Result;
using SULS.App.ViewModels.Problems;
using SULS.Services;
using System.Linq;

namespace SULS.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProblemService service;

        public HomeController(IProblemService service)
        {
            this.service = service;
        }

        [HttpGet(Url = "/")]
        public IActionResult IndexSlash()
        {
            return this.Index();
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult IndexLoggedIn()
        {
            var problems = this.service.GetAllProblems().Select(p => new ProblemHomeDetailsModel
            {
                Id = p.Id,
                Name = p.Name,
                Count = p.Submissions.Count

            })
            .ToList();

            return this.View(problems);
        }

    }
}