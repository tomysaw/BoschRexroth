using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenCvSharp;

namespace BoschRexroth.Root
{
    class Program
    {
        static void Main(string[] args)
        {
            var videoService = new RTSPVideoService();
            var videoSaver = new VideoSaver();

            //try
            //{
            //    videoService.Initialize();
            //    videoSaver.Initialize(videoService.Capture);

            //    var controller = new MQTTController();
            //    controller.Start();
            //    WaitFor(() => controller.IsInitialized);

            //    var queue = new ConcurrentQueue<Mat>();

            //    var source = new CancellationTokenSource();
            //    Task.Factory.StartNew(() => videoService.Start(queue, null, source.Token));
            //    Thread.Sleep(1000);

            //    videoService.SeedForAnalysis = true;
            //    Task.Factory.StartNew(() => videoSaver.ConsumeAndSaveAsync(queue, source.Token));

            //    var encoder = new Encoder(controller);
            //    //encoder.Calibrate_TimeSlot(1, 2, 3);
            //    //encoder.Calibrate_TimeSlot(1, 2, 3, 4, 5, 7);
            //    //encoder.Calibrate_TimeSlot(8, 10, 12, 15);
            //    //Thread.Sleep(10000);

            //    source.Cancel();
            //    //videoSaver.ConsumeAndSave(queue);
            //}
            //catch (Exception ex)
            //{
            //    videoSaver.Release();
            //    Console.WriteLine(ex);
            //}


            //try
            //{
            //    var controller = new MQTTController();
            //    controller.Start();

            //    Thread.Sleep(1000);
            //    controller.SetFrequency(1);

            //    Thread.Sleep(1000);
            //    controller.Move(eDirection.Left);

            //    Thread.Sleep(1000);
            //    controller.Stop();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}

            //Console.ReadKey();

            
            //var capture = new VideoCapture(@"https://hackathon:!Hackath0n@192.168.0.2:554/onvif/device_service");

            
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
