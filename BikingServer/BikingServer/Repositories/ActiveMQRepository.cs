using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private ISession session;
        private IMessageProducer messageProducer;

        public ActiveMQRepository(string uri = "activemq:tcp://localhost:61616")
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(uri);
            IConnection connection = connectionFactory.CreateConnection();
            connection.Start();
            session = connection.CreateSession();
        }

        public void SendMessageInQueue(string queueName, string message)
        {
            IDestination destination = session.GetQueue(queueName);
            messageProducer = session.CreateProducer(destination);
            messageProducer.DeliveryMode = MsgDeliveryMode.NonPersistent;
            ITextMessage msg = session.CreateTextMessage(message);
            messageProducer?.Send(msg);
        }
    }
}
