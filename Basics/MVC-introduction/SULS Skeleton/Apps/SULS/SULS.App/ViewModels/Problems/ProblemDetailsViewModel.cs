using System.Collections.Generic;

namespace SULS.App.ViewModels.Problems
{
    public class ProblemDetailsViewModel
    {
        public string Name { get; set; }

        public ICollection<ProblemSubmissionsModel> Submissions { get; set; }

    }
}
