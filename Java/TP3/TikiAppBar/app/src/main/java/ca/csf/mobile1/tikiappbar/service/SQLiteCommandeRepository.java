package ca.csf.mobile1.tikiappbar.service;

import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;

import java.util.ArrayList;

import ca.csf.mobile1.tikiappbar.model.Cocktail;
import ca.csf.mobile1.tikiappbar.model.CocktailRepository;
import ca.csf.mobile1.tikiappbar.model.Commande;
import ca.csf.mobile1.tikiappbar.model.CommandeRepository;

/**
 * Created by Gabby and Samuel on 2017-05-02.
 *
 * @author Gabby and Samuel
 */

public class SQLiteCommandeRepository implements CommandeRepository {

    private final SQLiteDatabase database;
    private final CocktailRepository cocktailRepository;

    public SQLiteCommandeRepository(SQLiteDatabase database,
                                    CocktailRepository cocktailRepository) {
        this.database = database;
        this.cocktailRepository = cocktailRepository;
    }

    /**
     * Requète préparée pour l'insertion d'une nouvelle commande.
     * @param cocktail cocktail que l'on désire ajouter à la commande.
     * @param commande commande à créer.
     */
    @Override
    public void create(Cocktail cocktail, Commande commande) {
        Cursor cursor = null;
        database.beginTransaction();
        try {
            cursor = database.rawQuery(CommandeDatableTable.INSERT_SQL, new String[]{
                    commande.getNomClient(),
                    String.valueOf(commande.getHeureCommande()),
                    String.valueOf(cocktail.getId())
            });
            cursor.moveToLast(); //Nécessaire pour effectuer l'écriture
            cursor.close();

            //Obtient le dernier id créé
            cursor = database.rawQuery(ApplicationDatabaseHelper.SELECT_LAST_ID_SQL, new String[]{});
            cursor.moveToNext();
            commande.setId(cursor.getLong(0));

            //Ajoute les enfants
            commande.setCocktail(cocktail);
            //for (Cocktail cocktail1 : shoppingList) {
            cocktailRepository.create(cocktail);
            //}

            database.setTransactionSuccessful(); //Pour confirmer que l'écriture peut se faire
        } finally {
            if (cursor != null) {
                cursor.close();
            }
            database.endTransaction();
        }
    }

    /**
     * Requète préparée pour retourner une liste contenant toutes les commandes.
     * @return ArrayList<Commande> liste de commandes.
     */
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
                            cursor.getString(1),
                            cursor.getString(2)));
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

    /**
     * Requète préparée pour sélectionner une commande spécifique.
     * @param idCommande l'id de la commande que l'on désire obtenir.
     * @return la commande que l'on veut retourner.
     */
    @Override
    public Commande retrieve(long idCommande) {
        Cursor cursor = null;
        Cursor subCursor = null;
        Commande commande = null;
        try {
            database.beginTransaction();
            cursor = database.rawQuery(CommandeDatableTable.SELECT_SQL, new String[]{
                    String.valueOf(idCommande)
            });

            while (cursor.moveToNext()) {
                /*
                subCursor = database.rawQuery(CocktailDatableTable.SELECT_SQL, new String[]{
                        String.valueOf(cursor.getLong(3))
                });
                while (subCursor.moveToNext()) {
                    commande=new Commande(cursor.getLong(0),
                            (new Cocktail(subCursor.getLong(0), subCursor.getString(1), subCursor.getString(2), subCursor.getString(3), subCursor.getString(4))),
                            cursor.getString(1),
                            cursor.getString(2));
                }*/
                Cocktail cocktail=cocktailRepository.retrieve(cursor.getLong(3));
                commande=new Commande(cursor.getLong(0),
                        cocktail,
                        cursor.getString(1),
                        cursor.getString(2));
            }
            database.setTransactionSuccessful();
            return commande;
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

    /**
     * Requète préparée afin de mettre à jour une commande.
     * @param commande commande à mettre à jour.
     */
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

    /**
     * Requète préparée afin de supprimer une commande.
     * @param idCommande l'id de la commande à supprimer.
     */
    @Override
    public void delete(long idCommande) {
        Cursor cursor = null;
        database.beginTransaction();
        try {
            //Supprime les enfants
            cursor = database.rawQuery(CommandeDatableTable.DELETE_SQL, new String[]{
                    String.valueOf(idCommande)
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
