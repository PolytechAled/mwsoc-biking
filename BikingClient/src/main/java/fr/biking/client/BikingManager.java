package fr.biking.client;

import fr.biking.client.service.BikingService;
import fr.biking.client.service.NavigationAnswer;

import java.util.ArrayList;
import java.util.List;

public class BikingManager {

    public static final BikingManager instance = new BikingManager();
    private BikingService bikingService;
    private NavigationAnswer answer;
    private List<IBikingEvent> events;

    public BikingManager() {
        bikingService = new BikingService();
        events = new ArrayList<>();
    }

    public void getPath(String start, String end){
        answer = bikingService.getBasicHttpBindingIBikingService().calculatePath(start,end);
        if (answer != null) {
            for (var e : events) {
                e.onNavigationChanged(answer);
            }
        }
    }

    public void addHandler(IBikingEvent iBikingEvent){
        this.events.add(iBikingEvent);
    }
}
