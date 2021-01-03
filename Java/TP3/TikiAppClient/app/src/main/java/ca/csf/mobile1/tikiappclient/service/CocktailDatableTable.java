package ca.csf.mobile1.tikiappclient.service;

/**
 * Classe constante CocktailDatableTable, elle ne peut être instanciées.
 * Elle contient des requêtes SQL statiques et constantes pour cette table.
 * Ces requètes seront utilisées par {@link SQLiteCocktailRepository}.
 *
 * @author Gabrielle-Ann Bédard-Hamelin
 */
public final class CocktailDatableTable {
    /**
     * Variable string statique et constante représentant la requète SQL pour la création de la
     * table cocktail.
     */
    public static final String CREATE_TABLE_SQL = "" +
            "CREATE TABLE cocktail (" +
            "idcocktail        INTEGER  PRIMARY KEY  AUTOINCREMENT, " +
            "nom               VARCHAR(45), " +
            "recette_anglais   TEXT, " +
            "recette_francais  TEXT, " +
            "nom_image         TEXT" +
            ")";

    /**
     * Variable string statique et constante représentant la requète SQL pour la destruction de la
     * table cocktail.
     */
    public static final String DROP_TABLE_SQL = "" +
            "DROP TABLE cocktail";

    /**
     * Variable string statique et constante représentant la requète SQL pour la sélection d'un
     * enregistrement précis de la table cocktail.
     */
    public static final String SELECT_SQL = "" +
            "SELECT * " +
            "FROM cocktail " +
            "WHERE idcocktail = ?";

    /**
     * Variable string statique et constante représentant la requète SQL pour la sélection de tous
     * les enregistrements de la table cocktail.
     */
    public static final String SELECT_ALL_SQL = "" +
            "SELECT * " +
            "FROM cocktail";

    /**
     * Variable string statique et constante représentant la requète SQL pour l'insertion d'un
     * nouvel enregistrement de la table cocktail.
     */
    public static final String INSERT_SQL = "" +
            "INSERT INTO cocktail (nom, recette_anglais, recette_francais, nom_image) " +
            "VALUES (?,?,?,?)";

    /**
     * Variable string statique et constante représentant la requète SQL pour la mise à jour d'un
     * enregistrement de la table cocktail.
     */
    public static final String UPDATE_SQL = "" +
            "UPDATE cocktail " +
            "SET (nom = ?, recette_anglais = ?, recette_francais = ?, nom_image = ?) " +
            "WHERE (idcocktail = ?)";

    /**
     * Variable string statique et constante représentant la requète SQL pour la suppression d'un
     * enregistrement de la table cocktail.
     */
    public static final String DELETE_SQL = "" +
            "DELETE FROM cocktail " +
            "WHERE idcocktail = ?";

    /**
     * Constructeur privé de la classe CocktailDatableTable.
     */
    private CocktailDatableTable() {

    }
}
