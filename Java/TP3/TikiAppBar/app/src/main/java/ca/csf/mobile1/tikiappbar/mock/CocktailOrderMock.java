package ca.csf.mobile1.tikiappbar.mock;

import android.database.sqlite.SQLiteDatabase;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;

import ca.csf.mobile1.tikiappbar.model.Cocktail;
import ca.csf.mobile1.tikiappbar.model.CocktailOrderInterface;
import ca.csf.mobile1.tikiappbar.model.CocktailRepository;
import ca.csf.mobile1.tikiappbar.model.Commande;
import ca.csf.mobile1.tikiappbar.model.CommandeRepository;

/**
 * Created by Gabby on 2017-05-13.
 */

public class CocktailOrderMock implements CocktailOrderInterface {
    private SQLiteDatabase database;
    private CommandeRepository commandeRepository;
    private CocktailRepository cocktailRepository;

    public CocktailOrderMock(SQLiteDatabase database, CommandeRepository commandeRepository, CocktailRepository cocktailRepository) {
        this.database = database;
        this.commandeRepository = commandeRepository;
        this.cocktailRepository = cocktailRepository;
    }

    @Override
    public void createNewOrder(long idCocktail, String clientName) {
        Date date = new Date();
        String currentTime = new SimpleDateFormat("HH:mm").format(date);
        Cocktail cocktail = cocktailRepository.retrieve(idCocktail);
        commandeRepository.create(cocktail, new Commande(0, cocktail, clientName, currentTime));
    }

    @Override
    public ArrayList<Commande> retrieveAllOrders() {
        return new ArrayList<Commande>();
    }

    @Override
    public Commande retrieveOrder(long idCommande) {
        return new Commande(idCommande, new Cocktail(1, "Mojito", "recetteAnglais", "recetteFrancais", "mojito.png"), "Segata Sanshiro", "16:20");
    }

    @Override
    public void markOrderAsDone(long idCommande) {
        //commandeRepository.delete(idCommande);
    }
}
