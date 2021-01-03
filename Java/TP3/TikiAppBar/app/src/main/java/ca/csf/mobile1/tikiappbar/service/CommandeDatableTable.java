package ca.csf.mobile1.tikiappbar.service;

/**
 * Classe constante CommandeDatableTable, elle ne peut être instanciées.
 * Elle contient des requêtes SQL statiques et constantes pour cette table.
 * Ces requètes seront utilisées par {@link SQLiteCommandeRepository}.
 *
 * @author Gabrielle-Ann Bédard-Hamelin
 */
public final class CommandeDatableTable {
    /**
     * Variable string statique et constante représentant la requète SQL pour la création de la
     * table commande.
     */
    public static final String CREATE_TABLE_SQL = "" +
            "CREATE TABLE commande (" +
            "idcommande           INTEGER  PRIMARY KEY  AUTOINCREMENT, " +
            "nom_client           VARCHAR(45), " +
            "heure_commande       TIME(5), " +
            "cocktail_idcocktail  INTEGER, " +
            "FOREIGN KEY(cocktail_idcocktail) REFERENCES cocktail(idcocktail)" +
            ")";

    /**
     * Variable string statique et constante représentant la requète SQL pour la destruction de la
     * table commande.
     */
    public static final String DROP_TABLE_SQL = "" +
            "DROP TABLE commande";

    /**
     * Variable string statique et constante représentant la requète SQL pour la sélection de tous
     * les enregistrements de la table commande.
     */
    public static final String SELECT_ALL_SQL = "" +
            "SELECT * " +
            "FROM commande";
    /**
     * Variable string statique et constante représentant la requète SQL pour la sélection d'une
     * seule commande spécifique.
     */
    public static final String SELECT_SQL = "" +
            "SELECT * " +
            "FROM commande " +
            "WHERE idcommande = ?";

    /**
     * Variable string statique et constante représentant la requète SQL pour l'insertion d'un
     * nouvel enregistrement de la table commande.
     */
    public static final String INSERT_SQL = "" +
            "INSERT INTO commande (nom_client, heure_commande, cocktail_idcocktail) " +
            "VALUES (?,?,?)";

    /**
     * Variable string statique et constante représentant la requète SQL pour la mise à jour d'un
     * enregistrement de la table commande.
     */
    public static final String UPDATE_SQL = "" +
            "UPDATE commande " +
            "SET (nom_client = ?, heure_commande = ?, cocktail_idcocktail = ?) " +
            "WHERE (idcommande = ?)";

    /**
     * Variable string statique et constante représentant la requète SQL pour la suppression d'un
     * enregistrement de la table commande.
     */
    public static final String DELETE_SQL = "" +
            "DELETE FROM commande " +
            "WHERE idcommande = ?";

    /**
     * Constructeur privé de la classe CommandeDatableTable.
     */
    private CommandeDatableTable() {

    }
}
