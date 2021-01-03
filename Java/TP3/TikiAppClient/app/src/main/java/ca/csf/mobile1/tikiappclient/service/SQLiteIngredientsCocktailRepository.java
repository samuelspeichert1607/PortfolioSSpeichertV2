package ca.csf.mobile1.tikiappclient.service;

import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;

import ca.csf.mobile1.tikiappclient.model.Cocktail;
import ca.csf.mobile1.tikiappclient.model.Ingredient;
import ca.csf.mobile1.tikiappclient.model.IngredientCocktail;
import ca.csf.mobile1.tikiappclient.model.IngredientCocktailRepository;

/**
 * Created by Gabby on 2017-05-02.
 */


public class SQLiteIngredientsCocktailRepository implements IngredientCocktailRepository {

    private final SQLiteDatabase database;

    public SQLiteIngredientsCocktailRepository(SQLiteDatabase database) {
        this.database = database;
    }

    @Override
    public void create(Cocktail cocktail, IngredientCocktail ingredientCocktail) { //Ingredient ingredient,
        Cursor cursor = null;
        database.beginTransaction();
        try {
            cursor = database.rawQuery(IngredientsCocktailDatableTable.INSERT_SQL, new String[]{
                    Float.toString(ingredientCocktail.getQuantite()),
                    ingredientCocktail.getMesure(),
                    Long.toString(ingredientCocktail.getIngredient().getId()),
                    Long.toString(cocktail.getId())
            });
            cursor.moveToLast(); //Nécessaire pour effectuer l'écriture
            cursor.close();

            //Obtient le dernier id créé
            cursor = database.rawQuery(ApplicationDatabaseHelper.SELECT_LAST_ID_SQL, new String[]{});
            cursor.moveToNext();
            ingredientCocktail.setId(cursor.getLong(0));

            database.setTransactionSuccessful(); //Pour confirmer que l'écriture peut se faire
        } finally {
            if (cursor != null) {
                cursor.close();
            }
            database.endTransaction();
        }
    }

    @Override
    public Cocktail retrieveAll(long cocktailId) {
        database.beginTransaction();
        Cursor cursor = null;
        Cursor subCursor = null;
        try {
            cursor = database.rawQuery(CocktailDatableTable.SELECT_SQL, new String[]{
                    String.valueOf(cocktailId)
            });
            cursor.moveToLast();

            Cocktail cocktail = new Cocktail(cursor.getLong(0), cursor.getString(1), cursor.getString(2), cursor.getString(3), cursor.getString(4));

            // On ferme le curseur
            cursor.close();

            // On refais une autre requète pour aller chercher les ingrédients du cocktail.
            cursor = database.rawQuery(IngredientsCocktailDatableTable.SELECT_ALL_SQL, new String[]{
                    String.valueOf(cocktailId)
            });
            while (cursor.moveToNext()) {
                subCursor = database.rawQuery(IngredientDatableTable.SELECT_SQL, new String[]{
                        String.valueOf(cursor.getInt(3))
                });
                while (subCursor.moveToNext()) {
                    cocktail.add(new IngredientCocktail(cursor.getLong(0), cursor.getFloat(1), cursor.getString(2),
                            new Ingredient(subCursor.getLong(0), subCursor.getString(1), subCursor.getString(2))));
                }
            }
            return cocktail;
        } finally {
            if (subCursor != null) {
                subCursor.close();
            }
            if (cursor != null) {
                cursor.close();
            }
            database.endTransaction();
        }

    }

    @Override
    public void update(IngredientCocktail ingredientCocktail) {
        Cursor cursor = null;
        database.beginTransaction();
        try {
            cursor = database.rawQuery(IngredientsCocktailDatableTable.UPDATE_SQL, new String[]{
                    Float.toString(ingredientCocktail.getQuantite()),
                    ingredientCocktail.getMesure(),
                    Long.toString(ingredientCocktail.getIngredient().getId()),
                    Long.toString(ingredientCocktail.getId())
            });
            cursor.moveToLast(); //Nécessaire pour effectuer l'écriture
            database.setTransactionSuccessful(); //Pour confirmer que l'écriture peut se faire
        } finally {
            if (cursor != null) {
                cursor.close();
            }
            database.endTransaction();
        }
    }

    @Override
    public void delete(IngredientCocktail ingredientCocktail) {
        Cursor cursor = null;
        database.beginTransaction();
        try {
            cursor = database.rawQuery(IngredientsCocktailDatableTable.DELETE_SQL, new String[]{
                    String.valueOf(ingredientCocktail.getId())
            });
            cursor.moveToLast(); //Nécessaire pour effectuer l'écriture
            database.setTransactionSuccessful(); //Pour confirmer que l'écriture peut se faire
        } finally {
            if (cursor != null) {
                cursor.close();
            }
            database.endTransaction();
        }
    }
}
