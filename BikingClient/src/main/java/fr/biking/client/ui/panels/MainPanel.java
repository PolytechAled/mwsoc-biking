package fr.biking.client.ui.panels;

import fr.biking.client.BikingManager;
import fr.biking.client.IBikingEvent;
import fr.biking.client.service.NavigationAnswer;
import fr.biking.client.service.NavigationError;
import fr.biking.client.ui.StepFrame;

import javax.swing.*;
import java.awt.*;

import static java.awt.GridBagConstraints.HORIZONTAL;

public class MainPanel extends JPanel implements IBikingEvent {

    private SearchPanel searchPanel;
    private MapPanel mapPanel;
    private StepFrame stepFrame;

    public MainPanel() {
        BikingManager.instance.addHandler(this);
        this.stepFrame = new StepFrame();
        this.searchPanel = new SearchPanel();
        this.mapPanel = new MapPanel();


        GridBagLayout layout = new GridBagLayout();
        setLayout(layout);
        GridBagConstraints c = new GridBagConstraints();
        c.fill = HORIZONTAL;

        c.gridx = 0;
        c.gridy = 0;
        add(searchPanel, c);

        c.gridx = 0;
        c.gridy = 1;
        add(mapPanel, c);
    }

    @Override
    public void onNavigationChanged(NavigationAnswer navigationAnswer) {
        if (navigationAnswer.getError().equals(NavigationError.SUCCESS)) {
            stepFrame.setVisible(true);
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
}
