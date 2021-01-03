package ca.csf.mobile1.tp1.chemical.element;

import android.content.BroadcastReceiver;
import android.content.ComponentName;
import android.content.ContentResolver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.IntentSender;
import android.content.ServiceConnection;
import android.content.SharedPreferences;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageManager;
import android.content.res.AssetManager;
import android.content.res.Configuration;
import android.content.res.Resources;
import android.database.DatabaseErrorHandler;
import android.database.sqlite.SQLiteDatabase;
import android.graphics.Bitmap;
import android.graphics.drawable.Drawable;
import android.net.Uri;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.os.UserHandle;
import android.support.annotation.Nullable;
import android.view.Display;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.FileReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.HashMap;
import java.util.Map;

import ca.csf.mobile1.tp1.R;
import ca.csf.mobile1.tp1.chemical.compound.ChemicalCompoundFactory;

/**
 * Représente un tableau périodique. Cette classe est en fait un dépôt de {@link ChemicalElement}. Il est possible de les
 * obtenir via leur {@link ChemicalElement#getSymbol() symbole} par la méthode {@link ChemicalElementRepository#get(String)}.
 * <br />
 * <br />
 * Avant d'être utilisée, ce dépôt doit être peuplé d'éléments en utilisant la méthode {@link ChemicalElementRepository#add(ChemicalElement)}.
 *
 * @author Benjamin Lemelin
 * @author Daniel Huot
 * @see ChemicalElement
 */
public class ChemicalElementRepository
{

    private Map<String, ChemicalElement> chemicalElements;

    /**
     * Construit un nouveau tableau périodique vide.
     */
    public ChemicalElementRepository(InputStream inputStream)
    {
        this.chemicalElements = new HashMap<>();

        //Note : J'ai voulu implémenter ton code mot-par-mot, mais cela n'a simplement JAMAIS fonctionné. Désolé.

        try
        {

            BufferedReader br = new BufferedReader(new InputStreamReader(inputStream));

            String line;

            while ((line = br.readLine()) != null)
            {
                String[] words = line.split(",");

                add(new ChemicalElement(words[0],words[1],Integer.parseInt(words[2]), Double.parseDouble(words[3])));
            }

        }
        catch(Exception e)
        {

        }

    }


    /**
     * Ajoute un {@link ChemicalElement} a ce tableau périodique. Il devient alors possible de l'obtenir via la méthode {@link ChemicalElementRepository#get(String)}.
     * Si un élément avec le même symbole existe déjà, il sera remplacé.
     * @param chemicalElement Élément à ajouter au tableau périodique.
     *                        Si un élément avec le même symbole existe déjà, il sera remplacé.
     * @see ChemicalElement
     */
    public void add(ChemicalElement chemicalElement)
    {
        chemicalElements.put(chemicalElement.getSymbol(), chemicalElement);
    }

    /**
     * Retourne le {@link ChemicalElement} correspondant au {@link ChemicalElement#getSymbol() symbole} reçu en paramêtre.
     * @param symbol Le {@link ChemicalElement#getSymbol() symbole} de l'élément voulu.
     * @return {@link ChemicalElement} correspondant au {@link ChemicalElement#getSymbol() symbole} reçu. Si l'élément demandé est
     * introuvable, retourne <code>null</code>.
     */
    public ChemicalElement get(String symbol)
    {
        return chemicalElements.get(symbol);
    }

}
