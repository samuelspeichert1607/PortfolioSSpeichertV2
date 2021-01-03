package ca.csf.mobile1.tikiappbar.model;

import android.database.sqlite.SQLiteDatabase;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;

/**
 * Created by Gabby on 2017-05-13.
 *
 * @author Gabrielle-Ann
 */

public class CocktailOrder implements CocktailOrderInterface {
    private SQLiteDatabase database;
    private CommandeRepository commandeRepository;
    private CocktailRepository cocktailRepository;

    public CocktailOrder(SQLiteDatabase database, CommandeRepository commandeRepository, CocktailRepository cocktailRepository) {
        this.database = database;
        this.commandeRepository = commandeRepository;
        this.cocktailRepository = cocktailRepository;
    }

    /**
     * Cette fonction va servir à créer une nouvel "order".
     * @param idCocktail l'id du cocktail du "order"
     * @param clientName le nom du client
     */
    @Override
    public void createNewOrder(long idCocktail, String clientName) {
        Date date = new Date();
        String currentTime = new SimpleDateFormat("HH:mm").format(date);
        Cocktail cocktail = cocktailRepository.retrieve(idCocktail);
        commandeRepository.create(cocktail, new Commande(0, cocktail, clientName, currentTime));
    }

    @Override
    public ArrayList<Commande> retrieveAllOrders() {
        return commandeRepository.retrieveAll();
    }

    @Override
    public Commande retrieveOrder(long idCommande) {
        Commande test=commandeRepository.retrieve(idCommande);
        return test;
    }

    @Override
    public void markOrderAsDone(long idCommande) {
        commandeRepository.delete(idCommande);
    }
}
