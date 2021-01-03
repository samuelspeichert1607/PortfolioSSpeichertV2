package ca.csf.mobile1.tikiappclient.model;

import org.junit.Test;

import static junit.framework.Assert.assertEquals;

/**
 * Created by Devilclown1607 on 2017-05-12.
 */
public class CommandeTest {
    @Test
    public void getId() throws Exception {
        Cocktail cocktail = new Cocktail(1, "Bloody Ceasar", "recetteAnglais", "recetteFrancais", "bloodyCeasar.png");
        Commande commande = new Commande(1, cocktail, "Jack", "16:00");
        assertEquals(commande.getId(), 1);
    }

    @Test
    public void getCocktail() throws Exception {
        Cocktail cocktail = new Cocktail(1, "Bloody Ceasar", "recetteAnglais", "recetteFrancais", "bloodyCeasar.png");
        Commande commande = new Commande(1, cocktail, "Jack", "16:00");
        assertEquals(commande.getCocktail(), cocktail);
    }

    @Test
    public void getNomClient() throws Exception {
        Cocktail cocktail = new Cocktail(1, "Bloody Ceasar", "recetteAnglais", "recetteFrancais", "bloodyCeasar.png");
        Commande commande = new Commande(1, cocktail, "Jack", "16:00");
        assertEquals(commande.getNomClient(), "Jack");
    }

    @Test
    public void getHeureCommande() throws Exception {
        Cocktail cocktail = new Cocktail(1, "Bloody Ceasar", "recetteAnglais", "recetteFrancais", "bloodyCeasar.png");
        Commande commande = new Commande(1, cocktail, "Jack", "16:00");
        assertEquals(commande.getHeureCommande(), "16:00");
    }

}