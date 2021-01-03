package ca.csf.mobile1.tp1.chemical.compound;

/**
 * Created by Samuel Speichert on 2017-02-20.
 */

/**
 * Exception appelée si un exponent est mal placé, au tout début par exemple.
 */
public class MisplacedExponentException extends Exception {

    @Override
    public String getMessage(){
        return "Exponent found before any other chemical element or parenthesis.";
    }

}
