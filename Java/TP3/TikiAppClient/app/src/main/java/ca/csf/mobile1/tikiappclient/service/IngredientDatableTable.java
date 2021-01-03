package ca.csf.mobile1.tikiappclient.service;

/**
 * Classe constante IngredientDatableTable, elle ne peut être instanciées.
 * Elle contient des requêtes SQL statiques et constantes pour cette table.
 * Ces requètes seront utilisées par {@link SQLiteIngredientRepository}.
 *
 * @author Gabrielle-Ann Bédard-Hamelin
 */
public final class IngredientDatableTable {
    /**
     * Variable string statique et constante représentant la requète SQL pour la création de la
     * table ingrédients.
     */
    public static final String CREATE_TABLE_SQL = "" +
            "CREATE TABLE ingredient (" +
            "idingredient         INTEGER  PRIMARY KEY  AUTOINCREMENT, " +
            "nom_anglais          VARCHAR(45), " +
            "nom_francais         VARCHAR(45)" +
            ")";

    /**
     * Variable string statique et constante représentant la requète SQL pour la destruction de la
     * table ingrédients.
     */
    public static final String DROP_TABLE_SQL = "" +
            "DROP TABLE ingredient";

    /**
     * Variable string statique et constante représentant la requète SQL pour la sélection d'un
     * enregistrements précis de la table ingrédients.
     */
    public static final String SELECT_SQL = "" +
            "SELECT * " +
            "FROM ingredient " +
            "WHERE idingredient = ?";

    /**
     * Variable string statique et constante représentant la requète SQL pour l'insertion d'un
     * nouvel enregistrement de la table ingrédients.
     */
    public static final String INSERT_SQL = "" +
            "INSERT INTO ingredient (nom_anglais, nom_francais) " +
            "VALUES (?,?)";

    /**
     * Variable string statique et constante représentant la requète SQL pour la mise à jour d'un
     * enregistrement de la table ingrédients.
     */
    public static final String UPDATE_SQL = "" +
            "UPDATE ingredient " +
            "SET (nom_anglais = ?, nom_francais = ?) " +
            "WHERE (idingredient = ?)";

    /**
     * Variable string statique et constante représentant la requète SQL pour la suppression d'un
     * enregistrement de la table ingrédients.
     */
    public static final String DELETE_SQL = "" +
            "DELETE FROM ingredient " +
            "WHERE idingredient = ?";

    /**
     * Constructeur privé de la classe IngredientDatableTable.
     */
    private IngredientDatableTable() {

    }
}
