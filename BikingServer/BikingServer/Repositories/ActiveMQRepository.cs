using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Microsoft.SqlServer.Server;
using ISession = Apache.NMS.ISession;

namespace BikingServer.Repositories
{
    public class ActiveMQRepository
    {
        private bool isStared;
        private string uri;
        private ISession session;
        private IMessageProducer messageProducer;

        public ActiveMQRepository(string uri = "activemq:tcp://localhost:61616")
        {
            this.uri = uri;
        }

        private void Connect()
        {
            isStared = false;

            try
            {
                ConnectionFactory connectionFactory = new ConnectionFactory(this.uri);
                IConnection connection = connectionFactory.CreateConnection();
                connection.Start();
                session = connection.CreateSession();
                isStared = true;
            }
            catch
            {
                Console.WriteLine("Unable to start activeMQ");
            }
        }

        public bool SendMessageInQueue(string queueName, string message)
        {
            try
            {
                if (!isStared)
                {
                    Connect();
                }

                if (isStared)
                {
                    IDestination destination = session.GetQueue(queueName);
                    messageProducer = session.CreateProducer(destination);
                    messageProducer.DeliveryMode = MsgDeliveryMode.NonPersistent;
                    ITextMessage msg = session.CreateTextMessage(message);
                    messageProducer?.Send(msg);
                    return true;
                }
                else
                {
                    Console.WriteLine("Unable to send message, activemq not started");
                }
            }
            catch
            {
                Console.WriteLine("Unable to send message: please check if activemq already run");
            }
            return false;
        }

        public string GetRandomQueueName()
        {
            Guid uuid = Guid.NewGuid();
            return uuid.ToString();
        }
    }
}
