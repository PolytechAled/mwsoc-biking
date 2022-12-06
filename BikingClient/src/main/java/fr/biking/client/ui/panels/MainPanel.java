package fr.biking.client.ui.panels;

import fr.biking.client.BikingManager;
import fr.biking.client.IBikingEvent;
import fr.biking.client.service.NavigationAnswer;
import fr.biking.client.service.NavigationError;

import javax.swing.*;
import java.awt.*;

import static java.awt.GridBagConstraints.HORIZONTAL;

public class MainPanel extends JPanel implements IBikingEvent {

    private SearchPanel searchPanel;
    private StepsPanel stepsPanel;
    private MapPanel mapPanel;

    public MainPanel() {
        BikingManager.instance.addHandler(this);
        this.searchPanel = new SearchPanel();
        this.stepsPanel = new StepsPanel();
        this.mapPanel = new MapPanel();


        GridBagLayout layout = new GridBagLayout();
        setLayout(layout);

        GridBagConstraints c = new GridBagConstraints();

        c.gridx = 0;
        c.gridy = 0;
        add(searchPanel, c);

        c.gridx = 1;
        c.gridy = 0;
        add(mapPanel, c);

        c.gridx = 0;
        c.gridy = 1;
        c.fill = HORIZONTAL;
        add(stepsPanel, c);
        stepsPanel.setVisible(false);
    }

    @Override
    public void onNavigationChanged(NavigationAnswer navigationAnswer) {
        if (navigationAnswer.getError().equals(NavigationError.SUCCESS)) {
            stepsPanel.setVisible(true);
        }
    }
}
