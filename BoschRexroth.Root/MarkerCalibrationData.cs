using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace BoschRexroth.Root
{
    class MarkerCalibrationData : IFrameProcessor
    {
        double FPS;
        Mat hsvColorSample = new Mat();
        Mat hsv = new Mat();

        static List<Point2f> ps = new List<Point2f>();

        static Point2f DetectColor(Mat hsv, Mat hsvMarker)
        {
            var (dh, ds, dv) = (20, 30, 40);
            var colorMask0 = hsv.InRange(new Scalar(159 - dh, 194 - 3 * ds, 209 - dv), new Scalar(159 + dh, 194 + 2 * ds, 255));

            var nz = colorMask0.FindNonZero();
            var m = Cv2.Moments(nz);
            var bb = Cv2.BoundingRect(nz);

            var p = new Point2d(m.M10 / m.M00, m.M01 / m.M00);
            //Debug.Assert(bb.Contains((Point)p));
            return (bb.TopLeft + bb.BottomRight) * 0.5;
        }


        public MarkerCalibrationData(double FPS, Mat colorSample)
        {
            this.FPS = FPS;
            Cv2.CvtColor(colorSample, hsvColorSample, ColorConversionCodes.BGR2HSV);
        }

        public static MarkerCalibrationData Start(double FPS, Mat colorSample)
        {
            return new MarkerCalibrationData(FPS, colorSample);
        }

        public int?[] AddFrame(DateTime ts, Mat frame)
        {
            Cv2.CvtColor(frame, hsv, ColorConversionCodes.BGR2HSV);
            var p = DetectColor(hsv, hsvColorSample);
            ps.Add(p);
            return new int?[0];
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void DbgShowOver(Mat frame)
        {
            foreach (var p in ps)
                frame.DrawMarker((int)p.X, (int)p.Y, Scalar.LightGreen);

            Trace.WriteLine(ps.Count());

            if (ps.Count() > 5)
            {
                var ellipse = Cv2.FitEllipse(ps.ToArray());
                Cv2.Ellipse(frame, ellipse, Scalar.Red);
            }
        }

        bool IsComplete => false;

        public bool CanStop => ps.Count() == 150;

        Point2f GetMarkerPoint(Mat frame)
        {
            throw new NotImplementedException();
        }

        float GetMarkerAngle(Mat frame)
        {
            throw new NotImplementedException();
        }

        Point2f AngleToPoint(double angle)
        {
            throw new NotImplementedException();
        }

        double PointToAngle(Point2f point)
        {
            throw new NotImplementedException();
        }

    }
}
