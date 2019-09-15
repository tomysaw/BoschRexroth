using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoschRexroth.Root
{
    interface IFrameProcessor
    {
        int?[] AddFrame(DateTime ts, Mat frame);
        void Stop();
    }
}
