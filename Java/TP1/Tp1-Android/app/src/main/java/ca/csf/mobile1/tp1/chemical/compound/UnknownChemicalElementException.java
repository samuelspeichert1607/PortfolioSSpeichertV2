package ca.csf.mobile1.tp1.chemical.compound;

/**
 * Created by Samuel Speichert on 2017-02-20.
 */

/**
 * Exception appelée si un élément chimique est placé.
 */
public class UnknownChemicalElementException extends Exception
{
    private String element;
    private String message;

    public UnknownChemicalElementException(String element)
    {
        this.element = element;
        message = "L'élément chimique " + element + "est inconnu.";
    }

    public String getElement()
    {
        return element;
    }

    @Override
    public String getMessage(){
        return message;
    }

}
