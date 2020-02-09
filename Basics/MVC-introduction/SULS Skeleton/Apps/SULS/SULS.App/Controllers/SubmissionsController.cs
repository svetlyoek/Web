using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;
using SULS.App.ViewModels.Submissions;
using SULS.Models;
using SULS.Services;
using SULS.Services.Submissions;
using System;

namespace SULS.App.Controllers
{
    public class SubmissionsController : Controller
    {
        private readonly ISubmissionService submissionService;
        private readonly IProblemService problemService;
        public SubmissionsController(ISubmissionService service, IProblemService problemService)
        {
            this.submissionService = service;
            this.problemService = problemService;
        }

        [Authorize]
        public IActionResult Create(string id)
        {
            var problem = this.problemService.GetProblemById(id);
            var problemViewModel = new SubmissionViewModel
            {
                Name = problem.Name,
                ProblemId = problem.Id
            };

            return this.View(problemViewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(SubmissionCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Submissions/Create");
            }

            Submission submission = new Submission
            {
                UserId = this.User.Id,
                CreatedOn = DateTime.UtcNow,
                Code = input.Code,
                ProblemId = input.ProblemId,
                AchievedResult = new Random().Next(0, 300)
            };

            this.submissionService.Create(submission);

            return this.Redirect("/Home/IndexLoggedIn");
        }

        [Authorize]
        public IActionResult Delete(string id)
        {
            this.submissionService.DeleteSubmissionById(id);

            return this.Redirect("/Home/IndexLoggedIn");
        }

    }
}
