package ca.csf.mobile1.tikiappbar;

import android.app.Activity;
import android.content.Intent;
import android.database.sqlite.SQLiteDatabase;
import android.graphics.Color;
import android.nfc.NdefMessage;
import android.nfc.NdefRecord;
import android.nfc.NfcAdapter;
import android.os.Bundle;
import android.os.Parcelable;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.Button;
import android.widget.GridLayout;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.List;

import ca.csf.mobile1.tikiappbar.model.Cocktail;
import ca.csf.mobile1.tikiappbar.model.CocktailOrder;
import ca.csf.mobile1.tikiappbar.model.CocktailOrderInterface;
import ca.csf.mobile1.tikiappbar.model.Commande;
import ca.csf.mobile1.tikiappbar.service.ApplicationDatabaseHelper;
import ca.csf.mobile1.tikiappbar.service.Listener;
import ca.csf.mobile1.tikiappbar.service.NfcAsyncTask;
import ca.csf.mobile1.tikiappbar.service.SQLiteCocktailRepository;
import ca.csf.mobile1.tikiappbar.service.SQLiteCommandeRepository;
import ca.csf.mobile1.tikiappbar.service.SQLiteIngredientsCocktailRepository;

public class MainActivity extends AppCompatActivity implements View.OnClickListener, Listener {

    private GridLayout conteneurCommandes;
    private View rootView;
    private List<Commande> commandes;
    private SQLiteDatabase db;
    private ApplicationDatabaseHelper dbHelper;
    private CocktailOrderInterface repositoryCommande;
    private int iddifference; //la différence entre le id d'une commande et son boutton supprimer
    NfcAsyncTask nfcAsyncTask = new NfcAsyncTask();

    private ArrayList<String> commandeRecu = new ArrayList<>();
    int compteurIdBouton = 1;

    //fonction pour ouvrir l'activité à partir d'une autre activitée
    public static void start(Activity parent) {
        Intent intent = new Intent(parent, MainActivity.class);
        parent.startActivity(intent);
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        rootView = findViewById(R.id.rootView);
        conteneurCommandes = (GridLayout) findViewById(R.id.GridLayout1);
        //on sort toutes les commandes
        commandes = new ArrayList<Commande>();
        repositoryCommande = getCocktailOrder();
        commandes = repositoryCommande.retrieveAllOrders();
        nfcAsyncTask.addListener(this);
        //on crée tous les bouttons de cocktail
        nfcAsyncTask.execute(getIntent());
        for (int i = 0; i < commandes.size(); i++) {
            //inserer tous les cocktails ici
            manageLayout(commandes.get(i));
        }
    }

    /**
     * @param currentCommande
     */
    private void manageLayout(Commande currentCommande) {
        //on crée le boutton pour voir la commande
        Button courant = new Button(conteneurCommandes.getContext());
        //courant.setId((int) currentCommande.getId());
        courant.setId((int) compteurIdBouton);
        compteurIdBouton++;
        courant.setText(getResources().getString(R.string.cocktail_display) + currentCommande.getCocktail().getNom()
                + "\n" + getResources().getString(R.string.client_display) + currentCommande.getNomClient()
                + "\n" + getResources().getString(R.string.hour_display) + currentCommande.getHeureCommande());
        courant.setOnClickListener(this);
        courant.setTextAlignment(View.TEXT_ALIGNMENT_TEXT_START);
        courant.setBackgroundColor(Color.TRANSPARENT);
        //on ajoute le boutton pour voir la commande
        conteneurCommandes.addView(courant);
    }

    //fonction OnClick de tous les bottons cocktail
    //v est le boutton sous forme de view
    //v contient donc toute l'information du boutton
    @Override
    public void onClick(View v) {
        Button appuye = (Button) v;
        int test = appuye.getId();
        CommandActivity.start(this, commandes.get(appuye.getId() - 1));
    }

    private CocktailOrderInterface getCocktailOrder() {
        //on crée la base de donnée
        dbHelper = new ApplicationDatabaseHelper(this.getApplicationContext(), getResources().getString(R.string.bd_name));
        db = dbHelper.getWritableDatabase();
        return new CocktailOrder(db,
                new SQLiteCommandeRepository(db, new SQLiteCocktailRepository(db, new SQLiteIngredientsCocktailRepository(db))),
                new SQLiteCocktailRepository(db, new SQLiteIngredientsCocktailRepository(db)));
    }

    @Override
    public void onResume() {
        super.onResume();
        //handleNfcIntent(getIntent());
    }

    @Override
    public void onNewIntent(Intent intent){
        //handleNfcIntent(intent);

        nfcAsyncTask.execute(intent);

    }

    public void receivedStuffFromAsyncTask(String[] tabString) {
        if (tabString != null) {
            if (tabString[0] == "Blank") {
                Snackbar.make(this.rootView, "Received Blank Parcel", Snackbar.LENGTH_LONG).show();
            }else{
                long id = Long.parseLong(tabString[0]);
                String nom = tabString[1];
                updateOrders(id,nom);
                Snackbar.make(this.rootView, "Received one order", Toast.LENGTH_LONG).show();
            }
        }
    }

    //TODO: faire asyncTask.
    /*private void handleNfcIntent(Intent NfcIntent){
        if (NfcIntent != null && NfcAdapter.ACTION_NDEF_DISCOVERED.equals(NfcIntent.getAction())) {
            Parcelable[] rawMessages =
                    NfcIntent.getParcelableArrayExtra(NfcAdapter.EXTRA_NDEF_MESSAGES);
            if (rawMessages != null) {
                commandeRecu.clear();
                NdefMessage receivedMessage = (NdefMessage) rawMessages[0];
                NdefRecord[] attachedRecords = receivedMessage.getRecords();

                for (NdefRecord record : attachedRecords) {
                    String string = new String(record.getPayload());
                    if (string.equals(getPackageName())) {
                        continue;
                    }
                    commandeRecu.add(string);
                }
                }
                Toast.makeText(this, "Received " + commandeRecu.size() +
                        " order", Toast.LENGTH_LONG).show();
                Toast.makeText(this, commandeRecu.get(0), Toast.LENGTH_LONG).show();
                //updateTextViews();
                parseOrder(commandeRecu.get(0));
            } else {
                Toast.makeText(this, "Received Blank Parcel", Toast.LENGTH_LONG).show();
            }
        }
    }

    private void parseOrder(String commande) {
        int compteur = 0;
        String id = "";
        String nom = "";
        while (commande.charAt(compteur) != ':') {
            id += commande.charAt(compteur);
            compteur++;
        }
        for (int i = ++compteur; i < commande.length(); i++) {
            nom += commande.charAt(compteur);
            compteur++;
        }
        uptadeOrders(Long.valueOf(id),nom);
    }*/

    private void updateOrders(Long idCocktail, String nomClient) {
        repositoryCommande.createNewOrder(idCocktail, nomClient);
        commandes = repositoryCommande.retrieveAllOrders();
        updateView();
    }

    private void updateView() {
        compteurIdBouton = 1;
        conteneurCommandes.removeAllViews();
        for (int i = 0; i < commandes.size(); i++) {
            //inserer tous les cocktails ici
            manageLayout(commandes.get(i));
        }
    }
}
