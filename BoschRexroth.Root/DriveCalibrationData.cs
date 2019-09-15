using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace BoschRexroth.Root
{
    class DriveCalibrationData : IFrameProcessor
    {
        private MarkerCalibrationData MCD;
        private double FPS;

        public DriveCalibrationData(MarkerCalibrationData MCD, double FPS)
        {
            this.MCD = MCD;
            this.FPS = FPS;
        }

        DriveCalibrationData Start(MarkerCalibrationData MCD, double FPS)
        {
            return new DriveCalibrationData(MCD, FPS);
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
