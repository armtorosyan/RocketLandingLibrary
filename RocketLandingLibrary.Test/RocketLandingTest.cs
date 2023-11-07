using RocketLandingLibrary.Common;
using RocketLandingLibrary.Exceptions;

namespace RocketLandingLibrary.Test
{
    public class RocketLandingTest
    {
        private ILandingPlatform _landingPlatform;

        public RocketLandingTest()
        {
            this._landingPlatform = new LandingPlatform(new Rectangle(5, 5, 10, 10));
        }

        [Fact]
        public void ShouldReturnOkForLandingTest()
        {

            var result = _landingPlatform.CheckPosition(7, 6);

            Assert.Equal("ok for landing", result);
        }

        [Fact]
        public void ShouldReturnOutOfPlatformTest()
        {
            var result = _landingPlatform.CheckPosition(17, 18);

            Assert.Equal("out of platform", result);
        }

        [Fact]
        public void ShouldReturnClashTest()
        {
            _landingPlatform.CheckPosition(8, 8);

            var result = _landingPlatform.CheckPosition(7, 7);

            Assert.Equal("clash", result);
        }

        [Fact]
        public void ShouldThrowExceptionTest()
        {
            var act = () => new LandingPlatform(new Rectangle(95, 95, 10, 10));            

            Assert.Throws<InvalidLandingPlatformException>(act);
        }
    }
}