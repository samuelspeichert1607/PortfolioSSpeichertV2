package ca.csf.mobile1.tikiappclient;

import android.app.Activity;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.database.sqlite.SQLiteDatabase;
import android.graphics.Bitmap;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import android.nfc.NdefMessage;
import android.nfc.NdefRecord;
import android.nfc.NfcAdapter;
import android.nfc.NfcEvent;
import android.os.Build;
import android.os.Bundle;
import android.provider.Settings;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.EditText;
import android.widget.GridLayout;
import android.widget.ImageView;
import android.widget.TextView;

import android.widget.Toast;

import java.util.ArrayList;
import java.util.Locale;

import ca.csf.mobile1.tikiappclient.model.Cocktail;
import ca.csf.mobile1.tikiappclient.model.IngredientCocktail;
import ca.csf.mobile1.tikiappclient.service.ApplicationDatabaseHelper;
import ca.csf.mobile1.tikiappclient.service.SQLiteCocktailRepository;
import ca.csf.mobile1.tikiappclient.service.SQLiteIngredientsCocktailRepository;

import static android.nfc.NdefRecord.createMime;

/**
 * Created by Alex on 2017-05-05.
 */

public class CocktailActivity extends AppCompatActivity implements NfcAdapter.OnNdefPushCompleteCallback, NfcAdapter.CreateNdefMessageCallback {

    private static final String COCKTAIL_ID_EXTRA = "COCKTAIL_ID";
    private Cocktail cocktail;
    private SQLiteDatabase db;
    private ApplicationDatabaseHelper dbHelper;
    private SQLiteCocktailRepository repositoryCocktail;
    private ImageView imageView;
    private TextView textViewNom;
    private GridLayout gridLayoutIngredients;
    private EditText editTextNomClient;
    private View rootView;

    private NfcAdapter nfcAdapter;
    private ArrayList<String> commandeAEnvoyer = new ArrayList<>();

    //fonction pour ouvrir l'activité à partir d'une autre activitée
    public static void start(Activity parent, Cocktail cocktail) {
        Intent intent = new Intent(parent, CocktailActivity.class);
        //intent.putExtra(COCKTAIL_ID_EXTRA, cocktail.getId());
        configureIntent(intent, cocktail);
        parent.startActivity(intent);
    }

    //A prendre ou a laisser
    public static void configureIntent(Intent intent, Cocktail cocktail) {
        intent.putExtra(COCKTAIL_ID_EXTRA, cocktail.getId());
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.cocktail_info);
        rootView = findViewById(R.id.rootView2);
        imageView = (ImageView) findViewById(R.id.cocktail_imageView);
        textViewNom = (TextView) findViewById(R.id.cocktail_name_textView);
        editTextNomClient = (EditText) findViewById(R.id.editText);
        gridLayoutIngredients = (GridLayout) findViewById(R.id.ingredients_GridLayout);
        //on crée la base de données
        dbHelper = new ApplicationDatabaseHelper(this.getApplicationContext(), getResources().getString(R.string.bd_name));
        db = dbHelper.getWritableDatabase();
        long idCoclktail = getIntent().getLongExtra(COCKTAIL_ID_EXTRA, 1);
        //on sort le cocktail
        repositoryCocktail = new SQLiteCocktailRepository(db, new SQLiteIngredientsCocktailRepository(db));
        cocktail = repositoryCocktail.retrieve((int) idCoclktail);
        //on importe l'image
        int largeurImage = (getApplicationContext().getResources().getDisplayMetrics().widthPixels) / 2;
        int imageID = getResources().getIdentifier(cocktail.getIdImage(), "drawable", getPackageName());
        Drawable image = getResources().getDrawable(imageID);
        //on redimentionne l'image
        Bitmap bitmap = ((BitmapDrawable) image).getBitmap();
        image = new BitmapDrawable(getResources(), Bitmap.createScaledBitmap(bitmap, largeurImage, largeurImage, true));
        //on insère l'image dans le imageView
        imageView.setImageDrawable(image);

        textViewNom.setText(cocktail.getNom());
        //on sort et on affiche tous les ingrédients
        for (int i = 0; i < cocktail.getIngredientCocktails().size(); i++) {
            TextView courant = new TextView(gridLayoutIngredients.getContext());
            IngredientCocktail ingredientCourant = cocktail.getIngredientCocktails().get(i);
            courant.setText(getStringForCurrentIngredient(ingredientCourant));
            gridLayoutIngredients.addView(courant);
        }

        //Vérifie si la technologie NFC et Android Beam est disponible sur l'appareil.
        PackageManager pm = this.getPackageManager();
        // Check whether NFC is available on device
        if (!pm.hasSystemFeature(PackageManager.FEATURE_NFC)) {
            // NFC is not available on the device.
            Toast.makeText(this, "The device does not has NFC hardware.",
                    Toast.LENGTH_SHORT).show();
        }
        // Check whether device is running Android 4.1 or higher
        else if (Build.VERSION.SDK_INT < Build.VERSION_CODES.JELLY_BEAN) {
            // Android Beam feature is not supported.
            Toast.makeText(this, "Android Beam is not supported.",
                    Toast.LENGTH_SHORT).show();
        } else {
            // NFC and Android Beam file transfer is supported.
            Toast.makeText(this, "Android Beam is supported on your device.",
                    Toast.LENGTH_SHORT).show();
        }

