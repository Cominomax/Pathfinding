using Xunit;
using Pathfinding.Lib.Extensions;
using Pathfinding.Lib.Maps.Grid;

namespace Pathfinding.Lib.UnitTests.Extensions
{
    public class DirectionExtensionsTests
    {
        [Fact]
        public void Direction_MonodirectionalShouldBeDetected()
        {
            //Arrange
            DirectionEnum down = DirectionEnum.Down, 
                            up = DirectionEnum.Up, 
                         right = DirectionEnum.Right, 
                          left = DirectionEnum.Left;
            //Act
            var testDown = down.HasMoreThan1Direction();
            var testUp = up.HasMoreThan1Direction();
            var testRight = right.HasMoreThan1Direction();
            var testLeft = left.HasMoreThan1Direction();

            //Assert
            Assert.False(testDown | testUp | testRight | testLeft);
        }

        [Fact]
        public void Direction_BidirectionShouldBeDetected()
        {
            //Arrange
            DirectionEnum down_left = (DirectionEnum.Down   | DirectionEnum.Left),
                           up_right = (DirectionEnum.Up     | DirectionEnum.Right),
                         down_right = (DirectionEnum.Down   | DirectionEnum.Right),
                            up_left = (DirectionEnum.Up     | DirectionEnum.Left);
            //Act
            var testDownLeft = down_left.HasMoreThan1Direction();
            var testUpRight = up_right.HasMoreThan1Direction();
            var testDownRight = down_right.HasMoreThan1Direction();
            var testUpLeft = up_left.HasMoreThan1Direction();

            //Assert
            Assert.True(testDownLeft & testUpRight & testDownRight & testUpLeft);
        }

        [Fact]
        public void Direction_IsPossibleDirectionShouldAllowEveryMonodirection()
        {
            //Arrange
            DirectionEnum possibleMoves = DirectionEnum.Origin;
            DirectionEnum down = DirectionEnum.Down,
                            up = DirectionEnum.Up,
                         right = DirectionEnum.Right,
                          left = DirectionEnum.Left;
            //Act
            var testDown = down.IsPossibleDirection(possibleMoves);
            var testUp = up.IsPossibleDirection(possibleMoves);
            var testRight = right.IsPossibleDirection(possibleMoves);
            var testLeft = left.IsPossibleDirection(possibleMoves);

            //Assert
            Assert.True(testDown & testUp & testRight & testLeft);
        }

        [Fact]
        public void Direction_PossibleDirectionsShouldNotAllowDiagonalMoveWhenOneDirectionIsBlocked()
        {
            //Arrange
            DirectionEnum possibleMoves = (DirectionEnum.Down | DirectionEnum.Left | DirectionEnum.Right); //obstacle on up direction.
            DirectionEnum down_left = (DirectionEnum.Down | DirectionEnum.Left),
                           up_right = (DirectionEnum.Up | DirectionEnum.Right),
                         down_right = (DirectionEnum.Down | DirectionEnum.Right),
                            up_left = (DirectionEnum.Up | DirectionEnum.Left);

            //Act
            var testDownLeft = down_left.IsPossibleDirection(possibleMoves);
            var testUpRight = up_right.IsPossibleDirection(possibleMoves);
            var testDownRight = down_right.IsPossibleDirection(possibleMoves);
            var testUpLeft = up_left.IsPossibleDirection(possibleMoves);

            //Assert
            Assert.True(testDownLeft & testDownRight);
            Assert.False(testUpRight | testUpLeft);
        }
    }
}
