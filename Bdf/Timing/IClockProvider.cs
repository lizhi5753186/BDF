using System;

namespace Bdf.Timing
{
    public interface IClockProvider
    {
        DateTime Now { get; }

        DateTime Normalize(DateTime dateTime);
    }
}
