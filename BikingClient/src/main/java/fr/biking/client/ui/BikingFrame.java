package fr.biking.client.ui;

import fr.biking.client.ui.panels.MainPanel;

import javax.swing.*;

public class BikingFrame extends JFrame {

    public BikingFrame() {
        super("Biking App");
        setContentPane(new MainPanel());

        pack();
        setResizable(true);
        setLocationRelativeTo(null);
        setDefaultCloseOperation(EXIT_ON_CLOSE);
    }


}
