
package fr.biking.client.service;

import javax.xml.bind.JAXBElement;
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlElementRef;
import javax.xml.bind.annotation.XmlSchemaType;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Classe Java pour NavigationAnswer complex type.
 * 
 * <p>Le fragment de schéma suivant indique le contenu attendu figurant dans cette classe.
 * 
 * <pre>
 * &lt;complexType name="NavigationAnswer"&gt;
 *   &lt;complexContent&gt;
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType"&gt;
 *       &lt;sequence&gt;
 *         &lt;element name="Error" type="{http://schemas.datacontract.org/2004/07/BikingServer.Models}NavigationError" minOccurs="0"/&gt;
 *         &lt;element name="ErrorDetails" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/&gt;
 *         &lt;element name="InterestPoints" type="{http://schemas.datacontract.org/2004/07/BikingServer.Models}ArrayOfInterestPoint" minOccurs="0"/&gt;
 *         &lt;element name="QueueName" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/&gt;
 *         &lt;element name="StepCount" type="{http://www.w3.org/2001/XMLSchema}int" minOccurs="0"/&gt;
 *         &lt;element name="UseBicycle" type="{http://www.w3.org/2001/XMLSchema}boolean" minOccurs="0"/&gt;
 *       &lt;/sequence&gt;
 *     &lt;/restriction&gt;
 *   &lt;/complexContent&gt;
 * &lt;/complexType&gt;
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "NavigationAnswer", propOrder = {
    "error",
    "errorDetails",
    "interestPoints",
    "queueName",
    "stepCount",
    "useBicycle"
})
public class NavigationAnswer {

    @XmlElement(name = "Error")
    @XmlSchemaType(name = "string")
    protected NavigationError error;
    @XmlElementRef(name = "ErrorDetails", namespace = "http://schemas.datacontract.org/2004/07/BikingServer.Models", type = JAXBElement.class, required = false)
    protected JAXBElement<String> errorDetails;
    @XmlElementRef(name = "InterestPoints", namespace = "http://schemas.datacontract.org/2004/07/BikingServer.Models", type = JAXBElement.class, required = false)
    protected JAXBElement<ArrayOfInterestPoint> interestPoints;
    @XmlElementRef(name = "QueueName", namespace = "http://schemas.datacontract.org/2004/07/BikingServer.Models", type = JAXBElement.class, required = false)
    protected JAXBElement<String> queueName;
    @XmlElement(name = "StepCount")
    protected Integer stepCount;
    @XmlElement(name = "UseBicycle")
    protected Boolean useBicycle;

    /**
     * Obtient la valeur de la propriété error.
     * 
     * @return
     *     possible object is
     *     {@link NavigationError }
     *     
     */
    public NavigationError getError() {
        return error;
    }

    /**
     * Définit la valeur de la propriété error.
     * 
     * @param value
     *     allowed object is
     *     {@link NavigationError }
     *     
     */
    public void setError(NavigationError value) {
        this.error = value;
    }

    /**
     * Obtient la valeur de la propriété errorDetails.
     * 
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public JAXBElement<String> getErrorDetails() {
        return errorDetails;
    }

    /**
     * Définit la valeur de la propriété errorDetails.
     * 
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public void setErrorDetails(JAXBElement<String> value) {
        this.errorDetails = value;
    }

    /**
     * Obtient la valeur de la propriété interestPoints.
     * 
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link ArrayOfInterestPoint }{@code >}
     *     
     */
    public JAXBElement<ArrayOfInterestPoint> getInterestPoints() {
        return interestPoints;
    }

    /**
     * Définit la valeur de la propriété interestPoints.
     * 
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link ArrayOfInterestPoint }{@code >}
     *     
     */
    public void setInterestPoints(JAXBElement<ArrayOfInterestPoint> value) {
        this.interestPoints = value;
    }

    /**
     * Obtient la valeur de la propriété queueName.
     * 
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public JAXBElement<String> getQueueName() {
        return queueName;
    }

    /**
     * Définit la valeur de la propriété queueName.
     * 
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public void setQueueName(JAXBElement<String> value) {
        this.queueName = value;
    }

    /**
     * Obtient la valeur de la propriété stepCount.
     * 
     * @return
     *     possible object is
     *     {@link Integer }
     *     
     */
    public Integer getStepCount() {
        return stepCount;
    }

    /**
     * Définit la valeur de la propriété stepCount.
     * 
     * @param value
     *     allowed object is
     *     {@link Integer }
     *     
     */
    public void setStepCount(Integer value) {
        this.stepCount = value;
    }

    /**
     * Obtient la valeur de la propriété useBicycle.
     * 
     * @return
     *     possible object is
     *     {@link Boolean }
     *     
     */
    public Boolean isUseBicycle() {
        return useBicycle;
    }

    /**
     * Définit la valeur de la propriété useBicycle.
     * 
     * @param value
     *     allowed object is
     *     {@link Boolean }
     *     
     */
    public void setUseBicycle(Boolean value) {
        this.useBicycle = value;
    }

}
