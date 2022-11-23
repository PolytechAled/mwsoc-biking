package fr.biking.client.ui.panels;

import javax.swing.*;
import java.awt.*;

public class SearchPanel extends JPanel {

    public SearchPanel() {
        GridLayout layout = new GridLayout(10,1, 10, 10);
        setLayout(layout);
        setBorder(BorderFactory.createEmptyBorder(0, 10, 10, 10));

        JTextField fromField = new JTextField();
        JTextField toField = new JTextField();
        JButton goButton = new JButton("Test");

        add(new JLabel("Start:"));
        add(fromField);
        add(new JLabel("End:"));
        add(toField);
        add(goButton);
    }

}
