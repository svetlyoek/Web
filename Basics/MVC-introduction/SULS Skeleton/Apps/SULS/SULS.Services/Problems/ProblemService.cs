using Microsoft.EntityFrameworkCore;
using SULS.Data;
using SULS.Models;
using System.Collections.Generic;
using System.Linq;

namespace SULS.Services
{
    public class ProblemService : IProblemService
    {
        private readonly SULSContext context;
        public ProblemService(SULSContext context)
        {
            this.context = context;
        }


        public void Create(Problem problem)
        {
            this.context.Problems.Add(problem);
            this.context.SaveChanges();

        }

        public ICollection<Problem> GetAllProblems()
        {
            return this.context.Problems.Include(s => s.Submissions).ToList();
        }

        public Problem GetProblemById(string id)
        {
            return this.context.Problems.FirstOrDefault(p => p.Id == id);
        }

        public Problem GetProblemSubmissionsById(string id)
        {
            return this.context.Problems.Where(p => p.Id == id).Include(s => s.Submissions).FirstOrDefault();
        }
    }
}
