using SULS.Data;
using SULS.Models;
using System.Collections.Generic;
using System.Linq;

namespace SULS.Services.Submissions
{
    public class SubmissionService : ISubmissionService
    {
        private readonly SULSContext context;

        public SubmissionService(SULSContext context)
        {
            this.context = context;
        }

        public void Create(Submission submission)
        {
            this.context.Submissions.Add(submission);

            this.context.SaveChanges();

        }

        public void DeleteSubmissionById(string id)
        {
            var submission = this.context.Submissions.FirstOrDefault(s => s.Id == id);

            this.context.Submissions.Remove(submission);

            this.context.SaveChanges();
        }

        public ICollection<Submission> GetSubmissionsByProblemName()
        {
            var submissions = this.context.Submissions.ToList();

            return submissions;
        }

        
    }
}
