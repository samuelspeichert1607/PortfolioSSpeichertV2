package ca.csf.mobile1.tikiappbar.service;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;

import ca.csf.mobile1.tikiappbar.R;

/**
 * La classe ApplicationDatabaseHelper offre les méthodes nécessaire pour aider l'application a une
 * bonne gestion de la base de données.
 *
 * @author Gabrielle-Ann Bédard-Hamelin
 * @see SQLiteOpenHelper
 * @see SQLiteDatabase
 */
public class ApplicationDatabaseHelper extends SQLiteOpenHelper {

    /**
     * Variable string statique et constante représentant la requète SQL pour la récupération de
     * l'id du dernier enregistrement entré.
     */
    public static final String SELECT_LAST_ID_SQL = "" +
            "SELECT last_insert_rowid()";

    /**
     * Variable entière statique et constante représentant le numéro de version
     * de la base de données.
     */
    private static final int DATABASE_VERSION = 1;

    /**
     * Variable de context constante représentant le context de l'appareil.
     */
    private final Context context;

    /**
     * Constructeur de la classe, il appel le constructeur de la super classe et initialise
     * le context.
     *
     * @param context  objet de type {@link Context} représentant le context de l'appareil.
     * @param fileName variable String représentant le nom du fichier de la base de données.
     */
    public ApplicationDatabaseHelper(Context context, String fileName) {
        super(context, fileName, null, DATABASE_VERSION);
        this.context = context;
    }

    /**
     * Méthode appelé si le fichier de la base de données est inexistant.
     * La méthode crée toutes les tables et les remplis des enregistrements de base.
     *
     * @param db objet de type {@link SQLiteDatabase} représentant la base de donnée.
     */
    @Override
    public void onCreate(SQLiteDatabase db) {
        db.execSQL(IngredientDatableTable.CREATE_TABLE_SQL);
        db.execSQL(CocktailDatableTable.CREATE_TABLE_SQL);
        db.execSQL(IngredientsCocktailDatableTable.CREATE_TABLE_SQL);
        db.execSQL(CommandeDatableTable.CREATE_TABLE_SQL);

        String startUpDatabaseQuery = getStartUpDataQuery(R.raw.sqlscript_startup_ingredients);
        db.execSQL(String.valueOf(startUpDatabaseQuery));
        startUpDatabaseQuery = getStartUpDataQuery(R.raw.sqlscript_startup_cocktail);
        db.execSQL(String.valueOf(startUpDatabaseQuery));
        startUpDatabaseQuery = getStartUpDataQuery(R.raw.sqlscript_startup_ingredients_cocktail);
        db.execSQL(String.valueOf(startUpDatabaseQuery));
    }

    /**
     * Méthode appelé pour la lecture des fichiers SQL.
     * La méthode reçoit en paramètres un entier équivalant a l'id de la ressource (raw sql).
     * Elle retourne une string représentant le contenu du fichier.
     *
     * @param idRessource variable entière représentant le int(ID) de la ressource.
     * @return String représentant la requête du fichier.
     */
    private String getStartUpDataQuery(int idRessource) {
        StringBuilder startUpDatabaseQuery = new StringBuilder();
        try (
                InputStream inputStream = context.getResources().openRawResource(idRessource);
                InputStreamReader reader = new InputStreamReader(inputStream);
                BufferedReader bufferedReader = new BufferedReader(reader)
        ) {
            String line;
            while ((line = bufferedReader.readLine()) != null) {
                startUpDatabaseQuery.append(line);
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
        return startUpDatabaseQuery.toString();
    }

    /**
     * Méthode appelé lors de la mise à jour de la base de données.
     * La méthode détruit les vieilles tables et appel de nouveau la méthode onCreate.
     *
     * @param db         objet de type {@link SQLiteDatabase} représentant la base de donnée.
     * @param oldVersion variable entière représentant le numéro de la dernière version.
     * @param newVersion variable entière représentant le numéro de la nouvelle version.
     */
    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        db.execSQL(IngredientDatableTable.DROP_TABLE_SQL);
        db.execSQL(CocktailDatableTable.DROP_TABLE_SQL);
        db.execSQL(IngredientsCocktailDatableTable.DROP_TABLE_SQL);
        db.execSQL(CommandeDatableTable.DROP_TABLE_SQL);
        onCreate(db);
    }
}
