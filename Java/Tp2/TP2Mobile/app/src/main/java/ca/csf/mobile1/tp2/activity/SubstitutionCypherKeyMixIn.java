package ca.csf.mobile1.tp2.activity;

import com.fasterxml.jackson.annotation.JsonCreator;
import com.fasterxml.jackson.annotation.JsonProperty;
/**
 * Created by Samuel Speichert and Alexandre Lachance on 2017-03-28.
 */

public abstract class SubstitutionCypherKeyMixIn extends SubstitutionCypherKey {


    @JsonCreator
    public SubstitutionCypherKeyMixIn(@JsonProperty("key") int key, @JsonProperty("inputCharacters") char[] inputCharacters, @JsonProperty("outputCharacters") char[] outputCharacters )
    {
        super(key, inputCharacters, outputCharacters);
    }

    @JsonProperty("key")
    public abstract int getKey();
    @JsonProperty("inputCharacters")
    public abstract char[] getInputCharacters();
    @JsonProperty("outputCharacters")
    public abstract char[] getOutputCharacters();
}