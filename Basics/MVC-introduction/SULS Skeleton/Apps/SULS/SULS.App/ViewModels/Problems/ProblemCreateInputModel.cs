using SIS.MvcFramework.Attributes.Validation;

namespace SULS.App.ViewModels.Problems
{
   public class ProblemCreateInputModel
    {
        private const string UsernameErrorMessage = "Username must be between 5 and 20 sybmols!";
        private const string PointsErrorMessage = "Points must be between 50 and 300!";

        [StringLengthSis(5,20,UsernameErrorMessage)]
        [RequiredSis]
        public string Name { get; set; }

        [RangeSis(50, 300, PointsErrorMessage)]
        [RequiredSis]
        public int Points { get; set; }
    }
}
