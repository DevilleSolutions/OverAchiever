namespace OverAchiever.Web.Models.Factories
{
    public interface IGoalFactory
    {
        IGoal Create(int desired);

        void Release(IGoal goal);
    }
}