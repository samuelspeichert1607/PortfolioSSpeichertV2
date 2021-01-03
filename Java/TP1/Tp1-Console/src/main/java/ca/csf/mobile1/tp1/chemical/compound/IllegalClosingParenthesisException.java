package ca.csf.mobile1.tp1.chemical.compound;

/**
 * Created by Samuel Speichert on 2017-02-20.
 */


/**
 * Exception appelée si deux parenthèses sont mal placés
 */
public class IllegalClosingParenthesisException extends Exception {
    @Override
    public String getMessage(){
        return "Il y a une parenthèse fermante sans sa parenthèse ouvrante. \\n";
    }
}
