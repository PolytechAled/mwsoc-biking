
package fr.biking.client.service;

import java.net.MalformedURLException;
import java.net.URL;
import javax.xml.namespace.QName;
import javax.xml.ws.Service;
import javax.xml.ws.WebEndpoint;
import javax.xml.ws.WebServiceClient;
import javax.xml.ws.WebServiceException;
import javax.xml.ws.WebServiceFeature;


/**
 * This class was generated by the JAX-WS RI.
 * JAX-WS RI 2.3.2
 * Generated source version: 2.2
 * 
 */
@WebServiceClient(name = "BikingService", targetNamespace = "http://tempuri.org/", wsdlLocation = "http://localhost:8733/Design_Time_Addresses/BikingServer/Service/?wsdl")
public class BikingService
    extends Service
{

    private final static URL BIKINGSERVICE_WSDL_LOCATION;
    private final static WebServiceException BIKINGSERVICE_EXCEPTION;
    private final static QName BIKINGSERVICE_QNAME = new QName("http://tempuri.org/", "BikingService");

    static {
        URL url = null;
        WebServiceException e = null;
        try {
            url = new URL("http://localhost:8733/Design_Time_Addresses/BikingServer/Service/?wsdl");
        } catch (MalformedURLException ex) {
            e = new WebServiceException(ex);
        }
        BIKINGSERVICE_WSDL_LOCATION = url;
        BIKINGSERVICE_EXCEPTION = e;
    }

    public BikingService() {
        super(__getWsdlLocation(), BIKINGSERVICE_QNAME);
    }

    public BikingService(WebServiceFeature... features) {
        super(__getWsdlLocation(), BIKINGSERVICE_QNAME, features);
    }

    public BikingService(URL wsdlLocation) {
        super(wsdlLocation, BIKINGSERVICE_QNAME);
    }

    public BikingService(URL wsdlLocation, WebServiceFeature... features) {
        super(wsdlLocation, BIKINGSERVICE_QNAME, features);
    }

    public BikingService(URL wsdlLocation, QName serviceName) {
        super(wsdlLocation, serviceName);
    }

    public BikingService(URL wsdlLocation, QName serviceName, WebServiceFeature... features) {
        super(wsdlLocation, serviceName, features);
    }

    /**
     * 
     * @return
     *     returns IBikingService
     */
    @WebEndpoint(name = "BasicHttpBinding_IBikingService")
    public IBikingService getBasicHttpBindingIBikingService() {
        return super.getPort(new QName("http://tempuri.org/", "BasicHttpBinding_IBikingService"), IBikingService.class);
    }

    /**
     * 
     * @param features
     *     A list of {@link javax.xml.ws.WebServiceFeature} to configure on the proxy.  Supported features not in the <code>features</code> parameter will have their default values.
     * @return
     *     returns IBikingService
     */
    @WebEndpoint(name = "BasicHttpBinding_IBikingService")
    public IBikingService getBasicHttpBindingIBikingService(WebServiceFeature... features) {
        return super.getPort(new QName("http://tempuri.org/", "BasicHttpBinding_IBikingService"), IBikingService.class, features);
    }

    private static URL __getWsdlLocation() {
        if (BIKINGSERVICE_EXCEPTION!= null) {
            throw BIKINGSERVICE_EXCEPTION;
        }
        return BIKINGSERVICE_WSDL_LOCATION;
    }

}
