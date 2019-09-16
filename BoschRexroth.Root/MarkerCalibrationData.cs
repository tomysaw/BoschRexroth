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
        RotatedRect ellipse;
        double delta;
        double N;

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
            var p = GetMarkerPoint(frame);
            ps.Add(p);

            if (ps.Count() > 5)
            {
                RotatedRect ellipse = Cv2.FitEllipse(ps.ToArray());
            }

            Trace.WriteLine($"Points count: {ps.Count()}");

            return new int?[0];
        }

        const int RequiredPointCount = 400;

        public void Stop()
        {
            if (ps.Count() < RequiredPointCount)
                throw new ApplicationException("Not enough data points for calibration");

            var y0 = ps.Take(ps.Count() / 2).Min(p => p.Y);
            ps = ps.SkipWhile(p => p.Y > y0).ToList();
            int i;
            for (i = 0; i < ps.Count; i++)
                if (i > 120 && ps[i].X >= ps[0].X && ps[i].X < ps[1].X)
                    break;

            //Debug.Assert(i < 200);
            N = i;
            ps = ps.Take(i + 3).ToList();
            delta = 360.0 / N;
        }

        public void DbgShowOver(Mat frame)
        {
            foreach (var p in ps)
                frame.DrawMarker((int)p.X, (int)p.Y, Scalar.LightGreen, MarkerStyle.CircleFilled, size: 4);

            Cv2.Ellipse(frame, ellipse, Scalar.Red);

            if (delta > 0)
                for (int angle = 0; angle < 360; angle += 30)
                {
                    var p = AngleToPoint(angle);
                    frame.DrawMarker((int)p.X, (int)p.Y, Scalar.Red, MarkerStyle.Cross, size: 10);
                }
        }

        public bool CanStop => ps.Count() >= RequiredPointCount;

        public Point2f GetMarkerPoint(Mat frame)
        {
            Cv2.CvtColor(frame, hsv, ColorConversionCodes.BGR2HSV);
            return DetectColor(hsv, hsvColorSample);
        }

        public float GetMarkerAngle(Mat frame)
        {
            return PointToAngle(GetMarkerPoint(frame));
        }

        Point2f AngleToPoint(double angle)
        {
            int n = 0;
            while (angle < 0)
                angle += 360;
            while (angle >= delta)
            {
                angle -= delta;
                n++;
            }
            var p0 = ps[n + 0];
            var p1 = ps[n + 1];
            return p0 * (1 - angle) + p1 * angle;
        }

        float PointToAngle(Point2f point)
        {
            try
            {
                double dd = 1000;
                int ii = -1;
                for (int i = 0; i < N; i++)
                {
                    var d = point.DistanceTo(ps[i + 0]) + point.DistanceTo(ps[i + 1]);
                    if (dd < d)
                        ii = i;
                }

                return (float)(delta * (ii + point.DistanceTo(ps[ii + 0]) / (point.DistanceTo(ps[ii + 0]) + point.DistanceTo(ps[ii + 1]))));
            }
            catch (Exception)
            {
                return 0;
            }
        }

    }
}
