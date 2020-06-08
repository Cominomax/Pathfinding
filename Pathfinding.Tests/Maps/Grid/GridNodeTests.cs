using Xunit;
using Pathfinding.Lib.Maps;
using Pathfinding.Lib.Maps.Grid;
using Pathfinding.Lib.UnitTests.Maps.Grid;

namespace Pathfinding.Lib.Tests.Maps.Grid
{
    public class GridNodeTests
    {
        [Fact]
        public void GridNode_ShouldCreateWithDefaultDirectionOrigin()
        {
            //Arrange
            int xTestValue = 15, yTestValue = 12;
            GridNode node;
            //Act
            node = new GridNode(xTestValue, yTestValue);
            //Assert
            Assert.Equal(xTestValue, node.X);
            Assert.Equal(yTestValue, node.Y);
            Assert.Equal(DirectionEnum.Origin, node.Direction);
        }

        [Fact]
        public void GridNode_ShouldCreateWithParameters()
        {
            //Arrange
            int xTestValue = 15, yTestValue = 12;
            DirectionEnum directionTestValue = DirectionEnum.Right | DirectionEnum.Up;
            GridNode node;
            //Act
            node = new GridNode(xTestValue, yTestValue, directionTestValue);
            //Assert
            Assert.Equal(xTestValue, node.X);
            Assert.Equal(yTestValue, node.Y);
            Assert.Equal(directionTestValue, node.Direction);
        }

        [Fact]
        public void GridNode_SettingParentShouldAlsoSetTheDistanceFromOriginBidirectional()
        {
            //Arrange
            var node = new GridNode(16, 12, DirectionEnum.Up | DirectionEnum.Right);
            var parent = new GridNode(15, 13);
            //Act
            node.SetParent(parent);
            //Assert
            Assert.Equal(parent, node.Parent);
            Assert.Equal(DistanceConstants.BidirectionalMove, node.DistanceFromOrigin);
        }

        [Fact]
        public void GridNode_SettingParentShouldAlsoSetTheDistanceFromOriginMonodirectional()
        {
            //Arrange
            var node = new GridNode(15, 12, DirectionEnum.Up);
            var parent = new GridNode(15, 13);
            //Act
            node.SetParent(parent);
            //Assert
            Assert.Equal(parent, node.Parent);
            Assert.Equal(DistanceConstants.MonodirectionalMove, node.DistanceFromOrigin);
        }

        [Fact]
        public void GridNode_CannotBeEqualToNull()
        {
            //Arrange
            var node = new GridNode(15, 12);
            //Act
            var isEqual = node.Equals(null);
            //Assert
            Assert.False(isEqual);
        }

        [Fact]
        public void GridNode_CannotBeEqualToOtherINodeImplementation()
        {
            //Arrange
            var node = new GridNode(15, 12);
            //Act
            var isEqual = node.Equals(new MockINode());
            //Assert
            Assert.False(isEqual);
        }

        [Fact]
        public void GridNode_EqualityOnlyTestsPosition()
        {
            //Arrange
            var node = new GridNode(15, 12);
            var otherNode = new GridNode(15, 12, DirectionEnum.Up);
            otherNode.SetParent(new GridNode(15, 13));
            otherNode.SetF(new GridNode(100, 113));
            //Act
            var isEqual = node.Equals(otherNode);
            //Assert
            Assert.True(isEqual);
        }

        [Fact]
        public void GridNode_CannotBeLowerThanNull()
        {
            //Arrange
            var node = new GridNode(15, 12);
            //Act
            var comparison = node.CompareTo(null);
            //Assert
            Assert.Equal(1, comparison);
        }

        [Fact]
        public void GridNode_CannotBeLowerThanOtherINodeImplementation()
        {
            //Arrange
            var node = new GridNode(15, 12);
            //Act
            var comparison = node.CompareTo(new MockINode());
            //Assert
            Assert.Equal(1, comparison);
        }

        [Fact]
        public void GridNode_ComparisonOnlyTestsFHeuristic()
        {
            //Arrange
            var origin = new GridNode(15, 11);
            var node = new GridNode(15, 12, DirectionEnum.Down);
            var nextNode = new GridNode(16, 13, DirectionEnum.Down | DirectionEnum.Right);
            node.SetParent(origin);
            nextNode.SetParent(node);
            node.SetF(nextNode);
            nextNode.SetF(nextNode);
            //Act
            var comparison = node.CompareTo(nextNode);
            //Assert
            Assert.Equal(-1, comparison);
        }
    }
}
