package ca.csf.mobile1.tikiappbar.model;


import java.util.ArrayList;
import java.util.Iterator;

/**
 * Created by Samuel on 2017-05-02.
 *
 * @author Samuel
 */

public class Cocktail implements Iterable<IngredientCocktail> {
    private long id;
    private String nom;
    private String recetteAnglais;
    private String recetteFrancais;
    private String nomImage;
    private ArrayList<IngredientCocktail> ingredientCocktails = new ArrayList<IngredientCocktail>();

    public Cocktail(long id, String nom, String recetteAnglais, String recetteFrancais, String nomImage) {
        this.id = id;
        this.nom = nom;
        this.recetteAnglais = recetteAnglais;
        this.recetteFrancais = recetteFrancais;
        this.nomImage = nomImage;
    }

    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    public String getNom() {
        return nom;
    }

    public void setNom(String nom) {
        this.nom = nom;
    }

    public String getRecetteAnglais() {
        return recetteAnglais;
    }

    public void setRecetteAnglais(String recetteAnglais) {
        this.recetteAnglais = recetteAnglais;
    }

    public String getRecetteFrancais() {
        return recetteFrancais;
    }

    public void setRecetteFrancais(String recetteFrancais) {
        this.recetteFrancais = recetteFrancais;
    }

    public String getIdImage() {
        return nomImage;
    }

    public void setIdImage(String nomImage) {
        this.nomImage = nomImage;
    }

    public ArrayList<IngredientCocktail> getIngredientCocktails() {
        return ingredientCocktails;
    }

    public void setIngredientCocktails(ArrayList<IngredientCocktail> ingredientCocktails) {
        this.ingredientCocktails = ingredientCocktails;
    }

    public void add(IngredientCocktail ingredientCocktail) {
        ingredientCocktails.add(ingredientCocktail);
    }

    @Override
    public Iterator<IngredientCocktail> iterator() {
        return ingredientCocktails.iterator();
    }
}
