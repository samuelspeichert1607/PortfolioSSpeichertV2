package ca.csf.mobile1.tikiappbar.service;

import android.content.Intent;
import android.nfc.NdefMessage;
import android.nfc.NdefRecord;
import android.nfc.NfcAdapter;
import android.os.AsyncTask;
import android.os.Parcelable;

import java.util.ArrayList;

/**
 * Classe asynchrone qui va servir à faire les opérations du NFC en parallèle
 * aux opérations faites par la tâche principale.
 * Cette classe sera appelée par {@link ca.csf.mobile1.tikiappbar.MainActivity}.
 *
 * @author Samuel
 */

public class NfcAsyncTask extends AsyncTask<Intent, Void, String[]> {

    private ArrayList<String> commandeRecues = new ArrayList<>();
    private ArrayList<Listener> listOfListeners = new ArrayList<Listener>();

    /**
     * Cette fonction comprend les tâches asynchrones faites en background.
     * @param params l'intent reçue via l'activité principale
     * @return cette fonction retourne un tableau de strings.
     */
    @Override
    protected String[] doInBackground(Intent... params) {
        if (params[0] != null && NfcAdapter.ACTION_NDEF_DISCOVERED.equals(params[0].getAction())) {
            Parcelable[] rawMessages =
                    params[0].getParcelableArrayExtra(NfcAdapter.EXTRA_NDEF_MESSAGES);
            if (rawMessages != null) {
                commandeRecues.clear();
                NdefMessage receivedMessage = (NdefMessage) rawMessages[0];
                NdefRecord[] attachedRecords = receivedMessage.getRecords();

                for (NdefRecord record : attachedRecords) {
                    String string = new String(record.getPayload());
                    if (string.equals("ca.csf.mobile1.tikiappbar")) {
                        continue;
                    }
                    commandeRecues.add(string);
                }
                String[] tabString = parseOrder(commandeRecues.get(0));
                return tabString;
            }
            String[] blankReturn = new String[2];
            blankReturn[0] = "Blank";
            return blankReturn;
        }
        return null;
    }

    /**
     * Cette fonction sert à séparer la string entrée en paramètre en deux
     * valeurs distinctes : une c'est le nom, l'autre l'id.
     * @param commande la chaine que l'on désire séparer.
     * @return le tableau de String contenant l'id et le nom
     */
    private String[] parseOrder(String commande) {
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
        String[] tabString = new String[2];
        tabString[0] = id;
        tabString[1] = nom;
        return tabString;
    }

    /**
     * Cette fonction sert à poster à la tâche
     * principale le contenu du paramètre.
     * @param tabString le tableau de string.
     *  Note : le paramètre de cette méthode
     *  est égal à la valeur de retour de la
     *  fonction ci-dessus : "doInBackground"
     */
    @Override
    protected void onPostExecute(String[] tabString) {
        for (Listener listener : listOfListeners) {
            listener.receivedStuffFromAsyncTask(tabString);
        }
    }

    /**
     * Cette fonction sert tout simplement
     * à ajouter à la liste de listeners
     * un nouveau listener.
     * @param listener le listener à ajouter.
     */
    public void addListener(Listener listener) {
        listOfListeners.add(listener);
    }
}
