using FluentAssertions;

namespace FluentAssertion_Demo
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            string actual = "ABCDEFGHI";
            actual.Should().StartWith("AB")
                .And
                .EndWith("GHI")
                .And
                .Contain("F")
                .And.HaveLength(9);
        }

        [Fact]
        public void TestWithArrayFail()
        {
            IEnumerable<int> numbers = [1,2,3];

            numbers.Should().OnlyContain(n => n > 0);
            numbers.Should().HaveCount(4, "because we thought we put four items in the collection");
        }
    }
}