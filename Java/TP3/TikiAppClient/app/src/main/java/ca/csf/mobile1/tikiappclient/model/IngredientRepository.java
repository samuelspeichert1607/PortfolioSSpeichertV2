package ca.csf.mobile1.tikiappclient.model;

import java.util.ArrayList;

/**
 * Created by Jeammy on 2017-05-02.
 */

public interface IngredientRepository {
    void create(Ingredient ingredient);

    ArrayList<Ingredient> retrieveAll(long ingredientCocktailId);

    void update(Ingredient ingredient);

    void delete(Ingredient ingredient);
}
