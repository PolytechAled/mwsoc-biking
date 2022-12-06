package fr.biking.client.ui;

import fr.biking.client.ui.panels.StepsPanel;

import javax.swing.*;

public class StepFrame extends JFrame {

    public StepFrame() {
        super();
        setContentPane(new StepsPanel());
        pack();
        setLocationRelativeTo(null);
        setVisible(false);
        setResizable(false);
        setDefaultCloseOperation(HIDE_ON_CLOSE);
    }

}