        //Appel de setNdefPushMessageCallback pour inialiser l'envoie NFC avec Android Beam.
        nfcAdapter = NfcAdapter.getDefaultAdapter(this);
        if (nfcAdapter != null) {
            //Envoie du message.
            nfcAdapter.setNdefPushMessageCallback(this, this);
            //Si le message a été envoyé avec succes.
            nfcAdapter.setOnNdefPushCompleteCallback(this, this);
        } else {
            //TODO: Faire une version anglais et francais de nfc pas dispo.
            Toast.makeText(this, "NFC pas disponible", Toast.LENGTH_SHORT).show();
        }
    }

    /**
     * @param ingredientCocktail
     * @return
     */
    private String getStringForCurrentIngredient(IngredientCocktail ingredientCocktail) {
        String ingredientInfo = "";
        if (ingredientCocktail.getQuantite() > 0) {
            ingredientInfo += (ingredientCocktail.getQuantite() + " ");
        }

        if (!ingredientCocktail.getMesure().equals("")) {
            ingredientInfo += (ingredientCocktail.getMesure() + " ");
            if (Locale.getDefault().getDisplayLanguage().equals(Locale.FRENCH.getDisplayLanguage())) {
                ingredientInfo += "de ";
            } else {
                ingredientInfo += "of ";
            }
        }
        if (Locale.getDefault().getDisplayLanguage().equals(Locale.FRENCH.getDisplayLanguage())) {
            ingredientInfo += ingredientCocktail.getIngredient().getNomFrancais();
        } else {
            ingredientInfo += ingredientCocktail.getIngredient().getNomAnglais();
        }
        return ingredientInfo;
    }

    public void onClickReturn(View view) {
        MainActivity.start(this);
    }

    public void onClickCommand(View view) {
        if (!editTextNomClient.getText().toString().equals("")) {
            String commande = cocktail.getId() + ":" + editTextNomClient.getText().toString();
            commandeAEnvoyer.add(commande);
            Snackbar.make(rootView, R.string.command_registered, Snackbar.LENGTH_LONG).show();
        } else {
            Snackbar.make(rootView, getResources().getString(R.string.Empty_name_error), Snackbar.LENGTH_LONG).show();
        }
    }

    /**
     * Quand un message à été envoyé avec succes.
     *
     * @param event
     */
    @Override
    public void onNdefPushComplete(NfcEvent event) {
        commandeAEnvoyer.clear();
    }

    /**
     * Quand un autre appareil NFC est détecté.
     *
     * @param event
     * @return
     */
    @Override
    public NdefMessage createNdefMessage(NfcEvent event) {
        if (commandeAEnvoyer.size() == 0) {
            return null;
        }

        //Code pour la vérification du nfc.
        // https://code.tutsplus.com/tutorials/sharing-files-with-nfc-on-android--cms-22501
        // Check whether NFC is enabled on device
        if (!nfcAdapter.isEnabled()) {
            // NFC is disabled, show the settings UI
            // to enable NFC
            Toast.makeText(this, "Please enable NFC.",
                    Toast.LENGTH_SHORT).show();
            startActivity(new Intent(Settings.ACTION_NFC_SETTINGS));
        }
        // Check whether Android Beam feature is enabled on device
        else if (!nfcAdapter.isNdefPushEnabled()) {
            // Android Beam is disabled, show the settings UI
            // to enable Android Beam
            Toast.makeText(this, "Please enable Android Beam.",
                    Toast.LENGTH_SHORT).show();
            startActivity(new Intent(Settings.ACTION_NFCSHARING_SETTINGS));
        } else {
            //Enregistrement du message dans un Mime record
            String messageToSend = cocktail.getId() + ":"
                    + editTextNomClient.getText().toString();

            NdefMessage message = new NdefMessage(
                    new NdefRecord[]{createMime(
                            "application/vnd.ca.csf.mobile1.tikiappbar", messageToSend.getBytes())
                            , NdefRecord.createApplicationRecord("ca.csf.mobile1.tikiappbar")
                    });
            return message;
        }
        return null;
    }

    /**
     * Prend chaques lettres de notre commande et l'enregistre dans un record.
     * @return {@link NdefRecord[]}
     */
    //prend plusieurs records inutiles maintenant.
    /*public NdefRecord[] createRecords(){
        NdefRecord[] records = new NdefRecord[commandeAEnvoyer.size()];

        for (int i = 0; i < commandeAEnvoyer.size(); i++) {
            byte[] payload = commandeAEnvoyer.get(i).
                    getBytes(Charset.forName("UTF-8"));

            NdefRecord record = createMime("text/plain",payload);
            records[i] = record;
        }
        records[commandeAEnvoyer.size()] =
                NdefRecord.createApplicationRecord("ca.csf.mobile1.tikiappbar");
        return records;
    }*/
}
