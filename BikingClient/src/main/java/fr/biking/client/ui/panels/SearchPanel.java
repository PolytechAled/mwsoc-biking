package fr.biking.client.ui.panels;

import javax.swing.*;

public class SearchPanel extends JPanel {

    public SearchPanel() {
        BoxLayout layout = new BoxLayout(this, BoxLayout.Y_AXIS);
        setLayout(layout);
        setBorder(BorderFactory.createEmptyBorder(0, 10, 10, 10));

        JTextField fromField = new JTextField();
        JTextField toField = new JTextField();
        JButton goButton = new JButton("Test");

        add(fromField);
        add(toField);
        add(goButton);
    }

}
