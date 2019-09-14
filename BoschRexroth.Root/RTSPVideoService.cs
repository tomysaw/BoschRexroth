using OpenCvSharp;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;

namespace BoschRexroth.Root
{
    public class RTSPVideoService
    {
        public VideoCapture Capture { get; private set; }

        public bool Seed { get; set; }


        public event Action<Mat> OnFrameReceived;

        #region API

        public void Initialize()
        {
            Capture = new VideoCapture(@"rtsp://hackathon:!Hackath0n@192.168.0.2:554");
            //Capture = new VideoCapture(0);
        }

        public void Start(ConcurrentQueue<Mat> queue, CancellationToken token)
        {
            var frame = new Mat();
            while (Capture.Read(frame))
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }

                if (frame.Width <= 0 || frame.Height <= 0)
                {
                    break;
                }

                if (!Seed)
                {
                    continue;
                }

                queue.Enqueue(frame);
            }
        }

        #endregion
    }
}
