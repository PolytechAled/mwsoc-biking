package fr.biking.client;

import fr.biking.client.service.NavigationAnswer;

public interface IBikingEvent {

    void onNavigationChanged(NavigationAnswer navigationAnswer);
}
