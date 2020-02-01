using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Mapping;
using SIS.MvcFramework.Result;
using SULS.App.ViewModels.Problems;
using SULS.Models;
using SULS.Services;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SULS.App.Controllers
{
    public class ProblemsController : Controller
    {
        private readonly IProblemService service;

        public ProblemsController(IProblemService service)
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
        public IActionResult Create(ProblemCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Problems/Create");
            }

            Problem problem = ModelMapper.ProjectTo<Problem>(model);

            this.service.Create(problem);

            return this.Redirect("/");
        }

        //public IActionResult All()
        //{
        //    ICollection<Problem> problems = this.service.GetProblems();

        //    if (problems.Count != 0)
        //    {
        //        return this.View(problems.Select(ModelMapper.ProjectTo<ProblemDetailsModel>).ToList());
        //    }

        //    return this.View(new List<ProblemDetailsModel>());
        //}

        public IActionResult Details()
        {
            var problem = this.service.GetProblem();

            ProblemDetailsModel problemModel = ModelMapper.ProjectTo<ProblemDetailsModel>(problem);

            if (problem == null)
            {
                return this.Redirect("/");
            }

            return this.View(problemModel);
        }
    }
}
