package fr.biking.client;

import fr.biking.client.service.ArrayOfNavigationStep;
import fr.biking.client.service.BikingService;

public class BikingManager {

    public static final BikingManager instance = new BikingManager();
    private BikingService bikingService;
    private ArrayOfNavigationStep steps;
    private IBikingEvent event;

    public BikingManager() {
        bikingService = new BikingService();
    }

    public void getPath(String start, String end){
        steps = bikingService.getBasicHttpBindingIBikingService().calculatePath(start,end);
        if(event!=null && steps!=null) event.onNavigationChanged();
    }

    public ArrayOfNavigationStep getSteps() {
        return steps;
    }

    public void setHandler(IBikingEvent iBikingEvent){
        this.event = iBikingEvent;
    }
}
