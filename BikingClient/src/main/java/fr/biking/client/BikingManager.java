package fr.biking.client;

import fr.biking.client.service.BikingService;
import fr.biking.client.service.NavigationAnswer;

public class BikingManager {

    public static final BikingManager instance = new BikingManager();
    private BikingService bikingService;
    private NavigationAnswer answer;
    private IBikingEvent event;

    public BikingManager() {
        bikingService = new BikingService();
    }

    public void getPath(String start, String end){
        answer = bikingService.getBasicHttpBindingIBikingService().calculatePath(start,end);
        if(event!=null && answer!=null) event.onNavigationChanged(answer);
    }

    public NavigationAnswer getAnswer() {
        return answer;
    }

    public void setHandler(IBikingEvent iBikingEvent){
        this.event = iBikingEvent;
    }
}
