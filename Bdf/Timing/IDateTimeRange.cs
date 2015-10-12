using System;

namespace Bdf.Timing
{
    public interface IDateTimeRange
    {
        DateTime StartTime { get; set; }

        DateTime EndTime { get; set; }
    }
}
