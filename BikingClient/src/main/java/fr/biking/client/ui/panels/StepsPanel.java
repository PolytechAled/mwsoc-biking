package fr.biking.client.ui.panels;

import javax.swing.*;
import java.awt.*;

public class StepsPanel extends JPanel {

    public StepsPanel() {
        BoxLayout layout = new BoxLayout(this, BoxLayout.Y_AXIS);
        setLayout(layout);
        setBorder(BorderFactory.createEmptyBorder(0, 10, 10, 10));

        add(new TextArea());
    }

}
