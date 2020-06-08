using Xunit;
using Pathfinding.Lib.Extensions;
using Pathfinding.Lib.Maps.Grid;
using System.Linq;

namespace Pathfinding.Lib.UnitTests.Extensions
{
    public class INodeExtensionsTests
    {
        [Fact]
        public void INode_CanTransformHierarchyToIEnumerableStartingFromOrigin()
        {
            //Arrange
            var origin = new GridNode(15, 11);
            var node1 = new GridNode(15, 12, DirectionEnum.Down);
            var node2 = new GridNode(16, 13, DirectionEnum.Down | DirectionEnum.Right);
            var node3 = new GridNode(17, 14, DirectionEnum.Down | DirectionEnum.Right);
            var end = new GridNode(18, 15, DirectionEnum.Down | DirectionEnum.Right);
            node1.SetParent(origin);
            node2.SetParent(node1);
            node3.SetParent(node2);
            end.SetParent(node3);
            //Act
            var nodeEnumerable = end.ToIEnumerable().ToList();
            //Assert
            Assert.Equal(nodeEnumerable[0], origin);
            Assert.Equal(nodeEnumerable[1], node1);
            Assert.Equal(nodeEnumerable[2], node2);
            Assert.Equal(nodeEnumerable[3], node3);
            Assert.Equal(nodeEnumerable[4], end);
        }
    }
}
