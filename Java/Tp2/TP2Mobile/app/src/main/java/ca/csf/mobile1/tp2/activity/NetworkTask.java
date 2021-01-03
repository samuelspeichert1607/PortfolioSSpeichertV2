package ca.csf.mobile1.tp2.activity;

import android.accounts.NetworkErrorException;
import android.os.AsyncTask;

import com.fasterxml.jackson.core.JsonParseException;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.ObjectMapper;

import java.io.IOException;
import java.util.ArrayList;

import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.Response;

/**
 * Created by Samuel Speichert et Alexandre Lachance on 2017-03-27.
 */

/**
 * Ceci est la tâche de fond qui va s'occuper de faire les appels réseaux
 */
public class NetworkTask extends AsyncTask<String, Void, SubstitutionCypherKey> {

    private ArrayList<Subscriber> subscriberList = new ArrayList<Subscriber>(); // Liste des abonnés
    private SubstitutionCypherKey substitutionCypherKey = null; // Clé de substitution
    private Subscriber subscriber; // Un abonné

    /**Cette fonction sert à ajouter un abonné à la liste des abonnés.
     * @param subscriber Le notifié de la liste.
     */
    public NetworkTask(Subscriber subscriber) {
        this.subscriber = subscriber;
        subscriberList.add(this.subscriber);
    }

    /**
     * C'est la tâche de fond qui va se charger d'aller chercher via le site cypherkeys
     * l'équivalent en message crypté de l'alphabet grâce à la clé rentrée par l'utilisateur.
     * @throw Une exception sera lancée si l'utilisateur n'est pas connecté à Internet ou s'il n'obtien pas
     * de réponse du serveur.
     * @param params Il s'agit de la clé rentrée par l'utilisateur.
     * @return La fonction va retourner la chaine serialisée contenant
     * un tableau de caractères contenant les lettres de A à Z (plus l'espace et le point)
     * ainsi que leur équivalent en message crypté.
     */
    @Override
    protected SubstitutionCypherKey doInBackground(String... params) {
        String json=null;
        OkHttpClient client = new OkHttpClient();
        Request request = new Request.Builder()
                .url("http://cypherkeys-acodebreak.rhcloud.com/substitution/key/" + params[0] +".json")
                .build();
        try (Response response = client.newCall(request).execute()) {
            if (response.isSuccessful()) {
                json = response.body().string();
            }
            else {
                throw new NetworkErrorException();
            }
            response.close();

        }
        catch (Exception e) {
            for (Subscriber subscriber:subscriberList) {
                subscriber.showError("NoInternet");
            }
        }

        try {
            if(json != null) {
                ObjectMapper objectMapper = new ObjectMapper();
                //Facultatif. Seulement si un "MixIn" est nécessaire
                objectMapper.addMixIn(SubstitutionCypherKey.class, SubstitutionCypherKeyMixIn.class);
                substitutionCypherKey = objectMapper.readValue(json, SubstitutionCypherKey.class);
            }
            else{
                char[] emptyCharArray = new char[]{};
                substitutionCypherKey = new SubstitutionCypherKey(0,emptyCharArray,emptyCharArray);
            }
        } catch (JsonParseException | JsonMappingException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }

        return substitutionCypherKey;
    }


    /** Cette fonction, qui est appelée automatiquement après la fonction "doInBackground",
     *  va envoyer le "substitionCypherKey" aux différents abonnés.
     * @param substitutionCypherKey C'est l'objet qui va contenir les "inputCharacters" et les "outputCharacters".
     */
    @Override
    protected void onPostExecute(SubstitutionCypherKey substitutionCypherKey)
    {
        for (Subscriber subscriber:subscriberList)
        {
            subscriber.receiveCypher(substitutionCypherKey);
        }
    }


}