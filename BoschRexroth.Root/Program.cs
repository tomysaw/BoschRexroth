using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace BoschRexroth.Root
{
    class Program
    {
        static void Main(string[] args)
        {
            var capture = new VideoCapture(@"VID_20190831_120949.mp4");

            var frame = new Mat();
            while (capture.Read(frame))
            {
                if (frame.Width <= 0 || frame.Height <= 0)
                {
                    break;
                }

                Cv2.ImShow("frame", frame);

                var delay = (int)(1000 / capture.Fps);
                var key = Cv2.WaitKey(delay);
                if (key == 27)
                    break;
            }

        }
    }
}
