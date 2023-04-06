using AVE_automower;
using AVE_automower.model;
using System.IO;
using System.Reflection;
using Xunit;

namespace AutoMowerTU
{
    public class AutoMowerTU
    {
        private readonly AutoMower autoMower;

        public AutoMowerTU()
        {
            autoMower = new AutoMower();
        }

        [Fact]
        public void CheckPostionIsOKWithTheLawn()
        {

            //arrange
            autoMower.Lawn = new Coordinate(5, 5);
            autoMower.Mowers.Add(new(3, 3, "N"));
            Position positionToTest = new(2, 3, "E");

            //Act
            var result = autoMower.CheckPostion(positionToTest);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void CheckPostionIsKOutsideOfTheLawn()
        {
            //arrange
            autoMower.Lawn = new Coordinate(5, 5);
            autoMower.Mowers.Add(new(3, 3, "N"));
            Position positionToTest = new(4, 6, "E");

            //Act
            var result = autoMower.CheckPostion(positionToTest);

            //Assert
            Assert.False(result);
        }


        [Fact]
        public void CheckPostionIsKOAnotherMowerIsOnThePostion()
        {
            //arrange
            autoMower.Lawn = new Coordinate(5, 5);
            autoMower.Mowers.Add(new(3, 3, "N"));
            Position positionToTest = new(3, 3, "E");

            //Act
            var result = autoMower.CheckPostion(positionToTest);

            //Assert
            Assert.False(result);
        }


        [Theory]
        [InlineData("N", 3, 4)]
        [InlineData("S", 3, 2)]
        [InlineData("E", 4, 3)]
        [InlineData("W", 2, 3)]
        public void ChangecoordinateOK(string orientation, int expectedX, int expectedY)
        {

            //Act
            var result = autoMower.Changecoordinate(new Mower(3, 3, orientation));

            //Assert 
            Assert.Equal(expectedX, result.CoordinateInfo.X);
            Assert.Equal(expectedY, result.CoordinateInfo.Y);

        }


        [Theory]
        [InlineData("N", 'R', "E")]
        [InlineData("N", 'L', "W")]
        [InlineData("S", 'R', "W")]
        [InlineData("S", 'L', "E")]
        [InlineData("E", 'R', "S")]
        [InlineData("E", 'L', "N")]
        [InlineData("W", 'R', "N")]
        [InlineData("W", 'L', "S")]
        public void ChangeOrientationOK(string orientation, char rotation, string expected)
        {
            //arrange
            var mower = new Mower(1, 1, orientation);

            //act
            autoMower.ChangeOrientation(mower, rotation);

            //Assert 
            Assert.Equal(expected, mower.PositionInfo.Orientation);
        }


    }

}

