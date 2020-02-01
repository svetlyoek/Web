using SIS.MvcFramework.Attributes.Validation;
using SULS.Models;
using System;

namespace SULS.App.ViewModels.Submissions
{
    public class SubmissionCreateInputModel
    {
        private const string CodeErrorMessage = "Code must be between 30 and 800 symbols!";
        private const string ResultErrorMessage = "Result must be between 0 and 300 symbols!";

        [RequiredSis]
        [StringLengthSis(30, 800, CodeErrorMessage)]
        public string Code { get; set; }

        //[RangeSis(0, 300, ResultErrorMessage)]
        //public int AchievedResult { get; set; }

        //public DateTime CreatedOn { get; set; }

        //public string Name { get; set; }

        //public int ProblemId { get; set; }


    }
}
