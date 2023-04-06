using AVE_automower;
using AVE_automower.model;
using System.Linq;
using TechTalk.SpecFlow;

using Xunit;

namespace automowerBDD.Steps
{
    [Binding]
    public sealed class AutomowerBDDDefinitions
    {
      
        private AutoMower _autoMower ;

        public AutomowerBDDDefinitions()
        {
            _autoMower = new AutoMower();
        }

        [Given(@"a lawn with a top right corner at X equal to (.*) and Y equal to (.*)")]
        public void GivenALawnWithATopRightCornerAtXEQualToAndYequalTo(int p0, int p1)
        {
            _autoMower.Lawn= new Coordinate(p0, p1);
        }


        [Given(@"a mower at position x equal to (.*) and y equal to (.*)  and orientation equal to (.*)")]
        public void GivenAMowerAtPositionXEqualToAndYEqualToAndOrientationEqualTo(int p0, int p1, string p2)
        {

            _autoMower.Mowers.Add(new Mower(p0, p1, p2));
        }

        [Given(@"a moveset equal to (.*)")]
        public void GivenAMovesetEqualTo(string p0)
        {
            _autoMower.Mowers.Last().Moveset=p0;
        }
       


        [When(@"treatment is launched")]
        public void WhenProcessIsLaunched()
        {
            _autoMower.Run();
        }


        [Then(@"the mower last position is X equal to (.*) and Y equal to (.*) and orientation is equal to (.*)")]
         public void ThenTheMowerLastPositionIsXEqualToAndYEqualToAndOrientationIsEqualTo(int p0, int p1, string p2)
        {
            var mower = _autoMower.Mowers[0];
            Assert.Equal(mower.PositionInfo.Orientation, p2);
            Assert.Equal(mower.PositionInfo.CoordinateInfo.Y, p1);
            Assert.Equal(mower.PositionInfo.CoordinateInfo.X, p0);
        }

    }
}