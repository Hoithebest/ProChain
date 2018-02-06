using FluentAssertions;
using System;
using Utilities.Timing;
using Xunit;

namespace Utilities.Unit.Tests.Timing
{
    public class TimestampTests
    {
        [Fact]
        public void GetTimestamp_ReturnsCorrectTimestamp()
        {
            DateTime first = new DateTime(1970, 1, 1);
            DateTime second = new DateTime(2018, 2, 1);

            first.GetTimestamp().Should().Be(0);
            second.GetTimestamp().Should().Be(1517443200);
        }
    }
}
