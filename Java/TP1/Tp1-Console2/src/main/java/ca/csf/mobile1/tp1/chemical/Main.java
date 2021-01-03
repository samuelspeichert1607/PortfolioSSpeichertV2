package ca.csf.mobile1.tp1.chemical;

import java.util.Scanner;
import java.util.regex.Matcher;
import java.util.regex.Pattern;


import ca.csf.mobile1.tp1.chemical.compound.ChemicalCompound;
import ca.csf.mobile1.tp1.chemical.compound.ChemicalCompoundFactory;



import ca.csf.mobile1.tp1.chemical.element.ChemicalElementRepository;

/**
 * Created by Devilclown1607 on 2017-02-21.
 */

public class Main
{
    public static void main(String[] args) throws Exception
    {
        //TODO Créer les objets du modèle
        //TODO Créer la vue
        //TODO Créer le controleur
        ChemicalElementRepository repository = new ChemicalElementRepository();

        ChemicalCompoundFactory factory = new ChemicalCompoundFactory(repository);
        System.out.println("Entrez une formule chimique :");

        Scanner reader = new Scanner(System.in);
        String text = reader.nextLine();
        ChemicalCompound chemicalCompound = factory.createFromString(text);

        System.out.println("Le poids de ce composant chimique est :" + chemicalCompound.getWeight());

        //Les exceptions doivent être déclarées dans le factory et se conformer aux tests unitaires présents.
        //Il est normal que les tests de controlleur ne fonctionne pas, test pas le seul

    }
}
