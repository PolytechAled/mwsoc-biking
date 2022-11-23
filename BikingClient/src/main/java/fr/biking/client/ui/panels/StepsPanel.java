package fr.biking.client.ui.panels;

import fr.biking.client.BikingManager;
import fr.biking.client.IBikingEvent;

import javax.swing.*;
import java.awt.*;

public class StepsPanel extends JPanel implements IBikingEvent {

    private TextArea textArea;

    public StepsPanel() {
        BikingManager.instance.setHandler(this);
        BoxLayout layout = new BoxLayout(this, BoxLayout.Y_AXIS);
        setLayout(layout);
        setBorder(BorderFactory.createEmptyBorder(0, 10, 10, 10));

        add(new JLabel("Steps:"));
        textArea = new TextArea();
        add(textArea);
    }

    @Override
    public void onNavigationChanged() {
        String text = "";
        for(var step : BikingManager.instance.getSteps().getNavigationStep()){
            text+=step.getText().getValue()+"\n";
        }
        textArea.setText(text);
    }
}
