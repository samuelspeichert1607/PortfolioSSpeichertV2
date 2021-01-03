package ca.csf.mobile1.tp2.activity;

import android.content.ClipData;
import android.content.ClipboardManager;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.provider.Settings;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.ProgressBar;
import android.widget.TextView;

import java.util.Random;

import ca.csf.mobile1.tp2.R;
import ca.csf.mobile1.tp2.modele.DecryptionCypher;
import ca.csf.mobile1.tp2.modele.EncryptionCypher;
import ca.csf.mobile1.tp2.view.dialog.KeyPickerDialog;
import ca.csf.mobile1.tp2.view.input.filter.CharactersInputFilter;

/** Ceci représente l'activité principale ou les échanges entre la vue et le modèle sont faits.
 * Il gère aussi les entrants ainsi que les sortants.
 */
public class MainActivity extends AppCompatActivity implements KeyPickerDialog.OnKeySelectedListener, Subscriber  {

    private EditText inputEditText; //Input textbox pour le message entré par l'utilisateur
    private ProgressBar progressBar;

    private View rootView;
    private Button encryptButton;
    private Button decryptButton;
    private int key;
    private TextView keyInfoText;
    private ImageButton copyButton;
    private TextView messageTextView; //Le textbox qui affiche le résultat de l'encryptage/decryptage
    private boolean intentKeyPicked = false;
    private NetworkTask networkTask;
    private SubstitutionCypherKey substitutionCypherKey;


    private CharactersInputFilter[] charactersInputFilters;
    private char[] inputCharacters; //Les charactères qui sont permis durant le input
    private char[] outputCharacters; //Les charactères d'encryptage correspondant à la clé



    /**
     * La première fonction appelée lors de la création de cet activité.
     * @param savedInstanceState, Sauvegarde les données lors de rotation, pause, etc.
     */
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        rootView = findViewById(R.id.rootView);
        inputEditText = (EditText)findViewById(R.id.inputEditText);
        progressBar = (ProgressBar)findViewById(R.id.progressBar);
        encryptButton = (Button)findViewById(R.id.encryptButton);
        decryptButton = (Button)findViewById(R.id.decryptButton);
        keyInfoText = (TextView)findViewById(R.id.keyInfoText);
        copyButton = (ImageButton)findViewById(R.id.copyButton);
        messageTextView = (TextView)findViewById(R.id.messageTextView);

        //Si l'utlisateur ouvre l'application pour la première fois, on assigne une clé aléatoire
        if(savedInstanceState==null) {
            Random random = new Random();
            key = random.nextInt(99999);
        }
        //Sinon on récupère les données déjà existante
        if(savedInstanceState!=null){
            inputEditText.setText(savedInstanceState.getString("INPUT_MESSAGE"));
            messageTextView.setText(savedInstanceState.getString("OUTPUT_MESSAGE"));
            key = savedInstanceState.getInt("KEY_VALUE");
            intentKeyPicked=savedInstanceState.getBoolean("INTENT_KEY_PICKED");
            inputCharacters=savedInstanceState.getCharArray("SUBSTITUTION_INPUT_ARRAY");
            outputCharacters=savedInstanceState.getCharArray("SUBSTITUTION_OUTPUT_ARRAY");
            substitutionCypherKey = new SubstitutionCypherKey(key,inputCharacters,outputCharacters);
        }

        keyInfoText.setText(keyInfoText.getText() + Integer.toString(key));
        networkTask = new NetworkTask(this); //création de network task pour l'obtention du Cypherkey (voir receiveCypher)
        networkTask.execute(Integer.toString(key));
        progressBar.setVisibility(View.VISIBLE);
        progressBar.setIndeterminate(true);

        Intent intent = getIntent();
        String action = intent.getAction();
        String type = intent.getType();

        charactersInputFilters = new CharactersInputFilter[1];

