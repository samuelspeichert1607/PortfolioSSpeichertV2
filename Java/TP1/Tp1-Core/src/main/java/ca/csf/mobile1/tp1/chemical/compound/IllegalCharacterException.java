package ca.csf.mobile1.tp1.chemical.compound;

/**
 * Created by Samuel Speichert on 2017-02-20.
 */


/**
 * Exception appelée si un caractère non-autorisé est vide.
 */
public class IllegalCharacterException extends Exception
{
    private char character;

    public IllegalCharacterException(char character)
    {
        this.character = character;
    }

    public char getCharacter()
    {
        return character;
    }


}
