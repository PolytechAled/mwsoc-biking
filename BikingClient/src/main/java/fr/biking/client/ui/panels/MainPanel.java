package fr.biking.client.ui.panels;

import javax.swing.*;

public class MainPanel extends JPanel {

    private SearchPanel searchPanel;
    private StepsPanel stepsPanel;
    private MapPanel mapPanel;

    public MainPanel() {
        this.searchPanel = new SearchPanel();
        this.stepsPanel = new StepsPanel();
        this.mapPanel = new MapPanel();

        BoxLayout mainLayout = new BoxLayout(this, BoxLayout.X_AXIS);
        setLayout(mainLayout);

        JPanel leftPanel = new JPanel();
        BoxLayout leftLayout = new BoxLayout(leftPanel, BoxLayout.Y_AXIS);
        leftPanel.setLayout(leftLayout);
        leftPanel.add(this.searchPanel);
        leftPanel.add(this.stepsPanel);

        setBorder(BorderFactory.createEmptyBorder(0, 10, 10, 10));
        add(leftPanel);
        add(this.mapPanel);
    }

}