        //Si l'utilisateur "share" un message à partir d'une autre application
        if (Intent.ACTION_SEND.equals(action) && type != null && !intentKeyPicked) {
            if ("text/plain".equals(type)) {
                KeyPickerDialog.show(this,key,5,this); //On lui demande une clé
                handleSendText(intent);
                intentKeyPicked=true;
            }
        }
    }

    /**
     * On SaveInstanceState permet de sauvegarder les données déjà présentes
     * @param outState Le "bundle" contenant toutes les données
     */
    @Override
    protected void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);

        outState.putString("INPUT_MESSAGE",inputEditText.getText().toString());
        outState.putString("OUTPUT_MESSAGE",messageTextView.getText().toString());
        outState.putInt("KEY_VALUE",key);
        outState.putBoolean("INTENT_KEY_PICKED",intentKeyPicked);
        outState.putCharArray("SUBSTITUTION_INPUT_ARRAY", inputCharacters);
        outState.putCharArray("SUBSTITUTION_OUTPUT_ARRAY", outputCharacters);
    }

    /**
     * onRestoreInstanceState Ceci permet d'assigner le bundle de données et de restaurer
     * @param savedInstanceState Sauvegarde les données lors de rotation, pause, etc.
     */
    @Override
    protected void onRestoreInstanceState(Bundle savedInstanceState) {
        super.onRestoreInstanceState(savedInstanceState);
    }

    /**
     * handleSendText permet d'afficher le message "sharer" dans le inputEditText si l'application est ouverte à partir d'une autre
     * @param intent le intent (si l'utilisateur "share" un message par exemple)
     */
    private void handleSendText(Intent intent) {
        String sharedText = intent.getStringExtra(Intent.EXTRA_TEXT);
        if (sharedText != null) {
            inputEditText.setText(sharedText);
        }
    }

    /**
     * Permet d'assigner un message dans le clipboard (ex: copier le résultat d'une encryption)
     * @param text le résultat d'une encryption/décryption
     */
    private void putTextInClipboard(String text) {
        ClipboardManager clipboard = (ClipboardManager)getSystemService(Context.CLIPBOARD_SERVICE);
        clipboard.setPrimaryClip(ClipData.newPlainText(getResources().getString(R.string.clipboard_encrypted_text), text));
    }

    /**
     * Fonction qui réagit d'après un click du bouton d'encryption
     * @param view le bouton Encryption
     */
    public void onEncryptButtonPress(View view){
        EncryptionCypher encryptionCypher = new EncryptionCypher();
        String cryptedMessage = encryptionCypher.encryptMessage(inputEditText.getText().toString(),inputCharacters,outputCharacters);
        messageTextView.setText(cryptedMessage);
    }

    /**
     * Fonction qui réagit d'après un click du bouton de décryption
     * @param view le bouton decryption
     */
    public void onDecryptButtonPress(View view){
        DecryptionCypher decryptionCypher = new DecryptionCypher();
        String decryptedMessage = decryptionCypher.decryptMessage(inputEditText.getText().toString(),inputCharacters,outputCharacters);
        messageTextView.setText(decryptedMessage);
    }


    /**
     * Fonction qui ouvre le keyPickerDialog suite à un click de l'utilisateur
     * @param view Le bouton keyPicker
     */
    public void onKeyButtonPress(View view){
        KeyPickerDialog.show(this,key,5,this);
    }

    /**
     * Fonction qui réagit au click du bouton copy
     * @param view le bouton copy
     */
    public void onCopyButtonPress(View view){
        putTextInClipboard(messageTextView.getText().toString());
    }

    /**
     * Fonction qui réagit suite à la confirmation d'une clé choisi par l'utilisateur, lance un asynctask pour la création d'un nouveau substitutionCypher
     * @param key la clé choisi par l'utilisateur
     */
    @Override
    public void onKeySelected(int key) {
        this.key = key;
        keyInfoText.setText(getText(R.string.text_key)+Integer.toString(key));
        networkTask = new NetworkTask(this);
        networkTask.execute(Integer.toString(key));
        progressBar.setIndeterminate(true);
    }

    /**
     *
     */
    @Override
    public void onKeySelectionCancelled() {

    }

    /**Cette fonction sera appelée quand un erreur sera produite. Un Snackbar sera créée afin d'avertir l'utilisateur.
     * @param message le code pour appeler le message d'erreur relié à la bonne situation.
     */
    public void showError(String message) {
        if(message.equals("NoInternet")) {
            //Trouver pour qu'un bouton sur le snackbar pour cliquer pour envoyer vers les parametres de wifi
            Snackbar.make(rootView, getResources().getString(R.string.no_internet), Snackbar.LENGTH_LONG).setAction(getResources().getString(R.string.activate_wifi), new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    startActivity(new Intent(Settings.ACTION_WIRELESS_SETTINGS));
                }
            }).show();
        }
    }

    /**
     * Fonction qui recoit les information du substitutionCypher suite au résultat du NetworkTask
     * @param substitutionCypherKey C'est l'objet qui va contenir les "inputCharacters" et les "outputCharacters".
     */
    public void receiveCypher(SubstitutionCypherKey substitutionCypherKey) {
        this.substitutionCypherKey = substitutionCypherKey;
        inputCharacters = substitutionCypherKey.getInputCharacters();
        outputCharacters = substitutionCypherKey.getOutputCharacters();
        progressBar.setIndeterminate(false);

        charactersInputFilters[0] = new CharactersInputFilter(inputCharacters);
        charactersInputFilters[0].setAcceptedCharacters(inputCharacters);
        inputEditText.setFilters(charactersInputFilters);
    }
}
