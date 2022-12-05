package fr.biking.client.ui.panels;

import fr.biking.client.BikingManager;
import fr.biking.client.IBikingEvent;
import fr.biking.client.activemq.ActiveMqClient;
import fr.biking.client.service.NavigationAnswer;
import fr.biking.client.service.NavigationError;

import javax.swing.*;

public class StepsPanel extends JPanel implements IBikingEvent {

    private JLabel infoLabel;
    private JLabel stepLabel;
    private String stepQueueId;

    public StepsPanel() {
        BikingManager.instance.addHandler(this);
        BoxLayout layout = new BoxLayout(this, BoxLayout.Y_AXIS);
        setLayout(layout);
        setBorder(BorderFactory.createEmptyBorder(0, 10, 10, 10));

        infoLabel = new JLabel("Infos: None");
        add(infoLabel);
        stepLabel = new JLabel("Next step: None");
        add(stepLabel);

        JButton nextStepBtn = new JButton("Next step");
        nextStepBtn.addActionListener((s) -> showNextStep());
        add(nextStepBtn);
    }

    @Override
    public void onNavigationChanged(NavigationAnswer navigationAnswer) {
       infoLabel.setText("Infos:\nUse bicycle: " + navigationAnswer.isUseBicycle());

       if (navigationAnswer.getError().equals(NavigationError.SUCCESS)) {
           stepQueueId = navigationAnswer.getQueueName().getValue();
           showNextStep();
       }
    }

    private void showNextStep() {
        if (stepQueueId != null && !stepQueueId.isEmpty() && !stepQueueId.isBlank()) {
            String text = ActiveMqClient.INSTANCE.getNextQueueMessage(stepQueueId);
            if (text != null) {
                stepLabel.setText(text);
            }
        }
    }
}
