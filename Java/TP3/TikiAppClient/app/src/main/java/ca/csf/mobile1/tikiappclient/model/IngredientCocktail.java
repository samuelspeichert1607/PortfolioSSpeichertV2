package ca.csf.mobile1.tikiappclient.model;

/**
 * Created by Samuel on 2017-05-02.
 * @author Samuel
 */

public class IngredientCocktail {
    private long id;
    private Ingredient ingredient;
    private float quantite;
    private String mesure;

    /**
     * Constructeur d'ingrédient cocktail.
     *
     * @param id         long id
     * @param quantite   float quantité
     * @param mesure     string mesure
     * @param ingredient Ingredient ingredient
     */
    public IngredientCocktail(long id, float quantite, String mesure, Ingredient ingredient) {
        this.id = id;
        this.ingredient = ingredient;
        this.quantite = quantite;
        this.mesure = mesure;
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
     * Accesseur de la quantité
     * @param  id id à assigner.
     */
    public void setId(long id) {
        this.id = id;
    }
    /**
     * Accesseur de la quantité
     *
     * @return float quantité.
     */
    public float getQuantite() {
        return quantite;
    }

    public void setQuantite(float quantite) {
        this.quantite = quantite;
    }

    /**
     * Accesseur de la mesure
     * @return string quantité.
     */
    public String getMesure() {
        return mesure;
    }

    public void setMesure(String mesure) {
        this.mesure = mesure;
    }

    /**
     * Accesseur de l'ingrédient.
     * @return string ingrédient.
     */
    public Ingredient getIngredient() {
        return ingredient;
    }

    /**
     * Assignateur de l'ingrédient
     * @param ingredient
     */
    public void setIngredient(Ingredient ingredient) {
        this.ingredient = ingredient;
    }
}
