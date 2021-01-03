package ca.csf.mobile1.tikiappclient.model;

/**
 * Created by Jeammy on 2017-05-02.
 */

public interface IngredientCocktailRepository {
    void create(Cocktail cocktail, IngredientCocktail ingredientCocktail); //Ingredient ingredient,

    Cocktail retrieveAll(long cocktailId);

    void update(IngredientCocktail ingredientCocktail);

    void delete(IngredientCocktail ingredientCocktail);
}
