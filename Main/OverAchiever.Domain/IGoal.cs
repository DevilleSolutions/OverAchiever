using System.Collections.Generic;

namespace OverAchiever.Domain
{
    public interface IGoal
    {
        IEnumerable<int> Milestones { get; }

        int GetCurrent();
    }
}