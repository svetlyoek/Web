using SULS.App.ViewModels.Submissions;
using SULS.Models;
using System;
using System.Collections.Generic;

namespace SULS.App.ViewModels.Problems
{
    public class ProblemDetailsModel
    {
       public string Id { get; set; }

        public string Name { get; set; }

        public User Username { get; set; }
       
        public ICollection<SubmissionDetailsModel> Submissions { get; set; }

    }
}
