package fr.biking.client;

import com.sun.tools.jxc.ap.Const;
import fr.biking.client.service.BikingService;
import fr.biking.client.service.NavigationAnswer;

import java.util.ArrayList;
import java.util.List;

public class BikingManager {

    public static final BikingManager instance = new BikingManager();
    private BikingService bikingService;
    private NavigationAnswer answer;
    private List<IBikingEvent> events;
    private boolean isConnected;

    public BikingManager() {
        isConnected = false;
        events = new ArrayList<>();
    }

    private void connect() {
        if (bikingService == null) {
            try {
                bikingService = new BikingService();
                isConnected = true;
            }
            catch (Exception ex) {
                isConnected = false;
                System.err.println(ex.getMessage());
            }
        }
    }

    public boolean getPath(String start, String end){
        try {
            if (!isConnected) connect();
            if (!isConnected || bikingService == null) return false;

            answer = bikingService.getBasicHttpBindingIBikingService().calculatePath(start,end);
            if (answer != null) {
                for (var e : events) {
                    try {
                        e.onNavigationChanged(answer);
                    } catch (Exception ignored) {
                    }
                }
            }
        } catch (Exception ex) {
            System.err.println(ex.getMessage());
        }

        return true;
    }

    public void addHandler(IBikingEvent iBikingEvent){
        this.events.add(iBikingEvent);
    }
}
