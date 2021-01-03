package ca.csf.mobile1.tikiappclient;

import android.app.Activity;
import android.content.Intent;
import android.database.sqlite.SQLiteDatabase;
import android.graphics.Bitmap;
import android.graphics.Color;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import android.nfc.NdefMessage;
import android.nfc.NdefRecord;
import android.nfc.NfcAdapter;
import android.os.Bundle;
import android.os.Parcelable;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.Button;
import android.widget.GridLayout;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.List;

import ca.csf.mobile1.tikiappclient.model.Cocktail;
import ca.csf.mobile1.tikiappclient.service.ApplicationDatabaseHelper;
import ca.csf.mobile1.tikiappclient.service.SQLiteCocktailRepository;
import ca.csf.mobile1.tikiappclient.service.SQLiteIngredientsCocktailRepository;

public class MainActivity extends AppCompatActivity implements View.OnClickListener {

    private GridLayout conteneurCocktails;
    private View rootView;
    private List<Cocktail> cocktails;
    private SQLiteDatabase db;
    private ApplicationDatabaseHelper dbHelper;
    private SQLiteCocktailRepository repositoryCocktail;

    private ArrayList<String> commandeRecu = new ArrayList<>();


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
        conteneurCocktails = (GridLayout) findViewById(R.id.GridLayout1);
        //on crée la base de donnée
        dbHelper = new ApplicationDatabaseHelper(this.getApplicationContext(), getResources().getString(R.string.bd_name));
        db = dbHelper.getWritableDatabase();
        //on sort touts les cocktails
        cocktails = new ArrayList<Cocktail>();
        repositoryCocktail = new SQLiteCocktailRepository(db, new SQLiteIngredientsCocktailRepository(db));
        cocktails = repositoryCocktail.retrieveAll();
        //on crée tous les bouttons de cocktail
        for (int i = 0; i < cocktails.size(); i++) {
            //inserer tous les cocktails ici
            manageLayout(cocktails.get(i));
        }

    }

    /**
     * @param currentCocktail
     */
    private void manageLayout(Cocktail currentCocktail) {
        Button courant = new Button(conteneurCocktails.getContext());
        courant.setId((int) currentCocktail.getId());
        courant.setText(currentCocktail.getNom());
        courant.setOnClickListener(this);
        courant.setBackgroundColor(Color.TRANSPARENT);
        //on importe l'image
        int largeur = (getApplicationContext().getResources().getDisplayMetrics().widthPixels) / 2;
        int imageID = getResources().getIdentifier(currentCocktail.getIdImage(), "drawable", getPackageName());
        Drawable image = getResources().getDrawable(imageID);
        //on redimentionne l'image
        Bitmap bitmap = ((BitmapDrawable) image).getBitmap();
        image = new BitmapDrawable(getResources(), Bitmap.createScaledBitmap(bitmap, largeur, largeur, true));
        //on insère l'image dans le boutton
        courant.setCompoundDrawablesWithIntrinsicBounds(
                null, image, null, null);
        //on ajoute le boutton au gridview
        conteneurCocktails.addView(courant);
    }

    //fonction OnClick de tous les bottons cocktail
    //v est le boutton sous forme de view
    //v contient donc toute l'information du boutton
    @Override
    public void onClick(View v) {
        Button appuye = (Button) v;
        CocktailActivity.start(this, cocktails.get(appuye.getId() - 1));
    }

}
