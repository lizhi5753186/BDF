using System;
using Bdf.Reflection;
using Shouldly;
using Xunit;

namespace Bdf.Tests.Reflection
{
    public class TypeHelperTests
    {
        [Fact]
        public void Test_IsFunc()
        {
            TypeHelper.IsFunc(new Func<object>(() => 42)).ShouldBe(true);
            TypeHelper.IsFunc(new Func<int>(() => 42)).ShouldBe(true);
            TypeHelper.IsFunc(new Func<string>(() => "12")).ShouldBe(true);

            TypeHelper.IsFunc("12").ShouldBe(false);
        }

        [Fact]
        public void Test_IsFuncOfTReturn()
        {
            TypeHelper.IsFunc<object>(new Func<object>(() => 42)).ShouldBe(true);
            TypeHelper.IsFunc<object>(new Func<int>(() => 42)).ShouldBe(false);
            TypeHelper.IsFunc<string>(new Func<string>(() => "12")).ShouldBe(true);

            TypeHelper.IsFunc("12").ShouldBe(false);
        }

        [Fact]
        public void Test_IsPrimitiveExtendedIncludingNullable()
        {
            TypeHelper.IsPrimitiveExtendedIncludingNullable(typeof(int)).ShouldBe(true);
            TypeHelper.IsPrimitiveExtendedIncludingNullable(typeof(int?)).ShouldBe(true);

            TypeHelper.IsPrimitiveExtendedIncludingNullable(typeof(Guid)).ShouldBe(true);
            TypeHelper.IsPrimitiveExtendedIncludingNullable(typeof(Guid?)).ShouldBe(true);

            TypeHelper.IsPrimitiveExtendedIncludingNullable(typeof(string)).ShouldBe(true);

            TypeHelper.IsPrimitiveExtendedIncludingNullable(typeof(TypeHelperTests)).ShouldBe(false);
        } 
    }
}