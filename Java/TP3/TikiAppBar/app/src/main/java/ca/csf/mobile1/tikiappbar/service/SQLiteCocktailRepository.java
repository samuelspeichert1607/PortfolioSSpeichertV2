package ca.csf.mobile1.tikiappbar.service;

import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;

import java.util.ArrayList;

import ca.csf.mobile1.tikiappbar.model.Cocktail;
import ca.csf.mobile1.tikiappbar.model.CocktailRepository;
import ca.csf.mobile1.tikiappbar.model.IngredientCocktail;
import ca.csf.mobile1.tikiappbar.model.IngredientCocktailRepository;

/**
 * Created by Gabby on 2017-05-02.
 *
 * @author Gabrielle-Ann Bédard-Hamelin
 */

public class SQLiteCocktailRepository implements CocktailRepository {
    private final SQLiteDatabase database;
    private final IngredientCocktailRepository ingredientCocktailRepository;

    public SQLiteCocktailRepository(SQLiteDatabase database, IngredientCocktailRepository ingredientCocktailRepository) {
        this.database = database;
        this.ingredientCocktailRepository = ingredientCocktailRepository;
    }

    @Override
    public void create(Cocktail cocktail) {
        Cursor cursor = null;
        try {
            database.beginTransaction();
            cursor = database.rawQuery(CocktailDatableTable.INSERT_SQL,
                    new String[]{
                            cocktail.getNom(),
                            cocktail.getRecetteAnglais(),
                            cocktail.getRecetteFrancais(),
                            cocktail.getIdImage()
                    });
            cursor.moveToLast();
            cursor.close(); //On referme le curseur
            cursor = database.rawQuery(ApplicationDatabaseHelper.SELECT_LAST_ID_SQL, new String[]{});
            cursor.moveToNext();
            cocktail.setId(cursor.getLong(0));
            for (IngredientCocktail ingredientCocktail : cocktail) {
                ingredientCocktailRepository.create(cocktail, ingredientCocktail);
            }
            database.setTransactionSuccessful();
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            if (cursor != null) {
                cursor.close();
            }
            database.endTransaction();
        }
    }

    @Override
    public ArrayList<Cocktail> retrieveAll() {
        Cursor cursor = null;
        ArrayList<Cocktail> allCocktail = new ArrayList<Cocktail>();
        try {
            database.beginTransaction();
            cursor = database.rawQuery(CocktailDatableTable.SELECT_ALL_SQL, new String[]{});
            while (cursor.moveToNext()) {
                String test = cursor.getString(1);
                allCocktail.add(ingredientCocktailRepository.retrieveAll(cursor.getLong(0)));
            }
        } catch (Exception e) {
            e.printStackTrace();//juste pour ce test...
        } finally {
            if (cursor != null) {
                cursor.close();
            }
            database.endTransaction();
        }
        return allCocktail;
    }

    @Override
    public Cocktail retrieve(long idCocktail) {
        /*Cursor cursor = null;
        Cocktail cocktail = null;
        try {
            database.beginTransaction();
            cursor = database.rawQuery(CocktailDatableTable.SELECT_SQL, new String[]{
                    String.valueOf(idCocktail)
            });
            while (cursor.moveToNext()) {
                //    idCocktail = cursor.getLong(0);

                cocktail = new Cocktail(cursor.getLong(0), cursor.getString(1), cursor.getString(2), cursor.getString(3), cursor.getString(4));
            }
            database.setTransactionSuccessful();
            return cocktail;
        } finally {
            if (cursor != null) {
                cursor.close();
            }
            database.endTransaction();
        }*/

        Cursor cursor = null;
        Cocktail cocktail = null;
        try {
            database.beginTransaction();

            cursor = database.rawQuery(CocktailDatableTable.SELECT_SQL, new String[]{
                    String.valueOf(idCocktail)
            });
            while (cursor.moveToNext()) {
                cocktail = new Cocktail(cursor.getLong(0), cursor.getString(1), cursor.getString(2), cursor.getString(3), cursor.getString(4));
            }

            cursor = database.rawQuery(IngredientsCocktailDatableTable.SELECT_ALL_SQL, new String[]{
                    String.valueOf(idCocktail)
            });

            cursor.close();

            ArrayList<IngredientCocktail> ingredientCocktailArrayList = new ArrayList<IngredientCocktail>();

            SQLiteIngredientsCocktailRepository sqLiteIngredientsCocktailRepository = new SQLiteIngredientsCocktailRepository(database);

            cocktail = sqLiteIngredientsCocktailRepository.retrieveAll(idCocktail);

            database.setTransactionSuccessful();

            return cocktail;
        } finally {
            if (cursor != null) {
                cursor.close();
            }
            database.endTransaction();
        }

    }

    @Override
    public void update(Cocktail cocktail) {
        Cursor cursor = null;
        database.beginTransaction();
        try {
            cursor = database.rawQuery(IngredientsCocktailDatableTable.UPDATE_SQL, new String[]{
                    cocktail.getNom(),
                    cocktail.getRecetteAnglais(),
                    cocktail.getRecetteFrancais(),
                    cocktail.getIdImage()
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
    public void delete(Cocktail cocktail) {
        Cursor cursor = null;
        database.beginTransaction();
        try {
            cursor = database.rawQuery(CommandeDatableTable.DELETE_SQL, new String[]{
                    String.valueOf(cocktail.getId())
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
