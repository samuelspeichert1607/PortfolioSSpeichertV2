package ca.csf.mobile1.tikiappbar.model;

/**
 * Created by Samuel on 2017-05-02.
 */

public class Commande {
    private long id;
    private Cocktail cocktail;
    private String nomClient;
    private String heureCommande;

    public Commande(long id, Cocktail cocktail, String nomClient, String heureCommande) {
        this.id = id;
        this.cocktail = cocktail;
        this.nomClient = nomClient;
        this.heureCommande = heureCommande;
    }

    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    public Cocktail getCocktail() {
        return cocktail;
    }

    public void setCocktail(Cocktail cocktail) {
        this.cocktail = cocktail;
    }

    public String getNomClient() {
        return nomClient;
    }

    public void setNomClient(String nomClient) {
        this.nomClient = nomClient;
    }

    public String getHeureCommande() {
        return heureCommande;
    }

    public void setHeureCommande(String heureCommande) {
        this.heureCommande = heureCommande;
    }


}
