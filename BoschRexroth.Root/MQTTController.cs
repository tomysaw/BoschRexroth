using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BoschRexroth.Root
{
    public class MQTTController
    {
        private IMqttClient _client;
        private Logger _logger;

        #region API
        public bool IsInitialized { get; private set; }

        public void Start(Logger logger)
        {
            _logger = logger;

            var factory = new MqttFactory();
            _client = factory.CreateMqttClient();
            var clientOptions = new MqttClientOptions
            {
                ChannelOptions = new MqttClientTcpOptions
                {
                    Server = "192.168.0.1",
                    Port = 1883
                }
            };

            _client.ConnectedHandler = new MqttClientConnectedHandlerDelegate(async e =>
            {
                _logger.Log("### CONNECTED WITH SERVER ###");

                await _client.SubscribeAsync(new TopicFilterBuilder().WithTopic("#").Build());
                IsInitialized = true;

                _logger.Log("### SUBSCRIBED ###");
            });

            _client.DisconnectedHandler = new MqttClientDisconnectedHandlerDelegate(async e =>
            {
                _logger.Log("### DISCONNECTED FROM SERVER ###");
                await Task.Delay(TimeSpan.FromSeconds(5));

                try
                {
                    await _client.ConnectAsync(clientOptions);
                }
                catch
                {
                    _logger.Log("### RECONNECTING FAILED ###");
                }
            });

            try
            {
                _client.ConnectAsync(clientOptions, new CancellationToken());
            }
            catch (Exception exception)
            {
                _logger.Log("### CONNECTING FAILED ###" + Environment.NewLine + exception);
            }

            _logger.Log("### WAITING FOR APPLICATION MESSAGES ###");
        }

        public void SetFrequency(ushort frequency)
        {
            if (frequency <= 0 || frequency >= 50)
            {
                throw new ArgumentException("frequency range");
            }

            var applicationMessage = new MqttApplicationMessageBuilder()
                        .WithTopic("freq")
                        .WithPayload(frequency.ToString())
                        .Build();

            _logger.Log("Set Freq {0}", frequency);
            Publish(applicationMessage);
        }

        public void Move(eDirection direction)
        {
            string dir;
            switch(direction)
            {
                case eDirection.Left:
                    dir = "left";
                    break;

                case eDirection.Right:
                    dir = "right";
                    break;

                default:
                    throw new ArgumentException("direction");
            }

            var applicationMessage = new MqttApplicationMessageBuilder()
                        .WithTopic("move")
                        .WithPayload(dir)
                        .Build();

            _logger.Log("Move {0}", dir);
            Publish(applicationMessage);
        }

        public void Stop()
        {
            var applicationMessage = new MqttApplicationMessageBuilder()
                        .WithTopic("move")
                        .WithPayload("stop")
                        .Build();

            _logger.Log("Stop");
            Publish(applicationMessage);
        }

        #endregion

        #region Private

        private void Publish(MqttApplicationMessage message)
        {
            int i = 0;
            while (true)
            {
                if (_client.IsConnected)
                {
                    break;
                }

                if (i > 5)
                {
                    throw new Exception("Client is not connected");
                }

                Thread.Sleep(1000);
                i++;
            }

            _client.PublishAsync(message);
        }

        #endregion
    }

    public enum eDirection
    {
        Left,
        Right
    }
}
