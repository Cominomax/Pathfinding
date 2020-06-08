using System;
using System.Collections.Generic;
using Xunit;
using Pathfinding.Lib.Extensions;

namespace Pathfinding.Lib.UnitTests.Extensions
{
    public class ICollectionExtensionsTests
    {
        [Fact]
        public void ICollection_PopMinShouldGetTheMinimumValueAndRemoveItFromTheList()
        {
            //Arrange
            var minValue = 0;
            var testList = new List<int>() { 1, 5, minValue, 4, 3, 10, 8, minValue, 4, 6, 2 };
            var countBeforeTest = testList.Count;

            //Act
            var value = testList.PopMin();

            //Assert
            Assert.Equal(minValue, value);
            Assert.Equal(countBeforeTest-1, testList.Count);
        }

        [Fact]
        public void ICollection_PopMaxShouldGetTheMinimumValueAndRemoveItFromTheList()
        {
            //Arrange
            var maxValue = int.MaxValue;
            var testList = new List<int>() { 1, 5, 4, maxValue, 3, 10, 8, 4, maxValue, 2 };
            var countBeforeTest = testList.Count;

            //Act
            var value = testList.PopMax();

            //Assert
            Assert.Equal(maxValue, value);
            Assert.Equal(countBeforeTest-1, testList.Count);
        }
    }
}
