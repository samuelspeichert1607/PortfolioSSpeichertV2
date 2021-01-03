package ca.csf.mobile1.tp2.activity;

/**
 * Created by Samuel Speichert and Alexandre Lachance on 2017-03-27.
 */

/**
 * Ceci est l'interface dont les abonnés vont hériter.
 */
public interface Subscriber {
    public void showError(String message);
    public void receiveCypher(SubstitutionCypherKey substitutionCypherKey);
}
