package ca.csf.mobile1.tikiappbar.model;

import java.util.ArrayList;

/**
 * Created by Jeammy on 2017-05-02.
 */

public interface CocktailRepository {
    void create(Cocktail cocktail);

    ArrayList<Cocktail> retrieveAll();

    Cocktail retrieve(long idCocktail);

    void update(Cocktail cocktail);

    void delete(Cocktail cocktail);
}
