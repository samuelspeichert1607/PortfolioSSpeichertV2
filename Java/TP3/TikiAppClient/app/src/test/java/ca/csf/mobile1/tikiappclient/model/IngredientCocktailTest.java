package ca.csf.mobile1.tikiappclient.model;

import org.junit.Test;

import static junit.framework.Assert.assertEquals;

/**
 * Created by Devilclown1607 on 2017-05-12.
 */
public class IngredientCocktailTest {
    @Test
    public void getId() throws Exception {
        Ingredient ingredient = new Ingredient(1, "rat poison", "mort-au-rat");
        IngredientCocktail ingredientCocktail = new IngredientCocktail(1, 1.2f, "oz", ingredient);
        assertEquals(ingredientCocktail.getId(), 1);
    }

    @Test
    public void getQuantite() throws Exception {
        Ingredient ingredient = new Ingredient(1, "rat poison", "mort-au-rat");
        IngredientCocktail ingredientCocktail = new IngredientCocktail(1, 1.2f, "oz", ingredient);
        assertEquals(ingredientCocktail.getQuantite(), 1.2f);
    }

    @Test
    public void getMesure() throws Exception {
        Ingredient ingredient = new Ingredient(1, "rat poison", "mort-au-rat");
        IngredientCocktail ingredientCocktail = new IngredientCocktail(1, 1.2f, "oz", ingredient);
        assertEquals(ingredientCocktail.getMesure(), "oz");
    }

    @Test
    public void getIngredient() throws Exception {
        Ingredient ingredient = new Ingredient(1, "rat poison", "mort-au-rat");
        IngredientCocktail ingredientCocktail = new IngredientCocktail(1, 1.2f, "oz", ingredient);
        assertEquals(ingredientCocktail.getIngredient(), ingredient);
    }

}