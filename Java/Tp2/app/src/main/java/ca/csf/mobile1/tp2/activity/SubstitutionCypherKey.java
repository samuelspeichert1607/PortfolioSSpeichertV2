package ca.csf.mobile1.tp2.activity;

/**
 * Created by Devilclown1607 on 2017-03-24.
 */

public class SubstitutionCypherKey
{


    private int key = 0;
    private char[] inputCharacters;
    private char[] outputCharacters;

    public SubstitutionCypherKey(int key, char[] inputCharacters, char[] outputCharacters) {
        this.key = key;
        this.inputCharacters = inputCharacters;
        this.outputCharacters = outputCharacters;
    }

    public int getKey() {
        return key;
    }

    public char[] getInputCharacters() {
        return inputCharacters;
    }

    public char[] getOutputCharacters() {
        return outputCharacters;
    }
}
