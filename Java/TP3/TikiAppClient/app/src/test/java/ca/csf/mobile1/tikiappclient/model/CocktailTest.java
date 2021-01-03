package ca.csf.mobile1.tikiappclient.model;

import org.junit.Test;

import java.util.ArrayList;

import static junit.framework.Assert.assertEquals;

/**
 * Created by Samuel on 2017-05-12.
 */
public class CocktailTest {
    @Test
    public void getId() throws Exception {
        Cocktail cocktail = new Cocktail(1, "Bloody Ceasar", "recetteAnglais", "recetteFrancais", "bloodyCeasar.png");
        assertEquals(cocktail.getId(), 1);
    }

    @Test
    public void getNom() throws Exception {
        Cocktail cocktail = new Cocktail(1, "Bloody Ceasar", "recetteAnglais", "recetteFrancais", "bloodyCeasar.png");
        assertEquals(cocktail.getNom(), "Bloody Ceasar");
    }

    @Test
    public void getRecetteAnglais() throws Exception {
        Cocktail cocktail = new Cocktail(1, "Bloody Ceasar", "recetteAnglais", "recetteFrancais", "bloodyCeasar.png");
        assertEquals(cocktail.getRecetteAnglais(), "recetteAnglais");
    }

    @Test
    public void getRecetteFrancais() throws Exception {
        Cocktail cocktail = new Cocktail(1, "Bloody Ceasar", "recetteAnglais", "recetteFrancais", "bloodyCeasar.png");
        assertEquals(cocktail.getRecetteFrancais(), "recetteFrancais");
    }

    @Test
    public void getIdImage() throws Exception {
        Cocktail cocktail = new Cocktail(1, "Bloody Ceasar", "recetteAnglais", "recetteFrancais", "bloodyCeasar.png");
        assertEquals(cocktail.getIdImage(), "bloodyCeasar.png");
    }

    @Test
    public void getIngredientCocktails() throws Exception {
        Cocktail cocktail = new Cocktail(1, "Bloody Ceasar", "recetteAnglais", "recetteFrancais", "bloodyCeasar.png");
        ArrayList<IngredientCocktail> ingredientCocktailArrayList = new ArrayList<IngredientCocktail>();
        cocktail.setIngredientCocktails(ingredientCocktailArrayList);
        assertEquals(cocktail.getIngredientCocktails(), ingredientCocktailArrayList);
    }

    @Test
    public void add() throws Exception {
        Cocktail cocktail = new Cocktail(1, "Bloody Ceasar", "recetteAnglais", "recetteFrancais", "bloodyCeasar.png");

        ArrayList<IngredientCocktail> ingredientCocktailArrayList = new ArrayList<IngredientCocktail>();

        cocktail.setIngredientCocktails(ingredientCocktailArrayList);

        IngredientCocktail ingredientCocktail = new IngredientCocktail(1, 0.4f, "oz", new Ingredient(1, "pineapple", "ananas"));

        cocktail.add(ingredientCocktail);

        assertEquals(cocktail.getIngredientCocktails().get(0), ingredientCocktail);

    }

}