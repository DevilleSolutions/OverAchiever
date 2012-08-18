using OverAchiever.Web.Models.Calculators;

namespace OverAchiever.Web.Models
{
    public interface IGoal
    {
        int Desired { get; }

        IGoalCalculator Calculator { get; }

        string Name { get; set; }

        string Summary { get; set; }

        string Description { get; set; }
    }
}