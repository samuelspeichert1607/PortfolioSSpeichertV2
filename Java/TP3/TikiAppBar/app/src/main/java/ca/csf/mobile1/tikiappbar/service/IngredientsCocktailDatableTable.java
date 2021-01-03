package ca.csf.mobile1.tikiappbar.service;

/**
 * Classe constante IngredientsCocktailDatableTable, elle ne peut être instanciées.
 * Elle contient des requêtes SQL statiques et constantes pour cette table.
 * Ces requètes seront utilisées par {@link SQLiteIngredientsCocktailRepository}.
 *
 * @author Gabrielle-Ann Bédard-Hamelin
 */
public final class IngredientsCocktailDatableTable {
    /**
     * Variable string statique et constante représentant la requète SQL pour la création de la
     * table ingrédients par cocktail.
     */
    public static final String CREATE_TABLE_SQL = "" +
            "CREATE TABLE ingredient_cocktail (" +
            "idingredient_cocktail   INTEGER  PRIMARY KEY  AUTOINCREMENT, " +
            "quantite                FLOAT, " +
            "mesure                  VARCHAR(45), " +
            "ingredient_idingredient INTEGER, " +
            "cocktail_idcocktail     INTEGER, " +
            "FOREIGN KEY(ingredient_idingredient) REFERENCES ingredient(idingredient), " +
            "FOREIGN KEY(cocktail_idcocktail) REFERENCES cocktail(idcocktail)" +
            ")";

    /**
     * Variable string statique et constante représentant la requète SQL pour la destruction de la
     * table ingrédients par cocktail.
     */
    public static final String DROP_TABLE_SQL = "" +
            "DROP TABLE ingredient_cocktail";

    /**
     * Variable string statique et constante représentant la requète SQL pour la sélection de tous
     * les enregistrements d'un cocktail précis de la table ingrédients par cocktail.
     */
    public static final String SELECT_ALL_SQL = "" +
            "SELECT * " +
            "FROM ingredient_cocktail " +
            "WHERE cocktail_idcocktail = ?";

    /**
     * Variable string statique et constante représentant la requète SQL pour l'insertion d'un
     * nouvel enregistrement de la table ingrédients par cocktail.
     */
    public static final String INSERT_SQL = "" +
            "INSERT INTO ingredient_cocktail (quantite, mesure, ingredient_idingredient, cocktail_idcocktail) " +
            "VALUES (?,?,?,?)";

    /**
     * Variable string statique et constante représentant la requète SQL pour la mise à jour d'un
     * enregistrement de la table ingrédients par cocktail.
     */
    public static final String UPDATE_SQL = "" +
            "UPDATE ingredient_cocktail " +
            "SET (quantite = ?, mesure = ?, ingredient_idingredient = ?) " +
            "WHERE idingredient_cocktail = ?";

    /**
     * Variable string statique et constante représentant la requète SQL pour la suppression d'un
     * enregistrement de la table ingrédients par cocktail.
     */
    public static final String DELETE_SQL = "" +
            "DELETE FROM ingredient_cocktail " +
            "WHERE idingredient_cocktail = ?";

    /**
     * Constructeur privé de la classe IngredientsCocktailDatableTable.
     */
    private IngredientsCocktailDatableTable() {

    }
}
