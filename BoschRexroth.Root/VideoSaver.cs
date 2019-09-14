using OpenCvSharp;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace BoschRexroth.Root
{
    public class VideoSaver
    {
        private VideoWriter _writer;

        public void Initialize(VideoCapture capture)
        {
            var size = new Size(capture.FrameWidth, capture.FrameHeight);
            var filename = Guid.NewGuid().ToString();
            _writer = new VideoWriter(@"E:\" + DateTime.Now.ToString("hh_mm_ss") + ".avi", FourCC.XVID, capture.Fps, size);
            //_writer = new VideoWriter(@"E:\1233.avi", FourCC.XVID, capture.Fps, size);
        }

        public void SaveFrame(Mat frame)
        {
            _writer.Write(frame);
        }

        public void ConsumeAndSave(ConcurrentQueue<Mat> queue)
        {
            while(true)
            {
                Mat frame;
                if (queue.TryDequeue(out frame))
                {
                    _writer.Write(frame);
                }
                else
                {
                    break;
                }
            }
        }

        public void ConsumeAndSaveAsync(ConcurrentQueue<Mat> queue, CancellationToken token)
        {
            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }

                Mat frame;
                if (queue.TryDequeue(out frame))
                {
                    _writer.Write(frame);
                }
            }
        }

        public void Release()
        {
            if (_writer != null)
            {
                _writer.Release();
            }
        }
    }
}
