using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace BoschRexroth.Root
{
    class MarkerCalibrationDataTest
    {
        static void Circles(Mat frame)
        {
            using (var gray = frame.CvtColor(ColorConversionCodes.BGR2GRAY))
            {
                Cv2.GaussianBlur(gray, gray, new Size(9, 9), 4, 4);
                Cv2.ImShow("gray", gray);
                var canny = gray.Canny(1, 10);
                Cv2.ImShow("canny", canny);
                var circles = Cv2.HoughCircles(gray, HoughMethods.Gradient, 3, gray.Rows / 8, 200, 200, 0, 0);
                foreach (var c in circles)
                {
                    frame.Circle((int)c.Center.X, (int)c.Center.Y, (int)c.Radius, Scalar.Red, 1);
                }
            }
        }

        static Mat strel5 = Cv2.GetStructuringElement(MorphShapes.Ellipse, new Size(5, 5));
        static Mat strel9 = Cv2.GetStructuringElement(MorphShapes.Ellipse, new Size(9, 9));
        public void Run()
        {
            var capture = new VideoCapture(@"../../Data/r_1.mp4");
            capture.PosMsec = 4000;

            MarkerCalibrationData MCD = MarkerCalibrationData.Start(capture.Fps, new Mat("MarkerColor.png"));

            bool step = false;
            var frame = new Mat();
            while (capture.Read(frame))
            {
                if (frame.Width <= 0 || frame.Height <= 0)
                {
                    break;
                }

                MCD.AddFrame(DateTime.Now, frame);
                MCD.DbgShowOver(frame);
                Cv2.ImShow("frame", frame);

                if (MCD.CanStop)
                    step = true;

                var delay = (int)(1000 / capture.Fps);
                var key = Cv2.WaitKey(1);
                if (key == 27)
                    break;
                if (key == 32 || step)
                {
                    step = false;
                    while ((key = Cv2.WaitKey()) != 32)
                    {
                        if (key == 27)
                            goto outer;
                        if (key == 13)
                        {
                            step = true;
                            break;
                        }

                    }

                }
            }
            outer:;
        }
    }
}
