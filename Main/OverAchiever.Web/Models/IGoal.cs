namespace OverAchiever.Web.Models
{
    public interface IGoal
    {
        int Current { get; }

        int Desired { get; set; }

        bool Achieved { get; }

        string Name { get; set; }

        string Summary { get; set; }

        string Description { get; set; }
    }
}