
package fr.biking.client.service;

import javax.xml.bind.JAXBElement;
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElementRef;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Classe Java pour anonymous complex type.
 * 
 * <p>Le fragment de schéma suivant indique le contenu attendu figurant dans cette classe.
 * 
 * <pre>
 * &lt;complexType&gt;
 *   &lt;complexContent&gt;
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType"&gt;
 *       &lt;sequence&gt;
 *         &lt;element name="startPoint" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/&gt;
 *         &lt;element name="endPoint" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/&gt;
 *       &lt;/sequence&gt;
 *     &lt;/restriction&gt;
 *   &lt;/complexContent&gt;
 * &lt;/complexType&gt;
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "", propOrder = {
    "startPoint",
    "endPoint"
})
@XmlRootElement(name = "CalculatePath", namespace = "http://tempuri.org/")
public class CalculatePath {

    @XmlElementRef(name = "startPoint", namespace = "http://tempuri.org/", type = JAXBElement.class, required = false)
    protected JAXBElement<String> startPoint;
    @XmlElementRef(name = "endPoint", namespace = "http://tempuri.org/", type = JAXBElement.class, required = false)
    protected JAXBElement<String> endPoint;

    /**
     * Obtient la valeur de la propriété startPoint.
     * 
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public JAXBElement<String> getStartPoint() {
        return startPoint;
    }

    /**
     * Définit la valeur de la propriété startPoint.
     * 
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public void setStartPoint(JAXBElement<String> value) {
        this.startPoint = value;
    }

    /**
     * Obtient la valeur de la propriété endPoint.
     * 
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public JAXBElement<String> getEndPoint() {
        return endPoint;
    }

    /**
     * Définit la valeur de la propriété endPoint.
     * 
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public void setEndPoint(JAXBElement<String> value) {
        this.endPoint = value;
    }

}
