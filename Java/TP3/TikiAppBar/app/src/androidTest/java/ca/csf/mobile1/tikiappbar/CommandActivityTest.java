package ca.csf.mobile1.tikiappbar;

import android.content.Intent;
import android.support.test.rule.ActivityTestRule;

import org.junit.Rule;
import org.junit.Test;

import ca.csf.mobile1.tikiappbar.model.Cocktail;
import ca.csf.mobile1.tikiappbar.model.Commande;
import ca.csf.mobile1.tikiappbar.model.Ingredient;
import ca.csf.mobile1.tikiappbar.model.IngredientCocktail;

/**
 * Created by Alex on 2017-05-13.
 *@author Samuel
 */
public class CommandActivityTest {
    @Rule
    public ActivityTestRule<CommandActivity> activityRule = new ActivityTestRule<>(CommandActivity.class, false, false);

    private void show() {
        activityRule.launchActivity(null);
    }

    private void show(Commande commande) {
        Intent intent = new Intent();
        CommandActivity.configureIntent(intent, commande);
        activityRule.launchActivity(intent);
    }

    private Commande createCommandeForTesting() {
        Ingredient ingredient = new Ingredient(1, "test", "test");
        IngredientCocktail ingredientCocktailUn = new IngredientCocktail(1, 2.5f, "oz", ingredient);
        IngredientCocktail ingredientCocktailDeux = new IngredientCocktail(2, 0.5f, "oz", ingredient);
        Cocktail cocktail = new Cocktail(1, "test", "test", "test", "amaretto_sour");
        cocktail.add(ingredientCocktailUn);
        cocktail.add(ingredientCocktailDeux);
        Commande commande = new Commande(1, cocktail, "Test", "Test");
        return commande;
    }

    @Test
    public void useAppContext() throws Exception {
        show(createCommandeForTesting());
    }

}