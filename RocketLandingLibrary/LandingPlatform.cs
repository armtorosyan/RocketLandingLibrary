using RocketLandingLibrary.Common;
using RocketLandingLibrary.Exceptions;

namespace RocketLandingLibrary
{
    public class LandingPlatform : ILandingPlatform
    {
        private readonly Rectangle _platform;
        private const int _landingAreaSize = 100;
        private static readonly object _lock = new object();
        private (int, int) _previousLandingPosition = (-1, -1);

        public LandingPlatform(Rectangle rect)
        {
            if(rect.x + rect.width > _landingAreaSize || rect.y + rect.height > _landingAreaSize 
                || rect.x + rect.width < 0 || rect.y + rect.height < 0)
            {
                throw new InvalidLandingPlatformException($"Can't create platform with the parameters x = {rect.x}, y = {rect.y}, width = {rect.width}, height = {rect.height}");
            }

            this._platform = rect;          
        }

        public string CheckPosition(int x, int y)
        {            
            if (!IsInsidePlatform(x, y))
            {
                return ReplyMesage.OutOfPlatform;
            }

            if (!IsClashWithPrevious(x, y))
            {
                lock (_lock)
                {         
                    if (!IsClashWithPrevious(x, y))
                    {                 
                        SavePosition(x, y);                        

                        return ReplyMesage.OkForLanding;
                    }                    
                }
            }
            
            return ReplyMesage.Clash;
        }

        private bool IsInsidePlatform(int landing_x, int landing_y)
        {
            if (landing_x <= _platform.x + _platform.width && landing_x >= _platform.x &&
                landing_y <= _platform.y + _platform.height && landing_y >= _platform.y)
            {
                return true;
            }

            return false;
        }

        private bool IsClashWithPrevious(int landing_x, int landing_y)
        {
            if (landing_x >= _previousLandingPosition.Item1 - 1 && landing_x <= _previousLandingPosition.Item1 + 1 
                && landing_y >= _previousLandingPosition.Item2 - 1 && landing_y <= _previousLandingPosition.Item2 + 1)
            {
                return true;
            }

            return false;
        }

        private void SavePosition(int x, int y) => _previousLandingPosition = (x, y);
    }
}