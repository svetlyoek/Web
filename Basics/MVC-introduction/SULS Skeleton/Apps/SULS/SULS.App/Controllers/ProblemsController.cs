using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Mapping;
using SIS.MvcFramework.Result;
using SULS.App.ViewModels.Problems;
using SULS.Models;
using SULS.Services;
using SULS.Services.Users;
using System.Linq;

namespace SULS.App.Controllers
{
    public class ProblemsController : Controller
    {
        private readonly IProblemService problemService;
        private readonly IUserService userService;
        public ProblemsController(IProblemService problemService, IUserService userService)
        {
            this.problemService = problemService;
            this.userService = userService;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(ProblemCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Problems/Create");
            }

            Problem problem = ModelMapper.ProjectTo<Problem>(model);

            this.problemService.Create(problem);

            return this.Redirect("/Home/IndexLoggedIn");
        }

        [Authorize]
        public IActionResult Details(string id)
        {
            var problem = this.problemService.GetProblemSubmissionsById(id);

            var problemToProject = new ProblemDetailsViewModel
            {
                Name = problem.Name,
                Submissions = problem.Submissions
                .Select(s => new ProblemSubmissionsModel
                {
                    Username = this.userService.GetUsernameById(s.UserId),
                    CreatedOn = s.CreatedOn,
                    AchievedResult = s.AchievedResult,
                    MaxPoints = problem.Points,
                    SubmissionId = s.Id

                })
                .ToList()

            };

            return this.View(problemToProject);
        }

    }
}
