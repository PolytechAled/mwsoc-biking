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
    internal class ActiveMQRepository
    {
        private Uri uri;
        private string queue;
        private ISession session;
        private IMessageProducer messageProducer;

        public ActiveMQRepository(string uri = "activemq:tcp://localhost:61616", string queue = "BikingSteps")
        {
            this.uri = new Uri(uri);
            this.queue = queue;

            ConnectionFactory connectionFactory = new ConnectionFactory(this.uri);

            // Create a single Connection from the Connection Factory.
            IConnection connection = connectionFactory.CreateConnection();
            connection.Start();

            // Create a session from the Connection.
            session = connection.CreateSession();

            // Use the session to target a queue.
            IDestination destination = session.GetQueue("test");

            // Create a Producer targetting the selected queue.
            messageProducer = session.CreateProducer(destination);

            // You may configure everything to your needs, for instance:
            messageProducer.DeliveryMode = MsgDeliveryMode.NonPersistent;
        }

        public void SendMessageInQueue(string message)
        {
            ITextMessage msg = session.CreateTextMessage(message);
            messageProducer?.Send(msg);
        }
    }
}
