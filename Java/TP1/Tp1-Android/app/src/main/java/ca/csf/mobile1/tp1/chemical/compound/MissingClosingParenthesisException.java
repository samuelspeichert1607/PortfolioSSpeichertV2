package ca.csf.mobile1.tp1.chemical.compound;

/**
 * Created by Devilclown1607 on 2017-02-20.
 */

/**
 * Exception appelée si une parenthèse orpheline est présente.
 */
public class MissingClosingParenthesisException extends Exception {
    @Override
    public String getMessage(){
        return "Il y a une parenthèse ouvrante sans sa parenthèse fermante. \\n";
    }
}
