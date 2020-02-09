using SIS.MvcFramework.Attributes.Validation;

namespace SULS.App.ViewModels.Submissions
{
    public class SubmissionCreateInputModel
    {
        private const string CodeLengthErrorMessage = "Code must be teween 30 and 800 symbols!";


        [StringLengthSis(30, 800, CodeLengthErrorMessage)]
        public string Code { get; set; }

        public string ProblemId { get; set; }

    }
}
