package fr.biking.client.ui.panels;

import com.sun.activation.viewers.ImageViewer;
import fr.biking.client.BikingManager;
import fr.biking.client.IBikingEvent;
import fr.biking.client.activemq.ActiveMqClient;
import fr.biking.client.service.NavigationAnswer;
import fr.biking.client.service.NavigationError;

import javax.imageio.ImageIO;
import javax.swing.*;
import java.awt.*;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;
import java.util.Objects;


public class StepsPanel extends JPanel implements IBikingEvent {

    private JLabel infoLabel;
    private JLabel stepLabel;
    private JLabel stepCountLabel;
    private BufferedImage bicycleImage;
    private int totalStepCount;
    private int currentStep;
    private String stepQueueId;

    public StepsPanel() {
        BikingManager.instance.addHandler(this);
        GridBagLayout layout = new GridBagLayout();
        setLayout(layout);

        setBorder(BorderFactory.createEmptyBorder(5, 5, 5, 5));


        infoLabel = new JLabel("Infos:");
        add(infoLabel, createGbc(0,0));

        try {
            bicycleImage = ImageIO.read(Objects.requireNonNull(this.getClass().getResourceAsStream("/icon_bicycle.png")));
            JLabel bicycleLabel = new JLabel(new ImageIcon(new ImageIcon(bicycleImage).getImage().getScaledInstance(20, 20, Image.SCALE_DEFAULT)));
            add(bicycleLabel, createGbc(1,1));
        } catch (IOException e) {
            throw new RuntimeException(e);
        }

        stepCountLabel = new JLabel("0/0");
        add(stepCountLabel, createGbc(0,1));

        stepLabel = new JLabel("Next step: None");
        add(stepLabel, createGbc(0,2));

        JButton nextStepBtn = new JButton("Next step");
        nextStepBtn.addActionListener((s) -> showNextStep());
        add(nextStepBtn, createGbc(0,3));
    }

    private GridBagConstraints createGbc(int x, int y) {
        GridBagConstraints gbc = new GridBagConstraints();
        gbc.gridx = x;
        gbc.gridy = y;
        gbc.gridwidth = 1;
        gbc.gridheight = 1;

        gbc.anchor = (x == 0) ? GridBagConstraints.WEST : GridBagConstraints.EAST;
        gbc.fill = (x == 0) ? GridBagConstraints.BOTH   : GridBagConstraints.HORIZONTAL;

        gbc.weightx = (x == 0) ? 0.1 : 1.0;
        gbc.weighty = 1.0;
        return gbc;
    }

    @Override
    public void onNavigationChanged(NavigationAnswer navigationAnswer) {
        if (navigationAnswer.getError().equals(NavigationError.SUCCESS)) {
            totalStepCount = navigationAnswer.getStepCount();
            currentStep = 1;
            stepQueueId = navigationAnswer.getQueueName().getValue();
            showNextStep();
        } else {
            String errorMessage = "An error occurred";
            switch (navigationAnswer.getError()) {
                case NO_LOCATION_FOUND:
                    errorMessage = "Address not found";
                    break;
                case INTERNAL_ERROR:
                    errorMessage = "Server internal error";
                    break;
                case NO_PATH_FOUND:
                    errorMessage = "No path found";
                    break;
            }
            if (!navigationAnswer.getErrorDetails().isNil()) {
                errorMessage += ": " + navigationAnswer.getErrorDetails().getValue();
            }

            JOptionPane.showMessageDialog(null, errorMessage, "Error", JOptionPane.ERROR_MESSAGE);
        }

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
