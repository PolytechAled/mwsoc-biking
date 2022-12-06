package fr.biking.client.ui.panels;


import fr.biking.client.BikingManager;
import fr.biking.client.IBikingEvent;
import fr.biking.client.service.NavigationAnswer;
import fr.biking.client.service.NavigationError;
import fr.biking.client.ui.utils.SelectionAdapter;
import org.jxmapviewer.JXMapViewer;
import org.jxmapviewer.OSMTileFactoryInfo;
import org.jxmapviewer.input.CenterMapListener;
import org.jxmapviewer.input.PanKeyListener;
import org.jxmapviewer.input.PanMouseInputListener;
import org.jxmapviewer.input.ZoomMouseWheelListenerCursor;
import org.jxmapviewer.painter.CompoundPainter;
import org.jxmapviewer.painter.Painter;
import org.jxmapviewer.viewer.DefaultTileFactory;
import org.jxmapviewer.viewer.DefaultWaypoint;
import org.jxmapviewer.viewer.GeoPosition;
import org.jxmapviewer.viewer.TileFactoryInfo;
import org.jxmapviewer.viewer.Waypoint;
import org.jxmapviewer.viewer.WaypointPainter;

import javax.swing.*;
import javax.swing.event.MouseInputListener;
import java.awt.*;
import java.util.*;
import java.util.List;
import java.util.stream.Collectors;

public class MapPanel extends JPanel implements IBikingEvent {

    private JXMapViewer mapViewer;
    private List<GeoPosition> waypoints;

    public MapPanel() {
        BikingManager.instance.addHandler(this);
        this.waypoints = new ArrayList<>();

        TileFactoryInfo info = new OSMTileFactoryInfo();
        DefaultTileFactory tileFactory = new DefaultTileFactory(info);

        mapViewer = new JXMapViewer();
        mapViewer.setTileFactory(tileFactory);
        mapViewer.setZoom(7);
        mapViewer.setAddressLocation(new GeoPosition(43.295155, 5.374407));

        // Add interactions
        MouseInputListener mia = new PanMouseInputListener(mapViewer);
        mapViewer.addMouseListener(mia);
        mapViewer.addMouseMotionListener(mia);
        mapViewer.addMouseListener(new CenterMapListener(mapViewer));
        mapViewer.addMouseWheelListener(new ZoomMouseWheelListenerCursor(mapViewer));
        mapViewer.addKeyListener(new PanKeyListener(mapViewer));

        // Add a selection painter
        SelectionAdapter sa = new SelectionAdapter(mapViewer);
        mapViewer.addMouseListener(sa);
        mapViewer.addMouseMotionListener(sa);

        BoxLayout layout = new BoxLayout(this, BoxLayout.Y_AXIS);
        setLayout(layout);
        setBorder(BorderFactory.createEmptyBorder(5, 5, 5, 5));

        add(mapViewer);
    }

    private void addWaypoint(double latitude, double longitude) {
        waypoints.add(new GeoPosition(latitude, longitude));
    }

    private void drawWaypoints() {
        if (waypoints != null && !waypoints.isEmpty()) {
            Set<Waypoint> waypoints = this.waypoints.stream().map(DefaultWaypoint::new).collect(Collectors.toSet());

            WaypointPainter<Waypoint> waypointPainter = new WaypointPainter<>();
            waypointPainter.setWaypoints(waypoints);

            List<Painter<JXMapViewer>> painters = new ArrayList<Painter<JXMapViewer>>();
            painters.add(waypointPainter);

            CompoundPainter<JXMapViewer> painter = new CompoundPainter<JXMapViewer>(painters);
            mapViewer.setOverlayPainter(painter);

            // Set the focus
            mapViewer.setAddressLocation(this.waypoints.get(0));
            mapViewer.zoomToBestFit(new HashSet<>(this.waypoints), 0.7);
        }
    }

    @Override
    public Dimension getPreferredSize() {
        return new Dimension(600, 600);
    }

    @Override
    public Dimension getMinimumSize() {
        return new Dimension(600,600);
    }

    @Override
    public void onNavigationChanged(NavigationAnswer navigationAnswer) {
        this.waypoints.clear();

        if (navigationAnswer.getError().equals(NavigationError.SUCCESS)) {
            if (!navigationAnswer.getInterestPoints().isNil()) {
                for(var ip : navigationAnswer.getInterestPoints().getValue().getInterestPoint()) {
                    addWaypoint(ip.getLatitude(), ip.getLongitude());
                }
                drawWaypoints();
            }
        }
    }
}
