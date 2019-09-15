using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace BoschRexroth.Root
{
    class DriveActionController : IFrameProcessor
    {
        public enum UserAction
        {
            Turn,
            Base,
            Stop,
            Pause,
        }

        public DriveActionController StartAction(DriveCalibrationData DCD, UserAction action, int? angle, int? speed)
        {
            throw new NotImplementedException();
        }
        public int?[] AddFrame(DateTime ts, Mat frame)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
