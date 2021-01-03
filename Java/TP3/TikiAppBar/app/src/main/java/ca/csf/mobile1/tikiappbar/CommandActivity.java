package ca.csf.mobile1.tikiappbar;

import android.app.Activity;
import android.content.Intent;
import android.database.sqlite.SQLiteDatabase;
import android.graphics.Bitmap;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import android.os.Bundle;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.GridLayout;
import android.widget.ImageView;
import android.widget.TextView;

import java.util.Locale;

import ca.csf.mobile1.tikiappbar.model.CocktailOrder;
import ca.csf.mobile1.tikiappbar.model.CocktailOrderInterface;
import ca.csf.mobile1.tikiappbar.model.Commande;
import ca.csf.mobile1.tikiappbar.model.IngredientCocktail;
import ca.csf.mobile1.tikiappbar.service.ApplicationDatabaseHelper;
import ca.csf.mobile1.tikiappbar.service.SQLiteCocktailRepository;
import ca.csf.mobile1.tikiappbar.service.SQLiteCommandeRepository;
import ca.csf.mobile1.tikiappbar.service.SQLiteIngredientsCocktailRepository;

/**
 * Created by Alex on 2017-05-12.
 */

public class CommandActivity extends AppCompatActivity {
    private static final String COMMAND_ID_EXTRA = "COMMANDE_ID";
    private Commande commande;
    private SQLiteDatabase db;
    private ApplicationDatabaseHelper dbHelper;
    private CocktailOrderInterface repositoryCommande;
    private TextView clientView;
    private TextView hourView;
    private ImageView imageView;
    private TextView cocktailNameTextView;
    private GridLayout gridLayoutIngredients;
    private TextView recepieView;
    private View rootView;

    //fonction pour ouvrir l'activité à partir d'une autre activitée
    public static void start(Activity parent, Commande commande) {
        Intent intent = new Intent(parent, CommandActivity.class);
        configureIntent(intent, commande);
        parent.startActivity(intent);
    }

    public static void configureIntent(Intent intent, Commande commande) {
        intent.putExtra(COMMAND_ID_EXTRA, commande.getId());
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_commande);
        rootView = findViewById(R.id.rootView2);
        clientView = (TextView) findViewById(R.id.client_textview);
        hourView = (TextView) findViewById(R.id.heure_textview);
        imageView = (ImageView) findViewById(R.id.cocktail_imageView);
        cocktailNameTextView = (TextView) findViewById(R.id.cocktail_name_textView);
        gridLayoutIngredients = (GridLayout) findViewById(R.id.ingredients_GridLayout);
        recepieView = (TextView) findViewById(R.id.recepie_textview);
        //on crée la base de données
        dbHelper = new ApplicationDatabaseHelper(this.getApplicationContext(), getResources().getString(R.string.bd_name));
        db = dbHelper.getWritableDatabase();
        long idCommande = getIntent().getLongExtra(COMMAND_ID_EXTRA, 1);
        //on sort le cocktail
        repositoryCommande = getCocktailOrder();
        commande = repositoryCommande.retrieveOrder(idCommande);
        clientView.setText(getResources().getString(R.string.client_display) + commande.getNomClient());
        hourView.setText(getResources().getString(R.string.hour_display) + commande.getHeureCommande());
        //on importe l'image
        int largeurImage = (getApplicationContext().getResources().getDisplayMetrics().widthPixels) / 2;
        int imageID = getResources().getIdentifier(commande.getCocktail().getIdImage(), "drawable", getPackageName());
        Drawable image = getResources().getDrawable(imageID);
        //on redimentionne l'image
        Bitmap bitmap = ((BitmapDrawable) image).getBitmap();
        image = new BitmapDrawable(getResources(), Bitmap.createScaledBitmap(bitmap, largeurImage, largeurImage, true));
        //on insère l'image dans le imageView
        imageView.setImageDrawable(image);

        cocktailNameTextView.setText(commande.getCocktail().getNom());
        //on sort et on affiche tous les ingrédients
        for (int i = 0; i < commande.getCocktail().getIngredientCocktails().size(); i++) {
            TextView courant = new TextView(gridLayoutIngredients.getContext());
            IngredientCocktail ingredientCourant = commande.getCocktail().getIngredientCocktails().get(i);
            courant.setText(getStringForCurrentIngredient(ingredientCourant));
            gridLayoutIngredients.addView(courant);
        }
        if (Locale.getDefault().getDisplayLanguage() == Locale.CANADA_FRENCH.getDisplayLanguage()) {
            recepieView.setText(commande.getCocktail().getRecetteFrancais());
        } else {
            recepieView.setText(commande.getCocktail().getRecetteFrancais());
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

    public void onClickSupprimer(View view) {
        repositoryCommande.markOrderAsDone(commande.getId());
        Snackbar.make(rootView, getResources().getString(R.string.confirmation_delete_order), Snackbar.LENGTH_LONG).show();
        MainActivity.start(this);
    }

    private CocktailOrderInterface getCocktailOrder() {
        return new CocktailOrder(db,
                new SQLiteCommandeRepository(db, new SQLiteCocktailRepository(db, new SQLiteIngredientsCocktailRepository(db))),
                new SQLiteCocktailRepository(db, new SQLiteIngredientsCocktailRepository(db)));
    }
}
