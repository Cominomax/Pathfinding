using Xunit;
using Pathfinding.Lib.Extensions;
using System.Linq;

namespace Pathfinding.Lib.UnitTests.Extensions
{
    public class IEnumerableExtensionsTests
    {
        [Fact]
        public void IEnumerable_ToCollectionStringShouldContainTheElementsOfTheIEnumerable()
        {
            //Arrange
            var testValues = Enumerable.Range(0, 10);

            //Act 
            var testListString = testValues.ToCollectionString();

            //Assert
            foreach (var item in testValues)
            {
                Assert.Contains(item.ToString(), testListString);
            }
        }
    }
}
