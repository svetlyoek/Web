using SULS.Models;
using System.Collections.Generic;

namespace SULS.Services
{
    public interface IProblemService
    {
        void Create(Problem problem);

        Problem GetProblemById(string id);

        Problem GetProblemSubmissionsById(string id);

        ICollection<Problem> GetAllProblems();

    }
}
