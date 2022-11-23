package fr.biking.client;

import fr.biking.client.service.BikingService;

public class BikingClient {

    public static void main(String[] args) {
        BikingService service = new BikingService();
        System.out.println(service.getBasicHttpBindingIBikingService().calculatePath("147 Boulevard de la Republique", "Polytech Nice, Antibes"));
    }

}
