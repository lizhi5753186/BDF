using Xunit;

namespace Bdf.Tests.Domain.Entities
{
    public class EntityTest
    {
        [Fact]
        public void Equality_Operator_Works()
        {
            var w1 = new Worker{Id = 1, Name = "Learninghard" };
            var w2 = new Worker { Id = 1, Name = "Learninghard" };
            Assert.True(w1 == w2, "Same class with same id must be equal");
            Assert.True(w1.Equals(w2), "Same class with same Id must be equal");

            var m1 = new Manager {Id = 1, Name = "Learninghard", Title = "Software Development"};
            Assert.True(m1 == w1, "Derived classes must be equal if their Ids are equal");

            var d1 = new Department { Id = 1, Name = "IVR" };

            Assert.False(m1 == d1, "Different classes must not be considered as equal even if their Ids are equal!");

            var w5 = w1;
            w5.Id = 2;

            Assert.True(w5 == w1, "Same object instance must be equal.");
        }
    }
}