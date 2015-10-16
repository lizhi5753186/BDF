using Bdf.Configuration;
using Shouldly;
using Xunit;

namespace Bdf.Tests.Configuration
{
    public class DictionaryBasedConfigTest
    {
        private readonly MyTestConfig _config;

        public DictionaryBasedConfigTest()
        {
            _config = new MyTestConfig();
        }

        [Fact]
        public void Should_Get_Value()
        {
            var testObject = new TestClass {Value = 12};
            _config["IntValue"] = 12;
            _config["StringValue"] = "Test string";
            _config["ObjectValue"] = testObject;

            _config["IntValue"].ShouldBe(12);
            _config.Get<int>("IntValue").ShouldBe(12);

            _config["StringValue"].ShouldBe("Test string");
            _config.Get<string>("StringValue").ShouldBe("Test string");

            _config["ObjectValue"].ShouldBeSameAs(testObject);
            _config.Get<TestClass>("ObjectValue").ShouldBeSameAs(testObject);
            _config.Get<TestClass>("ObjectValue").Value.ShouldBe(12);
        }


        [Fact]
        public void Should_Get_Default_If_No_Value()
        {
            _config["MyUndefinedName"].ShouldBeNull();
            _config.Get<string>("MyUndefinedName").ShouldBeNull();
            _config.Get<MyTestConfig>("MyUndefinedName").ShouldBeNull();
            _config.Get<int>("MyUndefinedName").ShouldBe(0);
        }

        private class MyTestConfig : DictionayBasedConfig
        {

        }

        private class TestClass
        {
            public int Value { get; set; }
        }
    }
}

