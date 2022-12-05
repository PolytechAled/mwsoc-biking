
package fr.biking.client.service;

import javax.xml.bind.annotation.XmlEnum;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Classe Java pour NavigationError.
 * 
 * <p>Le fragment de schéma suivant indique le contenu attendu figurant dans cette classe.
 * <p>
 * <pre>
 * &lt;simpleType name="NavigationError"&gt;
 *   &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string"&gt;
 *     &lt;enumeration value="SUCCESS"/&gt;
 *     &lt;enumeration value="INTERNAL_ERROR"/&gt;
 *     &lt;enumeration value="NO_PATH_FOUND"/&gt;
 *     &lt;enumeration value="NO_LOCATION_FOUND"/&gt;
 *   &lt;/restriction&gt;
 * &lt;/simpleType&gt;
 * </pre>
 * 
 */
@XmlType(name = "NavigationError")
@XmlEnum
public enum NavigationError {

    SUCCESS,
    INTERNAL_ERROR,
    NO_PATH_FOUND,
    NO_LOCATION_FOUND;

    public String value() {
        return name();
    }

    public static NavigationError fromValue(String v) {
        return valueOf(v);
    }

}
