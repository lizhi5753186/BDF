using System;
using Bdf.Extensions;
using Shouldly;
using Xunit;

namespace Bdf.Tests.Extensions
{
    public class ObjectExtensionsTests
    {
        [Fact]
        public void As_Test()
        {
            var obj = (object)new ObjectExtensionsTests();
            obj.As<ObjectExtensionsTests>().ShouldNotBe(null);

            obj = null;
            obj.As<ObjectExtensionsTests>().ShouldBe(null);
        }

        [Fact]
        public void To_Tests()
        {
            "12".To<int>().ShouldBeOfType<int>().ShouldBe(12);
            "12".To<Int32>().ShouldBeOfType<Int32>().ShouldBe(12);
            "28173829281734".To<long>().ShouldBeOfType<long>().ShouldBe(28173829281734);
            "28173829281734".To<Int64>().ShouldBeOfType<Int64>().ShouldBe(28173829281734);

            "false".To<bool>().ShouldBeOfType<bool>().ShouldBe(false);
            "True".To<bool>().ShouldBeOfType<bool>().ShouldBe(true);

            Assert.Throws<FormatException>(() => "test".To<bool>());
            Assert.Throws<FormatException>(() => "test".To<int>());
        }

        [Fact]
        public void IsIn_Test()
        {
            5.IsIn(1, 3, 5, 7).ShouldBe(true);
            6.IsIn(1, 3, 5, 7).ShouldBe(false);

            int? number = null;
            number.IsIn(2, 3, 5).ShouldBe(false);

            var str = "a";
            str.IsIn("a", "b", "c").ShouldBe(true);

            str = null;
            str.IsIn("a", "b", "c").ShouldBe(false);
        } 
    }
}