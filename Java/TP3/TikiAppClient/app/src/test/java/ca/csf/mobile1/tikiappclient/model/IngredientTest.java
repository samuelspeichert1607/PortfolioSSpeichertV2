package ca.csf.mobile1.tikiappclient.model;


import org.junit.Test;

import static junit.framework.Assert.assertEquals;

/**
 * Created by Devilclown1607 on 2017-05-12.
 */
public class IngredientTest {

    @Test
    public void getId() throws Exception {
        Ingredient ingredient = new Ingredient(1, "rat poison", "mort-au-rat");
        assertEquals(ingredient.getId(), 1);
    }

    @Test
    public void getNomAnglais() throws Exception {
        Ingredient ingredient = new Ingredient(1, "rat poison", "mort-au-rat");
        assertEquals(ingredient.getNomAnglais(), "rat poison");
    }

    @Test
    public void getNomFrancais() throws Exception {
        Ingredient ingredient = new Ingredient(1, "rat poison", "mort-au-rat");
        assertEquals(ingredient.getNomFrancais(), "mort-au-rat");
    }

}