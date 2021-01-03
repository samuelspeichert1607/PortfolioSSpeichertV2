package ca.csf.mobile1.tikiappclient.model;


import java.util.ArrayList;
import java.util.Iterator;

/**
 * Created by Samuel on 2017-05-02.
 */

public class Cocktail implements Iterable<IngredientCocktail> {
    private long id;
    private String nom;
    private String recetteAnglais;
    private String recetteFrancais;
    private String nomImage;
    private ArrayList<IngredientCocktail> ingredientCocktails = new ArrayList<IngredientCocktail>();

    /**
     * Constructeur de cocktail
     *
     * @param id              long id
     * @param nom             String nom
     * @param recetteAnglais  String recetteAnglais
     * @param recetteFrancais String recetteFrancais
     * @param nomImage        String nomImage
     */
    public Cocktail(long id, String nom, String recetteAnglais, String recetteFrancais, String nomImage) {
        this.id = id;
        this.nom = nom;
        this.recetteAnglais = recetteAnglais;
        this.recetteFrancais = recetteFrancais;
        this.nomImage = nomImage;
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
     * Accesseur de nom.
     *
     * @return string nom
     */
    public String getNom() {
        return nom;
    }

    public void setNom(String nom) {
        this.nom = nom;
    }

    /**
     * Accesseur de recetteAnglais.
     *
     * @return string recetteAnglais
     */
    public String getRecetteAnglais() {
        return recetteAnglais;
    }

    public void setRecetteAnglais(String recetteAnglais) {
        this.recetteAnglais = recetteAnglais;
    }

    /**
     * Accesseur de recetteFrancais.
     *
     * @return string recetteFrancais
     */
    public String getRecetteFrancais() {
        return recetteFrancais;
    }

    public void setRecetteFrancais(String recetteFrancais) {
        this.recetteFrancais = recetteFrancais;
    }

    /**
     * Accesseur de nomImage.
     *
     * @return string nomImage
     */
    public String getIdImage() {
        return nomImage;
    }

    public void setIdImage(String nomImage) {
        this.nomImage = nomImage;
    }

    /**
     * Accesseur de la liste ingredientCocktails. {@link IngredientCocktail}
     *
     * @return ArrayList<IngredientCocktail> ingredientCocktails
     */
    public ArrayList<IngredientCocktail> getIngredientCocktails() {
        return ingredientCocktails;
    }

    public void setIngredientCocktails(ArrayList<IngredientCocktail> ingredientCocktails) {
        this.ingredientCocktails = ingredientCocktails;
    }

    /**
     * Cette méthode permet d'ajouter dans la liste d'ingrédients-cocktails une nouvel
     * {@link IngredientCocktail}.
     *
     * @param ingredientCocktail IngredientCocktail ingredientCocktail
     */
    public void add(IngredientCocktail ingredientCocktail) {
        ingredientCocktails.add(ingredientCocktail);
    }

    @Override
    public Iterator<IngredientCocktail> iterator() {
        return ingredientCocktails.iterator();
    }
}
