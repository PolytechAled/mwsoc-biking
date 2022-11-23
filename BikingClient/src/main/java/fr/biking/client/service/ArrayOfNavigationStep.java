
package fr.biking.client.service;

import java.util.ArrayList;
import java.util.List;
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Classe Java pour ArrayOfNavigationStep complex type.
 * 
 * <p>Le fragment de schéma suivant indique le contenu attendu figurant dans cette classe.
 * 
 * <pre>
 * &lt;complexType name="ArrayOfNavigationStep"&gt;
 *   &lt;complexContent&gt;
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType"&gt;
 *       &lt;sequence&gt;
 *         &lt;element name="NavigationStep" type="{http://schemas.datacontract.org/2004/07/BikingServer.Models}NavigationStep" maxOccurs="unbounded" minOccurs="0"/&gt;
 *       &lt;/sequence&gt;
 *     &lt;/restriction&gt;
 *   &lt;/complexContent&gt;
 * &lt;/complexType&gt;
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "ArrayOfNavigationStep", propOrder = {
    "navigationStep"
})
public class ArrayOfNavigationStep {

    @XmlElement(name = "NavigationStep", nillable = true)
    protected List<NavigationStep> navigationStep;

    /**
     * Gets the value of the navigationStep property.
     * 
     * <p>
     * This accessor method returns a reference to the live list,
     * not a snapshot. Therefore any modification you make to the
     * returned list will be present inside the JAXB object.
     * This is why there is not a <CODE>set</CODE> method for the navigationStep property.
     * 
     * <p>
     * For example, to add a new item, do as follows:
     * <pre>
     *    getNavigationStep().add(newItem);
     * </pre>
     * 
     * 
     * <p>
     * Objects of the following type(s) are allowed in the list
     * {@link NavigationStep }
     * 
     * 
     */
    public List<NavigationStep> getNavigationStep() {
        if (navigationStep == null) {
            navigationStep = new ArrayList<NavigationStep>();
        }
        return this.navigationStep;
    }

}
