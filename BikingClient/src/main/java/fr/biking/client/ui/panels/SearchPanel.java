package fr.biking.client.ui.panels;

import fr.biking.client.BikingManager;

import javax.swing.*;
import java.awt.*;

public class SearchPanel extends JPanel {

    public SearchPanel() {
        GridLayout layout = new GridLayout(5,1);
        setLayout(layout);

        JTextField fromField = new JTextField();
        JTextField toField = new JTextField();
        JButton goButton = new JButton("Check path");
        goButton.addActionListener(l -> {
            BikingManager.instance.getPath(fromField.getText(),toField.getText());
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
