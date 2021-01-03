package ca.csf.mobile1.tikiappbar.model;

import java.util.ArrayList;

/**
 * Created by Gabrielle-Ann and Samuel on 2017-05-02.
 * @author Gabrielle-Ann and Samuel
 */

public interface CommandeRepository {

    void create(Cocktail cocktail, Commande commande);

    ArrayList<Commande> retrieveAll();

    Commande retrieve(long idCommande);

    void update(Commande commande);

    void delete(long idCommande);

}
