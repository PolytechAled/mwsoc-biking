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

    private final JLabel stepLabel;
    private final JLabel stepCountLabel;
    private final JLabel bicycleLabel;
    private final JFrame stepFrame;
    private final JButton nextStepBtn;
    private int totalStepCount;
    private int currentStep;
    private String stepQueueId;

    public StepsPanel(JFrame stepFrame) {
        this.stepFrame = stepFrame;

        BikingManager.instance.addHandler(this);
        BoxLayout layout = new BoxLayout(this, BoxLayout.Y_AXIS);
        setLayout(layout);

        setBorder(BorderFactory.createEmptyBorder(5, 5, 5, 5));

        JLabel infoLabel = new JLabel("Infos:");
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

        nextStepBtn = new JButton("Next step");
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
        if (currentStep > totalStepCount) {
            this.stepFrame.setVisible(false);
            return;
        }

        if (stepQueueId != null && !stepQueueId.isEmpty() && !stepQueueId.isBlank()) {
            String text = ActiveMqClient.INSTANCE.getNextQueueMessage(stepQueueId);
            if (text != null) {
                nextStepBtn.setText("Next step");
                stepLabel.setText(text);
                stepCountLabel.setText(currentStep++ + "/" + totalStepCount);
            }

            if (currentStep > totalStepCount) {
                nextStepBtn.setText("Close");
            }
        }
    }
}
