using OpenCvSharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BoschRexroth.Root
{
    public class MainController
    {
        private MQTTController _controller;
        private RTSPVideoService _videoService;

        private MarkerCalibrationData _mcd;
        private DriveCalibrationData _dcd;
        private DriveActionController _driveController;

        Logger _logger;

        public MainController(Logger logger)
        {
            _logger = logger;
        }


        public void Initialize()
        {
            _videoService = new RTSPVideoService();
            _videoService.Initialize();

            Task.Factory.StartNew(() => _videoService.StartAsync());
            _videoService.SubscribeGUIAction((frame) =>
            {
                if (_mcd != null)
                {
                    var marker = _mcd.GetMarkerPoint(frame);
                    frame.DrawMarker((int)marker.X, (int)marker.Y, Scalar.White, size: 20, thickness: 3);
                }

                Cv2.ImShow("cam", frame);
                Cv2.WaitKey(1);
            });

            _controller = new MQTTController();
            _controller.Start(_logger);
            WaitFor(() => _controller.IsInitialized);
            _controller.Stop();
        }

        public void CalibrateMarker()
        {
            var px = new Mat("MarkerColor.png");
            _mcd = MarkerCalibrationData.Start(_videoService.Capture.Fps, px);

            _videoService.SubscribeAnalyticsAction((frame) => _mcd.AddFrame(DateTime.Now, frame));

            _controller.SetFrequency(1);
            _controller.Move(eDirection.Right);
            Thread.Sleep(30000);
            _controller.Stop();

            _videoService.UnSubscribeAnalyticsAction();
            _mcd.Stop();

            _dcd = DriveCalibrationData.Start(_mcd, _videoService.Capture.Fps);
        }

        public void Turn(int angle, ushort speed)
        {
            DriveControllerAction(DriveActionController.UserAction.Turn, angle, speed);
        }

        public void Stop()
        {
            DriveControllerAction(DriveActionController.UserAction.Stop, null, null);
        }

        public void Pause(int ms)
        {
            DriveControllerAction(DriveActionController.UserAction.Stop, null, null);
            Thread.Sleep(ms);
        }

        public void Base(ushort speed)
        {
            DriveControllerAction(DriveActionController.UserAction.Base, null, speed);
        }

        public void Speed(ushort speed)
        {
            _controller.SetFrequency(speed);
        }

        private void DriveControllerAction(DriveActionController.UserAction action, int? angle, int? speed)
        {
            _driveController = DriveActionController.StartAction(_dcd, action, angle, speed);

            var isCompleted = false;
            _videoService.SubscribeAnalyticsAction(frame =>
            {
                var cmd = _driveController.AddFrame(DateTime.Now, frame);
                if (ProcessCallback(cmd))
                {
                    isCompleted = true;
                }
            });

            while (!isCompleted)
            {
                Thread.Sleep(1);
            }

            _videoService.UnSubscribeAnalyticsAction();
            _driveController.Stop();
        }

        private bool ProcessCallback(int?[] res)
        {
            if (!res.Any())
            {
                // Do nothing, operation not completed
                return false;
            }

            var cmd = res.First();
            if (cmd == null)
            {
                // Operation completed
                return true;
            }

            if (cmd.Value == 0)
            {
                // request stop, operation not completed
                _controller.Stop();
                return false;
            }

            if (cmd.Value > 0)
            {
                // request move right, operation not completed
                _controller.SetFrequency((ushort)cmd.Value);
                _controller.Move(eDirection.Right);
                return false;
            }

            if (cmd.Value < 0)
            {
                // request move right, operation not completed
                _controller.SetFrequency((ushort)Math.Abs(cmd.Value));
                _controller.Move(eDirection.Left);
                return false;
            }

            throw new ArgumentException("res");
        }

        private static void WaitFor(Func<bool> func, int maxIter = 5)
        {
            int i = 0;
            while (true)
            {
                if (func())
                {
                    break;
                }

                if (i++ > maxIter)
                {
                    throw new Exception("Operation hanging");
                }

                Thread.Sleep(1000);
            }
        }
    }
}
