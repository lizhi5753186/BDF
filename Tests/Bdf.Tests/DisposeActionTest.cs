using Shouldly;
using Xunit;

namespace Bdf.Tests
{
    public class DisposeActionTest
    {
        [Fact]
        public void Should_Call_Action_When_Disposed()
        {
            var actionIsCalled = false;

            using (new DisposeAction(() => actionIsCalled = true))
            {

            }

            actionIsCalled.ShouldBe(true);
        } 
    }
}