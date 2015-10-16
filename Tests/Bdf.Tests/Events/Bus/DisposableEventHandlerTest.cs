using Xunit;

namespace Bdf.Tests.Events.Bus
{
    public class DisposableEventHandlerTest : EventBusTestBase
    {
        [Fact]
        public void Should_Call_Handler_And_Dispose()
        {
            EventBus.Register<MySimpleEventData, MySimpleEventHandler>();

            EventBus.Trigger(new MySimpleEventData { Value = 1});
            EventBus.Trigger(new MySimpleEventData { Value = 2 });
            EventBus.Trigger(new MySimpleEventData { Value = 3 });

            Assert.Equal(MySimpleEventHandler.HandleCount, 3);
            Assert.Equal(MySimpleEventHandler.DisposeCount, 3);
        }
    }
}