using System;

namespace OverAchiever.Domain
{
    public interface IGoal
    {
        int Desired { get; set; }

        int Last { get; set; }

        Uri ServiceUri { get; set; }

        string Name { get; set; }

        string Summary { get; set; }

        string Description { get; set; }
    }
}