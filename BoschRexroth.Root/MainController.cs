using OpenCvSharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BoschRexroth.Root
{
    public class MainController
    {
        private ConcurrentQueue<Mat> _analysisQueue;
        private MQTTController _controller;

        RTSPVideoService _videoService;
        CancellationToken token;

        public ConcurrentQueue<Mat> GuiQueue;


        public void Initialize()
        {
            _videoService = new RTSPVideoService();
            _videoService.Initialize();
            //Task.Factory.StartNew(() => _videoService.Start(queue, source.Token));

            _controller = new MQTTController();
            _controller.Start();
            WaitFor(() => _controller.IsInitialized);


        }

        public void Calibrate()
        {
            var source = new CancellationTokenSource();
            //Task.Factory.StartNew(() => _videoService.Start(queue, source.Token));
            Thread.Sleep(1000);

            var frequencies = new ushort[] { 1, 2 };
            foreach (var freq in frequencies)
            {
                Task.Factory.StartNew(() =>
                {
                    var curFreq = freq;
                    Thread.Sleep(500);
                    _controller.SetFrequency(curFreq);
                    _controller.Move(eDirection.Right);

                    Thread.Sleep(4000);
                    _controller.Stop();
                });

                Thread.Sleep(7000);
            }

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
