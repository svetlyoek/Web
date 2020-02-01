using SULS.Models;
using System.Collections.Generic;

namespace SULS.Services
{
    public interface IProblemService
    {
        Problem Create(Problem problem);

        ICollection<Problem> GetProblems();

        Problem GetProblem();
    }
}
