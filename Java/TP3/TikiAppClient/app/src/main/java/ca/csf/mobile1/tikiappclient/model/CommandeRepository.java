package ca.csf.mobile1.tikiappclient.model;

import java.util.ArrayList;

/**
 * Created by Gabby and Samuel on 2017-05-02.
 */

public interface CommandeRepository {

    void create(Cocktail cocktail, Commande commande);

    ArrayList<Commande> retrieveAll();

    Commande retrieve(long idCommande);

    void update(Commande commande);

    void delete(Commande commande);

}
