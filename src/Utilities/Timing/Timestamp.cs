using System;

namespace Utilities.Timing
{
    public static class Timestamp
    {
        public static long GetTimestamp(this DateTime on)
        {
            return (int)on.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}
