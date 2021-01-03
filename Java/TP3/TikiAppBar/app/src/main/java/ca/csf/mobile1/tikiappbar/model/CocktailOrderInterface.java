package ca.csf.mobile1.tikiappbar.model;

import java.util.ArrayList;

/**
 * Created by Gabby on 2017-05-13.
 */

public interface CocktailOrderInterface {
    void createNewOrder(long idCocktail, String clientName);

    ArrayList<Commande> retrieveAllOrders();

    Commande retrieveOrder(long idCommande);

    void markOrderAsDone(long idCommande);
}
