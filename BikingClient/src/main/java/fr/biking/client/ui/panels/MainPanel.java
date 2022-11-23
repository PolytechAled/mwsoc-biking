package fr.biking.client.ui.panels;

import javax.swing.*;

public class MainPanel extends JPanel {

    private SearchPanel searchPanel;
    private StepsPanel stepsPanel;

    public MainPanel() {
        this.searchPanel = new SearchPanel();
        this.stepsPanel = new StepsPanel();

        BoxLayout layout = new BoxLayout(this, BoxLayout.X_AXIS);
        setLayout(layout);

        setBorder(BorderFactory.createEmptyBorder(0, 10, 10, 10));
        add(this.searchPanel);
        add(this.stepsPanel);
    }

}
