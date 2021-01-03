package ca.csf.mobile1.tikiappclient.model;

/**
 * Created by Samuel on 2017-05-02.
 */

/**
 * Classe commande.
 */
public class Commande {
    private long id;
    private Cocktail cocktail;
    private String nomClient;
    private String heureCommande;

    /**
     * Constructeur de la classe commande.
     *
     * @param id            L'id du constructeur.
     * @param cocktail      Le {@link Cocktail}. qui a été commandé.
     * @param nomClient     Le nom du client.
     * @param heureCommande L'heure ou la commande à été effectuée.
     */
    public Commande(long id, Cocktail cocktail, String nomClient, String heureCommande) {
        this.id = id;
        this.cocktail = cocktail;
        this.nomClient = nomClient;
        this.heureCommande = heureCommande;
    }

    /**
     * Accesseur de l'id
     *
     * @return long id.
     */
    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    /**
     * Accesseur du {@link Cocktail}.
     *
     * @return {@link Cocktail} cocktail.
     */
    public Cocktail getCocktail() {
        return cocktail;
    }

    public void setCocktail(Cocktail cocktail) {
        this.cocktail = cocktail;
    }

    /**
     * @return String nomClient.
     * @author Samuel Speichert
     * Accesseur du nom du client.
     */
    public String getNomClient() {
        return nomClient;
    }

    public void setNomClient(String nomClient) {
        this.nomClient = nomClient;
    }

    /**
     * Accesseur de l'heure de la commande.
     *
     * @return String heureCommande.
     */
    public String getHeureCommande() {
        return heureCommande;
    }

    public void setHeureCommande(String heureCommande) {
        this.heureCommande = heureCommande;
    }


}
