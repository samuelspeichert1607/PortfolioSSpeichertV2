package ca.csf.mobile1.tp1.chemical.compound;

/**
 * Created by Samuel Speichert on 2017-02-20.
 */

/**
 * Exception appelée si la formule entrée est vide.
 */
public class EmptyFormulaException extends Exception {
    @Override
    public String getMessage(){
        return "La formule entrée est vide...";
    }
}
