package ca.csf.mobile1.tikiappclient.service;

import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;

import java.util.ArrayList;

import ca.csf.mobile1.tikiappclient.model.Cocktail;
import ca.csf.mobile1.tikiappclient.model.CocktailRepository;
import ca.csf.mobile1.tikiappclient.model.Commande;
import ca.csf.mobile1.tikiappclient.model.CommandeRepository;

/**
 * Created by Gabby and Samuel on 2017-05-02.
 */

public class SQLiteCommandeRepository implements CommandeRepository {

    private final SQLiteDatabase database;
    private final CocktailRepository cocktailRepository;

    public SQLiteCommandeRepository(SQLiteDatabase database,
                                    CocktailRepository cocktailRepository) {
        this.database = database;
        this.cocktailRepository = cocktailRepository;
    }

    @Override
    public void create(Cocktail cocktail, Commande commande) {
        Cursor cursor = null;
        database.beginTransaction();
        try {
            cursor = database.rawQuery(CommandeDatableTable.INSERT_SQL, new String[]{
                    String.valueOf(commande.getId()),
                    String.valueOf(cocktail.getId()),
                    commande.getNomClient(),
                    String.valueOf(commande.getHeureCommande())
            });
            cursor.moveToLast(); //Ceci est nécessaire pour effectuer l'écriture
            cursor.close();

            //Obtient le dernier id créé
            cursor = database.rawQuery(ApplicationDatabaseHelper.SELECT_LAST_ID_SQL, new String[]{});
            cursor.moveToNext();
            commande.setId(cursor.getLong(0));

            //Ajoute les enfants
            commande.setCocktail(cocktail);
            cocktailRepository.create(cocktail);

            database.setTransactionSuccessful(); //Pour confirmer que l'écriture peut se faire
        } finally {
            if (cursor != null) {
                cursor.close();
            }
            database.endTransaction();
        }
    }

    @Override
    public ArrayList<Commande> retrieveAll() {
        Cursor cursor = null;
        Cursor subCursor = null; //Convenu avec Dan
        try {
            cursor = database.rawQuery(CommandeDatableTable.SELECT_ALL_SQL, new String[]{});

            ArrayList<Commande> commandeArrayList = new ArrayList<Commande>();
            while (cursor.moveToNext()) {
                subCursor = database.rawQuery(CocktailDatableTable.SELECT_SQL, new String[]{
                        String.valueOf(cursor.getInt(3))
                });
                while (subCursor.moveToNext()) {
                    commandeArrayList.add(new Commande(cursor.getLong(0),
                            (new Cocktail(subCursor.getLong(0), subCursor.getString(1), subCursor.getString(2), subCursor.getString(3), subCursor.getString(4))),
                            cursor.getString(2),
                            cursor.getString(1)));
                }
            }
            return commandeArrayList;
        } finally {
            if (subCursor != null) {
                subCursor.close();
            }
            if (cursor != null) {
                cursor.close();
            }
        }
    }

    @Override
    public Commande retrieve(long idCommande) {
        Cursor cursor = null;
        Cursor subCursor = null;
        Commande commande = null;
        try {
            cursor = database.rawQuery(CommandeDatableTable.SELECT_SQL, new String[]{
                    String.valueOf(idCommande)
            });
            while (cursor.moveToNext()) {
                subCursor = database.rawQuery(CocktailDatableTable.SELECT_SQL, new String[]{
                        String.valueOf(cursor.getInt(3))
                });
                commande = new Commande(cursor.getLong(0),
                        new Cocktail(subCursor.getLong(0), subCursor.getString(1), subCursor.getString(2), subCursor.getString(3), subCursor.getString(4)),
                        cursor.getString(2),
                        cursor.getString(3));
            }
            return commande;
        } finally {
            if (subCursor != null) {
                subCursor.close();
            }
            if (cursor != null) {
                cursor.close();
            }
        }
    }

    @Override
    public void update(Commande commande) {
        Cursor cursor = null;
        database.beginTransaction();
        try {
            cocktailRepository.update(commande.getCocktail());
            cursor = database.rawQuery(CommandeDatableTable.UPDATE_SQL, new String[]{
                    String.valueOf(commande.getId()),
                    commande.getNomClient(),
                    String.valueOf(commande.getHeureCommande())
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
    public void delete(Commande commande) {
        Cursor cursor = null;
        database.beginTransaction();
        try {
            //Supprime les enfants
            //for (ShoppingItem shoppingItem : shoppingList) {
            //    shoppingItemRepository.delete(shoppingItem);
            //}

            //commande.getCocktail().getIngredientCocktails().remove();

            cursor = database.rawQuery(CommandeDatableTable.DELETE_SQL, new String[]{
                    String.valueOf(commande.getId())
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
