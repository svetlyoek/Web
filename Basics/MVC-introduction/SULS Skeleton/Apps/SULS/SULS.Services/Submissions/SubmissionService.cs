using SULS.Data;
using SULS.Models;

namespace SULS.Services.Submissions
{
    public class SubmissionService : ISubmissionService
    {
        private readonly SULSContext context;

        public SubmissionService(SULSContext context)
        {
            this.context = context;
        }
        public Submission Create(Submission submission)
        {
            this.context.Submissions.Add(submission);
            this.context.SaveChanges();

            return submission;
        }
    }
}
