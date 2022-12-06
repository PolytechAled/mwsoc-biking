package fr.biking.client.ui.panels;

import fr.biking.client.BikingManager;
import fr.biking.client.IBikingEvent;
import fr.biking.client.activemq.ActiveMqClient;
import fr.biking.client.service.NavigationAnswer;
import fr.biking.client.service.NavigationError;

import javax.imageio.ImageIO;
import javax.swing.*;
import java.awt.*;
import java.awt.image.BufferedImage;
import java.io.IOException;
import java.util.Objects;


public class StepsPanel extends JPanel implements IBikingEvent {

    private JLabel infoLabel;
    private JLabel stepLabel;
    private JLabel stepCountLabel;
    private JLabel bicycleLabel;
    private int totalStepCount;
    private int currentStep;
    private String stepQueueId;

    public StepsPanel() {
        BikingManager.instance.addHandler(this);
        BoxLayout layout = new BoxLayout(this, BoxLayout.Y_AXIS);
        setLayout(layout);

        setBorder(BorderFactory.createEmptyBorder(5, 5, 5, 5));

        infoLabel = new JLabel("Infos:");
        add(infoLabel);

        try {
            BufferedImage bicycleImage = ImageIO.read(Objects.requireNonNull(this.getClass().getResourceAsStream("/icon_bicycle.png")));
            bicycleLabel = new JLabel(new ImageIcon(new ImageIcon(bicycleImage).getImage().getScaledInstance(20, 20, Image.SCALE_DEFAULT)));
            add(bicycleLabel);
        } catch (IOException e) {
            throw new RuntimeException(e);
        }

        stepCountLabel = new JLabel("0/0");
        add(stepCountLabel);

        stepLabel = new JLabel("Next step: None");
        add(stepLabel);

        JButton nextStepBtn = new JButton("Next step");
        nextStepBtn.addActionListener((s) -> showNextStep());
        add(nextStepBtn);
    }

    @Override
    public void onNavigationChanged(NavigationAnswer navigationAnswer) {
        if (navigationAnswer.getError().equals(NavigationError.SUCCESS)) {
            totalStepCount = navigationAnswer.getStepCount();
            currentStep = 1;
            stepQueueId = navigationAnswer.getQueueName().getValue();
            showNextStep();

            bicycleLabel.setVisible(navigationAnswer.isUseBicycle());
        }
    }

    @Override
    public Dimension getPreferredSize() {
        return new Dimension(600,super.getPreferredSize().height);
    }

    private void showNextStep() {
        if (stepQueueId != null && !stepQueueId.isEmpty() && !stepQueueId.isBlank()) {
            String text = ActiveMqClient.INSTANCE.getNextQueueMessage(stepQueueId);
            if (text != null) {
                stepLabel.setText(text);
                stepCountLabel.setText(currentStep++ + "/" + totalStepCount);
            }
        }
    }
}
