package ca.csf.mobile1.tikiappclient.service;

import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;

import java.util.ArrayList;

import ca.csf.mobile1.tikiappclient.model.Ingredient;
import ca.csf.mobile1.tikiappclient.model.IngredientRepository;

/**
 * Created by Gabby on 2017-05-02.
 */

public class SQLiteIngredientRepository implements IngredientRepository {

    private final SQLiteDatabase database;

    public SQLiteIngredientRepository(SQLiteDatabase database) {
        this.database = database;
    }

    @Override
    public void create(Ingredient ingredient) {
        Cursor cursor = null;
        database.beginTransaction();
        try {
            cursor = database.rawQuery(IngredientDatableTable.INSERT_SQL, new String[]{
                    String.valueOf(ingredient.getId()),
                    ingredient.getNomAnglais(),
                    ingredient.getNomFrancais()
            });
            cursor.moveToLast(); //Nécessaire pour effectuer l'écriture
            cursor.close();

            //Obtient le dernier id créé
            cursor = database.rawQuery(ApplicationDatabaseHelper.SELECT_LAST_ID_SQL, new String[]{});
            cursor.moveToNext();
            ingredient.setId(cursor.getLong(0));

            database.setTransactionSuccessful(); //Pour confirmer que l'écriture peut se faire
        } finally {
            if (cursor != null) {
                cursor.close();
            }
            database.endTransaction();
        }
    }

    @Override
    public ArrayList<Ingredient> retrieveAll(long ingredientCocktailId) {
        Cursor cursor = null;
        try {
            cursor = database.rawQuery(IngredientsCocktailDatableTable.SELECT_ALL_SQL, new String[]{
                    String.valueOf(ingredientCocktailId)
            });

            ArrayList<Ingredient> ingredientArrayList = null;
            if (cursor.moveToNext()) {
                ingredientArrayList = new ArrayList<Ingredient>();
                cursor.close();

                cursor = database.rawQuery(IngredientDatableTable.SELECT_SQL, new String[]{
                        String.valueOf(ingredientCocktailId)
                });
                while (cursor.moveToNext()) {

                    ingredientArrayList.add(new Ingredient(cursor.getLong(0), cursor.getString(1), cursor.getString(2)));
                }
            }
            return ingredientArrayList;
        } finally {
            if (cursor != null) {
                cursor.close();
            }
        }
    }

    @Override
    public void update(Ingredient ingredient) {
        Cursor cursor = null;
        database.beginTransaction();
        try {
            cursor = database.rawQuery(IngredientDatableTable.UPDATE_SQL, new String[]{
                    String.valueOf(ingredient.getId()),
                    ingredient.getNomAnglais(),
                    ingredient.getNomFrancais()
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
    public void delete(Ingredient ingredient) {
        Cursor cursor = null;
        database.beginTransaction();
        try {
            cursor = database.rawQuery(IngredientDatableTable.DELETE_SQL, new String[]{
                    String.valueOf(ingredient.getId())
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
