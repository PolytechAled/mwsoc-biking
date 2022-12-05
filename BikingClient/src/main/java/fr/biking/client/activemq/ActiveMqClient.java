package fr.biking.client.activemq;

import org.apache.activemq.ActiveMQConnection;
import org.apache.activemq.ActiveMQConnectionFactory;

import javax.jms.*;

public class ActiveMqClient {

    private static final String ACTIVEMQ_URI = ActiveMQConnection.DEFAULT_BROKER_URL;
    public static ActiveMqClient INSTANCE = new ActiveMqClient();

    private Connection connection;

    public ActiveMqClient() {
        try {
            ConnectionFactory connectionFactory = new ActiveMQConnectionFactory(ACTIVEMQ_URI);
            connection = connectionFactory.createConnection();
            connection.start();
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }

    public String getNextQueueMessage(String queueId) {
        try {
            Session session = connection.createSession(false, Session.AUTO_ACKNOWLEDGE);
            Destination destination = session.createQueue(queueId);
            MessageConsumer consumer = session.createConsumer(destination);
            javax.jms.Message message;
            message = consumer.receive();
            session.close();

            if (message instanceof TextMessage) {
                TextMessage textMessage = (TextMessage) message;
                return textMessage.getText();
            }
        } catch (JMSException e) {
            return null;
        }
        return null;
    }

}
