using System.Threading;
using System.Threading.Tasks;

namespace BoschRexroth.Root
{
    public class Encoder
    {
        private readonly MQTTController _controller;

        public Encoder(MQTTController controller)
        {
            _controller = controller;
        }

        #region Calibrate

        public void Calibrate_TimeSlot(params ushort[] frequencies)
        {
            foreach(var freq in frequencies)
            {
                Task.Factory.StartNew(() =>
                {
                    var curFreq = freq;
                    Thread.Sleep(500);
                    _controller.SetFrequency(curFreq);
                    _controller.Move(eDirection.Right);

                    Thread.Sleep(10000);
                    _controller.Stop();
                });

                Thread.Sleep(20000);
            }
        }

        #endregion

    }
}
