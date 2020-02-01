using Microsoft.EntityFrameworkCore;
using SULS.Data;
using SULS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SULS.Services
{
    public class ProblemService : IProblemService
    {
        private readonly SULSContext context;
        public ProblemService(SULSContext context)
        {
            this.context = context;
        }
        public Problem Create(Problem problem)
        {
            this.context.Problems.Add(problem);
            this.context.SaveChanges();

            return problem;
        }

        public Problem GetProblem()
        {
            return this.context.Problems.Include(s=>s.Submissions).FirstOrDefault();
        }

        public ICollection<Problem> GetProblems()
        {
            return this.context.Problems.Include(s=>s.Submissions).Select(p => p).ToList();
        }
    }
}
