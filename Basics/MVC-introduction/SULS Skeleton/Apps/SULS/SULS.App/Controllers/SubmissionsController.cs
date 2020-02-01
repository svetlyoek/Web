using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Mapping;
using SIS.MvcFramework.Result;
using SULS.App.ViewModels.Submissions;
using SULS.Models;
using SULS.Services.Submissions;
using System;

namespace SULS.App.Controllers
{
   public class SubmissionsController:Controller
    {
        private readonly ISubmissionService service;

        public SubmissionsController(ISubmissionService service)
        {
            this.service = service;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(SubmissionCreateInputModel model)
        {
            if(!this.ModelState.IsValid)
            {
                return this.Redirect("/Submissions/Create");
            }

            Submission submission = ModelMapper.ProjectTo<Submission>(model);
            //Random random = new Random();
            //submission.AchievedResult = random.Next(0, submission.Problem.Points);
            //submission.CreatedOn = DateTime.UtcNow;
            this.service.Create(submission);

            return this.Redirect("/");
        }
    }
}
