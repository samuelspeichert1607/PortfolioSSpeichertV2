package ca.csf.mobile1.tikiappclient.model;

/**
 * Created by Samuel on 2017-05-02.
 */

public class Ingredient {
    private long id;
    private String nomAnglais;
    private String nomFrancais;


    /**
     * Constructeur d'ingrédient
     *
     * @param id          long id
     * @param nomAnglais  String nomAnglais
     * @param nomFrancais String nomFrancais
     */
    public Ingredient(long id, String nomAnglais, String nomFrancais) {
        this.id = id;
        this.nomAnglais = nomAnglais;
        this.nomFrancais = nomFrancais;
    }

    /**
     * Accesseur de l'id
     *
     * @return long id.
     */
    public long getId() {
        return id;
    }

    /**
     * Assignateur de l'id.
     * @param id id à assigner.
     */
    public void setId(long id) {
        this.id = id;
    }

    /**
     * Accesseur du nom anglais.
     *
     * @return String nomAnglais.
     */
    public String getNomAnglais() {
        return nomAnglais;
    }

    /**
     * Accesseur du nom francais.
     *
     * @return String nomFrancais.
     */
    public String getNomFrancais() {
        return nomFrancais;
    }

}
