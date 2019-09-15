using OpenCvSharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BoschRexroth.Root
{
    public class RTSPVideoService
    {
        public VideoCapture Capture { get; private set; }

        public ConcurrentQueue<Mat> _guiQueue;

        public ConcurrentQueue<Mat> _analysisQueue;

        private CancellationTokenSource _guiTokenSource;

        private CancellationTokenSource _analyticsTokenSource;

        #region API

        public void Initialize()
        {
            Capture = new VideoCapture(@"rtsp://hackathon:!Hackath0n@192.168.0.2:554");
            _guiQueue = new ConcurrentQueue<Mat>();
            _analysisQueue = new ConcurrentQueue<Mat>();

            //Capture = new VideoCapture(0);
        }

        public void StartAsync()
        {
            var frame = new Mat();
            while (Capture.Read(frame))
            {
                if (frame.Width <= 0 || frame.Height <= 0)
                {
                    break;
                }

                if (_guiTokenSource != null)
                {
                    _guiQueue.Enqueue(frame);
                }

                if (_analyticsTokenSource != null)
                {
                    _analysisQueue.Enqueue(frame);
                }
            }
        }

        public void SubscribeAnalyticsAction(Action<Mat> action)
        {
            _analyticsTokenSource = new CancellationTokenSource();
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (_analyticsTokenSource.Token.IsCancellationRequested)
                    {
                        _analyticsTokenSource = null;
                        break;
                    }

                    Mat frame;
                    if (_analysisQueue.TryDequeue(out frame))
                    {
                        action(frame);
                    }
                }
            });
        }

        public void UnSubscribeAnalyticsAction()
        {
            _analyticsTokenSource.Cancel();
        }

        public void SubscribeGUIAction(Action<Mat> action)
        {
            _guiTokenSource = new CancellationTokenSource();
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (_guiTokenSource.Token.IsCancellationRequested)
                    {
                        _guiTokenSource = null;
                        break;
                    }

                    Mat frame;
                    if (_guiQueue.TryDequeue(out frame))
                    {
                        action(frame);
                    }
                }
            });
        }

        #endregion
    }
}
