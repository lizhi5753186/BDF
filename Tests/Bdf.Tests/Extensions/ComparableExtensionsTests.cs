using System;
using Bdf.Extensions;
using Shouldly;
using Xunit;

namespace Bdf.Tests.Extensions
{
    public class ComparableExtensionsTests
    {
        [Fact]
        public void IsBetween_Test()
        {
            const int number = 5;
            number.IsBetween(1, 10).ShouldBeTrue();
            number.IsBetween(1, 5).ShouldBeTrue();
            number.IsBetween(5,10).ShouldBeTrue();
            number.IsBetween(10,20).ShouldBeFalse();

            //DateTime
            var dateTimeValue = new DateTime(2014, 10, 4, 18, 20, 42, 0);
            dateTimeValue.IsBetween(new DateTime(2014, 1, 1), new DateTime(2015, 1, 1)).ShouldBe(true);
            dateTimeValue.IsBetween(new DateTime(2015, 1, 1), new DateTime(2016, 1, 1)).ShouldBe(false);
        }
    }
}