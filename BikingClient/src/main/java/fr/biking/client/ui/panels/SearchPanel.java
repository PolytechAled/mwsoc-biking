package fr.biking.client.ui.panels;

import fr.biking.client.BikingManager;

import javax.swing.*;
import java.awt.*;

public class SearchPanel extends JPanel {

    public SearchPanel() {
        GridLayout layout = new GridLayout(5,1);
        setLayout(layout);
        setBorder(BorderFactory.createEmptyBorder(5, 5, 5, 5));

        JTextField fromField = new JTextField();
        JTextField toField = new JTextField();
        JButton goButton = new JButton("Check path");
        goButton.addActionListener(l -> {
            if (!BikingManager.instance.getPath(fromField.getText(),toField.getText())) {
                JOptionPane.showMessageDialog(null, "Unable to contact server", "Error", JOptionPane.ERROR_MESSAGE);
            }
        });

        add(new JLabel("Start:"));
        add(fromField);
        add(new JLabel("End:"));
        add(toField);
        add(goButton);
    }

    @Override
    public Dimension getPreferredSize() {
        return new Dimension(200, super.getPreferredSize().height);
    }
}
