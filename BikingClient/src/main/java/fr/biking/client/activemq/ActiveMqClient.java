package fr.biking.client.activemq;

import org.apache.activemq.ActiveMQConnection;
import org.apache.activemq.ActiveMQConnectionFactory;

import javax.jms.*;

public class ActiveMqClient {

    private static final String ACTIVEMQ_URI = ActiveMQConnection.DEFAULT_BROKER_URL;
    public static ActiveMqClient INSTANCE = new ActiveMqClient();

    private Connection connection;
    private boolean isConnected;

    public ActiveMqClient() {
        isConnected = false;
    }

    private void connect() {
        try {
            ConnectionFactory connectionFactory = new ActiveMQConnectionFactory(ACTIVEMQ_URI);
            connection = connectionFactory.createConnection();
            connection.start();
            isConnected = true;
        } catch (Exception ex) {
            System.err.println(ex.getMessage());
            isConnected = false;
        }
    }

    public String getNextQueueMessage(String queueId) {
        try {
            if (!isConnected) connect();
            if (!isConnected) return null;

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
        } catch (Exception ignored) {
            return null;
        }
        return null;
    }

}
