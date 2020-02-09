using SULS.Models;
using System.Collections.Generic;

namespace SULS.Services.Submissions
{
    public interface ISubmissionService
    {
        void Create(Submission submission);

        void DeleteSubmissionById(string id);
        
        ICollection<Submission> GetSubmissionsByProblemName();
    }
}
